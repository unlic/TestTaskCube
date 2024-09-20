using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.Rendering.DebugUI;

public class HPBar : MonoBehaviour
{
    [SerializeField] private Image currentHPFill;
    [SerializeField] private TextMeshProUGUI currentHPText;
    [SerializeField] private Image currentSPFill;
    [SerializeField] private TextMeshProUGUI currentSPText;


    [SerializeField] private GameObject SPBar;

    int maxHP;
    int maxSP;
    int currentHP;
    int currentSP = 0;

    public void SetValues(int _maxHP, int _maxSP)
    {
        maxHP = _maxHP;
        maxSP = _maxSP;
        SetHP(maxHP);
    }

    public void SetHP(int value)
    {
        currentHP = Mathf.Clamp(value, 0, maxHP);

        float percent = currentHP;
        percent = percent / maxHP;

        currentHPFill.DOFillAmount(percent, 0.3f);

        currentHPText.text = $"{currentHP}/{maxHP}";
    }

    public void SetSP(int value)
    {
        currentSP = Mathf.Clamp(value, 0, maxSP);

        float percent = currentSP;
        percent = percent / maxSP;

        currentSPFill.DOFillAmount(percent, 0.3f).OnComplete(() => SPBar.SetActive(currentSP != 0));

        currentSPText.text = $"{currentSP}/{maxSP}";
    }
}
