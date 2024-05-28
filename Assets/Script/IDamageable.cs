using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDamageable 
{
    // Start is called before the first frame update
    void TakeHit(float damage, Vector3 hitPoint, Vector3 hitDirection);
    void TakeDamage(float damage);

    void Heal(float hp);
}
