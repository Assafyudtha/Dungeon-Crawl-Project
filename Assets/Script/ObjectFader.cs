using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectFader : MonoBehaviour
{
    [SerializeField]
    float fadeSpeed, fadeAmount;
    float originalOpacity;
    Material Mats;
    
    public bool DoFade=false;

    // Start is called before the first frame update
    void Start()
    {
        Mats = GetComponent<Renderer>().material;
        originalOpacity = Mats.color.a;
    }

    // Update is called once per frame
    void Update()
    {
        if(DoFade){
            FadeNow();
        }else{
            ResetFade();
        }
    }

    void FadeNow()//Menaikkan Transparansi Objek atau Alpha pada tekstur.
    {
        Color currentColor = Mats.color;
        Color smoothColor = new Color(currentColor.r, currentColor.g, currentColor.b, 
        Mathf.Lerp(currentColor.a,fadeAmount, fadeSpeed*Time.deltaTime));
        Mats.color=smoothColor;        
    }

    void ResetFade()// Reset Alpha pada tekstur
    {
        Color currentColor = Mats.color;
        Color smoothColor = new Color(currentColor.r, currentColor.g, currentColor.b, 
        Mathf.Lerp(currentColor.a,originalOpacity, fadeSpeed*Time.deltaTime));
        Mats.color=smoothColor; 
    }
}
