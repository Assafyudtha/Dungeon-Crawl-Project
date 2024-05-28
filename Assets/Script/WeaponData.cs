using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName ="Weapons/New Weapons")]
public class WeaponData : ScriptableObject
{
    public string weaponName;
    public float damagePower;
    public GameObject weaponObject;
}
