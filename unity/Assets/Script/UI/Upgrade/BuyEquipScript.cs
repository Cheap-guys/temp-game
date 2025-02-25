using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class BuyEquipScript : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    [SerializeField] private GameObject character;
    [SerializeField] private TapScript tapScript;
    [SerializeField] private string equipName;
    // [SerializeField] private int currentValue;
    private int currentMoney;
    [SerializeField] private int buyMoney;
    // [SerializeField] private int upgradeValue;
    // [SerializeField] private int buyMoneyRate;
    // [SerializeField] private float upgradeValueRate;
    
    // private TMP_Text currentValueText;
    private TMP_Text buyMoneyText;
    private TMP_Text upgradeValueText;

    [SerializeField] private bool isBuy;
    [SerializeField] private bool isEquip;

    [SerializeField] private Sprite buttonBasic;
    [SerializeField] private Sprite buttonPress;

    // Start is called before the first frame update
    void Start()
    {
        // currentValueText = transform.parent.GetChild(0).GetComponent<TextMeshProUGUI>();
        buyMoneyText = transform.GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>();
        // tapScript = character.GetComponent<TapScript>();
        upgradeValueText = transform.GetChild(1).GetComponent<TextMeshProUGUI>();
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        GetComponent<Image>().sprite = buttonBasic;

        currentMoney = tapScript.GetMoney();

        if(!isBuy && currentMoney >= buyMoney)
        {
            currentMoney -= buyMoney;
            // buyMoney *= buyMoneyRate;
            // currentValue += upgradeValue;
            // upgradeValue = (int)(upgradeValue * upgradeValueRate);

            // currentValueText.text = "미장착";
            upgradeValueText.text = "구매완료";
            tapScript.SetMoney(currentMoney);
            buyMoneyText.text = "미장착";

            isBuy = true;
        }

        else if(isBuy && !isEquip)
        {
            character.GetComponent<Animator>().runtimeAnimatorController = (RuntimeAnimatorController)Resources.Load("Animation/CountryBoy/"+equipName+"/"+equipName);
            buyMoneyText.text = "장착중";
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        GetComponent<Image>().sprite = buttonPress;
    }
}