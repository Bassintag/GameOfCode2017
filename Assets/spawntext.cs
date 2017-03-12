using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class spawntext : MonoBehaviour {

    public string[] TextSpawn;
    public Text Textbase;

	void Start () {
        StartCoroutine(SpawnText());
	}

    IEnumerator SpawnText()
    {
        for(int i = 0; i < TextSpawn[0].Length; i++)
        {
            yield return new WaitForSeconds(0.20f);
            Textbase.text += TextSpawn[0][i];
        }
        yield return new WaitForSeconds(0.55f);
        for (int i = TextSpawn[0].Length; i != 0; i--)
        {
            yield return new WaitForSeconds(0.05f);
            Textbase.text = Textbase.text.Remove(i - 1, 1);
        }
        Textbase.fontSize -= 35;
        for (int i = 0; i < TextSpawn[1].Length; i++)
        {
            yield return new WaitForSeconds(0.15f);
            Textbase.text += TextSpawn[1][i];
        }
        yield return new WaitForSeconds(0.25f);
        float time = GameObject.Find("FadingSystem").GetComponent<Fading>().BeginFade(1);
        yield return new WaitForSeconds(time);
        SceneManager.LoadScene(1);
    }
}
