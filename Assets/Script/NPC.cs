using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class NPC : MonoBehaviour
{
    [SerializeField]string npcName;
    public DialogueSO dialog;
    [SerializeField]TextMeshProUGUI namebox;
    [SerializeField]Camera cams;

    // Start is called before the first frame update
    void Start()
    {
        namebox.text = npcName;
        namebox.transform.rotation= cams.transform.rotation;
    }

}
