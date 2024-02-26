using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;

public class PlayerManager : MonoSingleton<PlayerManager>
{
    private int _credits = 100;
    private int _quizScore = 0;

    public int Credits()
    {
        return _credits;
    }

    public void AddWinnings(int winningAmount)
    {
        _credits += winningAmount;
    }

    public void RemoveBet(int betAmount)
    {
        _credits -= betAmount;
    }
    
    public void AssignCredits(int startAmount)
    {
        _credits = startAmount;
    }

    public void ReportQuizScore(int quizScore)
    {
        _quizScore = quizScore;
        //calculate Completion Score (chips)
        //add credits based on Completion Score
    }
}
