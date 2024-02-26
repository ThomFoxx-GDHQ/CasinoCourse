using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Reel Strip", menuName = "ScriptableObjects/Reel Strips")]
public class ReelStrip : ScriptableObject
{
    public Sprite[] images;
        

    public int ReelLength => images.Length;   


}
