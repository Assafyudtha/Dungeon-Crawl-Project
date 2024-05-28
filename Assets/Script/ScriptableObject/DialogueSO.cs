using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
[CreateAssetMenu(menuName ="Dialogue")]
public class DialogueSO : ScriptableObject
{
    public string[] charName;
    
    [TextArea]
    public string[] Dialogue;
}
