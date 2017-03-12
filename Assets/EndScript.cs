using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndScript : MonoBehaviour {

    public Text text;


    // Use this for initialization
    void Start () {
                 text.text = GameObject.Find("GameManager").GetComponent<GameManager>().end_sentence;
            }
	
	// Update is called once per frame
	void Update () {
		
	}
}
