using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostController : MonoBehaviour
{
    private IReadOnlyList<ICommand> commands;

    private int currentIndex;
    private bool isMoving;

    public void Init(IReadOnlyList<ICommand> cmds)
    {
        commands = cmds;
        currentIndex = 0;
    }

    public void Step()
    {

        Debug.Log(
        $"Step / isMoving={isMoving} / currentIndex={currentIndex} / count={commands?.Count}"
    );

        if (commands == null) return;
        if (isMoving) return;
        if (currentIndex >= commands.Count) return;

        StartCoroutine(ExecuteCurrentCommand());
    }

    public void PlayAll()
    {
        if (commands == null || isMoving)
        {
            return;
        }

        StartCoroutine(PlayAllRoutine());
    }

    private IEnumerator ExecuteCurrentCommand()
    {
        isMoving = true;

        ICommand command = commands[currentIndex];
        currentIndex++;

        yield return command.Execute(gameObject);

        isMoving = false;
    }

    private IEnumerator PlayAllRoutine()
    {
        while (currentIndex < commands.Count)
        {
            yield return ExecuteCurrentCommand();
        }
    }
}