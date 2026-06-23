using UnityEngine;

public class ContinueButtonHandler : MonoBehaviour
{
    public GameObject normalPanel;
    public GameObject myopiaPanel;
    public GameObject hyperopiaPanel;
    public GameObject presbyopiaPanel;
    public GameObject colorPanel;

    public void ShowCorrectPanel()
    {
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
            case "ColorGrayscale":
                colorPanel.SetActive(true);
                break;
             case "ColorRedGreen":
                colorPanel.SetActive(true);
                break;
        }
    }
}