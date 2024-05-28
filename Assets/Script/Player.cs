using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using Unity.AI;
using UnityEngine.AI;
using UnityEditor.Rendering;
using TMPro;
using System;
using System.Linq;
using Unity.VisualScripting;
public class Player : LivingEntity
{


    //---------Batalkan ClickToMove, Ubah Ke Movement WASD Karna Kombat Kurang Intense nanti----------//

    CustomActions input;
    private Vector3 _input;
    Rigidbody rb;
    [Header("Movement")]
    [SerializeField]private float _speed =5;
    [SerializeField]private LayerMask groundMask;
    [SerializeField]private Camera mainCamera;
    [SerializeField]UIScript ui;
    public Animator anims;
    public enum State {attack, idle};
    State currentState;
    [SerializeField]Transform Cam;
    Vector3 camForward;
    Vector3 move;
    Vector3 moveInput;
    float forwardAmount;
    float turnAmount;
    PlayerCombat combatControll;
    WeaponContoller weaponController;
    DialogueController dialogueController;
    int weaponSlot = 0;
    bool attack=true;
    [Header("UI Settings")]
    public Image healthBar;
    public Image staminaBar;
    float stamina;
    [SerializeField]GameObject interactNotif;
    
    

    void FixedUpdate()
    {
        if(currentState==State.idle)
        {
            Move();
        
        if (Cam !=null){
            camForward = Vector3.Scale(Cam.up,new Vector3(1,0,1)).normalized;
            //mungkin salah yang dibawah ini karena divideo memakai 2 variabel float
            //untuk menyimpan axis horizontal dan vertical
            move = _input.z*camForward+_input.x*Cam.right;
        }
        else{
            move = _input.z*Vector3.forward+_input.x*Vector3.right;
        }
        if(move.magnitude>1){
            move.Normalize();
        }
        Debug.DrawRay(transform.position, move, Color.green);

        moveAnims(move);}
        
    }

    void moveAnims(Vector3 move){
        if(move.magnitude>1){
            move.Normalize();
            print("Normalized");
        }
        this.moveInput = move;
        ConvertMoveInput();
        UpdateAnimator();
    }

    void ConvertMoveInput(){
        Vector3 localMove = transform.InverseTransformDirection(moveInput); //moveinput is converted toiso
        Debug.DrawRay(transform.position, new Vector3(localMove.x,0,localMove.z), Color.red);
        
        //pake toiso atau gak nanti mengikuti world axis
        turnAmount=localMove.x;
        forwardAmount=localMove.z;
        

    }

    void UpdateAnimator(){
        if(currentState!=State.attack){
        anims.SetFloat("MoveSpeed",forwardAmount);
        anims.SetFloat("MoveStrafe",turnAmount);
        }else{
            anims.SetFloat("MoveSpeed",0);
            anims.SetFloat("MoveStrafe",0);
        }
    }

    void Update(){
       
        GatherInput();
        
        
        Look();
       // Animation();
       //-----------------Bagian Input----------------//
       if(Input.GetButtonDown("Fire1")){
        if(attack)
        {
        combatControll.Attack();
        combatControll.ExitAttack();
        StartCoroutine(ResetCooldown());
        }
       }
       if(Input.GetButtonDown("SwitchWeapon")){
        if(weaponController.acquiredWeapons==null){
            print("No Weapon");
        }
        weaponController.EquipWeapon(weaponSlot);
        weaponSlot++;
        if(weaponSlot>weaponController.acquiredWeapons.Length){
            weaponSlot=0;
        }
        
       }
       if(Input.GetButtonDown("Pause")){
        ui.Pause();
       }
       if(Input.GetKeyDown(KeyCode.F)&&dialogueController.npcDialogue!=null){
            dialogueController.StartDialogue();
            this.enabled=false;
       }

       //----------------Bagian Ekor Input-------------//
    }
    void Awake(){
        input = new CustomActions();

        
    }

    IEnumerator ResetCooldown(){
        attack=false;
        currentState=State.attack;
        yield return new WaitForSeconds(0.5f);
        attack=true;
        currentState = State.idle;
    }

    void OnTriggerEnter(Collider coll){
        if(coll.tag == "NPC"){
            interactNotif.SetActive(true);
            dialogueController.npcDialogue = coll.GetComponent<NPC>();
        }
    }

    void OnTriggerExit(Collider coll){
        if(coll.tag == "NPC"){
            interactNotif.SetActive(false);
        }
    }

    void AssignInputs(){
      //  input.Main.Move.performed += ctx => ClickToMove();

    }
    void GatherInput(){
        
        _input = new Vector3(Input.GetAxisRaw("Horizontal"),0,Input.GetAxisRaw("Vertical"));
        
    }

    void Look(){

        var (success,position)=GetMousePosition();
        if(success){
            //Calculate the Direction
            var direction = position - transform.position;
            //stop the object look up
            direction.y=0;
            //Make the transform look in the direction
            transform.forward=direction;
        }
    }

    void Move(){
        //rb.MovePosition(transform.position+(transform.forward*_input.magnitude)*_speed*Time.deltaTime);
        //_input.ToIso() untuk mengubah matriks 45 derajat sehingga pergerakan sesuai kamera isometrik
        
        rb.MovePosition(transform.position+_input.ToIso()*_speed*Time.deltaTime);
        
    }

    private(bool success, Vector3 position)GetMousePosition(){
        var ray=mainCamera.ScreenPointToRay(Input.mousePosition);
        if(Physics.Raycast(ray, out var hitInfo,Mathf.Infinity,groundMask)){
            //raycast hit something, will return with the position of it
            return(success:true,position:hitInfo.point);
        }else{
            //if didnt, it wont do anything
            return(success:false,position:Vector3.zero);
        }
    }

    public override void TakeDamage(float damage)
    {
        base.TakeDamage(damage);
        health = Mathf.Clamp(health,0,100);
        healthBar.fillAmount = health/100f;
    }



    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        rb = GetComponent<Rigidbody>();
        Cam= Camera.main.transform;
        combatControll = GetComponent<PlayerCombat>();
        weaponController = GetComponent<WeaponContoller>();
        dialogueController = GetComponent<DialogueController>();
        weaponController.EquipWeapon(0);
        currentState = State.idle;
        
    }

    /*void Animation(){
        anims.SetFloat("MoveSpeed",Mathf.Abs(_input.ToIso().z));
        anims.SetFloat("MoveStrafe",Mathf.Abs(_input.ToIso().x));
    }*/
 
    

}