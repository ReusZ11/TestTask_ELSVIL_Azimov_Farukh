using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class QuestItemUI : MonoBehaviour
{
    [Header("UI Components")]
    public TextMeshProUGUI descriptionText;
    public TextMeshProUGUI progressText;
    public Image progressSlider;
    public GameObject completionMark;
    //public Image backgroundPanel;

    [Header("Colors")]
    public Color normalColor;
    public Color completedColor;

    public void UpdateUI(QuestData questData)
    {
        descriptionText.text = questData.description;
        if(questData.isTimeQuest)
        {
            int remainingSeconds = questData.targetValue - questData.currentValue;
            string timeText = FormatTime(remainingSeconds);
            progressText.text = timeText;
        }
        else progressText.text = $"{questData.currentValue} / {questData.targetValue}";
        progressSlider.fillAmount = questData.Progress;

        bool isCompleted = questData.IsCompleted;
        completionMark.SetActive(isCompleted);

        descriptionText.color = isCompleted ? completedColor : normalColor;
    }

    private string FormatTime(int totalSeconds)
    {
        int minutes = totalSeconds / 60;
        int seconds = totalSeconds % 60;
        return string.Format("{0:00}:{1:00}", minutes, seconds);
    }
}