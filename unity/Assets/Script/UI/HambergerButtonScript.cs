using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class HambergerButtonScript : MonoBehaviour, IPointerClickHandler
{
    private GameObject targetObject;
    private bool targetObjectActiveState = false;

    // Start is called before the first frame update
    void Start()
    {
        targetObject = transform.parent.GetChild(1).gameObject;
    }

    public void OnPointerClick(PointerEventData eventData) {
        if(targetObjectActiveState == true)
        {
            for(int i=0; i<targetObject.transform.childCount; i++)
            {
                targetObject.transform.GetChild(i).transform.localPosition = new Vector2(0, 0);
                // MoveToPosition(targetObject.transform.GetChild(i).transform.localPosition, new Vector2(0, 0), i);
            }

            targetObject.SetActive(false);
            targetObjectActiveState = false;
        }
        else
        {
            targetObject.SetActive(true);
            targetObjectActiveState = true;

            for(int i=0; i<targetObject.transform.childCount; i++)
            {
                targetObject.transform.GetChild(i).transform.localPosition = new Vector2(0, -120*(i+1));
                // MoveToPosition(targetObject.transform.GetChild(i).transform.localPosition, new Vector2(0, -120*(i+1)), i);
            }
        }
    }

    private IEnumerator MoveToPosition(Vector2 current, Vector2 target, int i)
    {
        while(Vector2.Distance(current, target) > 0.1f)
        {
            Vector2.MoveTowards(current, target, 10000 * (i+1) * Time.deltaTime);
            yield return null;
        }
        current = target;
    }
}