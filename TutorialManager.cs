using UnityEngine;

public class TutorialManager : MonoBehaviour
{
    [SerializeField] private GameObject[] panels;

    private int currentIndex = 0;

    private void Start()
    {
        foreach (GameObject panel in panels)
        {
            panel.SetActive(false);
        }

        currentIndex = 0;
    }
    public void Next()
    {
        if (currentIndex < panels.Length - 1)
        {
            ShowPanel(currentIndex + 1);
        }
    }

    public void Previous()
    {
        if (currentIndex > 0)
        {
            ShowPanel(currentIndex - 1);
        }
    }

    private void ShowPanel(int index)
    {
        for (int i = 0; i < panels.Length; i++)
        {
            panels[i].SetActive(i == index);
        }

        currentIndex = index;
    }

    public void CloseTutorial()
    {
        foreach (GameObject panel in panels)
        {
            panel.SetActive(false);
        }
    }
    public void OpenTutorial()
    {
        ShowPanel(0);
    }
}