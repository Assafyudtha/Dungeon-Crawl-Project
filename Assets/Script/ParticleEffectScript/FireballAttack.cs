using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skills : MonoBehaviour
{
    ParticleSystem particle;
    Enemy target;
    void Awake(){
        particle = GetComponentInChildren<ParticleSystem>();
    }

    void OnParticleCollision(GameObject other){
        if(other.tag == "enemy"){
            target = other.GetComponent<Enemy>();
            target.TakeDamage(10);
            print("Sukses");
        }
    }

    public void Cast(){
        if(particle == null){
        particle = GetComponentInChildren<ParticleSystem>();
        particle.Play();
        }else {
            particle.Play();
        }

    }

}
