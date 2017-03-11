using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Folder : MonoBehaviour {

	void Start() {
        int idx_surname = Random.Range(0, CharacterProvider.instance.surnames.Length - 1);
        CharacterProvider.Surname surname = CharacterProvider.instance.surnames[idx_surname];
        int idx_name = Random.Range(0, CharacterProvider.instance.names.Length - 1);
        string name = CharacterProvider.instance.names[idx_name];

        CharacterProvider.Avatar avatar = null;
        while (avatar == null || avatar.female != surname.female)
        {
            int idx_avatar = Random.Range(0, CharacterProvider.instance.avatars.Length - 1);
            avatar = CharacterProvider.instance.avatars[idx_avatar];
        }
        Debug.Log(surname + " " + name + " (" + avatar + ")");
    }
}
