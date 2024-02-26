using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct PositionV5
{
    public PositionV5(int r1, int r2, int r3, int r4, int r5)
    {
        R1 = r1;
        R2 = r2;
        R3 = r3;
        R4 = r4;
        R5 = r5;
    }

    [SerializeField] public int R1;
    [SerializeField] public int R2;
    [SerializeField] public int R3;
    [SerializeField] public int R4;
    [SerializeField] public int R5;

    public override string ToString() => $"({R1},{R2},{R3},{R4},{R5})";

}
     

[CreateAssetMenu(fileName = "New Reel Stops", menuName = "ScriptableObjects/Reel Stops")]
public class ReelStop : ScriptableObject
{
    public PositionV5[] positions;
}
