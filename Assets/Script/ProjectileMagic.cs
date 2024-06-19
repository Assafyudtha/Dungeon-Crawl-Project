using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.Animations.Rigging;

public class ProjectileMagic : MonoBehaviour
{
    [SerializeField]private float _triggerForce = 0.5f;
    [SerializeField]private float _explosionRadius = 5;
    [SerializeField]private float _explosionForce= 500;
    [SerializeField]private float explosionDamage= 20;
    [SerializeField]private float speed = 3;
    [SerializeField]private GameObject _particles;
    [SerializeField]int orientRotation=1;
    EnemyScriptMerge enemies;
    Rigidbody rbBall;
    Vector3 startingForward;
    void Start()
    {
        rbBall=GetComponent<Rigidbody>();
        Invoke("Explode", 3f);
        startingForward = transform.forward;
    }

    // Update is called once per frame
    void Update()
    {
        rbBall.AddForce(startingForward*speed,ForceMode.Impulse);
        transform.Rotate(Vector3.left*orientRotation);
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
                  
        }else if(coll.collider.tag =="Player"){
            return;
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
