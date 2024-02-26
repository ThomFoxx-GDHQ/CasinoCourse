using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Quiz Question", menuName = "ScriptableObjects/Quiz Question")]
public class QuizQuestion : ScriptableObject
{
    public string question = "";
    public string[] answers = new string[4];
    [Range(0, 3)] public int correctAnswer = 0;
}
