using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class QuestGiver : MonoBehaviour
{
    private Quest quest;

    public PlayerManager player;

    public GameObject questWindow;
    public TextMeshProUGUI titleText;
    public TextMeshProUGUI descriptionText;

    private List<Quest> questList = new List<Quest>()
    {
        new Quest(true, 
            "Find oxygen processor", 
            0, 
            "Get the oxygen processor from the ship",
            new QuestGoal(GoalType.Gathering, 1, 0),
            5),
        new Quest(true, 
            "Find red flower", 
            1, 
            "Hold the oxygen processor and pick up three red flowers to replenish your oxygen", 
            new QuestGoal(GoalType.Gathering, 3, 0),
            5),
        new Quest(true,
            "Kill wild ox-like creature",
            2,
            "Use the mining tool to kill one of the wild ox-like creatures roaming the planet",
            new QuestGoal(GoalType.Kill, 1, 0),
            5),
    };

    // 1. Check for quest completion, then move onto the next one based 
    // on its number variable
    // -> Have a list of quests (quest class) to access based upon the number
    // 2. Set the player quest, update the QuestWindow

    private void Start()
    {
        questWindow.SetActive(true);
        quest = questList[1];
        SetQuestWindow();
    }

    private void Update()
    {
        // if quest is completed
        if (!quest.isActive)
        {
            quest = questList[quest.number++];
            SetQuestWindow();
        }
    }

    public void SetQuestWindow()
    {
        titleText.text = quest.title;
        descriptionText.text = quest.description;
        player.quest = quest;
    }
}
