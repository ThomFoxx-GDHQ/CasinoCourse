using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class FloorGameManager : MonoBehaviour
{
    [SerializeField] private GameObject[] _slots;
    [SerializeField] private Image[] _lights;
    [Tooltip("Ratio should Equal to 10")]
    [SerializeField] private int[] _gameRatio = new int[] { 8, 2, 0 };
    private int[] _currentGameRatio = new int[] { 0, 0, 10 };

    [SerializeField] private Slider _slider;

    [SerializeField] private bool _liveUpdate;

    private int _currentSlotID;

    public void SlotSelect(int slotID)
    {
        _currentSlotID = slotID;
    }

    public void GameSelect(int gameID)
    {
        var slot = _slots[_currentSlotID].transform.GetComponent<FloorGameSlot>();
        
        if (slot == null)
            Debug.LogError("Slot Var is Null");

        _currentGameRatio[slot.currentID]--;
        _currentGameRatio[gameID]++;

        slot.ChangeMachine(gameID);

        if (_liveUpdate)
        {
            if (_currentGameRatio[gameID] <= _gameRatio[gameID])
            {
                _lights[_currentSlotID].color = Color.green;
            }
            else
            {
                _lights[_currentSlotID].color = Color.red;
            }
            SliderUpdate();
        }
    }

    public void SliderUpdate()
    {
        int correct = 0;
        foreach (Image image in _lights)
        {
            if (image.color == Color.green)
                correct++;
        }

        _slider.SetValueWithoutNotify(correct);
    }

    public void LightsUpdate()
    {
        for (int i = 0; i < _lights.Length; i++)
        {
            int gameId = _slots[i].GetComponent<FloorGameSlot>().currentID;
            if (_currentGameRatio[gameId] <= _gameRatio[gameId])
            {
                _lights[i].color = Color.green;
            }
            else
            {
                _lights[i].color = Color.red;
            }
        }
    }

}
