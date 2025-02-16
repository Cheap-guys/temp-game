using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class TapScript : MonoBehaviour
{
    private Animator mainCharacterAnimator;
    private Animator creatureAnimator;
    private GameObject effect;
    [SerializeField] private GameObject HPSlider;
    private int ATK;
    private int money;
    [SerializeField] private TMP_Text moneyText;

    // Start is called before the first frame update
    void Start()
    {
        ATK = 1;
        money = 0;
        mainCharacterAnimator = transform.parent.parent.GetChild(0).GetChild(0).GetComponent<Animator>();
        // creatureAnimator = transform.parent.parent.GetChild(1).GetChild(0).GetChild(0).GetComponent<Animator>();
        effect = transform.parent.parent.GetChild(1).GetChild(1).gameObject;
    }

    // Update is called once per frame
    // void Update()
    // {
    //     moneyText.text = money.ToString();
    //     creatureAnimator = transform.parent.parent.GetChild(1).GetChild(0).GetChild(0).GetComponent<Animator>();
    // }

    private void OnMouseDown() {
        creatureAnimator = transform.parent.parent.GetChild(1).GetChild(0).GetChild(0).GetComponent<Animator>();

        if (EventSystem.current.IsPointerOverGameObject())
        {
            return;
        }

        money += ATK;
        moneyText.text = money.ToString();
        mainCharacterAnimator.Play("Atk", -1, 0f);
        creatureAnimator.Play("Hit", -1, 0f);
        effect.GetComponent<EffectAnimationHandler>().playEffectAnimation(ATK);
        HPSlider.GetComponent<HPHandleScript>().ReduceHP(ATK);
    }

    public int GetMoney()
    {
        return money;
    }

    public void SetMoney(int setMoney)
    {
        money = setMoney;
        moneyText.text = money.ToString();
    }

    public int GetATK()
    {
        return ATK;
    }

    public void SetATK(int setATK)
    {
        ATK = setATK;
    }

    public void PlusMoney(int ATK)
    {
        money += ATK;
        moneyText.text = money.ToString();
    }
}
