using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.HID;

public class ThirdPersonMovement : MonoBehaviour
{
    public AudioManager audioManager;
    public PlayerWeapon sword;
    public PlayerWeapon heavySword;
    public GameObject placeButton;
    public GameObject Camera;
    public Transform holder;
    public Transform armorHolder;
    public ItemInstance activeWeapon;
    public ItemInstance activeArmor;
    public ItemCheckerScript InteractChecker;
    public GameObject InventoryInteface;
    public GameObject chestInteface;
    PlayerInput playerInput;
    private CharacterController characterController;
    public Animator animator;

    int isWalkingHash;
    int isRunningHash;
    int isJumpingHash;
    int isActionHash;
    int isAttackHash;
    int isHeavyAttackHash;
    int isSkillHash;
    int isRolloverHash;
    //bool isJumpAnimating;

    Vector2 currentMovementInput;
    Vector3 currentMovement;
    Vector3 currentRunMovement;
    bool isMovementPressed;
    bool isWalkPressed;
    bool isJumpPressed = false;
    bool isActionPressed = false;
    bool isRollPressed = false;
    bool isSkillPressed = false;
    public Transform cam;
    public float turnSmoothTime = 0.1f;
    
    [SerializeField]
    private float playerSpeed = 3.0f;
    [SerializeField]
    private float playerRunSpeed = 9.0f;
    [SerializeField]
    private float defaultSpeed = 9.0f;
    float groundGravity = -7.05f;
    float gravity = -9.81f;
    private bool _isGround = true;
    [SerializeField]
    private int AttackCount = 0;
    [SerializeField]
    private int HeavyAttackCount = 0;
    float turnSmoothVelocity;
    
    
    float jumpHeight = 14.0f;

    bool isJumping = false;



    private Vector3 playerVelocity;

    void Start()
    {
        activeWeapon = null;
        Cursor.visible = false;
    }
    void Awake()
    {
        playerInput = new PlayerInput();
        characterController = gameObject.GetComponent<CharacterController>();

        isWalkingHash = Animator.StringToHash("isWalking");
        isRunningHash = Animator.StringToHash("isRunning");
        isJumpingHash = Animator.StringToHash("isJumping");
        isActionHash = Animator.StringToHash("isAction");
        isAttackHash = Animator.StringToHash("isAttack");
        isHeavyAttackHash = Animator.StringToHash("isHeavyAttack");
        isSkillHash = Animator.StringToHash("isSkill");

        isRolloverHash = Animator.StringToHash("isRollover");

        playerInput.CharacterControls.Move.started += OnMovementInput;
        playerInput.CharacterControls.Move.canceled += OnMovementInput;
        playerInput.CharacterControls.Move.performed += OnMovementInput;
        playerInput.CharacterControls.Walk.canceled += onWalk;
        playerInput.CharacterControls.Jump.started += onJump;
        playerInput.CharacterControls.Action.started += onAction;
        playerInput.CharacterControls.Action.canceled += onAction;

        playerInput.CharacterControls.Roll.started += onRoll;

        playerInput.CharacterControls.Attack.started += onAttack;
        playerInput.CharacterControls.HeavyAttack.started += onHeavyAttack;

        playerInput.CharacterControls.Skill.started += onSkill;
        
    }

