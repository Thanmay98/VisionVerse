using UnityEngine;

public class ClassroomPanelLoader : MonoBehaviour
{
    public GameObject normalPanel;
    public GameObject myopiaPanel;
    public GameObject hyperopiaPanel;
    public GameObject presbyopiaPanel;
    public GameObject colorPanel;

    void Start()
    {
        normalPanel.SetActive(false);
        myopiaPanel.SetActive(false);
        hyperopiaPanel.SetActive(false);
        presbyopiaPanel.SetActive(false);
        colorPanel.SetActive(false);

        switch (ConditionManager.selectedCondition)
        {
            case "Normal":
                normalPanel.SetActive(true);
                break;

            case "Myopia":
                myopiaPanel.SetActive(true);
                break;

            case "Hyperopia":
                hyperopiaPanel.SetActive(true);
                break;

            case "Presbyopia":
                presbyopiaPanel.SetActive(true);
                break;

            case "Color":
                colorPanel.SetActive(true);
                break;
        }
    }
}