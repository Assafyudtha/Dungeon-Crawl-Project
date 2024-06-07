using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions.Comparers;

public class Skills : MonoBehaviour
{
    public ProjectileMagic fire;
    [SerializeField]Sprite SkillIcon;
    [SerializeField] float staminaCost;
    public void Cast(Vector3 castPos, Quaternion rotation){
       
        fire = GetComponent<ProjectileMagic>();
        ProjectileMagic fireball = Instantiate(fire, castPos, rotation);
    }

}

