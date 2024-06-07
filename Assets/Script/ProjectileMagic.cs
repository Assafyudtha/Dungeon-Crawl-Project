using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class ProjectileMagic : MonoBehaviour
{
    [SerializeField]private float _triggerForce = 0.5f;
    [SerializeField]private float _explosionRadius = 5;
    [SerializeField]private float _explosionForce= 500;
    [SerializeField]private float explosionDamage= 20;
    [SerializeField]private float speed = 3;
    [SerializeField]private GameObject _particles;
    EnemyScriptMerge enemies;
    void Start()
    {
        Invoke("Explode", 3f);
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate (Vector3.forward*Time.deltaTime*speed);
    }

    void OnCollisionEnter(Collision coll){
        if(coll.collider.tag=="Enemy"){
            
            Collider[] surroundings = Physics.OverlapSphere(transform.position,_explosionRadius);
            Collider[] enemyColliders = Array.FindAll(surroundings, collider => collider.tag == "Enemy");
            foreach(Collider obj in enemyColliders){
                Rigidbody rb = obj.GetComponent<Rigidbody>();
                enemies = obj.GetComponent<EnemyScriptMerge>();
                enemies.TakeDamage(explosionDamage);    
                if(rb == null)continue;

                rb.AddExplosionForce(_explosionForce,transform.position,_explosionRadius);
            }
            Explode();
                  
        }else{
            Explode();
        }
    }

    void Explode(){
        GameObject effect = Instantiate (_particles,transform.position,Quaternion.LookRotation(Vector3.up)); //gak hilang di scene
        Destroy(effect, 1f);
        Destroy(gameObject);
    }
}
