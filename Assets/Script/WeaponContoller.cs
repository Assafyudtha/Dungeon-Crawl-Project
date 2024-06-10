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
        imageWeaponSkill_Slot2.sprite=equippedWeapon.weaponSkill2.SkillIcon;
        imageWeaponSkill_Slot3.sprite=equippedWeapon.weaponSkill3.SkillIcon;
        imageBackgroundWeaponSkill_Slot1.sprite=equippedWeapon.weaponSkill1.SkillIcon;
        imageBackgroundWeaponSkill_Slot2.sprite=equippedWeapon.weaponSkill2.SkillIcon;
        imageBackgroundWeaponSkill_Slot3.sprite=equippedWeapon.weaponSkill3.SkillIcon;

    }

    public void EquipWeapon(int weaponSlot){
        EquipWeapon(acquiredWeapons[weaponSlot]);
    }

    public void Skill1(Player playerStamina)
    {
        equippedWeapon.Skill1(castPoint.transform.position, castPoint.transform.rotation, playerStamina) ;
    }

    public void Skill2( ){
        

    }

    public void Skill3( ){
        

    }
}
