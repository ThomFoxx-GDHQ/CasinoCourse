using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CardFlipManager : MonoBehaviour
{
    [SerializeField] Animator _cardPanelAnim;
    [SerializeField] TMP_Text _creditsDisplayText;
    bool _isFlipped = false;
    [SerializeField] Button _playButton;
    [SerializeField] TMP_Text _playButtonText;
    [SerializeField] string _playText, _resetText;
    [SerializeField] GameObject[] _toggleGroups;
    [SerializeField] Toggle[] _redBlackToggles;
    [SerializeField] Toggle[] _suitsToggles;
    [SerializeField] TMP_Text _betAmountText, _winAmountText;
    [SerializeField] GameObject _winAmountLabel;
    [SerializeField] bool _isSuitsGame = false;
    [SerializeField] int _maxBetAmount = 1000;
    int _betAmount = 0;

    private void Start()
    {
        _winAmountLabel.SetActive(false);
        UpdateCreditsDisplay();
        ResetBet();
        SetSuitGame(_isSuitsGame);
    }

    public void PlayBet()
    {
        if (_isFlipped)
        {
            ResetFlip();
            return;
        }

        if (_betAmount <= 0 || PlayerManager.Instance.Credits() <= 0)
            return;

        if (!_isSuitsGame && (_redBlackToggles[0].isOn || _redBlackToggles[1].isOn))
            BetAction();

        else if (_isSuitsGame)
            if (_suitsToggles[0].isOn || _suitsToggles[1].isOn || _suitsToggles[2].isOn || _suitsToggles[3].isOn)
                BetAction();

    }

    private void BetAction()
    {
        _playButton.interactable = false;
        _isFlipped = true;
        int RNG = Random.Range(0, 4);

        _cardPanelAnim.SetInteger("CardID", RNG);
        _cardPanelAnim.SetBool("CardFlipped", _isFlipped);

        PlayerManager.Instance.RemoveBet(_betAmount);
        UpdateCreditsDisplay();
        StartCoroutine(CardFlipDelay(RNG));
        _playButtonText.text = _resetText;
    }

    public void ResetFlip()
    {
        _isFlipped = false;
        _cardPanelAnim.SetBool("CardFlipped", _isFlipped);
        _winAmountLabel.SetActive(false);

        if (!_isSuitsGame)
            foreach (Toggle toggle in _redBlackToggles)
            {
                toggle.isOn = false;
            }
        else
            foreach (Toggle toggle in _suitsToggles)
            {
                toggle.isOn = false;
            }

        _playButtonText.text = _playText;
        _playButton.interactable = true;
    }

    IEnumerator CardFlipDelay(int result)
    {
        yield return new WaitForSeconds(3);
        CheckBet(result);
        _playButton.interactable = true;
    }

    private void CheckBet(int result)
    {
        int winnings = 0;
        if (!_isSuitsGame)
        {
            if (result < 2 && _redBlackToggles[0].isOn)
                winnings = _betAmount * 2;
            else if (result >= 2 && _redBlackToggles[1].isOn)
                winnings = _betAmount * 2;
        }
        else
            switch (result)
            {
                case 0: //Clubs
                    if (_suitsToggles[0].isOn)
                        winnings = _betAmount * 4;
                    break;
                case 1: //Spades
                    if (_suitsToggles[1].isOn)
                        winnings = _betAmount * 4;
                    break;
                case 2: //Hearts
                    if (_suitsToggles[2].isOn)
                        winnings = _betAmount * 4;
                    break;
                case 3: //Diamonds
                    if (_suitsToggles[3].isOn)
                        winnings = _betAmount * 4;
                    break;
                default:
                    break;
            }

        if (winnings > 0)
        {
            _winAmountText.text = $"{winnings}";
            PlayerManager.Instance.AddWinnings(winnings);
        }
        else _winAmountText.text = "Lost!";


        UpdateCreditsDisplay();
        _winAmountLabel.SetActive(true);

        if (_betAmount > PlayerManager.Instance.Credits())
        {
            _betAmount = PlayerManager.Instance.Credits();
            _betAmountText.text = _betAmount.ToString();
        }
    }

    private void UpdateCreditsDisplay()
    {
        _creditsDisplayText.text = $"Credits: {PlayerManager.Instance.Credits()}";
    }

    private void ResetBet()
    {
        _betAmountText.text = "0";
        _betAmount = 0;
    }

    public void ChangeBetAmount(int amount)
    {
        int betCheck = _betAmount + amount;
        if (betCheck >= 0 && betCheck <= _maxBetAmount && betCheck <= PlayerManager.Instance.Credits())
        {
            _betAmount += amount;
            _betAmountText.text = _betAmount.ToString();
        }
    }

    public void SetSuitGame(bool suitsGame)
    {
        if (suitsGame)
        {
            _isSuitsGame = true;
            _toggleGroups[0].SetActive(true);
            _toggleGroups[1].SetActive(false);
        }
        else
        {
            _isSuitsGame=false;
            _toggleGroups[0].SetActive(false);
            _toggleGroups[1].SetActive(true);
        }
        Vector3 rotation = transform.rotation.eulerAngles;
        rotation.z += 180;
        Instantiate(_winAmountLabel, transform.position + new Vector3(0, -1.28f, 0), Quaternion.Euler(rotation));
    }    
}
