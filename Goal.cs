using UnityEngine;

public class Goal : MonoBehaviour
{
    [SerializeField]
    private GameManager gameManager;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag(GameTagsAndLayers.Player))
        {
            return;
        }

        gameManager?.ClearStage();
    }
}