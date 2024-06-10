using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LivingEntity : MonoBehaviour, IDamageable
{
    public float startingHealth;
    public float health{get; protected set;}
    public float startingStamina;
    [SerializeField]float staminaRegenRate;
    public float stamina{get; private set;}
    protected bool dead;
    public event System.Action OnDeath;
    // Start is called before the first frame update
    protected virtual void Start()
    {
        health = startingHealth;
        stamina = startingStamina;
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

    public virtual void costOfStamina(float staminaCost){
        stamina-= staminaCost;
    }

    public virtual void Heal(float hp){
        health += hp;
    }

    public virtual void replenishStamina(float staminaReplenish){
        stamina+=staminaReplenish;
    }

    public virtual void Die(){
        dead = true;
        if(OnDeath!=null){
            OnDeath();
        }
        GameObject.Destroy(gameObject);
    }

    public virtual IEnumerator StaminaRegen(){
        while(true){
            if (stamina < startingStamina){
                    stamina = Mathf.Clamp(stamina+staminaRegenRate,0,startingStamina);
                    print("regen");
                    yield return new WaitForSeconds(1f);
                    
            }else{
                    print("waiting");
                    yield return null;
            }
        }
    }


}
