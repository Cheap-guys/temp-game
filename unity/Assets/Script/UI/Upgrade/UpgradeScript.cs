using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UpgradeScript : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public TapScript TapScript;
    private int currentValue;
    private int currentMoney;
    private int upgradeMoney;
    private int upgradeValue;
    
    private TMP_Text currentValueText;
    private TMP_Text upgradeMoneyText;
    private TMP_Text upgradeValueText;

    [SerializeField] private Sprite buttonBasic;
    [SerializeField] private Sprite buttonPress;

    // Start is called before the first frame update
    void Start()
    {
        currentValue = 1;
        upgradeValue = 2;
        upgradeMoney = 1;

        currentValueText = transform.parent.GetChild(0).GetComponent<TextMeshProUGUI>();
        upgradeMoneyText = transform.GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>();
        upgradeValueText = transform.GetChild(1).GetComponent<TextMeshProUGUI>();
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        GetComponent<Image>().sprite = buttonBasic;

        currentMoney = TapScript.GetMoney();

        if(currentMoney >= upgradeMoney)
        {
            currentMoney -= upgradeMoney;
            upgradeMoney *= 2;
            currentValue += upgradeValue;
            upgradeValue = (int)(upgradeValue * 1.8);

            currentValueText.text = currentValue.ToString();
            upgradeValueText.text = "+" + upgradeValue.ToString() + " ATK";
            TapScript.SetMoney(currentMoney);
            upgradeMoneyText.text = upgradeMoney.ToString();

            TapScript.SetATK(currentValue);
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        GetComponent<Image>().sprite = buttonPress;
    }
}
