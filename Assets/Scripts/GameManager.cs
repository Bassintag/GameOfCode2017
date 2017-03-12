using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

    [HideInInspector]
    public float karma = 0;
    public float bad_karma_hell = 0;
    public float bad_karma_heaven = 0;
    public float good_karma_hell = 0;
    public float good_karma_heaven = 0;
    public string end_sentence;
    [HideInInspector]
    public List<Folder> folders;
    [HideInInspector]
    public List<Folder> heavenQueue;
    [HideInInspector]
    public List<Folder> hellQueue;

    private int folderCount = 10;
    private int maxHeaven;
    private int maxHell;

    private bool _heavenLocked;
    private bool heavenLocked { get { return _heavenLocked; } set
        {
            _heavenLocked = value;
            GameObject.FindGameObjectWithTag("tray_heaven").GetComponent<Collider>().enabled = !value;
        }
    }
    private bool _hellLocked;
    private bool hellLocked
    {
        get { return _hellLocked; }
        set
        {
            _hellLocked = value;
            GameObject.FindGameObjectWithTag("tray_hell").GetComponent<Collider>().enabled = !value;
        }
    }

    [Range(1, 3)]
    public int level = 1;
    public Folder prefab;
    public Text indicatorHeaven;
    public Text indicatorHell;

    private List<Folder> createNewFolders(int number_folders, int max_good_action, int max_bad_actions)
    {
        List<Folder> folders = new List<Folder>();
        while (folders.Count < number_folders)
        {
            Folder folder = Instantiate(prefab, transform.position, Quaternion.identity);
            folder.gameObject.transform.eulerAngles = prefab.transform.eulerAngles + new Vector3(0, Random.Range(-20, 20));
            folder.avatar.gameObject.transform.eulerAngles = folder.gameObject.transform.eulerAngles + new Vector3(0, 0 , Random.Range(-10, 10));
            int tmp_max_bad_action = max_bad_actions;
            int tmp_max_good_action = max_good_action;
            while (tmp_max_good_action + tmp_max_bad_action != 0)
            {
                if (tmp_max_good_action > 0)
                {
                    Actions.Action action = Actions.instance.getRandomNiceAction();
                    if (!folder.actions.Contains(action))
                    {
                        folder.actions.Add(action);
                        tmp_max_good_action--;
                    }
                }
                if (tmp_max_bad_action > 0)
                {
                    Actions.Action action = Actions.instance.getRandomNaughtyAction();
                    if (!folder.actions.Contains(action))
                    {
                        folder.actions.Add(action);
                        tmp_max_bad_action--;
                    }
                }
            }
            for (int i = 0; i < folder.actions.Count; i++)
            {
                Actions.Action temp = folder.actions[i];
                int randomIndex = Random.Range(i, folder.actions.Count);
                folder.actions[i] = folder.actions[randomIndex];
                folder.actions[randomIndex] = temp;
            }
            folders.Add(folder);
        }
        return folders;
    }

    public List<Folder> createYourFolder()
    {
        Folder yFolder = Instantiate(prefab, transform.position, Quaternion.identity);
        yFolder.createYourFolder(bad_karma_heaven, bad_karma_hell, good_karma_heaven, good_karma_hell);
        folders.Add(yFolder);

        return folders;
    }

    void Start ()
    {
        DontDestroyOnLoad(this);
        Reset();
    }

    public void Reset()
    {
        foreach (Folder f in folders)
            Destroy(f.gameObject);
        foreach (Folder f in heavenQueue)
            Destroy(f.gameObject);
        foreach (Folder f in hellQueue)
            Destroy(f.gameObject);
        folders = new List<Folder>();
        heavenQueue = new List<Folder>();
        hellQueue = new List<Folder>();
        heavenLocked = false;
        hellLocked = false;
        if (level == 1)
        {
            folderCount = 4;
            maxHeaven = 2;
            maxHell = 2;
            folders.AddRange(createNewFolders(1, 1, 0));
            folders.AddRange(createNewFolders(1, 0, 1));
            folders.AddRange(createNewFolders(1, 2, 0));
            folders.AddRange(createNewFolders(1, 0, 2));
        }
        else if (level == 2)
        {
            folderCount = 10;
            maxHeaven = 5;
            maxHell = 5;
            folders.AddRange(createNewFolders(2, 1, 1));
            folders.AddRange(createNewFolders(1, 2, 1));
            folders.AddRange(createNewFolders(1, 1, 2));
            folders.AddRange(createNewFolders(1, 2, 2));
            folders.AddRange(createNewFolders(1, 0, 2));
            folders.AddRange(createNewFolders(1, 2, 0));
            folders.AddRange(createNewFolders(1, 3, 2));
            folders.AddRange(createNewFolders(2, 1, 3));
        }
        else
        {
            folderCount = 1;
            Folder folder = Instantiate(prefab, transform.position, Quaternion.identity);
            folder.createYourFolder(bad_karma_heaven, bad_karma_hell, good_karma_heaven, good_karma_hell);
            folder.gameObject.transform.eulerAngles = prefab.transform.eulerAngles + new Vector3(0, Random.Range(-20, 20));
            folder.avatar.gameObject.transform.eulerAngles = folder.gameObject.transform.eulerAngles + new Vector3(0, 0, Random.Range(-10, 10));
            folders.Add(folder);
            if (defineIfWin())
            {
                maxHeaven = 1;
                maxHell = 0;
            }
            else
            {
                maxHeaven = 0;
                maxHell = 1;
            }
        }
        for (int i = 0; i < folders.Count; i++)
        {
            Folder temp = folders[i];
            int randomIndex = Random.Range(i, folders.Count);
            folders[i] = folders[randomIndex];
            folders[randomIndex] = temp;
        }
        for (int i = 0; i < folders.Count; i++)
        {
            folders[i].name = "Folder (id: " + i + ")";
            folders[i].transform.position += new Vector3(0, (folders.Count - i) / 50f, 0);
            folders[i].UpdateStartPosition();
        }
        UpdateIndicators();
    }

    void Update ()
    {
        if (folders.Count == 0)
            return;
        Vector3 rotation = Camera.main.transform.eulerAngles;
        if (Mathf.Abs(rotation.y) < 20 || Mathf.Abs(rotation.y) > 340)
            folders[0].highlighted = true;
        else
            folders[0].highlighted = false;
    }

    private void UpdateIndicators()
    {
        int relHeaven = maxHeaven - heavenQueue.Count;
        int relHell = maxHell - hellQueue.Count;
        indicatorHeaven.text = "Remaining: " + relHeaven;
        indicatorHell.text = "Remaining: " + relHell;
        if (relHeaven <= 0 && !heavenLocked)
        {
            heavenLocked = true;
            FindObjectOfType<takeObject>().StopAllCoroutines();
        }
        if (relHell <= 0 && !hellLocked)
        {
            hellLocked = true;
            FindObjectOfType<takeObject>().StopAllCoroutines();
        }
    }

    private void AddToQueue(GameObject gobj, bool heaven)
    {
        float averageKarma = 0;

        Folder f = folders[0];
        folders.Remove(f);
        for (int i = 0; i < f.actions.Count; i++)
            averageKarma += f.actions[i].karma;
        if (heaven)
        {
            f.tampon_heaven.enabled = true;
        }
        else
        {
            f.tampon_hell.enabled = true;
        }
        f.Slide(gobj.transform.position + new Vector3(-1.1f, .1f - folders.Count / 500f, -1.5f), gobj.transform.eulerAngles + new Vector3(90, 0));
        if (heaven)
        {
            if (averageKarma > 0.0f)
                karma += averageKarma;
            else
                karma -= averageKarma;
            heavenQueue.Add(f);
        }
        else
        {
            if (averageKarma <= 0.0f)
                karma += averageKarma;
            else
                karma -= averageKarma;
            hellQueue.Add(f);
        }
        UpdateIndicators();
    }

    public void OnSelectTray(takeObject obj)
    {
        GameObject gobj;
        if (folders.Count > 0 && (gobj = obj.hovered.gameObject) != null)
        {
            if (gobj.tag == "tray_heaven")
                AddToQueue(gobj, true);
            else if (gobj.tag == "tray_hell")
                AddToQueue(gobj, false);
        }
        if ((heavenLocked && hellLocked) || folders.Count == 0)
        {
            foreach (Folder f in heavenQueue)
            {
                foreach (Actions.Action a in f.actions)
                {
                    if (a.karma < 0)
                        bad_karma_heaven += a.karma;
                    else
                        good_karma_heaven += a.karma;
                }
            }
            foreach (Folder f in hellQueue)
            {
                foreach (Actions.Action a in f.actions)
                {
                    if (a.karma > 0)
                        bad_karma_hell += a.karma;
                    else
                        good_karma_hell += a.karma;
                }
            }
            print(level);
            if (level < 3)
                StartCoroutine(LoadNextLevel());
            else
            {
                StartCoroutine(fade());             
            }
        }
    }

    IEnumerator fade()
    {
        float time = GameObject.Find("FadingSystem").GetComponent<Fading>().BeginFade(1);
        yield return new WaitForSeconds(time);
        if (defineIfWin() == true)
            end_sentence = "In fact your judgment was quite good !\nWe decided you to send you to the Heaven !\n\nRest in peace.";
        else
            end_sentence = "In fact your judgment was unbalanced !\nYou are going to stay in Hell for eternity !\n\nHasta la vista baby";
        SceneManager.LoadScene(2);
    }

    IEnumerator LoadNextLevel()
    {
        yield return new WaitForSeconds(2);
        level = (level + 1) % 4;
        Reset();
    }

    public bool defineIfWin()
    {
        if ((Mathf.Abs(good_karma_heaven) + Mathf.Abs(good_karma_hell)) > (Mathf.Abs(bad_karma_heaven) + Mathf.Abs(bad_karma_hell)))
            return true;
        else
            return false;
    }
}
