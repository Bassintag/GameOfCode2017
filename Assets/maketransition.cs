using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class maketransition : MonoBehaviour {

    public string[] TextSpawn;
    public Text Textbase;

    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void gotransit(int lvl)
    {
        StartCoroutine(SpawnText(lvl));
    }

    IEnumerator SpawnText(int lvl)
    {
        this.gameObject.SetActive(true);
        for (int i = 0; i < TextSpawn[0].Length; i++)
        {
            yield return new WaitForSeconds(0.20f);
            Textbase.text += TextSpawn[0][i];
        }
        Textbase.text += lvl;
        yield return new WaitForSeconds(0.35f);
        this.gameObject.SetActive(false);
    }
}
