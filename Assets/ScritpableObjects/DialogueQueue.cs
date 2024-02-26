using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Dialogue", menuName = "ScriptableObjects/Dialogue Queue")]
public class DialogueQueue : ScriptableObject
{
    public string[] sentences;

    public int Length => sentences.Length;

    public string GetSentence(int index)
    {
        if (index <  sentences.Length) 
            return sentences[index];
        else return string.Empty;
    }
}
