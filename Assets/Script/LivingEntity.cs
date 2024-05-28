using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LivingEntity : MonoBehaviour, IDamageable
{
    public float startingHealth;
    public float health{get; protected set;}
    protected bool dead;
    public event System.Action OnDeath;
    // Start is called before the first frame update
    protected virtual void Start()
    {
        health = startingHealth;
    }

    public virtual void TakeHit(float damage,Vector3 hitpPoint,Vector3 hitDirection){
        TakeDamage(damage);
    }

    public virtual void TakeDamage(float damage){
        health -=damage;
        if(health<=0){
            Die();
        }
    }

    public virtual void Heal(float hp){
        health += hp;
    }

    public virtual void Die(){
        dead = true;
        if(OnDeath!=null){
            OnDeath();
        }
        GameObject.Destroy(gameObject);
    }


}
