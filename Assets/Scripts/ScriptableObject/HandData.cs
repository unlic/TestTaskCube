using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Hand", menuName = "HandData", order = 53)]
public class HandData : ScriptableObject
{
    public Side Top;
    public Side Left;
    public Side Right;
    public Side Bottom;
    public Side Front;
    public Side Backward;

    public PicturesCubeSides Pictures;
    public Sides CurrentSide = Sides.Top;

    public Side TakeDataByCurrentSide()
    {
        switch (CurrentSide)
        {
            case Sides.Top:
                return Top;
            case Sides.Left:
                return Left;
            case Sides.Right:
                return Right;
            case Sides.Bottom:
                return Bottom;
            case Sides.Front:
                return Front;
            case Sides.Backward:
                return Backward;
            default:
                return Top;
        }
    }
}
