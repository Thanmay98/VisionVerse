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
    public void LoadQuizDifficulty()
    {
        SceneManager.LoadScene("quizdifficulty");
    }
    public void LoadeasyUI()
    {
        SceneManager.LoadScene("easyUI");
    }
    public void LoadMediumUI()
    {
        SceneManager.LoadScene("mediumUI");
    }

    public void LoadHardUI()
    {
        SceneManager.LoadScene("hardUI");
    }
    public void LoadChatbot()
    {
        SceneManager.LoadScene("Chatbot");
    }
}
