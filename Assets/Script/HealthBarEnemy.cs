using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarEnemy : MonoBehaviour
{
    [SerializeField] Slider healthBar;
    [SerializeField]private Camera camera;
    [SerializeField]private Transform target;
    [SerializeField]private Vector3 offset;
    // Start is called before the first frame update
    public void updateHealth(float currentHealth, float maxHealth){
        healthBar.value= currentHealth/maxHealth;
    }
    void LateUpdate(){
        transform.rotation= camera.transform.rotation;
        transform.position = target.position + offset;
    }
}
