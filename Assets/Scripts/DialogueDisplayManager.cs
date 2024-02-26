using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class DialogueDisplayManager : MonoBehaviour
{
    [SerializeField]
    private DialogueQueue _queue;
    [SerializeField]
    private TMP_Text _dialogueText;
    private WaitForSeconds _shortWait = new WaitForSeconds(.001f);
    private int _currentIndex = -1;

    [SerializeField]
    private Button _exitButton;

    [SerializeField]
    private Image _dialogueCharacterImage;
    [SerializeField]
    private Sprite[] _characterSprites;

    
    public void NextLine()
    {
        _currentIndex++;
        if (_currentIndex < _queue.Length)
            StartCoroutine(LoadLineRoutine(_currentIndex));
        else
            _exitButton.gameObject.SetActive(true);

    }

    IEnumerator LoadLineRoutine(int index)
    {
        yield return _shortWait;
        _dialogueText.text = "";
        yield return _shortWait;
        _dialogueText.text = _queue.GetSentence(index);
    }

    public void CharacterPortraitChange(int index)
    {
        if (index < _characterSprites.Length)
            _dialogueCharacterImage.sprite = _characterSprites[index];
        else
            _dialogueCharacterImage.sprite = _characterSprites[0];
    }
}
