using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Quiz : MonoBehaviour
{
    [Header("Perguntas")]
    [SerializeField] TextMeshProUGUI questionText;
    [SerializeField] QuestionSO question;

    [Header("Respostas")]
    [SerializeField] GameObject[] answerButtons;
    int correctAnswerIndex;
    bool hasAnsweredEarly;

    [Header("Cor dos Botões")]
    [SerializeField] Sprite defaultAnswerSprite;
    [SerializeField] Sprite correctAnswerSprite;

    [Header("Cronometro")]
    [SerializeField] Image timerImage;
    Timer timer;


    void Start()
    {
        timer = FindObjectOfType<Timer>();
        GetNextQuestion();
        //DisplayQuestion();
    }

    void Update()
    {
        timerImage.fillAmount = timer.fillFraction;
        if (timer.loadNextQuestion)
        {
            hasAnsweredEarly = false;
            GetNextQuestion();
            timer.loadNextQuestion = false;
        }
        else if(!hasAnsweredEarly && !timer.isAnsweringQuestion)
        {
            DisplayAnswer(-1);
            SetButtonState(false);
        }
    }

    public void onAnswerSelected(int index)
    {
        {
            hasAnsweredEarly = true;
            DisplayAnswer(index);
            SetButtonState(false);
            timer.CancelTimer();
        }
    }

    void DisplayAnswer(int index)
    {
        Image buttonImage;
            
            if (index == question.GetCorrectAnswerIndex())
            {
                questionText.text = "Correto!";
                buttonImage = answerButtons[index].GetComponent<Image>();
                buttonImage.sprite = correctAnswerSprite;
            }
            else
            {
                correctAnswerIndex = question.GetCorrectAnswerIndex();
                string correctAnswer = question.GetAnswer(correctAnswerIndex);
                questionText.text = "Resposta errada, resposta correta é \n" + correctAnswer;
                buttonImage = answerButtons[correctAnswerIndex].GetComponent<Image>();
                buttonImage.sprite = correctAnswerSprite;
            }
    }

    void GetNextQuestion()
    {
        SetButtonState(true);
        SetDefaultButtonSprites();
        DisplayQuestion();
    }

    void DisplayQuestion()
    {
        questionText.text = question.GetQuestion();

        for (int i = 0; i < answerButtons.Length; i++)
        {
            TextMeshProUGUI buttonText = answerButtons[i].GetComponentInChildren<TextMeshProUGUI>();
            buttonText.text = question.GetAnswer(i);
        }
    }

    void SetButtonState(bool state)
    {
        for (int i = 0; i < answerButtons.Length; i++)
        {
            Button button = answerButtons[i].GetComponent<Button>();
            button.interactable = state;
        }
    }


    void SetDefaultButtonSprites()
    {
        for (int i = 0; i < answerButtons.Length; i++)
        {
            Image buttonImage = answerButtons[i].GetComponent<Image>();
            buttonImage.sprite = defaultAnswerSprite;
        }
    }
}
