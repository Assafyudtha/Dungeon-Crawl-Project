using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions.Comparers;

public class Skills : MonoBehaviour
{
    public ProjectileMagic fire;
    public Sprite SkillIcon;
    [SerializeField] float staminaCost;
    Player playerBase;

    public void Cast(Vector3 castPos, Quaternion rotation,Player playerStamina){
        if(playerStamina!=null){
            if(playerStamina.stamina>=staminaCost){
                if(fire==null){    
                    fire = GetComponent<ProjectileMagic>();
                }else{
                    _ = Instantiate(fire, castPos, rotation);
                    playerStamina.costOfStamina(staminaCost);
                }
            }else{
                print("no stamina");
            }
        }else{
            print("No Player");
        }
    }

}

