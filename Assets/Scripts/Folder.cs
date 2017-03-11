using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Folder : MonoBehaviour {

    private bool wasHighlighted;
    public bool highlighted { get; set; }

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
        highlighted = false;
        wasHighlighted = false;
    }

    IEnumerator bringToFront()
    {
        float start = transform.eulerAngles.x;
        for (int i = 0; i <= 100; i += 4)
        {
            if (!highlighted)
                break;
            transform.eulerAngles = new Vector3(Mathf.LerpAngle(start, -90, i / 100f), 0);
            yield return new WaitForSeconds(.01f);
        }
    }

    IEnumerator bringToBack()
    {
        float start = transform.eulerAngles.x;
        for (int i = 0; i <= 100; i += 4)
        {
            if (highlighted)
                break;
            transform.eulerAngles = new Vector3(start - Mathf.LerpAngle(0, start, i / 100f), 0);
            yield return new WaitForSeconds(.01f);
        }
    }

    void Update()
    {
        if (wasHighlighted && !highlighted)
        {
            StartCoroutine(bringToBack());
            wasHighlighted = highlighted;
        }
        if (!wasHighlighted && highlighted)
        {
            StartCoroutine(bringToFront());
            wasHighlighted = highlighted;
        }
    }
}
