using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class QuizPanelManager : MonoBehaviour
{
    [SerializeField] Button[] _quizButtons;
    [SerializeField] QuizQuestion[] _possibleQuestions;
    QuizQuestion[] _questions;
    List<int> _selectedQuestions;
    private TMP_Text _buttonText;
    private int _score = 0;
    private int _questionsAnswered = 0;
    [SerializeField] int _questionsToAnswer = 9;


    private void Start()
    {
        ShuffleAndPickQuestions();
        LoadButtons();

    }

    private void ShuffleAndPickQuestions()
    {
        _selectedQuestions = new List<int>();
        _questions = new QuizQuestion[_quizButtons.Length];

        if (_possibleQuestions.Length < _quizButtons.Length)
            return;

        for (int i = 0; i < _quizButtons.Length; i++)
        {
            int RNG = Random.Range(0, _possibleQuestions.Length);

            while (_selectedQuestions.Contains(RNG))
            {
                RNG = Random.Range(0, _possibleQuestions.Length);
            }

            _selectedQuestions.Add(RNG);
            _questions[i] = _possibleQuestions[RNG];
        }
    }

    private void LoadButtons()
    {
        for (int i =0; i < _quizButtons.Length; ++i)
        {
            _buttonText = _quizButtons[i].GetComponentInChildren<TMP_Text>();
            _buttonText.text = _questions[i].name;
        }
    }

    public QuizQuestion GetButtonQuestion(int buttonID)
    {
        return _questions[buttonID];
    }

    public void MarkButton(int buttonID, bool isCorrect)
    {
        if (isCorrect)
        {
            _quizButtons[buttonID].transform.GetChild(0).gameObject.SetActive(true);
            _score++;
        }
        else
            _quizButtons[buttonID].transform.GetChild(1).gameObject.SetActive(true);

        _quizButtons[buttonID].interactable = false;
        _questionsAnswered++;

        if (_questionsAnswered == _questionsToAnswer)
            PlayerManager.Instance.ReportQuizScore(_score);
    }
}
