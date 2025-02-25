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
    private int criticalValue;
    private float criticalDamageRate;
    private int money;
    [SerializeField] private TMP_Text moneyText;

    // Start is called before the first frame update
    void Start()
    {
        ATK = 1;
        criticalValue = 0;
        criticalDamageRate = 1.1f;
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

        int criticalDamage = ATK;
        int damageColor = 0;

        if(Random.Range(0, 100) < criticalValue)
        {
            criticalDamage = (int)(ATK * criticalDamageRate);
            damageColor = 1;
        }
        money += criticalDamage;
        moneyText.text = money.ToString();
        mainCharacterAnimator.Play("Atk", -1, 0f);
        creatureAnimator.Play("Hit", -1, 0f);
        effect.GetComponent<EffectAnimationHandler>().playEffectAnimation(criticalDamage, damageColor);
        HPSlider.GetComponent<HPHandleScript>().ReduceHP(criticalDamage);
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

    public void SetCriticalValue(int criticalValue)
    {
        this.criticalValue = criticalValue;
    }

    public void SetCriticalDamageRate(float criticalDamageRate)
    {
        this.criticalDamageRate = criticalDamageRate;
    }

    public void PlusMoney(int ATK)
    {
        money += ATK;
        moneyText.text = money.ToString();
    }
}
