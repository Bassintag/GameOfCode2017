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
    public Text characterActions;
    public bool finish;

    public List<Actions.Action> actions;

    void Awake() {
        finish = false;
        actions = new List<Actions.Action>();
        StartCoroutine(createNewFolder());
        highlighted = false;
        wasHighlighted = false;
    }

    IEnumerator createNewFolder()
    {
        characterName.text = "Doe";
        characterSurname.text = "John";
        string span = "<span class=\"heavyhuge\">";
        string endspan = " </span></p>";
        WWW www2 = new WWW("http://www.behindthename.com/random/random.php?number=1&gender=m&surname=&randomsurname=yes&all=no&usage_fairy=1&usage_fntsy=1");
        yield return www2;
        if (www2.text.Contains(span))
        {
            int ix = www2.text.IndexOf(span);
            int ix2 = www2.text.Substring(ix + span.Length + 2).IndexOf(endspan);
            if (ix != -1 && ix2 != -1)
            {
                string code = www2.text.Substring(ix + span.Length + 2, ix2);
                string[] codes = code.Split(' ');
                characterSurname.text = codes[0];
                characterName.text = codes[codes.Length - 1];
            }
        }
        WWW www = new WWW("http://api.adorable.io/avatars/285/" + WWW.EscapeURL(characterSurname.text + characterName.text) + ".png");
        yield return www;
        avatar.sprite = Sprite.Create(www.texture, new Rect(0, 0, www.texture.width, www.texture.height), new Vector2(0.5f, 0.5f));
        GenerateText();
        finish = true;
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

    void GenerateText()
    {
        string str = "";
        foreach (Actions.Action a in actions)
        {
            str += "- " + a.text + "\n\n";
        }
        characterActions.text = str;
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
