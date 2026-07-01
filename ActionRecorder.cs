using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ActionRecorder : MonoBehaviour
{
    private readonly List<ICommand> commands = new();

    [SerializeField] private int maxActions = 5;
    [SerializeField] private GameManager gameManager;
    [SerializeField] private TextMeshProUGUI ghostText;
    [SerializeField] private string ghostMoveText = "Ghost Move!";

    private bool ghostSpawned;

    public IReadOnlyList<ICommand> Commands => commands;

    private void Start()
    {
        UpdateGhostText();
    }

    public void Record(ICommand command)
    {
        if (ghostSpawned)
        {
            return;
        }

        commands.Add(command);
        UpdateGhostText();

        if (commands.Count < maxActions)
        {
            return;
        }

        if (gameManager == null)
        {
            Debug.LogError("ゲームマネージャーがアサインされていない");
            return;
        }

        gameManager.SpawnGhost(new List<ICommand>(commands));

        ghostSpawned = true;
        ghostText.text = ghostMoveText;
    }

    public void SetMaxActions(int value)
    {
        maxActions = value;
        UpdateGhostText();
    }

    private void UpdateGhostText()
    {
        if (ghostText == null)
        {
            return;
        }

        ghostText.text = $"Ghost Spawn   {commands.Count} / {maxActions}";
    }
}