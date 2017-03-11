using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Folder : MonoBehaviour {

    private bool wasHighlighted;
    public bool highlighted { get; set; }
    public Image avatar;

    void Start() {
        int idx_surname = Random.Range(0, CharacterProvider.instance.surnames.Length - 1);
        string surname = CharacterProvider.instance.surnames[idx_surname];
        int idx_name = Random.Range(0, CharacterProvider.instance.names.Length - 1);
        string name = CharacterProvider.instance.names[idx_name];

        StartCoroutine(createNewFolder(surname, name));
            
   /*     while (avatar == null || avatar.female != surname.female)
        {
            int idx_avatar = Random.Range(0, CharacterProvider.instance.avatars.Length - 1);
            avatar = CharacterProvider.instance.avatars[idx_avatar];
        }*/
        highlighted = false;
        wasHighlighted = false;
    }

    IEnumerator createNewFolder(string surname, string name)
    {
        WWW www = new WWW("http://api.adorable.io/avatars/285/" + surname + name + ".png");
        yield return www;
        avatar.sprite = Sprite.Create(www.texture, new Rect(0, 0, www.texture.width, www.texture.height), new Vector2(0.5f, 0.5f));
     //   WWW www2 = new WWW("http://www.behindthename.com/random/random.php?number=1&gender=m&surname=&randomsurname=yes&all=no&usage_fairy=1&usage_fntsy=1");
      //  yield return www2;
      //  Debug.Log(www2.text);
    }

    IEnumerator bringToFront()
    {
        float start = transform.eulerAngles.x;
        for (int i = 0; i <= 100; i += 4)
        {
            if (!highlighted)
                break;
            transform.eulerAngles = new Vector3(Mathf.LerpAngle(start, 0, i / 100f), 0);
            yield return new WaitForSeconds(.005f);
        }
    }

    IEnumerator bringToBack()
    {
        float start = transform.eulerAngles.x;
        for (int i = 0; i <= 100; i += 4)
        {
            if (highlighted)
                break;
            transform.eulerAngles = new Vector3(start - Mathf.LerpAngle(90, start, i / 100f), 0);
            yield return new WaitForSeconds(.005f);
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
