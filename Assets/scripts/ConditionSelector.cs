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
        ConditionManager.selectedCondition = "Color";
        SceneManager.LoadScene("Classroom");
    }
}