using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Threading.Tasks;
using System;

public class HPHandleScript : MonoBehaviour
{
    private TMP_Text stageText;
    private int maxHP;
    private int currentHP;
    private int currentStage;
    private Slider slider;
    public GameObject creatures;
    [SerializeField] private GameObject character;
    [SerializeField] private GameObject background;
    [SerializeField] private GameObject blackScreen; 
    [SerializeField] private GameObject tapScreen;
    private TMP_Text HPText;

    // Start is called before the first frame update
    void Start()
    {
        HPText = transform.GetChild(2).GetComponent<TMP_Text>();
        stageText = transform.parent.GetChild(1).GetComponent<TMP_Text>();
        maxHP = 100;
        currentStage = 0;
        RaiseMaxHP();
        slider = GetComponent<Slider>();
    }

    private async Task RaiseMaxHP()
    {
        tapScreen.SetActive(false);
        for(int i=1; i<character.transform.childCount; i++)
        {
            character.transform.GetChild(i).GetComponent<FriendScript>().enabled = false;
        }

        string creatureName;
        creatures.GetComponent<MakeCreatures>().DestroyCreature();

        await Task.Delay(1000);
        
        character.transform.GetChild(0).GetComponent<Animator>().Play("Run");
        for(int i=1; i<character.transform.childCount; i++)
        {
            character.transform.GetChild(i).GetComponent<Animator>().Play("Run");
        }

        if( (currentStage % 10 == 0) && (currentStage != 0) )
        {
            creatureName = await ChangeStage();

            character.transform.GetChild(0).GetComponent<Animator>().Play("Idle");
            for(int i=1; i<character.transform.childCount; i++)
            {
                character.transform.GetChild(i).GetComponent<FriendScript>().enabled = true;
                character.transform.GetChild(i).GetComponent<Animator>().Play("Idle");
            }
            tapScreen.SetActive(true);
        }
        else
        {
            creatureName = creatures.GetComponent<MakeCreatures>().NextStage();
        }

        await MoveToPositionAsync(background, new Vector3(5 - 10.0f*(currentStage%10)/10, 0, 20), 0.17f, Vector3.left);
        
        maxHP *= 2;
        currentHP = maxHP;
        currentStage += 1;
        stageText.text = "Stage. " + currentStage + " " + creatureName;

        // tapScreen.SetActive(true);
        // for(int i=1; i<character.transform.childCount; i++)
        // {
        //     character.transform.GetChild(i).GetComponent<FriendScript>().enabled = true;
        // }

        // creatures.transform.Translate(Vector3.left * 25.0f / 4);
        // creatures.transform.localPosition = Vector2.MoveTowards(transform.localPosition, new Vector2(-25.0f*currentStage, 0), 10f);
    }

    public async void ReduceHP(int ATK)
    {
        currentHP -= ATK;
        if(currentHP <= 0)
        {
            UpdateHP();
            await RaiseMaxHP();
        }

        UpdateHP();
    }

    private void UpdateHP()
    {
        slider.value = 1.0f*currentHP/maxHP;
        HPText.text = currentHP + " / " + maxHP;
    }

    private async Task<String> ChangeStage()
    {
        await Task.Delay(2500);
        await MoveToPositionAsync(character, new Vector3(25, 0, 0), 5f, Vector3.right);

        await Task.Delay(500);
        character.transform.localPosition = new Vector3(-15, 0, 0);
        MoveToPositionAsync(blackScreen, new Vector3(110, 0, -15), 40f, Vector3.left);

        await Task.Delay(500);
        background.transform.localPosition = new Vector3(5, 0, 20);
        string creatureName = creatures.GetComponent<MakeCreatures>().NextStage2();

        await Task.Delay(2500);
        blackScreen.transform.localPosition = new Vector3(-110, 0, -15);
        await MoveToPositionAsync(character, new Vector3(0, 0, 0), 5f, Vector3.right);
        
        await Task.Delay(500);

        return creatureName;
    }
    
    private async Task MoveToPositionAsync(GameObject obj, Vector3 target, float speed, Vector3 direction)
{
        if(obj.transform.localPosition.x < target.x)
        {
            while (obj.transform.localPosition.x < target.x)
            {
                obj.transform.Translate(speed * Time.deltaTime * direction);
                await Task.Yield();
            }
        }
        else
        {
            while (obj.transform.localPosition.x > target.x)
            {
                obj.transform.Translate(speed * Time.deltaTime * direction);
                await Task.Yield();
            }
        }

        obj.transform.localPosition = target;
    }
}
