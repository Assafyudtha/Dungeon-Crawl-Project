using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] PlayerCombat combat;
    public List<AttackAnimation> weaponAttackAnimation;
    public AttackAnimation weaponSkill1Animation;
    public AttackAnimation weaponSkill2Animation;
    public AttackAnimation weaponSkill3Animation;
    public LivingEntity enemy;

    float damage;
    Collider collision;
    //damage seharusnya kena ke living pake idamagable
    // Start is called before the first frame update
    void Start()
    {
        collision = GetComponent<SphereCollider>();
        collision.enabled=false;
        combat=GetComponentInParent<PlayerCombat>();
    }

    // Update is called once per frame
    void Update()
    {
        damage=combat.activeDamage;
    }

    private void OnTriggerEnter(Collider coll){
        if(coll.tag == "Enemy"&&coll.GetType()==typeof(CapsuleCollider)){
            enemy = coll.GetComponent<EnemyScriptMerge>();
            enemy.TakeDamage(damage);
            print("damage: "+damage);
        }
    }
    public void ActivateCollision(){
        collision.enabled=true;
    }

    public void DisableCollision(){
        collision.enabled=false;
    }

    public void Skill1(){

    }
    public void Skill2(){

    }

    public void Skill3(){
        
    }
}
