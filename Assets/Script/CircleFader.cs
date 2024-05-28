using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircleFader : MonoBehaviour
{
    public static int PosID= Shader.PropertyToID("_Position");
    public static int SizeID= Shader.PropertyToID("_Size");
    public static int TransparentID = Shader.PropertyToID("");

    [SerializeField]Material WallMaterial;
    [SerializeField] Camera cameraPlayer;
    [SerializeField] LayerMask wallMask;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        var dir = cameraPlayer.transform.position-transform.position;
        var ray= new Ray(transform.position, dir.normalized);

        if(Physics.Raycast(ray, 1000, wallMask))
        WallMaterial.SetFloat(SizeID,1);
        else
        WallMaterial.SetFloat(SizeID, 0);

        var view = cameraPlayer.WorldToViewportPoint(transform.position);
        var pos2D = new Vector2(view.x, view.y);

        WallMaterial.SetVector(PosID, pos2D);
    }
}
