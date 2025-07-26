using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class player : MonoBehaviour
{
    [Header("MOVEMENT SETTINGS")]
    [Space(5)]

    // Public variables to set movement and look speed, and the player camera
    public float moveSpeed = 8f; // Speed at which the player moves
    public float lookSpeed; // Sensitivity of the camera movement
    public float gravity = -9.81f; // Gravity value
    public float jumpHeight = 1.0f; // Height of the jump
    //public Transform playerCamera; // Reference to the player's camera

    // Private variables to store input values and the character controller
    private Vector2 _moveInput; // Stores the movement input from the player
    private Vector2 _lookInput;
    private Vector3 _velocity; // Velocity of the player
    private CharacterController _characterController; // Reference to the CharacterController component


    //dash
    public bool canDodge = true;

    //camera stuff
    public GameObject playerCamHolder;
    public GameObject Room1Cam;

    //pause stuff
    public bool isPaused = false;
    public GameObject pauseScreen;

    //spell casting
    public Transform spellSpawnPoint;
    public manaManager manaManager;
    

    public GameObject fireProjectile;
    public float fireProjectileSpeed;
    public bool isShootingSpell;
    public bool hasFireSpell = false;

    public GameObject waterProjectile;
    public float waterProjectileSpeed;
    public bool hasWaterSpell = false;

    public bool hasRockSpell = false;

    public bool hasWindSpell = false;

    //potion stuff
    public potionManager potionManager;
    public healthManager healthManager;
   
    public bool manaPotionEquipped = false;
    public GameObject manaPotionDisplay;
    
    public bool healthPotionEquipped = true;
    public GameObject healthPotionDisplay;

    [Header("CROUCH SETTINGS")]
    [Space(5)]
    public float crouchHeight = 1f; //make short
    public float standingHeight = 2f; //make normal
    public float crouchSpeed = 1.5f; //short speed
    public bool isCrouching = false; //if short or normal

    public Rigidbody rb;

    private void OnEnable()
    {

        // Create a new instance of the input actions
        var playerInput = new Controls();

        // Enable the input actions
        playerInput.Player.Enable();

        // Subscribe to the movement input events
        playerInput.Player.Movement.performed += ctx => _moveInput = ctx.ReadValue<Vector2>(); // Update moveInput when movement input is performed
        playerInput.Player.Movement.canceled += ctx => _moveInput = Vector2.zero; // Reset moveInput when movement input is canceled

        // Subscribe to the look input events
        playerInput.Player.Look.performed += ctx => _lookInput = ctx.ReadValue<Vector2>(); // Update lookInput when look input is performed

        //playerInput.Player.LookAround.performed += ctx => currentScheme = ctx.control;
        playerInput.Player.Look.canceled += ctx => _lookInput = Vector2.zero; // Reset lookInput when look input is canceled

        // Subscribe to the jump input event
        playerInput.Player.Jump.performed += ctx => Jump(); // Call the Jump method when jump input is performed


        // Subscribe to the light fire input event
        playerInput.Player.Interact.performed += ctx => Interact(); // Call the Interact method when pick-up input is performed

        //Subscribe to the sprint
        playerInput.Player.Dodge.performed += ctx => Dodge(); // dodge

        //Subscribe to the drink potion
        playerInput.Player.DrinkPotion.performed += ctx => DrinkPotion(); // drink potion

        //Subscribe to the cast spell
        playerInput.Player.CastSpell.performed += ctx => CastSpell(); // cast spell

        //Subscribe to the change spell
        playerInput.Player.ChangeSpell.performed += ctx => ChangeSpell(); // change current spell

        //Subscribe to the change potion
        playerInput.Player.ChangePotion.performed += ctx => ChangePotion(); // use fuel

        //Subscribe to the pause
        playerInput.Player.Pause.performed += ctx => Pause(); // pause
    }

    private void Awake()
    {
        // Get and store the CharacterController component attached to this GameObject
        _characterController = GetComponent<CharacterController>();
        // playerAnim = GetComponent<Animator>();

    }

    public void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    public void Update()
    {
        Move();
        Look();
        ApplyGravity();
    }

    public void Move()
    {

        if(isPaused == false && isShootingSpell == false)
        {
            // Create a movement vector based on the input
            Vector3 move = new Vector3(-_moveInput.x, 0, -_moveInput.y);

            // Transform direction from local to world space
            move = transform.TransformDirection(move);

            var currentSpeed = isCrouching ? crouchSpeed : moveSpeed;

            // Move the character controller based on the movement vector and speed
            _characterController.Move(move * currentSpeed * Time.deltaTime);
        }
        
    }

   

    public void Look()
    {
        if(isPaused == false)
        {
            // Only use horizontal input (left/right)
            float lookX = _lookInput.x * lookSpeed;

            // Rotate the player left/right around the Y-axis
            transform.Rotate(0f, lookX, 0f);
        }
        
    }



    public void ApplyGravity()
    {
        if (_characterController.isGrounded && _velocity.y < 0)
        {
            _velocity.y = -0.5f; // Small value to keep the player grounded
        }

        _velocity.y += gravity * Time.deltaTime; // Apply gravity to the velocity
        _characterController.Move(_velocity * Time.deltaTime); // Apply the velocity to the character
    }

    public void Jump()
    {
        if(_characterController.isGrounded && isPaused == false)
        {
            _velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }
       
    }

    public void Dodge()
    {
        if(canDodge == true)
        {
            StartCoroutine(TheDodge());
        }
    }

    public void Interact()
    {

    }

    public void CastSpell()
    {
        //fire spell
        if(manaManager.currentMana > 4.99f && hasFireSpell == true)
        {
            var projectile = Instantiate(fireProjectile, spellSpawnPoint.position, spellSpawnPoint.rotation);
            var rb = projectile.GetComponent<Rigidbody>();
            rb.velocity = spellSpawnPoint.forward * fireProjectileSpeed;
            manaManager.UseFireSpell();
        }

        //water spell
        if (manaManager.currentMana > 4.99f && hasWaterSpell == true)
        {
            var projectile = Instantiate(waterProjectile, spellSpawnPoint.position, spellSpawnPoint.rotation);
            var rb = projectile.GetComponent<Rigidbody>();
            rb.velocity = spellSpawnPoint.forward * waterProjectileSpeed;
            manaManager.UseFireSpell();
        }
    }

    public void DrinkPotion()
    {
        if (healthPotionEquipped == true && potionManager.healthPotion > 0 && healthManager.currentHealth < 100)
        {
            healthManager.PlayerHeal();
            potionManager.subtractHealthPotion();
            Debug.Log("player should heal and lose 1 potion");
        }

        if (manaPotionEquipped == true && potionManager.manaPotion > 0 && manaManager.currentMana < 100)
        {
            manaManager.DrankManaPotion();
            potionManager.subtractManaPotion();
            Debug.Log("player should restore mana and lose 1 potion");
        }
    }

    public void ChangeSpell()
    {
        
            
    }

    public void ChangePotion()
    {
        if(healthPotionEquipped == true) 
        {
            StartCoroutine(HealthPotionSwitch());
        }

        if (manaPotionEquipped == true)
        {
           StartCoroutine(ManaPotionSwitch());
     
        }
    }

    public void Pause()
    {
        if (isPaused == false)
        {
            isPaused = true;
            pauseScreen.SetActive(true);
            Time.timeScale = 0;
            Debug.Log("should pause");
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }

        else if (isPaused == true)
        {
            isPaused = false;
            pauseScreen.SetActive(false);
            Time.timeScale = 1;
            Debug.Log("should unpaused");
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        if(other.tag == "ManaPotion")
        {
            potionManager.addManaPotion();
            Destroy(other.gameObject);
        }

        if (other.tag == "HealthPotion")
        {
            potionManager.addHealthPotion();
            Destroy(other.gameObject);
        }

        if(other.tag == "FireSpell")
        {
            hasFireSpell = true;
            Destroy(other.gameObject);
        }

        if (other.tag == "WaterSpell")
        {
            hasWaterSpell = true;
            Destroy(other.gameObject);
        }
    }
    public void OnTriggerStay (Collider other)
    {
        if(other.tag == "Room1")
        {
            playerCamHolder.SetActive(false);
            Room1Cam.SetActive(true);
        }
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.tag == "Room1")
        {
            playerCamHolder.SetActive(true);
            Room1Cam.SetActive(false);
        }
    }

    private IEnumerator TheDodge()
    {
        yield return new WaitForSeconds(0f);
        canDodge = false;
        moveSpeed = moveSpeed + 8f;
        yield return new WaitForSeconds(0.3f);
        moveSpeed = moveSpeed - 8f;
        yield return new WaitForSeconds(2f);
        canDodge = true;
    }

    private IEnumerator HealthPotionSwitch()
    {
        yield return new WaitForSeconds(0f);
        healthPotionEquipped = false;
        healthPotionDisplay.SetActive(false);

        yield return new WaitForSeconds(0.01f);
        manaPotionEquipped = true;
        manaPotionDisplay.SetActive(true);
        Debug.Log("should change to mana potion");
    }

    private IEnumerator ManaPotionSwitch()
    {
        yield return new WaitForSeconds(0f);
        manaPotionEquipped = false;
        manaPotionDisplay.SetActive(false);

        yield return new WaitForSeconds(0.01f);
        healthPotionEquipped = true;
        healthPotionDisplay.SetActive(true);
        Debug.Log("should change to health potion");
    }
}
