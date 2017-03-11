using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    [HideInInspector]
    public List<Folder> folders;

    public Folder folder;

    void Start ()
    {
		folders = new List<Folder>();
        folders.Add(folder);
    }
	
	void Update ()
    {
        Vector3 rotation = Camera.main.transform.eulerAngles;
        //(Mathf.Abs(rotation.y));
        if (Mathf.Abs(rotation.y) < 45 || Mathf.Abs(rotation.y) > 315)
        {
            folders[0].highlighted = true;
        }
        else
        {
            folders[0].highlighted = false;
        }
    }
}
