using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(Player))]
public class HealingScript : MonoBehaviour
{
    [SerializeField]TextMeshProUGUI hpPotionAmountText;
    [SerializeField]TextMeshProUGUI staminaPotionAmountText;
    [SerializeField]GameObject notifUI;
    [SerializeField]TextMeshProUGUI Notification;
    Player playerScript;
    int healPotionAmount;
    int staminaPotionAmount;
    float heal=25f;
    float staminaReplenish=25f;
    // Start is called before the first frame update
    void Start()
    {
        healPotionAmount = PlayerPrefs.GetInt("healPotion");
        staminaPotionAmount = PlayerPrefs.GetInt("staminaPotion");
        playerScript = GetComponent<Player>();
    }

    public void UseHpPotion(){
        if(healPotionAmount>0)
        {
            playerScript.Heal(heal);
            healPotionAmount--;
            hpPotionAmountText.text = healPotionAmount.ToString();

        }else{
            notifUI.SetActive(true);
            Notification.text ="No Health Jamu"; // CHANGED Buat Notif baru
            Invoke("HideUI",1.5f);            
        }
    }

    public void UseStaminaPotion(){
        if(staminaPotionAmount>0)
        {
            playerScript.replenishStamina(staminaReplenish);
            staminaPotionAmount--;
            staminaPotionAmountText.text = staminaPotionAmount.ToString();

        }else{
            notifUI.SetActive(true);
            Notification.text ="No Stamina Jamu";
            Invoke("HideUI",1.5f);
        }
    }

    public void increaseHealPotionAmount(){
        healPotionAmount ++;
        hpPotionAmountText.text = healPotionAmount.ToString();
    }

    public void increaseStaminaPotionAmount(){
        staminaPotionAmount ++;
        staminaPotionAmountText.text = staminaPotionAmount.ToString();
    }

    void HideUI(){
        notifUI.SetActive(false);
    }
}
