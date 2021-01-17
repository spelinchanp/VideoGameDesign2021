using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Quest
{
    public bool isActive;
    public string title;
    public int number;
    public string description;
    public int questExperience;
    public QuestGoal goal;

    public Quest(bool isActive, string title, int number, string description, QuestGoal goal, int questExperience)
    {
        this.isActive = isActive;
        this.title = title;
        this.number = number;
        this.description = description;
        this.goal = goal;
        this.questExperience = questExperience;
    }

    public void Complete()
    {
        isActive = false;
        Debug.Log(title + " was completed!");
    }
}
