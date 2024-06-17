using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class DialogueControllerCutscene : MonoBehaviour
{
    public TextMeshProUGUI topText;
    public TextMeshProUGUI botText;
    [SerializeField]DialogueSO[] dialog;
    [SerializeField]GameObject textUI;
    public float textSpeed;
    private int index;
    private int indexDialogSO;
    Player playerInput;

    void OnEnable(){
        textUI.SetActive(true);
        botText.text =string.Empty;
        topText.text = string.Empty;
       
        index=0;
        startDialogue();
        
    }
    // Update is called once per frame

    void startDialogue(){
            index = 0;
            botText.text =string.Empty;
            topText.text = string.Empty;
            StartCoroutine(TypeLine(indexDialogSO));
    }

    IEnumerator TypeLine(int idxDialog){
        foreach (char c in dialog[idxDialog].DialogAtas[index].ToCharArray()){
            topText.text +=c;
            yield return new WaitForSeconds(textSpeed);
        }
        foreach (char c in dialog[idxDialog].DialogBawah[index].ToCharArray()){
            botText.text +=c;
            yield return new WaitForSeconds(textSpeed);
        }
        yield return new WaitForSeconds(1f);
        nextLine(idxDialog);
    }

    public void nextLine(int idxDialog){
        if(index<dialog[idxDialog].DialogBawah.Length-1){
            index++;
            topText.text = string.Empty;
            botText.text=string.Empty;
            StartCoroutine(TypeLine(idxDialog));
        }else if(idxDialog<dialog.Length-1){
            idxDialog++;
            index=0;
            topText.text = string.Empty;
            botText.text=string.Empty;
            StartCoroutine(TypeLine(idxDialog));
        }else{
            topText.text = string.Empty;
            botText.text=string.Empty;
            textUI.SetActive(false);
            return;
        }
    }

    /* jadi dialognya akan diambil jika menyentuh collider npc seperti take damage ke musuh
    lalu nanti show UI dialog nya dan ubah text nama dan dialognya sesuai dari isi variable di NPC */
}
