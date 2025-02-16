using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;

public class MakeCreatures : MonoBehaviour
{
    [SerializeField] private GameObject creatureList;
    [SerializeField] private GameObject backgroundList;
    [SerializeField] private int currentStage;
    [SerializeField] private GameObject character;
    [SerializeField] private GameObject tapScreen;

    // void Start()
    // {
    //     currentStage = 9;
    // }
    
    public void Make10Creatures()
    {
        // StopAllCoroutines();

        transform.localPosition = new Vector3(25, 0, 0);
        currentStage = -1;

        // StartCoroutine(  MoveToPosition(new Vector2(0, 0))  );

        int creatureCount = creatureList.transform.childCount;
        int backgroundCount = backgroundList.transform.childCount;

        for(int i=transform.childCount; i>1; i--)
        {
            Destroy(transform.GetChild(i-1).gameObject);
        }

        for(int i=0; i<10; i++)
        {
            GameObject creatures = creatureList.transform.GetChild(Random.Range(0, creatureCount)).gameObject;
            GameObject instantCreautre = Instantiate(creatures, transform);
            instantCreautre.transform.localPosition = new Vector3(25*i, 0, 1);
            instantCreautre.transform.localScale = new Vector3(1, 1, 1);
            instantCreautre.name = creatures.name;
        }

        for(int i=0; i<Random.Range(3, 10); i++)
        {
            GameObject backgrounds = backgroundList.transform.GetChild(Random.Range(0, backgroundCount)).gameObject;
            GameObject instantBackground = Instantiate(backgrounds, transform);
            instantBackground.transform.localPosition = new Vector3(Random.Range(-25f, 225), -1, 2);
            instantBackground.transform.localScale = new Vector3(1, 1, 1);
            instantBackground.SetActive(true);
        }
    }

    public void DestroyCreature()
    {
        if(currentStage >= 9)
        {
            Make10Creatures();
        }

        Destroy(transform.GetChild(0).gameObject);
    }

    public string NextStage()
    {
        // StopAllCoroutines();
        // transform.localPosition = new Vector2(-25.0f*currentStage++, 0f);
        StartCoroutine(  MoveToPosition(new Vector2(-25.0f*++currentStage, 0f))  );

        GameObject currentCreature = transform.GetChild(0).gameObject;
        currentCreature.SetActive(true);

        return currentCreature.name;
    }

    public string NextStage2()
    {
        currentStage++;
        transform.localPosition = new Vector3(0, 0, 0);

        GameObject currentCreature = transform.GetChild(0).gameObject;
        currentCreature.SetActive(true);

        return currentCreature.name;
    }

    private IEnumerator MoveToPosition(Vector2 target)
    {
        while (transform.localPosition.x >= target.x)
        {
            transform.Translate(4 * Time.deltaTime * Vector3.left);
            yield return null;
        }

        character.transform.GetChild(0).GetComponent<Animator>().Play("Idle");
        for(int i=1; i<character.transform.childCount; i++)
        {
            character.transform.GetChild(i).GetComponent<FriendScript>().enabled = true;
            character.transform.GetChild(i).GetComponent<Animator>().Play("Idle");
        }
        tapScreen.SetActive(true);
        transform.localPosition = target;
    }
}
