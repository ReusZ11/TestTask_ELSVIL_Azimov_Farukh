using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New QuestData", menuName = "Quest", order = 1)]
public class QuestData : ScriptableObject
{
    public QuestType type;
    public string description;
    public int targetValue;
    public int currentValue;
    public CubeType targetCubeType;
    public bool isTimeQuest = false;

    public float Progress => Mathf.Clamp01((float)currentValue / targetValue);
    public bool IsCompleted => currentValue >= targetValue;


    public void IncrementValue(int amount = 1)
    {
        if (!IsCompleted)
        {
            currentValue = Mathf.Min(currentValue + amount, targetValue);
        }
    }
}

public enum QuestType
{
    SurviveTime,
    DestroyAnyCubes,
    DestroySpecificCubes
}


