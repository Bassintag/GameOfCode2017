using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

    [HideInInspector]
    public List<Folder> folders;
    [HideInInspector]
    public List<Folder> heavenQueue;
    [HideInInspector]
    public List<Folder> hellQueue;

    private int folderCount = 10;

    [Range(1, 7)]
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

    void Start ()
    {
        folders = new List<Folder>();
        heavenQueue = new List<Folder>();
        hellQueue = new List<Folder>();
        if (level != 7)
        {
            folderCount = 10;
            folders.AddRange(createNewFolders(folderCount / 2, 3, 0));
            folders.AddRange(createNewFolders(folderCount / 2, 0, 3));
        }
        else
        {
            folderCount = 1;
            folders.AddRange(createNewFolders(folderCount, 0, 1));
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
        indicatorHeaven.text = "Remaining: " + (folderCount / 2 - heavenQueue.Count);
        indicatorHell.text = "Remaining: " + (folderCount / 2 - hellQueue.Count);
    }

    private void AddToQueue(GameObject gobj, bool heaven)
    {
        Folder f = folders[0];
        folders.Remove(f);
        f.Slide(gobj.transform.position + new Vector3(-1.1f, .1f - folders.Count / 500f, -1.5f), gobj.transform.eulerAngles + new Vector3(90, 0));
        if (heaven)
            heavenQueue.Add(f);
        else
            hellQueue.Add(f);
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
    }
}
