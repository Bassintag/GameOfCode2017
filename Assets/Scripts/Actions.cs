using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Actions : MonoBehaviour {

    public static Actions instance;
    public List<Action> actions;

    void Awake () {
        if (instance != null)
            Destroy(instance);
        actions = new List<Action>();
        actions.Add(new Action(-1f, "Killed someone"));
        actions.Add(new Action(-1f, "Sold his soul to the devil"));
        actions.Add(new Action(-0.8f, "Escaped from  prison"));
        actions.Add(new Action(-0.8f, "Robbed an homeless"));
        actions.Add(new Action(-0.6f, "Cheated on his wife"));
        actions.Add(new Action(-0.6f, "Ate his hamster"));
        actions.Add(new Action(-0.4f, "Ate the last cupcake"));
        actions.Add(new Action(-0.4f, "Made fun of an overweight person"));
        actions.Add(new Action(-0.4f, "Went to jail"));
        actions.Add(new Action(-0.4f, "Left his family alone"));
        actions.Add(new Action(-0.2f, "Confused Link and Zelda"));
        actions.Add(new Action(-0.2f, "Didn't help an old lady to cross the road"));
        actions.Add(new Action(-0.2f, "Peed on the street"));

        actions.Add(new Action(1f, "Saved someone's life"));
        actions.Add(new Action(1f, "Arrested a Criminal"));
        actions.Add(new Action(1f, "Won a Hackaton"));
        actions.Add(new Action(0.8f, "Stopped playing League of Legends"));
        actions.Add(new Action(0.8f, "Save Krillin with the Dragon balls"));
        actions.Add(new Action(0.8f, "Saved people from a fire"));
        actions.Add(new Action(0.6f, "Gave money to homeless people"));
        actions.Add(new Action(0.6f, "Has been honest all his life"));
        actions.Add(new Action(0.6f, "Bought ecological things"));
        actions.Add(new Action(0.4f, "Saved a cat stuck in a tree"));
        actions.Add(new Action(0.4f, "Stopped a thief"));
        actions.Add(new Action(0.2f, "Documented his code"));
        actions.Add(new Action(0.2f, "Fed well his goldfish"));
        actions.Add(new Action(0.2f, "Used public transport"));
        instance = this;
    }

    public Action getRandomNiceAction()
    {
        Action action = null;
        while (action == null || action.karma < 0)
            action = actions[Random.Range(0, actions.Count)];
        return action;
    }

    public Action getRandomNaughtyAction()
    {
        Action action = null;
        while (action == null || action.karma > 0)
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
        public bool Equals(Action other)
        {
            return this.karma == other.karma;
                //&& this.text == other.text;
        }
    }
}
