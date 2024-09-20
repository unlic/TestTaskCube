using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Hero", menuName = "Hero", order = 51)]
public class Hero : ScriptableObject
{
    [SerializeField] private Sprite avatar;
    [SerializeField] private List<HandData> handDatas;

    [SerializeField] private int maxHealsPoints = 20;
    [SerializeField] private int maxShieldPoints = 5;


    public int MaxShieldPoints { get { return maxShieldPoints; } }
    public int MaxHealsPoints { get { return maxHealsPoints; } }
    public Sprite Avatar { get { return avatar; } }
    public List<HandData> TakeHandsList()
    {
        return handDatas; 
    }
}
