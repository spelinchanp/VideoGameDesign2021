using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class QuestGiver : MonoBehaviour
{
    public Quest quest;

    public PlayerManager player;

    public GameObject questWindow;
    public TextMeshProUGUI titleText;
    public TextMeshProUGUI descriptionText;

    public void UpdateQuestWindow()
    {
        questWindow.SetActive(true);
        titleText.text = quest.title;
        descriptionText.text = quest.description;
        quest.isActive = true;
        player.quest = quest;
    }

    public void AcceptQuest()
    {
        /*questWindow.SetActive(false);*/
        quest.isActive = true;
        // give to player
    }
}
