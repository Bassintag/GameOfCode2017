using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class lvlmanager : MonoBehaviour {

    public int lvl;
    public void change_lvl()
    {
        StartCoroutine(Fadeswitch());
        SceneManager.LoadScene(lvl);
    }

    IEnumerator Fadeswitch()
    {
        float time = GameObject.Find("FadingSystem").GetComponent<Fading>().BeginFade(1);
        yield return new WaitForSeconds(time);
    }
}
