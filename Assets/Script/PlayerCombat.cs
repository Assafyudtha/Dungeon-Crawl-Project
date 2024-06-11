using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    
    float lastClickedTime;
    float lastComboEnd;
    int comboCounter;
    public float activeDamage;
    [SerializeField]Animator anims;
    [SerializeField]WeaponContoller weaponController;
    public float cooldown;
    AnimationClip clip;
    // Start is called before the first frame update
    void Start()
    {
        anims = GetComponentInChildren<Animator>();
        weaponController=GetComponent<WeaponContoller>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Attack(){
        
        if(Time.time -lastComboEnd>0.1f && comboCounter <=weaponController.equippedWeaponAttackAnimation.Count){
            CancelInvoke("EndCombo");
            

            if(Time.time - lastClickedTime>=0.1f)
            {
                weaponController.equippedWeapon.ActivateCollision();
                anims.runtimeAnimatorController=weaponController.equippedWeaponAttackAnimation[comboCounter].animatorOV;
                anims.Play("Attacks",0,0);
                weaponController.equippedWeapon.weaponDamage = weaponController.equippedWeaponAttackAnimation[comboCounter].damage;
                comboCounter++;
                lastClickedTime=Time.time;
                
                if(comboCounter>weaponController.equippedWeaponAttackAnimation.Count-1){
                    comboCounter=0;
                }
                StartCoroutine(DisableCollision());
                
                
            }
        }
    }
    
    IEnumerator DisableCollision(){
        yield return new WaitForSeconds(0.3f);
        weaponController.equippedWeapon.DisableCollision();
    }
    public void ExitAttack(){
        if(anims.GetCurrentAnimatorStateInfo(0).normalizedTime>1f&& anims.GetCurrentAnimatorStateInfo(0).IsTag("Attack")){
            Invoke("EndCombo",0.5f);
        }
    }

    void EndCombo(){
        weaponController.equippedWeapon.DisableCollision();
        comboCounter=0;
        lastComboEnd=Time.time;
    }
}