    void onRoll(InputAction.CallbackContext context)
    {
        if(!isRollPressed)
        {
            isRollPressed = true;
            StartCoroutine(RollCoroutine());
        }
        
    }
    void handleRoll()
    {
        if(!isJumping && isMovementPressed && isRollPressed)
        {
            animator.SetBool(isRolloverHash, true);
            playerRunSpeed = 0f;
            
            //transform.GetComponent<Collider>().enabled = false;
        }
        // else if(!isJumpPressed && isJumping && _isGround)
        // {
        //     isJumping = false;
        // }
    }
    IEnumerator RollCoroutine()
    {
        //отжатие кувырка
        yield return new WaitForSeconds(0.1f);
        isRollPressed = false;
    }
    void onSkill(InputAction.CallbackContext context)
    {
        isSkillPressed = context.ReadValueAsButton();
        playerRunSpeed = 0.0f;
        animator.SetBool(isSkillHash, true);
    }
    void onAttack(InputAction.CallbackContext context)
    {
        //isAttackPressed = context.ReadValueAsButton();
        if(AttackCount < 2)
        {
            AttackCount++;
        }
        
    }
    void onHeavyAttack(InputAction.CallbackContext context)
    {
        //isAttackPressed = context.ReadValueAsButton();
        if(HeavyAttackCount < 2)
        {
            HeavyAttackCount++;
        }
        
    }
    void handleSkill()
    {
        if(!isJumping && _isGround && isSkillPressed)
        {
            isJumping = true;
            playerRunSpeed = 0.0f;
            animator.SetBool(isSkillHash, true);
            return;
        }
    }
    void handleAttack()
    {
        if(!isJumping && _isGround && AttackCount>0 && !isSkillPressed)
        {
            isJumping = true;
            playerRunSpeed = 0.0f;
            animator.SetBool(isAttackHash, true);
            sword.GameObject().SetActive(true);
            return;
        }
        else
        {
            sword.GameObject().SetActive(false);
            playerRunSpeed = defaultSpeed;
            animator.SetBool(isAttackHash, false);
        }
        if(!isJumping && _isGround && HeavyAttackCount>0 && !isSkillPressed)
        {
            isJumping = true;
            playerRunSpeed = 0.0f;
            animator.SetBool(isHeavyAttackHash, true);
            heavySword.GameObject().SetActive(true);
            return;
        }
        else
        {
            heavySword.GameObject().SetActive(false);
            playerRunSpeed = defaultSpeed;
            animator.SetBool(isHeavyAttackHash, false);
        }
    }
    void handleJump()
    {
        if(!isJumping && _isGround && isJumpPressed)
        {
            isJumping = true;
            animator.SetBool(isJumpingHash, true);
            _isGround = false;
            turnSmoothTime = 2f;
            playerVelocity.y += Mathf.Sqrt(jumpHeight * -3.0f * gravity);
            
        }
        else if(!isJumpPressed && isJumping && _isGround)
        {
            isJumping = false;
        }
    }
    void handleAction()
    {
        if (!isJumping && _isGround && isActionPressed)
        {
            if(InteractChecker.GetInteract() != null)
            {
                if(InteractChecker.GetInteract().layer == 7)//предмет на земле
                {
                    animator.SetBool(isActionHash, true);
                }
                if(InteractChecker.GetInteract().layer == 8)//сундук
                {
                    OnDisable();
                    chestInteface.SetActive(true);
                    InventoryInteface.SetActive(true);
                    chestInteface.GetComponent<chestUI>().inventory = InteractChecker.GetInteract().GetComponent<Inventory>();
                    //chestInteface.transform.GetChild(3).GetComponent<GameObject>().SetActive(true);
                    InventoryInteface.transform.GetComponent<InventoryUI>().updateUI();
                    chestInteface.GetComponent<chestUI>().updateUI();
                    placeButton.SetActive(true);
                    Cursor.visible = true;
                }
                if(InteractChecker.GetInteract().layer == 9)//КОСТЁР
                {
                    InteractChecker.GetInteract().GetComponent<BoneFireScript>().UseBoneFire();
                    Debug.Log(InteractChecker.GetInteract().GetComponent<BoneFireScript>().ReSpawnPosition.position);
                    gameObject.GetComponent<PlayerLogic>().SetSpawn(InteractChecker.GetInteract().GetComponent<BoneFireScript>().ReSpawnPosition);
                }
            }
        }
        
    }
    
    void onAction(InputAction.CallbackContext context)
    {
        isActionPressed = context.ReadValueAsButton();
    }
    void onJump(InputAction.CallbackContext context)
    {
        if(!isJumpPressed)
        {
            isJumpPressed = true;
            StartCoroutine(JumpCoroutine());
        }
        
    }
    IEnumerator JumpCoroutine()
    {
        //отжатие прыжка
        yield return new WaitForSeconds(0.1f);
        isJumpPressed = false;
    }
    
