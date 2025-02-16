using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ListButtonScript : MonoBehaviour, IPointerClickHandler
{
    private GameObject abilityObject;

    // Start is called before the first frame update
    void Start()
    {
        abilityObject = transform.parent.gameObject;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        abilityObject.GetComponent<AbilityListButton>().ChangeState(this.name);
    }
}
