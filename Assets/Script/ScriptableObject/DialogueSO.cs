using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
[CreateAssetMenu(menuName ="Dialogue")]
public class DialogueSO : ScriptableObject
{
    [TextArea]
    public string[] DialogAtas;
    
    [TextArea]
    public string[] DialogBawah;
}
