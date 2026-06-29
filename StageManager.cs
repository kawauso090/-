using System.Collections.Generic;
using UnityEngine;

public class StageManager : MonoBehaviour
{
    [SerializeField] private List<StageData> stages;
    [SerializeField] private GameObject player;
    [SerializeField] private ActionRecorder actionRecorder;

    private int currentIndex;
    private GameObject currentStage;

    private void Start()
    {
        LoadStage(0);
    }

    public void LoadStage(int index)
    {
        if (index < 0 || index >= stages.Count)
        {
            Debug.LogError($"Invalid stage index: {index}");
            return;
        }

        StageData data = stages[index];

        if (data == null)
        {
            Debug.LogError("StageData is null.");
            return;
        }

        if (currentStage != null)
        {
            Destroy(currentStage);
        }

        currentStage = Instantiate(data.StagePrefab);

        if (player != null)
        {
            player.transform.position = data.PlayerStartPosition;
        }

        actionRecorder?.SetMaxActions(data.MaxActions);

        currentIndex = index;
    }

    public void NextStage()
    {
        int nextIndex = currentIndex + 1;

        if (nextIndex >= stages.Count)
        {
            Debug.Log("Game Clear!");
            return;
        }

        LoadStage(nextIndex);
    }
}