using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Folder : MonoBehaviour {

    private bool wasHighlighted;
    private bool isYou;
    public bool highlighted { get; set; }
    public Image avatar;
    public Text characterName;
    public Text characterSurname;
    public Text characterActions;
    public bool finish;
    public Image tampon_hell;
    public Image tampon_heaven;
    public List<Actions.Action> actions;

    private Vector3 start;

    void Awake() {
        isYou = false;
        finish = false;
        actions = new List<Actions.Action>();
        StartCoroutine(createNewFolder());
        highlighted = false;
        wasHighlighted = false;
        UpdateStartPosition();
    }

    public void createYourFolder(float bad_karma_heaven, float bad_karma_hell, float good_karma_heaven, float good_karma_hell)
    {
        isYou = true;
        characterName.text = "?????";
        characterSurname.text = "?????";
        actions.Clear();
        bad_karma_heaven = Mathf.Abs(bad_karma_heaven);
        bad_karma_hell = Mathf.Abs(bad_karma_hell);
        good_karma_heaven = Mathf.Abs(good_karma_heaven);
        good_karma_hell = Mathf.Abs(good_karma_hell);
        actions.Add(new Actions.Action(0, "Judged good people for bad choices they were forced to make, despite of their feelings"));
        actions.Add(new Actions.Action(0, Math.Round(Mathf.Abs(bad_karma_hell / (bad_karma_hell + good_karma_hell) * 100), 1) + "% of the people that this guy sent to hell had a part of goodness in them!"));
        actions.Add(new Actions.Action(0, Math.Round(Mathf.Abs(bad_karma_heaven / (good_karma_heaven + bad_karma_heaven) * 100), 1) + "% of the people that this guy sent to heaven had a part of badness in them!"));

        wasHighlighted = false;
        highlighted = true;
        UpdateStartPosition();
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
            if (!isYou && ix != -1 && ix2 != -1)
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
        float a = transform.eulerAngles.x;
        float y = transform.position.y;
        float x = transform.position.x;
        float z = transform.position.z;
        for (int i = 0; i <= 100; i += 4)
        {
            if (!highlighted)
                break;
            transform.eulerAngles = new Vector3(Mathf.LerpAngle(a, 0, i / 100f), 0);
            transform.position = new Vector3(x, Mathf.Lerp(y, start.y + 2.5f, i / 100f), Mathf.Lerp(z, start.z - 1f, i / 100f));
            yield return new WaitForSeconds(.005f);
        }
    }

    IEnumerator bringToBack()
    {
        float a = transform.eulerAngles.x;
        float y = transform.position.y;
        float x = transform.position.x;
        float z = transform.position.z;
        for (int i = 0; i <= 100; i += 4)
        {
            if (highlighted)
                break;
            transform.eulerAngles = new Vector3(Mathf.LerpAngle(a, 90, i / 100f), 0);
            transform.position = new Vector3(x, Mathf.Lerp(y, start.y, i / 100f), Mathf.Lerp(z, start.z, i / 100f));
            yield return new WaitForSeconds(.005f);
        }
    }

    public void Slide(Vector3 targetPosition, Vector3 targetAngles)
    {
        StartCoroutine(SlideToPosition(targetPosition + new Vector3(0, 1f), targetAngles, targetPosition, targetAngles, true));
    }

    IEnumerator SlideToPosition(Vector3 position, Vector3 angles, Vector3 nextPosition, Vector3 nextAngles, bool next)
    {
        Vector3 actualPos = transform.position;
        Vector3 actualAngles = transform.eulerAngles;
        for (float f = 0; f <= 1f; f += 0.05f)
        {
            transform.position = Vector3.Lerp(actualPos, position, f);
            transform.eulerAngles = Vector3.Lerp(actualAngles, angles, f);
            yield return new WaitForEndOfFrame();
        }
        transform.position = position;
        transform.eulerAngles = angles;
        if (next)
            StartCoroutine(SlideToPosition(nextPosition, nextAngles, Vector3.zero, Vector3.zero, false));
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

    public void UpdateStartPosition()
    {
        start = transform.position;
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
