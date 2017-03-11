using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class takeObject : MonoBehaviour
{

    /*Transform child = transform.Find("ObjectName");
    Text t = child.GetComponent<Text>();*/
    public GameObject Cross;
    public GameObject Cross2;
    public Sprite newSprite;
    public Sprite oldSprite;
    public Image fill;
    public float timetoTake;

    void OnTriggerEnter(Collider col)
    {
        //this.transform.getComponent<UnityEngine.UI.Image>().sprite = newSprite;
        //Cross.sprite = newSprite;
        Cross.SetActive(true);
        Cross2.SetActive(false);
        Debug.Log("YOLO");
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
        Debug.Log("STAY");
        fill.fillAmount += 1.0f / timetoTake * Time.deltaTime;
        StartCoroutine("TakeItem");
    }

    IEnumerator Unfill()
    {
        yield return new WaitForSeconds(0.1f);
        fill.fillAmount = 0;
    }

    IEnumerator TakeItem()
    {
        yield return new WaitForSeconds(timetoTake);
        //MAKE ACTION HERE
        Debug.Log("COROUTINE");
    }
}
