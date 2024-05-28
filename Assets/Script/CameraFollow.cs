using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Unity.AI;
using UnityEngine.AI;
using Unity.VisualScripting;

public class CameraFollow : MonoBehaviour
{
    [SerializeField]
    Transform player;
    
    public Vector3 cameraOffset;
    float followSpeed=20f;
    public float xMin=0f;
    Vector3 velocity= Vector3.zero;


    // Update is called once per frame
    void Update()
    {
        CameraOnPlayers();

        Vector3 forward = transform.TransformDirection(Vector3.forward) * 10;
        Debug.DrawRay(Input.mousePosition, transform.forward, Color.green);
    }

    void CameraOnPlayers(){
        if(player!=null){
            Vector3 targetPos= player.position+cameraOffset;
            //Math clamp untuk memberitahu posisi player agar tidak melewati 
            //batas axis x pada kamera agar tetap dalam sorotan kamera
            Vector3 clampedPos = new Vector3(Mathf.Clamp(targetPos.x, xMin, float.MaxValue), targetPos.y, targetPos.z);
            Vector3 smoothPos = Vector3.SmoothDamp(transform.position, clampedPos,ref velocity, followSpeed * Time.fixedDeltaTime);

            transform.position= smoothPos;
        }
    }

}
