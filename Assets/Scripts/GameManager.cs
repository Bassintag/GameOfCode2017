using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    [HideInInspector]
    public List<Folder> folders;
    public Folder prefab;

    private List<Folder> createNewFolders(int number_folders, int max_good_action, int max_bad_actions)
    {
        List<Folder> folders = new List<Folder>();
        while (folders.Count < number_folders)
        {
            Folder folder = Instantiate(prefab, transform.position, Quaternion.identity);
            folder.gameObject.transform.eulerAngles = prefab.transform.eulerAngles + new Vector3(0, Random.Range(-20, 20));
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
        folders.AddRange(createNewFolders(5, 3, 0));
        folders.AddRange(createNewFolders(5, 0, 3));
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
    }
	
	void Update ()
    {
        if (folders.Count == 0)
            return;
        Vector3 rotation = Camera.main.transform.eulerAngles;
        if (Mathf.Abs(rotation.y) < 20 || Mathf.Abs(rotation.y) > 340)
        {
            folders[0].highlighted = true;
        }
        else
        {
            folders[0].highlighted = false;
        }
    }
}
