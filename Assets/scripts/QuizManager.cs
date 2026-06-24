using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class QuizManager : MonoBehaviour
{
    public TMP_Text questionText;
    public TMP_Text scoreText;

    public TMP_Text optionAText;
    public TMP_Text optionBText;
    public TMP_Text optionCText;
    public TMP_Text optionDText;

    public GameObject optionA;
    public GameObject optionB;
    public GameObject optionC;
    public GameObject optionD;

    public QuestionData[] questions;

    private int currentQuestion = 0;
    private int score = 0;

    void Start()
    {
        LoadQuestion();
    }

    void LoadQuestion()
    {
        if (currentQuestion >= questions.Length)
        {
            questionText.text =
                "Quiz Complete!\n\nScore: " +
                score + "/" + questions.Length;

            scoreText.text = "";

            optionA.SetActive(false);
            optionB.SetActive(false);
            optionC.SetActive(false);
            optionD.SetActive(false);

            Invoke("ReturnToDifficulty", 3f);
            return;
        }

        questionText.text = questions[currentQuestion].question;

        optionAText.text = questions[currentQuestion].optionA;
        optionBText.text = questions[currentQuestion].optionB;
        optionCText.text = questions[currentQuestion].optionC;
        optionDText.text = questions[currentQuestion].optionD;

        scoreText.text = "Score: " + score;
    }

    public void CheckAnswer(int optionIndex)
    {
        string selectedAnswer = "";

        if (optionIndex == 0)
            selectedAnswer = optionAText.text;
        else if (optionIndex == 1)
            selectedAnswer = optionBText.text;
        else if (optionIndex == 2)
            selectedAnswer = optionCText.text;
        else if (optionIndex == 3)
            selectedAnswer = optionDText.text;

        if (selectedAnswer == questions[currentQuestion].correctAnswer)
        {
            score++;
        }

        currentQuestion++;
        LoadQuestion();
    }

    void ReturnToDifficulty()
    {
        SceneManager.LoadScene("quizdifficulty");
    }
}