using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EffectAnimationHandler : MonoBehaviour
{
    private Animator effectAnimator;
    private GameObject damageText;

    // Start is called before the first frame update
    void Start()
    {
        effectAnimator = GetComponent<Animator>();
        damageText = transform.parent.GetChild(2).GetChild(0).gameObject;
    }

    public void playEffectAnimation(int ATK)
    {
        damageText.transform.localPosition = new Vector3(0, 0, -1);
        StopAllCoroutines();
        StartCoroutine(MoveToPosition(new Vector3(0, 40, -1)));
        damageText.GetComponent<TMP_Text>().text = ATK.ToString();
        transform.localPosition = new Vector3(Random.Range(-1.5f, 1.5f), Random.Range(-1.5f, 1.5f), -1);
        GetComponent<Animator>().Play("tapEffect1", -1, 0f);
    }

    private void stopEffectAnimation()
    {
        transform.localPosition = new Vector3(0, 0, 1);
        damageText.transform.localPosition = new Vector3(0, 0, 0);
    }

    private IEnumerator MoveToPosition(Vector3 target)
    {
        while(Vector2.Distance(damageText.transform.localPosition, target) > 0.1f)
        {
            damageText.transform.localPosition = Vector3.MoveTowards(damageText.transform.localPosition, target, 500*Time.deltaTime);
            yield return null;
        }
        damageText.transform.localPosition = target;
    }
}
