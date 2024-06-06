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
        if(coll.collider.tag=="Enemy"&&coll.collider.GetType() == typeof(CapsuleCollider)){
            if(coll.relativeVelocity.magnitude>2){
                var surroundings = Physics.OverlapSphere(transform.position,_explosionRadius);
                foreach(var obj in surroundings){
                    var rb = obj.GetComponent<Rigidbody>();
                    enemies = obj.GetComponent<EnemyScriptMerge>();
                    enemies.TakeDamage(explosionDamage);    
                    if(rb == null)continue;

                    rb.AddExplosionForce(_explosionForce,transform.position,_explosionRadius);
                }
                Explode();
            }        
        }
    }

    void Explode(){
        Instantiate (_particles,transform.position,Quaternion.identity);
        Destroy(gameObject);
    }
}
