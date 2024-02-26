using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class FloorGameSlot : MonoBehaviour
{
    [SerializeField] private TMP_Text _displayText;
    [SerializeField] private string _defaultText;
    [SerializeField] private int _gameID;

    public int currentID => _gameID;

    public void ChangeMachine(int gameID)
    {
        _gameID = gameID;
        _displayText.text = _defaultText + (_gameID + 1);
    }
}
