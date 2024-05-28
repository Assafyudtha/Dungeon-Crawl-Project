using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName ="Attacks/Normal Attack")]
public class AttackAnimation : ScriptableObject
{
    public AnimatorOverrideController animatorOV;
    public float damage;
}
