using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FriendScript : MonoBehaviour
{
    public TapScript tapScript;
    [SerializeField] private GameObject HPSlider;
    [SerializeField] private float attackSpeed;
    [SerializeField] private int ATK;

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("Attack", 1f, attackSpeed);
    }

    public void SetATK(int currentATK)
    {
        ATK = currentATK;
    }

    public int GetATK()
    {
        return ATK;
    }
    
    private void Attack()
    {
        GetComponent<Animator>().Play("Atk");
        HPSlider.GetComponent<HPHandleScript>().ReduceHP(ATK);
        tapScript.PlusMoney(ATK);
    }

    private void OnDisable() {
        CancelInvoke("Attack");    
    }

    private void OnEnable()
    {
        InvokeRepeating("Attack", 1f, attackSpeed);
    }
}
