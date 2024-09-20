using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SideInfo : MonoBehaviour
{
    [SerializeField] private RawImage icon;
    [SerializeField] private TextMeshProUGUI power;

    public void SetData(Material material, string powerValue)
    {
        icon.texture = material.mainTexture;   
        power.text = powerValue;
    }
}
