using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class AbilityListButton : MonoBehaviour
{
    private bool activeState;
    private int buttonState;
    private int changeButtonState;
    private GameObject itemPanel;
    RectTransform rectTransform;

    // Start is called before the first frame update
    void Start()
    {
        activeState = false;
        buttonState = 0;
        changeButtonState = 0;
        rectTransform = transform.GetComponent<RectTransform>();
        itemPanel = transform.GetChild(0).gameObject;
    }

    public void ChangeState(string gameObjectName)
    {
        if(gameObjectName == "statButton")
        {
            changeButtonState = 0;
        }
        else if(gameObjectName == "friendButton")
        {
            changeButtonState = 1;
        }
        else if(gameObjectName == "skillButton")
        {
            changeButtonState = 2;
        }
        else if(gameObjectName == "artifectButton")
        {
            changeButtonState = 3;
        }

        if(activeState == true)
        {
            if(changeButtonState == buttonState)
            {
                ClosePanel();
            }

            else
            {
                OpenPanel(changeButtonState);
            }
        }

        else
        {
            OpenPanel(changeButtonState);
        }
    }

    private void OpenPanel(int changeButtonState)
    {
        itemPanel.transform.GetChild(buttonState).gameObject.SetActive(false);
        itemPanel.transform.GetChild(changeButtonState).gameObject.SetActive(true);

        activeState = true;
        StopAllCoroutines();
        StartCoroutine(MoveToPosition(new Vector2(0, 400)));
        buttonState = changeButtonState;
    }

    private void ClosePanel()
    {
        StopAllCoroutines();
        StartCoroutine(MoveToPosition(new Vector2(0, -345)));
        activeState = false;
    }

    private IEnumerator MoveToPosition(Vector2 target)
    {
        while (Vector2.Distance(rectTransform.anchoredPosition, target) > 0.1f)
        {
            rectTransform.anchoredPosition = Vector2.MoveTowards(rectTransform.anchoredPosition, target, 10000 * Time.deltaTime);
            yield return null;
        }
        rectTransform.anchoredPosition = target;
    }
}
