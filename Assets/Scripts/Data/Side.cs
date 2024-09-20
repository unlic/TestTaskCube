using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;

[Serializable]
public class Side
{
    public SideType Type;
    public int Power;
    public int Buf;
    public int Debuf;

    public string TakeString()
    {
        if (Debuf == 0&&Buf==0)
        {
            return Power.ToString();
        }
        else if (Debuf == 0 && Buf != 0)
        {
            return $"{Power}+<color=#019EFF>{Buf}</color>";
        }
        else if(Debuf != 0 && Buf == 0)
        {
            return $"{Power}+<color=#28FF00>{Debuf}</color>";
        }

        return $"{Power}+<color=#019EFF>{Buf}</color>+<color=#28FF00>{Debuf}</color>";

    }
}
