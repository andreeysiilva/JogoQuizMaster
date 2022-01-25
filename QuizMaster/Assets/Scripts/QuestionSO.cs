using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="Quiz Question", fileName ="New Question")]
public class QuestionSO : ScriptableObject
{
    [TextArea(2, 6)]
    [SerializeField] string question = "Digita sua pergunta aqui!!";
    [SerializeField] string[] answers = new string[5];
    [SerializeField] int correctAnswerIndex;

    public string GetQuestion()
    {
        return question;
    }
    
    public string GetAnswer(int index) {
        {
            return answers[index];
        }
    }

    public int GetCorrectAnswerIndex()
    {
        return correctAnswerIndex;
    }

}
