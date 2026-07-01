using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject ghostPrefab;
    [SerializeField] private GameObject clearCanvas;
    [SerializeField] private Transform playerTransform;

    private GhostController currentGhost;
    private Vector3 playerStartPosition;

    private void Start()
    {
        if (playerTransform == null)
        {
            Debug.LogError("プレイヤーTransformがアサインされていない");
            return;
        }

        playerStartPosition = playerTransform.position;
    }

    public void SpawnGhost(List<ICommand> commands)
    {
        if (currentGhost != null)
        {
            return;
        }

        if (ghostPrefab == null)
        {
            Debug.LogError("GhostのPrefabがアサインされていない");
            return;
        }

        GameObject ghost =
            Instantiate(
                ghostPrefab,
                playerStartPosition,
                Quaternion.identity);

        if (!ghost.TryGetComponent(out GhostController controller))
        {
            Debug.LogError("GhostControllerが見つからない");
            Destroy(ghost);
            return;
        }

        currentGhost = controller;
        currentGhost.Init(commands);
    }

    public void StepGhost()
    {
        currentGhost?.Step();
    }

    public void ClearStage()
    {
        clearCanvas?.SetActive(true);

        Time.timeScale = 0f;
    }

    private void OnDisable()
    {
        Time.timeScale = 1f;
    }
}