    void onWalk(InputAction.CallbackContext context)
    {
        if(isWalkPressed == true)
        {
            isWalkPressed = false;
        }
        else
        {
            isWalkPressed = true;
        }
    }
    void OnMovementInput(InputAction.CallbackContext context)
    {
        currentMovementInput = context.ReadValue<Vector2>();
        currentMovement.x = currentMovementInput.x;
        currentMovement.z = currentMovementInput.y;
        currentRunMovement.x = currentMovementInput.x * playerRunSpeed;
        currentRunMovement.z = currentMovementInput.y * playerRunSpeed;
        isMovementPressed = currentMovementInput.x != 0 || currentMovementInput.y != 0;
        
    }
    
    void handleAnimation()
    {
        bool isWalking = animator.GetBool("isWalking");
        bool isRunning = animator.GetBool("isRunning");

        if((isMovementPressed && isWalkPressed) && !isWalking)
        {
            animator.SetBool(isWalkingHash, true);
        }
        else if((!isMovementPressed || !isWalkPressed) && isWalking)
        {
            animator.SetBool(isWalkingHash, false);
        }
        
        if(isMovementPressed && !isRunning)
        {
            animator.SetBool(isRunningHash, true);
        }
        else if(!isMovementPressed && isRunning)
        {
            animator.SetBool(isRunningHash, false);
        }

        
    }

    void handleGravity()
    {
        if(_isGround)
        {
            animator.SetBool(isJumpingHash, false);
            isJumping = false;
            playerVelocity.y = groundGravity;
            turnSmoothTime = 0.1f;
            
        }
        else
        {
            animator.SetBool(isJumpingHash, true);
            playerVelocity.y += gravity * Time.deltaTime;
        }
    }
    
    void handleInterfaceButton()
    {
        if(Input.GetKeyDown(KeyCode.I))
        {
            if(InventoryInteface.activeSelf == false)
            {
                
                InventoryInteface.SetActive(true);
                OnDisable();
                Cursor.visible = true;
            }
            else
            {
                InventoryInteface.SetActive(false);
                placeButton.SetActive(false);
                chestInteface.SetActive(false);
                chestInteface.GetComponent<chestUI>().inventory = null;
                
                OnEnable();
                Cursor.visible = false;
            }
        }
    }

    void Update()
    {
        
        handleInterfaceButton();
        handleAnimation();
        handleAction();
        
        handleRoll();

        if(currentMovement.magnitude >= 0.1)
        {
            ResetAnim();



            float targetAngle = Mathf.Atan2(currentMovement.x, currentMovement.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);
            
            Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;

            if(!isWalkPressed)
            {
                characterController.Move(moveDir.normalized * playerRunSpeed * Time.deltaTime);
            }
            else
            {
                characterController.Move(moveDir.normalized * playerSpeed * Time.deltaTime);
            }
        }

        handleGravity();
        handleJump();
        handleAttack();
        handleSkill();
        characterController.Move(playerVelocity * Time.deltaTime);
    }



    public void OnEnable() 
    {
        playerInput.CharacterControls.Enable();
        Camera.SetActive(true);
    }
    public void OnDisable()
    {
        playerInput.CharacterControls.Disable();
        if(Camera != null)
        {
            Camera.SetActive(false);
        }
        
    }

    public void GroundSet(bool state)
    {
        _isGround = state;
    }

    public void TakingEnd()
    {
        try{
            ItemInstance item = InteractChecker.GetInteract().transform.GetComponent<ItemContainer>().item;
            int amount = InteractChecker.GetInteract().transform.GetComponent<ItemContainer>().amount;
            int remaining = GetComponent<Inventory>().addItems(item, amount);
            InteractChecker.GetInteract().transform.GetComponent<ItemContainer>().pickUp(remaining);
            InventoryInteface.transform.GetComponent<InventoryUI>().updateUI();
        }
        catch{

        }
        finally{
            InteractChecker.Clear();
            animator.SetBool(isActionHash, false);
        }
        

        
    }

    public void ResetAnim()
    {
        animator.SetBool(isActionHash, false);
    }
    public void RollSound()
    {
        audioManager.Play("roll");
        
    }
    public void RollEnd()
    {
        //Debug.Log("RollEnd");
         animator.SetBool(isRolloverHash, false);
         playerRunSpeed = defaultSpeed;
         //transform.GetComponent<Collider>().enabled = true;
         playerInput.CharacterControls.Enable();
    }

