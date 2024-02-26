using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DesignGameManager : MonoBehaviour
{
    private int _prizeSize = 1;
    private int _prizeFrequency = 5;
    private bool[] _resultArray = new bool[7];
    //Array key: 
    //  0 = Backgrounds
    //  1 = Symbols
    //  2 = Features
    //  3 = Spins
    //  4 = Jackpot
    //  5 = Prize
    //  6 = Market

    //background => B is Correct
    //symbols => B is correct
    //Feature => Any but None
    //Free Spins => Yes
    //Jackpot => Yes
    //Size => Opposite of Frequency
    //Frequency => Opposite of Size
    //Market => "Both"Correct

    [SerializeField] private Slider _resultSlider;

    public void BackgroundCheck(string buttonValue)
    {
        if (buttonValue == "B")
            _resultArray[0] = true;
        else
            _resultArray[0] = false;
    }

    public void SymbolCheck(string buttonValue)
    {
        if (buttonValue == "B")
            _resultArray[1] = true;
        else
            _resultArray[1] = false;
    }

    public void FeatureCheck(TMP_Dropdown dropdown)
    {
        if (dropdown.value > 0)
            _resultArray[2] = true;
        else
            _resultArray[2] = false;
    }

    public void SpinsCheck(Slider slider)
    {
        if (slider.value == 1)
            _resultArray[3] = true;
        else
            _resultArray[3] = false;
    }

    public void JackpotCheck(Slider slider)
    {
        if (slider.value == 1)
            _resultArray[4] = true;
        else
            _resultArray[4] = false;
    }

    public void SizeCheck(int buttonValue)
    {
        _prizeSize = buttonValue;
        if (_prizeSize == _prizeFrequency)
            _resultArray[5] = true;
        else
            _resultArray[5] = false;
    }

    public void FrequencyCheck(int buttonValue)
    {
        _prizeFrequency = buttonValue;
        if (_prizeFrequency == _prizeSize)
            _resultArray[5] = true;
        else
            _resultArray[5] = false;
    }

    public void MarketCheck(string buttonValue)
    {
        if (buttonValue == "C")
            _resultArray[6] = true;
        else
            _resultArray[6] = false;
    }

    public void ResultDisplay()
    {
        _resultSlider.value = 0;
        foreach (bool result in _resultArray)
        {
            if (result)
                _resultSlider.value++;
        }
    }

}
