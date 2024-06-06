using UnityEngine;

public class Skills : MonoBehaviour
{
    ProjectileMagic fire;
    public void Cast(Vector3 castPos, Quaternion rotation){
        fire = GetComponent<ProjectileMagic>();
        ProjectileMagic fireball = Instantiate(fire, castPos, rotation);
    }

}
