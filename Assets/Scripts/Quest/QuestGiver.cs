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
            "Find the oxygen processor",
            0,
            "Walk up to your ship and press e to retrieve the oxygen processor",
            new QuestGoal(GoalType.Gathering, 1, 0), 
            5),
        new Quest(true,
            "Equip the oxygen processor",
            1,
            "Use the scroll wheel to switch to the oxygen processor",
            new QuestGoal(GoalType.Tutorial, 1, 0), 
            5),
        new Quest(true,
            "Find red flower",
            2,
            "Hold the oxygen processor and pick up three red flowers to replenish your oxygen. You " +
            "will have to do this throughout your visit to TR-640 to survive",
            new QuestGoal(GoalType.Gathering, 3, 0),
            5),
        /* End of day 1 */
        new Quest(true,
            "Equip the laser beam",
            3,
            "Use the scroll wheel to switch to the laser beam",
            new QuestGoal(GoalType.Tutorial, 1, 0),
            5),
        new Quest(true,
            "Kill wild ox-like creature",
            4,
            "Use the laser beam to kill one of the wild ox-like creatures roaming the planet",
            new QuestGoal(GoalType.Kill, 1, 0),
            5),
        new Quest(true,
            "Open your inventory",
            5,
            "Open your inventory by pressing the TAB button",
            new QuestGoal(GoalType.Tutorial, 1, 0),
            5),
        new Quest(true,
            "Eat the meat",
            6,
            "With your inventory open, click the meat to eat it and restore your health",
            new QuestGoal(GoalType.Tutorial, 1, 0),
            5),
        /* End of day 2 */
        new Quest(true,
            "Equip the mining tool",
            7,
            "Use the scroll wheel to equip the mining tool",
            new QuestGoal(GoalType.Tutorial, 1, 0),
            5),
        new Quest(true,
            "Find 5 iron ore",
            8,
            "Find 5 iron ore and click it to harvest it with the mining tool",
            new QuestGoal(GoalType.Tutorial, 1, 0),
            5),
        new Quest(true,
            "Repair your ship",
            9,
            "Return to your ship and press E to repair your ship with the collected iron",
            new QuestGoal(GoalType.Tutorial, 1, 0),
            5),
        /* End of day 3 */

    };

    // 1. Check for quest completion, then move onto the next one based 
    // on its number variable
    // -> Have a list of quests (quest class) to access based upon the number
    // 2. Set the player quest, update the QuestWindow

    private void Start()
    {
        questWindow.SetActive(true);
        quest = questList[3];
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
