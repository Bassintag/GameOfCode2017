using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class takeObject : MonoBehaviour
{
    public GameObject Cross;
    public GameObject Cross2;
    public Sprite newSprite;
    public Sprite oldSprite;
    public Image fill;
    public float timetoTake;

    public UnityEvent onTake;

    public Collider hovered;

    void OnTriggerEnter(Collider col)
    {
        Cross.SetActive(true);
        Cross2.SetActive(false);
        StartCoroutine("TakeItem");
    }

    void OnTriggerExit(Collider col)
    {
        Cross.SetActive(false);
        Cross2.SetActive(true);
        StartCoroutine("Unfill");
        StopCoroutine("TakeItem");
    }

    void OnTriggerStay(Collider col)
    {
        fill.fillAmount += 1.0f / timetoTake * Time.deltaTime;
        hovered = col;
    }

    new public void StopAllCoroutines()
    {
        Cross.SetActive(false);
        Cross2.SetActive(true);
        base.StopAllCoroutines();
    }

    IEnumerator Unfill()
    {
        yield return new WaitForSeconds(0.1f);
        fill.fillAmount = 0;
    }

    IEnumerator TakeItem()
    {
        yield return new WaitForSeconds(timetoTake);
        onTake.Invoke();
    }
}
