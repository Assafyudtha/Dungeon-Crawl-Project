using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] PlayerCombat combat;
    public List<AttackAnimation> weaponAttackAnimation;
    public Skills weaponSkill1;
    public Skills weaponSkill2;
    public Skills weaponSkill3;
    public LivingEntity enemy;

    public float weaponDamage;
    Collider collision;
    //damage seharusnya kena ke living pake idamagable
    // Start is called before the first frame update
    void Start()
    {
        collision = GetComponent<Collider>();
        collision.enabled=false;
        combat=GetComponentInParent<PlayerCombat>();
    }

    // Update is called once per frame

    private void OnTriggerEnter(Collider coll){
        if(coll.tag == "Enemy"&&coll.GetType()==typeof(CapsuleCollider)){
            enemy = coll.GetComponent<EnemyScriptMerge>();
            SoundManager.PlaySound(SoundManager.Sound.hitEffect,true);
            enemy.TakeDamage(weaponDamage);
            print("damage: "+weaponDamage);
        }
    }
    public void ActivateCollision(){
        collision.enabled=true;
    }

    public void DisableCollision(){
        collision.enabled=false;
    }

    public void Skill1(Vector3 position, Quaternion rot, Player playerStamina){
        weaponSkill1.Cast(position,rot, playerStamina);
    }
    public void Skill2(Vector3 position, Quaternion rot){
        
    }

    public void Skill3(Vector3 position, Quaternion rot){
        
    }
}
