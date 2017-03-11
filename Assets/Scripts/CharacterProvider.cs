using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterProvider : MonoBehaviour {

    public Avatar[] avatars;
    public Surname[] surnames;
    public string[] names;

    public static CharacterProvider instance;

    void Start()
    {
        if (instance != null)
            Destroy(this);
        instance = this;
    }

    [System.Serializable]
    public class Surname
    {
        public string surname;
        public bool female;
    }

    [System.Serializable]
    public class Avatar
    {
        public Sprite avatar;
        public bool female;
    }
}
