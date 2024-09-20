using DG.Tweening;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    [SerializeField] private HPBar HPBar;
    [SerializeField] private Image avatar;

    public Action DieAction;

    private Color healsColor = Color.green;
    private Color damageColor = Color.red;
    private Color bufColor = Color.blue;
    private Color dieColor = Color.black;
    

    int currentBuf = 0;
    int currentDebuf = 0;

    int currentHP;
    int maxHP;
    int maxSP;


    public void SetData(Hero data)
    {
        maxHP = data.MaxHealsPoints;
        maxSP = data.MaxShieldPoints;

        avatar.sprite = data.Avatar;

        currentHP = maxHP;

        HPBar.SetValues(maxHP, maxSP);

        ResetBufAndDebufData();
    }
    
    public void TakeDamage(int damage)
    {

        if(damage == 0)
            return;

        if (currentBuf > 0 && currentBuf >= damage) 
        {
            currentBuf = currentBuf - damage;
        }
        else if (currentBuf > 0)
        {
            damage = damage - currentBuf;
            currentBuf = 0;
            currentHP = Mathf.Clamp(currentHP - damage, 0, maxHP);
        }
        else
        {
            currentHP = Mathf.Clamp(currentHP - damage, 0, maxHP);
        }
        HPBar.SetHP(currentHP);
        HPBar.SetSP(currentBuf);

        StartCoroutine(AnimateAvatar(damageColor));

        if (currentHP == 0)
            StartCoroutine(Die());
    }

    public void Heals(int value)
    {
        currentHP = Mathf.Clamp(currentHP + value, 0, maxHP);
        HPBar.SetHP(currentHP);
        StartCoroutine(AnimateAvatar(healsColor));
    }

    public void AddBuf(int value)
    {
        currentBuf = Mathf.Clamp(currentBuf + value, 0, maxSP);

        HPBar.SetSP(currentBuf);
        StartCoroutine(AnimateAvatar(bufColor));
    }

    public void AddDebuf(int value)
    {
        currentDebuf += value;
    }

    public void StepEnd()
    {
        TakeDamage(currentDebuf);
    }

    public void ResetBufAndDebufData()
    {
        currentBuf = 0;
        currentDebuf = 0;
        HPBar.SetSP(currentBuf);
    }
    private IEnumerator Die()
    {
        StartCoroutine(AnimateAvatar(dieColor));
        yield return new WaitForSeconds(0.2f);
        DieAction?.Invoke();
    }
    private IEnumerator AnimateAvatar(Color color)
    {
        avatar.DOColor(color, 0.2f);
        yield return new WaitForSeconds(0.2f);
        avatar.DOColor(Color.white, 0.2f);
    }


}
