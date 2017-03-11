using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Folder : MonoBehaviour {

    private bool wasHighlighted;
    public bool highlighted { get; set; }
    public Image avatar;
    public Text characterName;
    public Text characterSurname;

    void Start() {
        int idx_surname = Random.Range(0, CharacterProvider.instance.surnames.Length - 1);
        string surname = CharacterProvider.instance.surnames[idx_surname];
        int idx_name = Random.Range(0, CharacterProvider.instance.names.Length - 1);
        string name = CharacterProvider.instance.names[idx_name];

        characterName.text = name;
        characterSurname.text = surname;

        StartCoroutine(createNewFolder(surname, name));
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
        float y = transform.position.y;
        float x = transform.position.x;
        float z = transform.position.z;
        for (int i = 0; i <= 100; i += 4)
        {
            if (!highlighted)
                break;
            transform.eulerAngles = new Vector3(Mathf.LerpAngle(start, 0, i / 100f), 0);
            transform.position = new Vector3(x, Mathf.Lerp(y, 2.5f, i / 100f), z);
            yield return new WaitForSeconds(.005f);
        }
    }

    IEnumerator bringToBack()
    {
        float start = transform.eulerAngles.x;
        float y = transform.position.y;
        float x = transform.position.x;
        float z = transform.position.z;
        for (int i = 0; i <= 100; i += 4)
        {
            if (highlighted)
                break;
            transform.eulerAngles = new Vector3(Mathf.LerpAngle(start, 90, i / 100f), 0);
            transform.position = new Vector3(x, Mathf.Lerp(y, 0, i / 100f), z);
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
