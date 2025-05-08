using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
    [Header("Quests")]
    public QuestData[] quests;

    [Header("UI References")]
    public RectTransform questsPanel;
    public GameObject questPrefab;

    private float gameTime = 0f;
    private GameObject[] questUIElements;

    void Start()
    {
        foreach (var quest in quests)
        {
            quest.currentValue = 0;
        }

        CreateQuestUI();

        CubeDeathSystem.onCubeDied += OnCubeDied;
    }

    void OnDestroy()
    {
        CubeDeathSystem.onCubeDied -= OnCubeDied;
    }

    void Update()
    {
        UpdateSurvivalQuest();

        UpdateQuestUI();
    }

    private void CreateQuestUI()
    {
        questUIElements = new GameObject[quests.Length];

        for (int i = 0; i < quests.Length; i++)
        {
            GameObject questUI = Instantiate(questPrefab, questsPanel);
            questUIElements[i] = questUI;

            QuestItemUI questItemUI = questUI.GetComponent<QuestItemUI>();

            if (questItemUI != null)
            {
                questItemUI.UpdateUI(quests[i]);
            }
        }
    }
    private void UpdateQuestUI()
    {
        for (int i = 0; i < quests.Length; i++)
        {
            QuestItemUI questItemUI = questUIElements[i].GetComponent<QuestItemUI>();

            if (questItemUI != null)
            {
                questItemUI.UpdateUI(quests[i]);
            }
        }
    }

    private void UpdateSurvivalQuest()
    {
        gameTime += Time.deltaTime;

        foreach (var quest in quests)
        {
            if (quest.type == QuestType.SurviveTime)
            {
                quest.currentValue = Mathf.Min((int)gameTime, quest.targetValue);
            }
        }
    }

    private void OnCubeDied(CubeType type, Vector3 position)
    {
        foreach (var quest in quests)
        {
            if (quest.type == QuestType.DestroyAnyCubes)
            {
                quest.IncrementValue();
            }
            else if (quest.type == QuestType.DestroySpecificCubes && type == quest.targetCubeType)
            {
                quest.IncrementValue();
            }
        }
    }
}
