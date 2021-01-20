using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class QuestGoal 
{
    public GoalType goalType;

    public int requiredAmount;
    public int currentAmount;

    public int questNumber;

    public QuestGoal(GoalType goalType, int requiredAmount, int currentAmount)
    {
        this.goalType = goalType;
        this.requiredAmount = requiredAmount;
        this.currentAmount = currentAmount;
    }

    public bool IsReached()
    {
        return (currentAmount >= requiredAmount);
    }

    public void EnemyKilled()
    {
        if (goalType == GoalType.Kill)
            currentAmount++;
    }
    public void ItemCollected()
    {
        if (goalType == GoalType.Gathering)
            currentAmount++;
    }

    public void TutorialCompleted()
    {
        currentAmount++;
    }
}

public enum GoalType
{
    Kill, 
    Gathering,
    Tutorial
}
