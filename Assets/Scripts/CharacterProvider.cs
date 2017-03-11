using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterProvider : MonoBehaviour {

    public string[] surnames;
    public string[] names;

    public static CharacterProvider instance;

    void Awake()
    {
        if (instance != null)
            Destroy(this);
        instance = this;
    }
}
