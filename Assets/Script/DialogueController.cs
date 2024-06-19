using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class DialogueController : MonoBehaviour
{
    UIScript canvasUi;
    [SerializeField] DialogueSO dialog;
    public float textSpeed;
    private int index;
    // Start is called before the first frame update
    void Start()
    {
        canvasUi = FindObjectOfType<UIScript>();
        canvasUi.nameText.text =string.Empty;
        canvasUi.dialogueText.text = string.Empty;
    }

    // Update is called once per frame

    public void StartDialogue(){
        index = 0;
        canvasUi.dialogueText.text =string.Empty;
        canvasUi.nameText.text = string.Empty;
        canvasUi.Dialogue.SetActive(true);
        StartCoroutine(TypeLine());
    }

    IEnumerator TypeLine(){
        foreach (char c in dialog.DialogBawah[index].ToCharArray()){
            canvasUi.nameText.text= dialog.DialogAtas[index];
            canvasUi.dialogueText.text +=c;
            yield return new WaitForSeconds(textSpeed);
        }
        nextLine();
    }

    public void nextLine(){
        if(index<dialog.DialogBawah.Length-1){
            index++;
            canvasUi.nameText.text = string.Empty;
            canvasUi.dialogueText.text=string.Empty;
            StartCoroutine(TypeLine());
        }else{
            canvasUi.Dialogue.SetActive(false);

        }
    }

    /* jadi dialognya akan diambil jika menyentuh collider npc seperti take damage ke musuh
    lalu nanti show UI dialog nya dan ubah text nama dan dialognya sesuai dari isi variable di NPC */
}
