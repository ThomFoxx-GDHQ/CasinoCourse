using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class QuestionPanel : MonoBehaviour
{
    [SerializeField] QuizPanelManager _quizPanel;
    private QuizQuestion _question;
    int _buttonID;
    [SerializeField] TMP_Text _questionText;
    [SerializeField] TMP_Text[] _answerTexts;
    [SerializeField] GameObject[] _answerHighlights;
    [SerializeField] Toggle[] _answerToggles;
    string[] _answers;
    int _correctAnswer;
    [SerializeField] Color _rightColor, _wrongColor;
    [SerializeField] GameObject _correctMark, _wrongMark;
    [SerializeField] Button _submitButton, _exitButton;
    ToggleGroup _group;
    [SerializeField] Animator _animator;
    
    public void SetButtonID(int buttonID)
    {
        _buttonID = buttonID;
    }

    public void LoadPanel()
    {
        _group = _answerToggles[0].transform.parent.GetComponent<ToggleGroup>();
        _question = _quizPanel.GetButtonQuestion(_buttonID);
        _questionText.text = _question.question;
        ShuffleAnswers();
        ClearResults();
        _animator.SetBool("IsOpen", true);
    }

    private void ShuffleAnswers()
    {
        _answers = new string[_question.answers.Length];

        //Feed Answers into new Array because _answers will act as reference to SO otherwise and shuffle SO Answers.
        for (int i=0;i<_question.answers.Length;i++)
        {
            _answers[i] = _question.answers[i];
        }
        
        _correctAnswer = _question.correctAnswer;

        string hold = "";
        int RNG;

        //Shuffle New Array
        for (int i = 0;  i < _answers.Length; i++)
        {
            RNG = Random.Range(0, _answers.Length);

            hold = _answers[RNG];
            _answers[RNG] = _answers[i];
            _answers[i] = hold;

            if (i == _correctAnswer)
                _correctAnswer = RNG;
            else if (RNG == _correctAnswer)
                _correctAnswer = i;
        }

        for (int i =0; i < _answerTexts.Length; i++)
        {
            _answerTexts[i].text = _answers[i];
        }
    }

    public void CheckAnswer()
    {
        

        if (!_group.AnyTogglesOn())
            return;

        _submitButton.interactable = false;

        for (int i = 0; i < _answerToggles.Length; i++)
        {
            if (_answerToggles[i].isOn)
                if (i == _correctAnswer)
                {
                    _correctMark.SetActive(true);
                    //Highlight Correct Answer
                    _answerHighlights[i].SetActive(true);
                    _answerHighlights[i].GetComponent<Image>().color = _rightColor;
                    _exitButton.gameObject.SetActive(true);
                    _quizPanel.MarkButton(_buttonID, true);
                    return;
                }
                else
                {
                    _wrongMark.SetActive(true);
                    //Highlight Wrong Answer
                    _answerHighlights[i].SetActive(true);
                    _answerHighlights[i].GetComponent<Image>().color = _wrongColor;
                    //Highlight Correct Answer
                    _answerHighlights[_correctAnswer].SetActive(true);
                    _answerHighlights[_correctAnswer].GetComponent<Image>().color = _rightColor;
                    _exitButton.gameObject.SetActive(true);
                    _quizPanel.MarkButton(_buttonID, false);
                    return;
                }
        }
    }

    private void ClearResults()
    {
        foreach (GameObject highlight in _answerHighlights)
            highlight.SetActive(false);

        _correctMark.SetActive(false);
        _wrongMark.SetActive(false);
        _submitButton.interactable = true;
        _group.SetAllTogglesOff();
    }

    public void ClosePanel()
    {
        _animator.SetBool("IsOpen", false);
    }
}
