using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private ActionRecorder recorder;
    [SerializeField] private GameManager gameManager;

    private bool isMoving;
    private int obstacleLayerMask;

    private void Awake()
    {
        obstacleLayerMask = LayerMask.GetMask("Wall", "GhostWall");
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        if (!context.performed || isMoving)
        {
            return;
        }

        Vector2 input = context.ReadValue<Vector2>();

        if (input == Vector2.zero)
        {
            return;
        }

        StartCoroutine(Move(GetMoveDirection(input)));
    }

    private IEnumerator Move(Vector2 dir)
    {
        isMoving = true;

        ICommand command = new MoveCommand(dir, 1f);

        recorder?.Record(command);

        while (!IsBlocked(dir))
        {
            transform.Translate(dir * moveSpeed * Time.deltaTime);
            yield return null;
        }

        gameManager?.StepGhost();

        isMoving = false;
    }

    private bool IsBlocked(Vector2 dir)
    {
        RaycastHit2D hit =
            Physics2D.Raycast(transform.position, dir, 0.6f, obstacleLayerMask);

        return hit.collider != null;
    }

    private Vector2 GetMoveDirection(Vector2 input)
    {
        if (Mathf.Abs(input.x) > Mathf.Abs(input.y))
        {
            return input.x > 0 ? Vector2.right : Vector2.left;
        }

        return input.y > 0 ? Vector2.up : Vector2.down;
    }

    public void OnReset(InputAction.CallbackContext context)
    {
        if (!context.performed)
        {
            return;
        }

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}