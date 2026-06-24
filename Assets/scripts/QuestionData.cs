using UnityEngine;

[System.Serializable]
public class QuestionData
{
    [TextArea(2, 5)]
    public string question;

    public string optionA;
    public string optionB;
    public string optionC;
    public string optionD;
    public string correctAnswer;
}