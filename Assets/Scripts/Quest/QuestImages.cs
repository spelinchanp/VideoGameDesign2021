using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestImages : MonoBehaviour
{
    public PlayerManager playerManager;

    public int currentQuest = 0;

    private void Update()
    {
        currentQuest = playerManager.quest.number;
        UpdateQuestImage();
    }

    void UpdateQuestImage()
    {
        int i = 0;
        foreach (Transform questImage in transform)
        {
            if (i == currentQuest)
            {
                questImage.gameObject.SetActive(true);
            }
            else
            {
                questImage.gameObject.SetActive(false);
            }
            i++;
        }
    }
}
