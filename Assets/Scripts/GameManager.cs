using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    [HideInInspector]
    public List<Folder> folders;

	void Start ()
    {
		folders = new List<Folder>();
    }
	
	void Update ()
    {
        Vector3 rotation = Camera.main.transform.eulerAngles;
        if (Mathf.Abs(rotation.y) < 45)
        {
            folders[0].highlighted = true;
            Debug.Log("in");
        }
        else
        {
            folders[0].highlighted = false;
            Debug.Log("out");
        }
    }
}
