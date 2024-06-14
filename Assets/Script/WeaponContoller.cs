using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponContoller : MonoBehaviour
{
    public Transform weaponPos;
    public Weapon[] acquiredWeapons;
    public Weapon equippedWeapon;
    public List<AttackAnimation> equippedWeaponAttackAnimation;
    public GameObject castPoint;
    bool skill1Ready=true;
    bool skill2Ready=true;
    bool skill3Ready=true;
    float currentFillAmount;


    [SerializeField]Image imageWeaponSkill_Slot1;
    [SerializeField]Image imageBackgroundWeaponSkill_Slot1;
    [SerializeField]Image imageWeaponSkill_Slot2;
    [SerializeField]Image imageBackgroundWeaponSkill_Slot2;
    [SerializeField]Image imageWeaponSkill_Slot3;
    [SerializeField]Image imageBackgroundWeaponSkill_Slot3;


    
    public void EquipWeapon(Weapon weaponToEquip){
        if(equippedWeapon !=null){
            Destroy(equippedWeapon.gameObject);
        }
        equippedWeapon=Instantiate(weaponToEquip);
        equippedWeapon.transform.SetParent(weaponPos);
        equippedWeapon.transform.localPosition=Vector3.zero;
        equippedWeapon.transform.localRotation=Quaternion.identity;
        equippedWeaponAttackAnimation = equippedWeapon.weaponAttackAnimation;
        imageWeaponSkill_Slot1.sprite=equippedWeapon.weaponSkill1.SkillIcon;
        //imageWeaponSkill_Slot2.sprite=equippedWeapon.weaponSkill2.SkillIcon;
        //imageWeaponSkill_Slot3.sprite=equippedWeapon.weaponSkill3.SkillIcon;
        imageBackgroundWeaponSkill_Slot1.sprite=equippedWeapon.weaponSkill1.SkillIcon;
        //imageBackgroundWeaponSkill_Slot2.sprite=equippedWeapon.weaponSkill2.SkillIcon;
        //imageBackgroundWeaponSkill_Slot3.sprite=equippedWeapon.weaponSkill3.SkillIcon;

    }

    public void EquipWeapon(int weaponSlot){
        EquipWeapon(acquiredWeapons[weaponSlot]);
    }

    public void Skill1(Player playerStamina)
    {
        if(skill1Ready){
            //SoundManager.PlaySound(SoundManager.Sound.playerSpellCast,false);
            currentFillAmount = 0f;
            imageBackgroundWeaponSkill_Slot1.fillAmount=currentFillAmount;
            equippedWeapon.Skill1(castPoint.transform.position, castPoint.transform.rotation, playerStamina) ;
            skill1Ready=false;
            StartCoroutine(StartCooldownSkill1());
        }else{
            print("On Cooldown");
        }
    }

    public void Skill2( ){
        

    }

    public void Skill3( ){
        

    }

    IEnumerator StartCooldownSkill1(){
        float timer = equippedWeapon.weaponSkill1.cooldown;
        while(!skill1Ready){
            timer -=Time.deltaTime;
            imageWeaponSkill_Slot1.fillAmount = Mathf.Lerp(1,0,timer/equippedWeapon.weaponSkill1.cooldown);
            if(timer<0){
            skill1Ready = true;
            }
            yield return null;
        }
    }

    IEnumerator StartCooldownSkill2(){
        float timer = equippedWeapon.weaponSkill2.cooldown;
        while(!skill1Ready){
            timer -=Time.deltaTime;
            imageWeaponSkill_Slot2.fillAmount = Mathf.Lerp(1,0,timer/equippedWeapon.weaponSkill2.cooldown);
            if(timer<0){
            skill3Ready = true;
            }
            yield return null;
        }
    }

    IEnumerator StartCooldownSkill3(){
        float timer = equippedWeapon.weaponSkill3.cooldown;
        while(!skill1Ready){
            timer -=Time.deltaTime;
            imageWeaponSkill_Slot3.fillAmount = Mathf.Lerp(1,0,timer/equippedWeapon.weaponSkill3.cooldown);
            if(timer<0){
            skill3Ready = true;
            }
            yield return null;
        }
    }
}
