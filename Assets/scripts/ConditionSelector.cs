using UnityEngine;
using UnityEngine.SceneManagement;

public class ConditionSelector : MonoBehaviour
{
    public void SelectNormalVision()
    {
        ConditionManager.selectedCondition = "Normal";
        SceneManager.LoadScene("Classroom");
    }

    public void SelectMyopia()
    {
        ConditionManager.selectedCondition = "Myopia";
        SceneManager.LoadScene("Classroom");
    }

    public void SelectHyperopia()
    {
        ConditionManager.selectedCondition = "Hyperopia";
        SceneManager.LoadScene("Classroom");
    }

    public void SelectPresbyopia()
    {
        ConditionManager.selectedCondition = "Presbyopia";
        SceneManager.LoadScene("Classroom");
    }

    public void SelectColorBlindness()
    {
        // Intentionally does nothing here now —
        // this button will instead show the color blindness sub-menu (wired separately in Unity)
    }

    public void SelectColorGrayscale()
    {
        ConditionManager.selectedCondition = "ColorGrayscale";
        SceneManager.LoadScene("Classroom");
    }

    public void SelectColorRedGreen()
    {
        ConditionManager.selectedCondition = "ColorRedGreen";
        SceneManager.LoadScene("Classroom");
    }
}