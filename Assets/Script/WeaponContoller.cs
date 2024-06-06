using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;

public class WeaponContoller : MonoBehaviour
{
    public Transform weaponPos;
    public Weapon[] acquiredWeapons;
    public Weapon equippedWeapon;
    public List<AttackAnimation> equippedWeaponAttackAnimation;
    public GameObject castPoint;
    
    public void EquipWeapon(Weapon weaponToEquip){
        if(equippedWeapon !=null){
            Destroy(equippedWeapon.gameObject);
        }
        equippedWeapon=Instantiate(weaponToEquip);
        equippedWeapon.transform.SetParent(weaponPos);
        equippedWeapon.transform.localPosition=Vector3.zero;
        equippedWeapon.transform.localRotation=Quaternion.identity;
        equippedWeaponAttackAnimation = equippedWeapon.weaponAttackAnimation;
    }

    public void EquipWeapon(int weaponSlot){
        EquipWeapon(acquiredWeapons[weaponSlot]);
    }

    public void Skill1(){
        equippedWeapon.Skill1(castPoint.transform.position, castPoint.transform.rotation);
    }

    public void Skill2(){
        equippedWeapon.Skill2();

    }

    public void Skill3(){
        equippedWeapon.Skill3();

    }
}
