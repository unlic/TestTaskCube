using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Sprites", menuName = "SpritesCube", order = 52)]

public class PicturesCubeSides : ScriptableObject
{
    [SerializeField] private Material Attack;
    [SerializeField] private Material AttackAndShield;
    [SerializeField] private Material VenomAttack;
    [SerializeField] private Material Shield;
    [SerializeField] private Material Heals;

    public Material TakeTexture(SideType type)
    {
        switch (type)
        {
            case SideType.Attack:
                return Attack;
            case SideType.AttackAndShield:
                return AttackAndShield;
            case SideType.VenomAttack:
                return VenomAttack;
            case SideType.Shield:
                return Shield;
            case SideType.Heals:
                return Heals;
            default:
                return Attack;
        }
    }
}
