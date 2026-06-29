using System.Collections;
using UnityEngine;

public class MoveCommand : ICommand
{
    private const float MoveSpeed = 10f;
    private const float CastRadius = 0.4f;
    private const float CastDistance = 0.1f;

    private readonly Vector2 direction;
    private readonly float distance;

    public MoveCommand(Vector2 direction, float distance)
    {
        this.direction = direction;
        this.distance = distance;
    }

    public IEnumerator Execute(GameObject target)
    {
        Transform targetTransform = target.transform;

        int layerMask = GetLayerMask(target);

        while (!IsBlocked(targetTransform.position, layerMask))
        {
            targetTransform.Translate(direction * MoveSpeed * Time.deltaTime);
            yield return null;
        }
    }

    public void Undo(GameObject target)
    {
        target.transform.Translate(-direction * distance);
    }

    private bool IsBlocked(Vector2 position, int layerMask)
    {
        RaycastHit2D hit =
            Physics2D.CircleCast(
                position,
                CastRadius,
                direction,
                CastDistance,
                layerMask);

        return hit.collider != null;
    }

    private static int GetLayerMask(GameObject target)
    {
        return target.CompareTag("Ghost")
            ? LayerMask.GetMask("Wall")
            : LayerMask.GetMask("Wall", "GhostWall");
    }
}