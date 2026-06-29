using UnityEngine;

public class ButtonSwitch : MonoBehaviour
{
    [SerializeField] private GameObject door;

    private int stayCount;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!IsActivator(other))
        {
            return;
        }

        stayCount++;

        OpenDoor();
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (!IsActivator(other))
        {
            return;
        }

        stayCount = Mathf.Max(0, stayCount - 1);

        if (stayCount == 0)
        {
            CloseDoor();
        }
    }

    private bool IsActivator(Collider2D other)
    {
        return other.CompareTag("Player")
            || other.CompareTag("Ghost");
    }

    private void OpenDoor()
    {
        if (door == null)
        {
            return;
        }

        door.SetActive(false);
    }

    private void CloseDoor()
    {
        if (door == null)
        {
            return;
        }

        door.SetActive(true);
    }
}