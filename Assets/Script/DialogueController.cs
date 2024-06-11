using System;
using System.Collections;
using System.Collections.Generic;
using Palmmedia.ReportGenerator.Core.Parser.Analysis;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class DialogueController : MonoBehaviour
{
    public GameObject dialogueCanvas;
    public GameObject combatUI;
    public TextMeshProUGUI npcName;
    public TextMeshProUGUI textDialogUI;
    public NPC npcDialogue;
    public float textSpeed;
    private int index;
    Player playerInput;
    // Start is called before the first frame update
    void Start()
    {
        textDialogUI.text =string.Empty;
        npcName.text = string.Empty;
        playerInput = GetComponent<Player>();
    }

    // Update is called once per frame

    public void StartDialogue(){
        index = 0;
        textDialogUI.text =string.Empty;
        npcName.text = string.Empty;
        combatUI.SetActive(false);
        dialogueCanvas.SetActive(true);
        StartCoroutine(TypeLine());
    }

    IEnumerator TypeLine(){
        foreach (char c in npcDialogue.dialog.DialogBawah[index].ToCharArray()){
            npcName.text= npcDialogue.dialog.DialogAtas[index];
            textDialogUI.text +=c;
            yield return new WaitForSeconds(textSpeed);
        }
    }

    public void nextLine(){
        if(index<npcDialogue.dialog.DialogBawah.Length-1){
            index++;
            npcName.text = string.Empty;
            textDialogUI.text=string.Empty;
            StartCoroutine(TypeLine());
        }else{
            dialogueCanvas.SetActive(false);
            combatUI.SetActive(true);
            playerInput.enabled=true;
        }
    }

    /* jadi dialognya akan diambil jika menyentuh collider npc seperti take damage ke musuh
    lalu nanti show UI dialog nya dan ubah text nama dan dialognya sesuai dari isi variable di NPC */
}
