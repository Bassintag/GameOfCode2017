using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Actions : MonoBehaviour {

    public static Actions instance;
    public List<Action> actions;

    void Start () {
        if (instance != null)
            Destroy(instance);
        actions = new List<Action>();
        actions.Add(new Action(1f, "Mon action très gentille"));
        actions.Add(new Action(0.5f, "Mon action gentille"));
        actions.Add(new Action(0f, "Mon action neutre"));
        actions.Add(new Action(-0.5f, "Mon action méchante"));
        actions.Add(new Action(-1f, "Mon action très méchante"));
        instance = this;
    }
	
    public Action getRandomNiceAction()
    {
        Action action = null;
        while (action == null || action.karma <= 0)
            action = actions[Random.Range(0, actions.Count)];
        return action;
    }

    public Action getRandomNaughtyAction()
    {
        Action action = null;
        while (action == null || action.karma >= 0)
            action = actions[Random.Range(0, actions.Count)];
        return action;
    }

    public class Action
    {
        public float karma;
        public string text;
        public Action(float karma, string text)
        {
            this.karma = karma;
            this.text = text;
        }
    }
}
