using UnityEngine;

public class PanelToggle : MonoBehaviour
{
    public GameObject menuPanel;
    public GameObject[] contentPanels;

    public void ShowContent(int indexToShow)
    {
        menuPanel.SetActive(false);
        for (int i = 0; i < contentPanels.Length; i++)
        {
            contentPanels[i].SetActive(i == indexToShow);
        }
    }

    public void ShowMenu()
    {
        menuPanel.SetActive(true);
        for (int i = 0; i < contentPanels.Length; i++)
        {
            contentPanels[i].SetActive(false);
        }
    }
}