    public void IsRollStart()
    {
        transform.GetComponent<PlayerLogic>().SetIsRoll(true);
        playerInput.CharacterControls.Disable();
    }
    public void IsRollEnd()
    {
        transform.GetComponent<PlayerLogic>().SetIsRoll(false);
    }







    public void use(int i)
    {
        ItemInstance item = GetComponent<Inventory>().getItem(i);
        if(item == null) return;

        if(item.use(this))
        {
            GetComponent<Inventory>().removeItem(i);
        }
        InventoryInteface.transform.GetComponent<InventoryUI>().updateUI();
    }

    public void drop(int i)
    {
        ItemInstance item = GetComponent<Inventory>().getItem(i);
        if(item == null) return;

        if(activeWeapon == item)
        {
            Destroy(holder.transform.GetChild(0).gameObject);
            activeWeapon = null;
        }

        if(activeArmor == item)
        {
            Destroy(armorHolder.transform.GetChild(0).gameObject);
            activeArmor = null;
        }
        GetComponent<Inventory>().dropItem(i);
        InventoryInteface.transform.GetComponent<InventoryUI>().updateUI();
    }

    public void destroy(int i)
    {
        ItemInstance item = GetComponent<Inventory>().getItem(i);
        if(item == null) return;

        if(activeWeapon == item)
        {
            Destroy(holder.transform.GetChild(0).gameObject);
            activeWeapon = null;
        }

        if(activeArmor == item)
        {
            Destroy(armorHolder.transform.GetChild(0).gameObject);
            activeArmor = null;
        }

        GetComponent<Inventory>().destroyItem(i);
        InventoryInteface.transform.GetComponent<InventoryUI>().updateUI();
    }

    public void take(int i)
    {
        if(GetComponent<Inventory>().slots.Count != GetComponent<Inventory>().size)
        {
            ItemInstance item = chestInteface.GetComponent<chestUI>().inventory.GetComponent<Inventory>().getItem(i);
        
            if(item == null) return;

            
            int amount = chestInteface.GetComponent<chestUI>().inventory.GetComponent<Inventory>().getAmount(i);
            GetComponent<Inventory>().addItems(item, amount);
            chestInteface.GetComponent<chestUI>().inventory.GetComponent<Inventory>().removeItem(i);/////////////////PLOBLEM?????

            

            InventoryInteface.transform.GetComponent<InventoryUI>().updateUI();
            chestInteface.GetComponent<chestUI>().updateUI();


        }
        
    }
    public void place(int i)
    {
        if(chestInteface.GetComponent<chestUI>().inventory.GetComponent<Inventory>().slots.Count != chestInteface.GetComponent<chestUI>().inventory.GetComponent<Inventory>().size)
        {
            ItemInstance item = GetComponent<Inventory>().getItem(i);
            if(item == null) return;

            
            int amount = GetComponent<Inventory>().getAmount(i);
            GetComponent<Inventory>().destroyItem(i);
            chestInteface.GetComponent<chestUI>().inventory.GetComponent<Inventory>().addItems(item, amount);

            if(activeWeapon == item)
            {
                Destroy(holder.transform.GetChild(0).gameObject);
                activeWeapon = null;
            }

            if(activeArmor == item)
            {
                Destroy(armorHolder.transform.GetChild(0).gameObject);
                activeArmor = null;
            }

            InventoryInteface.transform.GetComponent<InventoryUI>().updateUI();
            chestInteface.GetComponent<chestUI>().updateUI();   
        }
        
    }



    public void Step()
    {
        
        audioManager.Play("step");
    }
    public void Stroke()
    {
        audioManager.Play("sword");
    }
    public void NextAttack()
    {
        if(AttackCount>0)
        {
            AttackCount--;
            return;
        }
        if (AttackCount == 0)
        {
            animator.SetBool(isAttackHash, false);
        }
    }
    public void NextHeavyAttack()
    {
        if(HeavyAttackCount>0)
        {
            HeavyAttackCount--;
            return;
        }
        if (HeavyAttackCount == 0)
        {
            animator.SetBool(isHeavyAttackHash, false);
        }
    }

    public void SkillEnd()
    {
        animator.SetBool(isSkillHash, false);
        playerRunSpeed = defaultSpeed;
        isSkillPressed = false;
    }
}
