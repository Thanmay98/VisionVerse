using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public void LoadScenarioSelection()
    {
        SceneManager.LoadScene("scenarioselection");
    }

    public void LoadEyeConditionSelection()
    {
        SceneManager.LoadScene("eyeconditionselection");
    }

    public void LoadMainMenu()
    {
        SceneManager.LoadScene("main menu");
    }
    public void LoadClassroom()
    {
        SceneManager.LoadScene("Classroom");
    }
}