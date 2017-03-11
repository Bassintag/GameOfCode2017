using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    [HideInInspector]
    public List<Folder> folders;
    public Folder prefab;

    public List<Folder> createNewFolders(int number_folders, int max_good_action, int max_bad_actions)
    {
        List<Folder> folders = new List<Folder>();
        while (folders.Count < number_folders)
        {
            Folder folder = Instantiate(prefab);
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
            // Melanger folder.actions !
            folders.Add(folder);
        }
        return folders;
    }

    void Start ()
    {
        List<Folder> folders = new List<Folder>();
        folders.AddRange(createNewFolders(2, 2, 0));
        folders.AddRange(createNewFolders(2, 0, 2));
        //Melanger folders !
    }
	
	void Update ()
    {
        if (folders.Count == 0)
            return;
        Vector3 rotation = Camera.main.transform.eulerAngles;
        if (Mathf.Abs(rotation.y) < 45 || Mathf.Abs(rotation.y) > 315)
        {
            folders[0].highlighted = true;
        }
        else
        {
            folders[0].highlighted = false;
        }
    }
}
