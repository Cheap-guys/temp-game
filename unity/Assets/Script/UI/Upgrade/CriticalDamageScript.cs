using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CriticalDamageScript : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public TapScript TapScript;
    [SerializeField] private int currentValue;
    private int currentMoney;
    [SerializeField] private int upgradeMoney;
    [SerializeField] private int upgradeValue;
    [SerializeField] private int upgradeMoneyRate;
    // [SerializeField] private float upgradeValueRate;
    
    private TMP_Text currentValueText;
    private TMP_Text upgradeMoneyText;
    private TMP_Text upgradeValueText;

    [SerializeField] private Sprite buttonBasic;
    [SerializeField] private Sprite buttonPress;

    // Start is called before the first frame update
    void Start()
    {
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
            upgradeMoney *= upgradeMoneyRate;
            currentValue += upgradeValue;
            // upgradeValue = (int)(upgradeValue + upgradeValueRate);

            currentValueText.text = currentValue.ToString();
            upgradeValueText.text = "+" + upgradeValue.ToString() + " %";
            TapScript.SetMoney(currentMoney);
            upgradeMoneyText.text = upgradeMoney.ToString();

            TapScript.SetCriticalValue(currentValue);

            if(currentValue >= 100)
            {
                upgradeValueText.text = "완료";
                upgradeMoneyText.text = "";
                enabled = false;
            }
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        GetComponent<Image>().sprite = buttonPress;
    }
}