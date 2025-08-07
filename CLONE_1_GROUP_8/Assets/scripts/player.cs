using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class player : MonoBehaviour
{
    [Header("MOVEMENT SETTINGS")]
    [Space(5)]

    // Public variables to set movement and look speed, and the player camera
    public float moveSpeed = 8f; // Speed at which the player moves
    public float lookSpeed; // Sensitivity of the camera movement
    public float gravity = -9.81f; // Gravity value
    public float jumpHeight = 1.0f; // Height of the jump
    public float windJumpHeight = 1.0f; //wind jump height
    //public Transform playerCamera; // Reference to the player's camera

    // Private variables to store input values and the character controller
    private Vector2 _moveInput; // Stores the movement input from the player
    private Vector2 _lookInput;
    private Vector3 _velocity; // Velocity of the player
    private CharacterController _characterController; // Reference to the CharacterController component

    [Header("CROUCH SETTINGS")]
    [Space(5)]
    public float crouchHeight = 1f; //make short
    public float standingHeight = 2f; //make normal
    public float crouchSpeed = 1.5f; //short speed
    public bool isCrouching = false; //if short or normal

    public Rigidbody rb;

    //key stuff
    public keyManager keyManager;
    public GameObject keyTextPopUp;

    //dash
    public bool canDodge = true;

    //camera stuff
    public GameObject playerCamHolder;
    public GameObject Room1Cam;
    public GameObject Room2Cam;
    public GameObject Room3Cam;
    public GameObject Room4Cam;
    public GameObject Room5Cam;
    public GameObject Room6Cam;
    public GameObject Room7Cam;
    public GameObject Room8Cam;
    public GameObject Room9Cam;
    public GameObject Room10Cam;
    public GameObject Room11Cam;
    public GameObject Room12Cam;
    public GameObject Room13Cam;
    public GameObject Room14Cam;
    public GameObject Room15Cam;
    public GameObject Room16Cam;

    public GameObject tutorial1Cam;
    public GameObject tutorial2Cam;
    public GameObject tutorial3Cam;

    //tutorial note
    public GameObject tutorialNote;
    public bool hasPotionBag = false;
    public bool hasStaff = false;
    public GameObject teleportPoint;
    public GameObject teleportScreen;
    public GameObject tutorialLevel;

    public GameObject moveText;
    public GameObject aimText;
    public GameObject jumpText;
    public GameObject dashText;
    public GameObject pauseText;

    //pause stuff
    public bool isPaused = false;
    public GameObject pauseScreen;

    //spell casting
    public Transform spellSpawnPoint;
    public Transform spellSpawnPoint2;
    public Transform spellSpawnPoint3;
    public manaManager manaManager;
    
    //fire 
    public GameObject fireProjectile;
    public float fireProjectileSpeed;
    public bool isShootingSpell;
    public bool fireSpellEquipped = false;
    public bool unknownSpellOneEquipped = true;
    public bool hasUnlockedFireSpell = false;
    public GameObject fireTextPopUp;
    public GameObject unknownSpell1;
    public GameObject fireSpellUI;
    public GameObject fireEmblem;

    //water
    public GameObject waterShield;
    public bool waterSpellEquipped = false;
    public bool unknownSpellTwoEquipped = false;
    public bool hasUnlockedWaterSpell = false;
    public GameObject waterTextPopUp;
    public GameObject unknownSpell2;
    public GameObject waterSpellUI;
    public bool isUsingWaterShield = false;
    public GameObject waterEmblem;

    //rock
    public GameObject rockProjectile;
    public float rockProjectileSpeed;
    public bool rockSpellEquipped = false;
    public bool unknownSpellThreeEquipped = false;
    public bool hasUnlockedRockSpell = false;
    public GameObject rockTextPopUp;
    public GameObject unknownSpell3;
    public GameObject rockSpellUI;
    public GameObject rockEmblem;

    //wind
    public bool windSpellEquipped = false;
    public bool unknownSpellFourEquipped = false;
    public bool hasUnlockedWindSpell = false;
    public GameObject windTextPopUp;
    public GameObject unknownSpell4;
    public GameObject windSpellUI;
    public bool isJumping = false;
    public GameObject windEmblem;

    //potion stuff
    public potionManager potionManager;
    public healthManager healthManager;
   
    public bool manaPotionEquipped = false;
    public GameObject manaPotionDisplay;
    public GameObject manaTextPopUp;
    
    public bool healthPotionEquipped = true;
    public GameObject healthPotionDisplay;
    public GameObject healthTextPopUp;

    //interact stuff
    public Transform playerNose;
    public float minRange = 1f;
    
    //door stuff
    public GameObject unlockDoorText;
    public GameObject doorLockedText;
   
    //checkpoint stuff
    public GameObject prayText;
    public GameObject checkpointSetText;
    //these bools are for when you die, it will check which bool is true and respawn you there 
    public bool checkpointOne = false;
    public bool checkpointTwo = false;
    public bool checkpointThree = false;
    public bool checkpointFour = false;
    public bool checkpointFive = false;

    //king coin and queen crystal stuff
    public kingCoin kingCoin;
    public GameObject kingCoinText;

    public queenCrystal queenCrystal;
    public GameObject queenCrystalText;

    //staff
    public GameObject spellFrame;
    public GameObject staff;
    public GameObject staffText;

    //potionBagStuff
    public GameObject potionFrame;
    public GameObject potionText;
    public GameObject potionBag;

    //boss door
    public spellCounter spellCounter;
    public GameObject notStrongEnoughText;

    //killbox stuff
    public GameObject killboxRespawnPoint;
    public GameObject killbox2RespawnPoint;
    public GameObject killbox3RespawnPoint;
    public GameObject killbox4RespawnPoint;

    //shooting enemy stuff
    public enemyLookAt enemyLookAt;
    public ghostLookAt ghostLookAt;
    public bossLookAt bossLookAt;

    //boss room 
    public GameObject bossHealthBar;
    public GameObject bossRoomDoor;

    //particles
    public ParticleSystem checkpointParticle1;
    public ParticleSystem checkpointParticle2;
    public ParticleSystem checkpointParticle3;
    public ParticleSystem checkpointParticle4;

    public ParticleSystem gotCollectibleParticle;
    public ParticleSystem playerHitParticle;
    public ParticleSystem tornadoParticle;

    public GameObject startDoor;

    public GameObject dungeonSong;
    public GameObject bossSong;

    public AudioSource worldSounds;
    public AudioClip shootingSpellSFX;

    public AudioSource worldSounds2;
    

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
        StartCoroutine(StartText());
    }

    public void Update()
    {
        Move();
        Look();
        ApplyGravity();
        CheckInteract();
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
        unlockDoorText.SetActive(false);
        doorLockedText.SetActive(false);
        prayText.SetActive(false);
        checkpointSetText.SetActive(false);
        Ray ray = new Ray(playerNose.position, playerNose.forward);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, minRange))
        {

            if(hit.collider.CompareTag("Door"))
            {
                if (keyManager.key > 0.99)
                {
                    Destroy(hit.collider.gameObject);
                    keyManager.subtractKey();
                }
                else
                {
                    doorLockedText.SetActive(true);
                    StartCoroutine(DoorLocked());
                    return;
                }
            }

            if (hit.collider.CompareTag("Checkpoint1"))
            {
                checkpointSetText.SetActive(true);
                StartCoroutine(CheckpointSet());
                checkpointOne = true;
                checkpointTwo = false;
                checkpointThree = false;
                checkpointFour = false;
                checkpointFive = false;
                healthManager.FullHeal();
                manaManager.FullMana();
                checkpointParticle1.Play();
            }

            if (hit.collider.CompareTag("Checkpoint2"))
            {
                checkpointSetText.SetActive(true);
                StartCoroutine(CheckpointSet());
                checkpointOne = false;
                checkpointTwo = true;
                checkpointThree = false;
                checkpointFour = false;
                checkpointFive = false;
                healthManager.FullHeal();
                manaManager.FullMana();
                checkpointParticle2.Play();
            }

            if (hit.collider.CompareTag("Checkpoint3"))
            {
                checkpointSetText.SetActive(true);
                StartCoroutine(CheckpointSet());
                checkpointOne = false;
                checkpointTwo = false;
                checkpointThree = true;
                checkpointFour = false;
                checkpointFive = false;
                healthManager.FullHeal();
                manaManager.FullMana();
                checkpointParticle3.Play();
            }

            if (hit.collider.CompareTag("Checkpoint4"))
            {
                checkpointSetText.SetActive(true);
                StartCoroutine(CheckpointSet());
                checkpointOne = false;
                checkpointTwo = false;
                checkpointThree = false;
                checkpointFour = true;
                checkpointFive = false;
                healthManager.FullHeal();
                manaManager.FullMana();
                checkpointParticle4.Play();
            }

            if (hit.collider.CompareTag("Checkpoint5"))
            {
                checkpointSetText.SetActive(true);
                StartCoroutine(CheckpointSet());
                checkpointOne = false;
                checkpointTwo = false;
                checkpointThree = false;
                checkpointFour = false;
                checkpointFive = true;
                healthManager.FullHeal();
                manaManager.FullMana();
            }

            if (hit.collider.CompareTag("BossDoor"))
            {
                if(spellCounter.spellCount < 4)
                {
                    StartCoroutine(NotStrongEnough());
                }

                if(spellCounter.spellCount == 4)
                {
                    Destroy(hit.collider.gameObject);
                }
            }
        }
    }

    public void CheckInteract()
    {
        Ray ray = new Ray(playerNose.position, playerNose.forward);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, minRange))
        {
            if (hit.collider.CompareTag("Door"))
            {
                unlockDoorText.SetActive(true);
                Debug.Log("door text should show");
            }

            if (hit.collider.CompareTag("Checkpoint1"))
            {
                prayText.SetActive(true);
                Debug.Log("pray text should show");
            }

            if (hit.collider.CompareTag("Checkpoint2"))
            {
                prayText.SetActive(true);
                Debug.Log("pray text should show");
            }

            if (hit.collider.CompareTag("Checkpoint3"))
            {
                prayText.SetActive(true);
                Debug.Log("pray text should show");
            }

            if (hit.collider.CompareTag("Checkpoint4"))
            {
                prayText.SetActive(true);
                Debug.Log("pray text should show");
            }

            if (hit.collider.CompareTag("Checkpoint5"))
            {
                prayText.SetActive(true);
                Debug.Log("pray text should show");
            }

            if (hit.collider.CompareTag("BossDoor"))
            {
                unlockDoorText.SetActive(true);
                Debug.Log("door text should show");
            }
        }
        else
        {
            unlockDoorText.SetActive(false);
            prayText.SetActive(false);
        }
    }

    public void CastSpell()
    {
        
        //fire spell
        if(manaManager.currentMana > 4.99f && fireSpellEquipped == true)
        {
            var projectile = Instantiate(fireProjectile, spellSpawnPoint.position, spellSpawnPoint.rotation);
            var rb = projectile.GetComponent<Rigidbody>();
            rb.velocity = spellSpawnPoint.forward * fireProjectileSpeed;
            manaManager.UseFireSpell();
            Destroy(projectile, 1f);
            worldSounds.clip = shootingSpellSFX;
            worldSounds.Play();
        }

        //water spell
        if (manaManager.currentMana > 4.99f && waterSpellEquipped == true)
        {
            StartCoroutine(WaterShield());
            manaManager.UseWaterSpell();
            isUsingWaterShield = true;
            worldSounds.clip = shootingSpellSFX;
            worldSounds.Play();

        }

        //rock spell
        if(manaManager.currentMana > 14.99 && rockSpellEquipped == true)
        {
            var projectile1 = Instantiate(rockProjectile, spellSpawnPoint.position, spellSpawnPoint.rotation);
            var rb1 = projectile1.GetComponent<Rigidbody>();
            rb1.velocity = spellSpawnPoint.forward * rockProjectileSpeed;

            var projectile2 = Instantiate(rockProjectile, spellSpawnPoint2.position, spellSpawnPoint2.rotation);
            var rb2 = projectile2.GetComponent<Rigidbody>();
            rb2.velocity = spellSpawnPoint2.forward * rockProjectileSpeed;

            var projectile3 = Instantiate(rockProjectile, spellSpawnPoint3.position, spellSpawnPoint3.rotation);
            var rb3 = projectile3.GetComponent<Rigidbody>();
            rb3.velocity = spellSpawnPoint3.forward * rockProjectileSpeed;
            manaManager.UseRockSpell();
            worldSounds.clip = shootingSpellSFX;
            worldSounds.Play();
        }

        //wind spell
        if(manaManager.currentMana > 4.99f && windSpellEquipped == true && isJumping == false)
        {
            _velocity.y = Mathf.Sqrt(windJumpHeight * -2f * gravity);
            manaManager.UseWindSpell();
            isJumping = true;
            StartCoroutine(CapJump());
            tornadoParticle.Play();
            worldSounds.clip = shootingSpellSFX;
            worldSounds.Play();
        }
    }

    public void DrinkPotion()
    {
        if (healthPotionEquipped == true && potionManager.healthPotion > 0 && healthManager.currentHealth < healthManager.maxHealth)
        {
            healthManager.PlayerHeal();
            potionManager.subtractHealthPotion();
            Debug.Log("player should heal and lose 1 potion");
        }

        if (manaPotionEquipped == true && potionManager.manaPotion > 0 && manaManager.currentMana < manaManager.maxMana)
        {
            manaManager.DrankManaPotion();
            potionManager.subtractManaPotion();
            Debug.Log("player should restore mana and lose 1 potion");
        }
    }

    public void ChangeSpell()
    {
        //locked spells segment for if you don't the spell you've equipped but either have or don't have the next spell
        if(unknownSpellOneEquipped == true && hasUnlockedWaterSpell == false)
        {
            StartCoroutine(USOneToUSTwo());
        }
        else if(unknownSpellOneEquipped == true && hasUnlockedWaterSpell == true)
        {
            StartCoroutine(USOneToWaterSpell());
        }

        if(unknownSpellTwoEquipped == true && hasUnlockedRockSpell == false)
        {
            StartCoroutine(USTwoToUSThree());
        }
        else if (unknownSpellTwoEquipped == true && hasUnlockedRockSpell == true)
        {
            StartCoroutine(USTwoToRockSpell());
        }

        if(unknownSpellThreeEquipped == true && hasUnlockedWindSpell == false)
        {
            StartCoroutine(USThreeToUSFour());
        }
        else if (unknownSpellThreeEquipped == true && hasUnlockedWindSpell == true)
        {
            StartCoroutine(USThreeToWindSpell());
        }

        if (unknownSpellFourEquipped == true && hasUnlockedFireSpell == false)
        {
            StartCoroutine(USFourToUSOne());
        }
        else if(unknownSpellFourEquipped == true && hasUnlockedFireSpell== true)
        {
            StartCoroutine(USFourToFireSpell());
        }

        //unlocked spells segment for if you have the spell you've equipped but do or don't have the next spell equipped 
        if(fireSpellEquipped == true && hasUnlockedWaterSpell == false)
        {
            StartCoroutine(FireSpellToUSTwo());
        }
        else if(fireSpellEquipped == true && hasUnlockedWaterSpell == true)
        {
            StartCoroutine(FireSpellToWaterSpell());
        }

        if(waterSpellEquipped == true && hasUnlockedRockSpell == false)
        {
            StartCoroutine(WaterSpellToUSThree());
        }
        else if(waterSpellEquipped == true && hasUnlockedRockSpell == true)
        {
            StartCoroutine(WaterSpellToRockSpell());
        }
        
        if(rockSpellEquipped == true && hasUnlockedWindSpell == false)
        {
            StartCoroutine(RockSpellToUSFour());
        }

        if(rockSpellEquipped == true && hasUnlockedWindSpell == true)
        {
            StartCoroutine(RockSpellToWindSpell());
        }

        if(windSpellEquipped == true && hasUnlockedFireSpell == false)
        {
            StartCoroutine(WindSpellToUSOne());
        }
        else if(windSpellEquipped == true && hasUnlockedFireSpell== true)
        {
            StartCoroutine(WindSpellToFireSpell());
        }

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
            StartCoroutine(ManaPotionPopUp());
        }

        if (other.tag == "HealthPotion")
        {
            potionManager.addHealthPotion();
            Destroy(other.gameObject);
            StartCoroutine(HealthPotionPopUp());
        }

        if(other.tag == "FireSpell")
        {
            hasUnlockedFireSpell = true;
            //setting fire spell bool to true
            fireSpellEquipped = true;
            waterSpellEquipped = false;
            rockSpellEquipped = false;
            windSpellEquipped = false;
            unknownSpellOneEquipped = false;
            unknownSpellTwoEquipped = false;
            unknownSpellThreeEquipped = false;
            unknownSpellFourEquipped = false;

            //setting UI to display correctly
            fireSpellUI.SetActive(true);
            waterSpellUI.SetActive(false);
            windSpellUI.SetActive(false);
            rockSpellUI.SetActive(false);
            unknownSpell1.SetActive(false);
            unknownSpell2.SetActive(false);
            unknownSpell3.SetActive(false);
            unknownSpell4.SetActive(false);

            Destroy(other.gameObject);
            StartCoroutine(FireSpellPopUp());
            fireEmblem.SetActive(true);
            spellCounter.AddSpell();
            gotCollectibleParticle.Play();
            worldSounds2.Play();
        }

        if (other.tag == "WaterSpell")
        {
            hasUnlockedWaterSpell = true;
            //setting water spell bool to true
            fireSpellEquipped = false;
            waterSpellEquipped = true;
            rockSpellEquipped = false;
            windSpellEquipped = false;
            unknownSpellOneEquipped = false;
            unknownSpellTwoEquipped = false;
            unknownSpellThreeEquipped = false;
            unknownSpellFourEquipped = false;

            //setting UI to display correctly
            fireSpellUI.SetActive(false);
            waterSpellUI.SetActive(true);
            windSpellUI.SetActive(false);
            rockSpellUI.SetActive(false);
            unknownSpell1.SetActive(false);
            unknownSpell2.SetActive(false);
            unknownSpell3.SetActive(false);
            unknownSpell4.SetActive(false);

            Destroy(other.gameObject);
            StartCoroutine(WaterSpellPopUp());
            waterEmblem.SetActive(true);
            spellCounter.AddSpell();
            gotCollectibleParticle.Play();
            worldSounds2.Play();
        }

        if(other.tag == "WindSpell")
        {
            hasUnlockedWindSpell = true;
            //setting wind spell bool to true
            fireSpellEquipped = false;
            waterSpellEquipped = false;
            rockSpellEquipped = false;
            windSpellEquipped = true;
            unknownSpellOneEquipped = false;
            unknownSpellTwoEquipped = false;
            unknownSpellThreeEquipped = false;
            unknownSpellFourEquipped = false;

            //setting UI to display correctly
            fireSpellUI.SetActive(false);
            waterSpellUI.SetActive(false);
            windSpellUI.SetActive(true);
            rockSpellUI.SetActive(false);
            unknownSpell1.SetActive(false);
            unknownSpell2.SetActive(false);
            unknownSpell3.SetActive(false);
            unknownSpell4.SetActive(false);

            Destroy(other.gameObject);
            StartCoroutine(WindSpellPopUp());
            windEmblem.SetActive(true);
            spellCounter.AddSpell();
            gotCollectibleParticle.Play();
            worldSounds2.Play();
        }

        if(other.tag == "RockSpell")
        {
            hasUnlockedRockSpell = true;    
            //setting rock spell bool to true
            fireSpellEquipped = false;
            waterSpellEquipped = false;
            rockSpellEquipped = true;
            windSpellEquipped = false;
            unknownSpellOneEquipped = false;
            unknownSpellTwoEquipped = false;
            unknownSpellThreeEquipped = false;
            unknownSpellFourEquipped = false;

            //setting UI to display correctly
            fireSpellUI.SetActive(false);
            waterSpellUI.SetActive(false);
            windSpellUI.SetActive(false);
            rockSpellUI.SetActive(true);
            unknownSpell1.SetActive(false);
            unknownSpell2.SetActive(false);
            unknownSpell3.SetActive(false);
            unknownSpell4.SetActive(false);

            Destroy(other.gameObject);
            StartCoroutine(RockSpellPopUp());
            rockEmblem.SetActive(true); 
            spellCounter.AddSpell();
            gotCollectibleParticle.Play();
            worldSounds2.Play();
        }

        if (other.tag == "Key")
        {
            Destroy(other.gameObject);
            keyManager.addKey();
            StartCoroutine(KeyPopUp());
        }

        if(other.tag == "KingCoin")
        {
            Destroy(other.gameObject);
            kingCoin.AddKingCoin();
            kingCoin.CheckCoinCount();
            StartCoroutine(KingCoinAcquired());
            gotCollectibleParticle.Play();
            worldSounds2.Play();
        }

        if(other.tag == "QueenCrystal")
        {
            Destroy(other.gameObject);
            queenCrystal.AddQueenCrystal();
            queenCrystal.CheckCrystalCount();
            StartCoroutine(QueenCrystalAcquired());
            gotCollectibleParticle.Play();
            worldSounds2.Play();
        }

        if(other.tag == "WaterObstacle" && isUsingWaterShield == false)
        {
            healthManager.FireWallHit();
        }

        if(other.tag == "WaterObstacle" && isUsingWaterShield == true)
        {
            Destroy(other.gameObject);
        }

        if(other.tag == "Staff")
        {
            Destroy(other.gameObject);
            spellFrame.SetActive(true);
            staff.SetActive(true);
            StartCoroutine(StaffAcquired());    
            hasStaff = true;
            gotCollectibleParticle.Play();
            worldSounds2.Play();
        }

        if(other.tag == "PotionBag")
        {
            Destroy(other.gameObject);
            potionFrame.SetActive(true);
            potionBag.SetActive(true);
            StartCoroutine(PotionBagAcquired());
            hasPotionBag = true;
            gotCollectibleParticle.Play();
            worldSounds2.Play();
        }

        if(other.tag == "Note")
        {
            Destroy(other.gameObject);
            isPaused = true;
            tutorialNote.SetActive(true);
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }

        if(other.tag == "Teleporter" && hasPotionBag == true && hasStaff == true)
        {
            StartCoroutine(Teleport());
        }


        if (other.tag == "KillBox")
        {
            healthManager.PlayerHit();
            StartCoroutine(KillBox());
            playerHitParticle.Play();

        }

        if (other.tag == "KillBox2")
        {
            healthManager.PlayerHit();
            StartCoroutine(KillBox2());
            playerHitParticle.Play();
        }

        if (other.tag == "KillBox3")
        {
            healthManager.PlayerHit();
            StartCoroutine(KillBox3());
            playerHitParticle.Play();
        }

        if (other.tag == "KillBox4")
        {
            healthManager.PlayerHit();
            StartCoroutine(KillBox4());
            playerHitParticle.Play();
        }

        if(other.tag == "EnemyBullet")
        {
            healthManager.PlayerHit();
            Destroy(other.gameObject);
            playerHitParticle.Play();
        }

        if (other.tag == "BossBullet")
        {
            healthManager.PlayerHitALot();
            Destroy(other.gameObject);
            playerHitParticle.Play();
        }

        if (other.tag == "UltimateKillBox")
        {
            healthManager.FullKill();
        }

        if(other.tag == "BossRoom")
        {
            bossHealthBar.SetActive(true);
            bossRoomDoor.SetActive(true);
            dungeonSong.SetActive(false);
            bossSong.SetActive(true);
        }
    }
    public void OnTriggerStay(Collider other)
    {
        if (other.tag == "Room1")
        {
            playerCamHolder.SetActive(false);
            Room1Cam.SetActive(true); //this one
            Room2Cam.SetActive(false);
            Room3Cam.SetActive(false);
            Room4Cam.SetActive(false);
            Room5Cam.SetActive(false);
            Room6Cam.SetActive(false);
            Room7Cam.SetActive(false);
            Room8Cam.SetActive(false);
            Room9Cam.SetActive(false);
            Room10Cam.SetActive(false);
            Room11Cam.SetActive(false);
            Room12Cam.SetActive(false);
            Room13Cam.SetActive(false);
            Room14Cam.SetActive(false);
            Room15Cam.SetActive(false);
            Room16Cam.SetActive(false);
            tutorial1Cam.SetActive(false);
            tutorial2Cam.SetActive(false);
            tutorial3Cam.SetActive(false);

        }

        if (other.tag == "Room2")
        {
            playerCamHolder.SetActive(false);
            Room1Cam.SetActive(false);
            Room2Cam.SetActive(true); //this one
            Room3Cam.SetActive(false);
            Room4Cam.SetActive(false);
            Room5Cam.SetActive(false);
            Room6Cam.SetActive(false);
            Room7Cam.SetActive(false);
            Room8Cam.SetActive(false);
            Room9Cam.SetActive(false);
            Room10Cam.SetActive(false);
            Room11Cam.SetActive(false);
            Room12Cam.SetActive(false);
            Room13Cam.SetActive(false);
            Room14Cam.SetActive(false);
            Room15Cam.SetActive(false);
            Room16Cam.SetActive(false);
            tutorial1Cam.SetActive(false);
            tutorial2Cam.SetActive(false);
            tutorial3Cam.SetActive(false);
        }

        if (other.tag == "Room3")
        {
            playerCamHolder.SetActive(false);
            Room1Cam.SetActive(false);
            Room2Cam.SetActive(false);
            Room3Cam.SetActive(true);//this one
            Room4Cam.SetActive(false);
            Room5Cam.SetActive(false);
            Room6Cam.SetActive(false);
            Room7Cam.SetActive(false);
            Room8Cam.SetActive(false);
            Room9Cam.SetActive(false);
            Room10Cam.SetActive(false);
            Room11Cam.SetActive(false);
            Room12Cam.SetActive(false);
            Room13Cam.SetActive(false);
            Room14Cam.SetActive(false);
            Room15Cam.SetActive(false);
            Room16Cam.SetActive(false);
            tutorial1Cam.SetActive(false);
            tutorial2Cam.SetActive(false);
            tutorial3Cam.SetActive(false);
        }

        if (other.tag == "Room4")
        {
            playerCamHolder.SetActive(false);
            Room1Cam.SetActive(false);
            Room2Cam.SetActive(false);
            Room3Cam.SetActive(false);
            Room4Cam.SetActive(true);//this one
            Room5Cam.SetActive(false);
            Room6Cam.SetActive(false);
            Room7Cam.SetActive(false);
            Room8Cam.SetActive(false);
            Room9Cam.SetActive(false);
            Room10Cam.SetActive(false);
            Room11Cam.SetActive(false);
            Room12Cam.SetActive(false);
            Room13Cam.SetActive(false);
            Room14Cam.SetActive(false);
            Room15Cam.SetActive(false);
            Room16Cam.SetActive(false);
            tutorial1Cam.SetActive(false);
            tutorial2Cam.SetActive(false);
            tutorial3Cam.SetActive(false);
        }

        if (other.tag == "Room5")
        {
            playerCamHolder.SetActive(false);
            Room1Cam.SetActive(false);
            Room2Cam.SetActive(false);
            Room3Cam.SetActive(false);
            Room4Cam.SetActive(false);
            Room5Cam.SetActive(true);//this one
            Room6Cam.SetActive(false);
            Room7Cam.SetActive(false);
            Room8Cam.SetActive(false);
            Room9Cam.SetActive(false);
            Room10Cam.SetActive(false);
            Room11Cam.SetActive(false);
            Room12Cam.SetActive(false);
            Room13Cam.SetActive(false);
            Room14Cam.SetActive(false);
            Room15Cam.SetActive(false);
            Room16Cam.SetActive(false);
            tutorial1Cam.SetActive(false);
            tutorial2Cam.SetActive(false);
            tutorial3Cam.SetActive(false);
        }

        if (other.tag == "Room6")
        {
            playerCamHolder.SetActive(false);
            Room1Cam.SetActive(false);
            Room2Cam.SetActive(false);
            Room3Cam.SetActive(false);
            Room4Cam.SetActive(false);
            Room5Cam.SetActive(false);
            Room6Cam.SetActive(true); //this one
            Room7Cam.SetActive(false);
            Room8Cam.SetActive(false);
            Room9Cam.SetActive(false);
            Room10Cam.SetActive(false);
            Room11Cam.SetActive(false);
            Room12Cam.SetActive(false);
            Room13Cam.SetActive(false);
            Room14Cam.SetActive(false);
            Room15Cam.SetActive(false);
            Room16Cam.SetActive(false);
            tutorial1Cam.SetActive(false);
            tutorial2Cam.SetActive(false);
            tutorial3Cam.SetActive(false);
        }

        if (other.tag == "Room7")
        {
            playerCamHolder.SetActive(false);
            Room1Cam.SetActive(false);
            Room2Cam.SetActive(false);
            Room3Cam.SetActive(false);
            Room4Cam.SetActive(false);
            Room5Cam.SetActive(false);
            Room6Cam.SetActive(false);
            Room7Cam.SetActive(true); //this one
            Room8Cam.SetActive(false);
            Room9Cam.SetActive(false);
            Room10Cam.SetActive(false);
            Room11Cam.SetActive(false);
            Room12Cam.SetActive(false);
            Room13Cam.SetActive(false);
            Room14Cam.SetActive(false);
            Room15Cam.SetActive(false);
            Room16Cam.SetActive(false);
            tutorial1Cam.SetActive(false);
            tutorial2Cam.SetActive(false);
            tutorial3Cam.SetActive(false);
        }

        if (other.tag == "Room8")
        {
            playerCamHolder.SetActive(false);
            Room1Cam.SetActive(false);
            Room2Cam.SetActive(false);
            Room3Cam.SetActive(false);
            Room4Cam.SetActive(false);
            Room5Cam.SetActive(false);
            Room6Cam.SetActive(false);
            Room7Cam.SetActive(false);
            Room8Cam.SetActive(true); //this one
            Room9Cam.SetActive(false);
            Room10Cam.SetActive(false);
            Room11Cam.SetActive(false);
            Room12Cam.SetActive(false);
            Room13Cam.SetActive(false);
            Room14Cam.SetActive(false);
            Room15Cam.SetActive(false);
            Room16Cam.SetActive(false);
            tutorial1Cam.SetActive(false);
            tutorial2Cam.SetActive(false);
            tutorial3Cam.SetActive(false);
        }

        if (other.tag == "Room9")
        {
            playerCamHolder.SetActive(false);
            Room1Cam.SetActive(false);
            Room2Cam.SetActive(false);
            Room3Cam.SetActive(false);
            Room4Cam.SetActive(false);
            Room5Cam.SetActive(false);
            Room6Cam.SetActive(false);
            Room7Cam.SetActive(false);
            Room8Cam.SetActive(false);
            Room9Cam.SetActive(true); //this one
            Room10Cam.SetActive(false);
            Room11Cam.SetActive(false);
            Room12Cam.SetActive(false);
            Room13Cam.SetActive(false);
            Room14Cam.SetActive(false);
            Room15Cam.SetActive(false);
            Room16Cam.SetActive(false);
            tutorial1Cam.SetActive(false);
            tutorial2Cam.SetActive(false);
            tutorial3Cam.SetActive(false);
        }

        if (other.tag == "Room10")
        {
            playerCamHolder.SetActive(false);
            Room1Cam.SetActive(false);
            Room2Cam.SetActive(false);
            Room3Cam.SetActive(false);
            Room4Cam.SetActive(false);
            Room5Cam.SetActive(false);
            Room6Cam.SetActive(false);
            Room7Cam.SetActive(false);
            Room8Cam.SetActive(false);
            Room9Cam.SetActive(false);
            Room10Cam.SetActive(true); //this one
            Room11Cam.SetActive(false);
            Room12Cam.SetActive(false);
            Room13Cam.SetActive(false);
            Room14Cam.SetActive(false);
            Room15Cam.SetActive(false);
            Room16Cam.SetActive(false);
            tutorial1Cam.SetActive(false);
            tutorial2Cam.SetActive(false);
            tutorial3Cam.SetActive(false);
        }

        if (other.tag == "Room11")
        {
            playerCamHolder.SetActive(false);
            Room1Cam.SetActive(false);
            Room2Cam.SetActive(false);
            Room3Cam.SetActive(false);
            Room4Cam.SetActive(false);
            Room5Cam.SetActive(false);
            Room6Cam.SetActive(false);
            Room7Cam.SetActive(false);
            Room8Cam.SetActive(false);
            Room9Cam.SetActive(false);
            Room10Cam.SetActive(false);
            Room11Cam.SetActive(true); //this one
            Room12Cam.SetActive(false);
            Room13Cam.SetActive(false);
            Room14Cam.SetActive(false);
            Room15Cam.SetActive(false);
            Room16Cam.SetActive(false);
            tutorial1Cam.SetActive(false);
            tutorial2Cam.SetActive(false);
            tutorial3Cam.SetActive(false);
            Debug.Log("should turn on camera");
        }

        if (other.tag == "Room12")
        {
            playerCamHolder.SetActive(false);
            Room1Cam.SetActive(false);
            Room2Cam.SetActive(false);
            Room3Cam.SetActive(false);
            Room4Cam.SetActive(false);
            Room5Cam.SetActive(false);
            Room6Cam.SetActive(false);
            Room7Cam.SetActive(false);
            Room8Cam.SetActive(false);
            Room9Cam.SetActive(false);
            Room10Cam.SetActive(false);
            Room11Cam.SetActive(false);
            Room12Cam.SetActive(true); //this one
            Room13Cam.SetActive(false);
            Room14Cam.SetActive(false);
            Room15Cam.SetActive(false);
            Room16Cam.SetActive(false);
            tutorial1Cam.SetActive(false);
            tutorial2Cam.SetActive(false);
            tutorial3Cam.SetActive(false);
        }

        if (other.tag == "Room13")
        {
            playerCamHolder.SetActive(false);
            Room1Cam.SetActive(false);
            Room2Cam.SetActive(false);
            Room3Cam.SetActive(false);
            Room4Cam.SetActive(false);
            Room5Cam.SetActive(false);
            Room6Cam.SetActive(false);
            Room7Cam.SetActive(false);
            Room8Cam.SetActive(false);
            Room9Cam.SetActive(false);
            Room10Cam.SetActive(false);
            Room11Cam.SetActive(false);
            Room12Cam.SetActive(false);
            Room13Cam.SetActive(true); //this one
            Room14Cam.SetActive(false);
            Room15Cam.SetActive(false);
            Room16Cam.SetActive(false);
            tutorial1Cam.SetActive(false);
            tutorial2Cam.SetActive(false);
            tutorial3Cam.SetActive(false);
        }

        if (other.tag == "Room14")
        {
            playerCamHolder.SetActive(false);
            Room1Cam.SetActive(false);
            Room2Cam.SetActive(false);
            Room3Cam.SetActive(false);
            Room4Cam.SetActive(false);
            Room5Cam.SetActive(false);
            Room6Cam.SetActive(false);
            Room7Cam.SetActive(false);
            Room8Cam.SetActive(false);
            Room9Cam.SetActive(false);
            Room10Cam.SetActive(false);
            Room11Cam.SetActive(false);
            Room12Cam.SetActive(false);
            Room13Cam.SetActive(false);
            Room14Cam.SetActive(true); //this one
            Room15Cam.SetActive(false);
            Room16Cam.SetActive(false);
            tutorial1Cam.SetActive(false);
            tutorial2Cam.SetActive(false);
            tutorial3Cam.SetActive(false);
        }

        if (other.tag == "Room15")
        {
            playerCamHolder.SetActive(false);
            Room1Cam.SetActive(false);
            Room2Cam.SetActive(false);
            Room3Cam.SetActive(false);
            Room4Cam.SetActive(false);
            Room5Cam.SetActive(false);
            Room6Cam.SetActive(false);
            Room7Cam.SetActive(false);
            Room8Cam.SetActive(false);
            Room9Cam.SetActive(false);
            Room10Cam.SetActive(false);
            Room11Cam.SetActive(false);
            Room12Cam.SetActive(false);
            Room13Cam.SetActive(false);
            Room14Cam.SetActive(false);
            Room15Cam.SetActive(true); //this one
            Room16Cam.SetActive(false);
            tutorial1Cam.SetActive(false);
            tutorial2Cam.SetActive(false);
            tutorial3Cam.SetActive(false);
        }

        if (other.tag == "Room16")
        {
            playerCamHolder.SetActive(false);
            Room1Cam.SetActive(false);
            Room2Cam.SetActive(false);
            Room3Cam.SetActive(false);
            Room4Cam.SetActive(false);
            Room5Cam.SetActive(false);
            Room6Cam.SetActive(false);
            Room7Cam.SetActive(false);
            Room8Cam.SetActive(false);
            Room9Cam.SetActive(false);
            Room10Cam.SetActive(false);
            Room11Cam.SetActive(false);
            Room12Cam.SetActive(false);
            Room13Cam.SetActive(false);
            Room14Cam.SetActive(false);
            Room15Cam.SetActive(false);
            Room16Cam.SetActive(true); //this one
            tutorial1Cam.SetActive(false);
            tutorial2Cam.SetActive(false);
            tutorial3Cam.SetActive(false);
        }

        if (other.tag == "Tutorial1")
        {
            playerCamHolder.SetActive(false);
            Room1Cam.SetActive(false);
            Room2Cam.SetActive(false);
            Room3Cam.SetActive(false);
            Room4Cam.SetActive(false);
            Room5Cam.SetActive(false);
            Room6Cam.SetActive(false);
            Room7Cam.SetActive(false);
            Room8Cam.SetActive(false);
            Room9Cam.SetActive(false);
            Room10Cam.SetActive(false);
            Room11Cam.SetActive(false);
            Room12Cam.SetActive(false);
            Room13Cam.SetActive(false);
            Room14Cam.SetActive(false);
            Room15Cam.SetActive(false);
            Room16Cam.SetActive(false);
            tutorial1Cam.SetActive(true); //this one
            tutorial2Cam.SetActive(false);
            tutorial3Cam.SetActive(false);
        }

        if (other.tag == "Tutorial2")
        {
            playerCamHolder.SetActive(false);
            Room1Cam.SetActive(false);
            Room2Cam.SetActive(false);
            Room3Cam.SetActive(false);
            Room4Cam.SetActive(false);
            Room5Cam.SetActive(false);
            Room6Cam.SetActive(false);
            Room7Cam.SetActive(false);
            Room8Cam.SetActive(false);
            Room9Cam.SetActive(false);
            Room10Cam.SetActive(false);
            Room11Cam.SetActive(false);
            Room12Cam.SetActive(false);
            Room13Cam.SetActive(false);
            Room14Cam.SetActive(false);
            Room15Cam.SetActive(false);
            Room16Cam.SetActive(false);
            tutorial1Cam.SetActive(false);
            tutorial2Cam.SetActive(true); //this one
            tutorial3Cam.SetActive(false);
        }

        if (other.tag == "Tutorial3")
        {
            playerCamHolder.SetActive(false);
            Room1Cam.SetActive(false);
            Room2Cam.SetActive(false);
            Room3Cam.SetActive(false);
            Room4Cam.SetActive(false);
            Room5Cam.SetActive(false);
            Room6Cam.SetActive(false);
            Room7Cam.SetActive(false);
            Room8Cam.SetActive(false);
            Room9Cam.SetActive(false);
            Room10Cam.SetActive(false);
            Room11Cam.SetActive(false);
            Room12Cam.SetActive(false);
            Room13Cam.SetActive(false);
            Room14Cam.SetActive(false);
            Room15Cam.SetActive(false);
            Room16Cam.SetActive(false);
            tutorial1Cam.SetActive(false);
            tutorial2Cam.SetActive(false);
            tutorial3Cam.SetActive(true); //this one
        }


        if (other.tag == "WaterObstacle")
        {
            healthManager.FireWallHit();
            playerHitParticle.Play();
        }

        if (other.tag == "GhostDamage")
        {
            healthManager.FireWallHit();
            playerHitParticle.Play();
        }

        if (other.tag == "BossDamage")
        {
            healthManager.BossWallHit();
            playerHitParticle.Play();
        }

        if (other.CompareTag("ShootingEnemyRange"))
        {
            enemyLookAt enemy = other.GetComponentInParent<enemyLookAt>(); 

            if (enemy != null)
            {
                enemy.isInShootingRange = true;
                
            }

        }

        if (other.CompareTag("GhostTrigger"))
        {
            ghostLookAt ghost = other.GetComponentInParent<ghostLookAt>();

            if (ghost != null)
            {
                ghost.isInGhostRange = true;
                Debug.Log("is in ghost range");
            }

        }

        if(other.tag == "BossTrigger")
        {
            bossLookAt.isInBossRange = true;
        }
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.tag == "Room1" || other.tag == "Room2" || other.tag == "Room3" || other.tag == "Room4" || other.tag == "Room5" || other.tag == "Room6" || other.tag == "Room7" || other.tag == "Room8" || other.tag == "Room9" || other.tag == "Room10" || other.tag == "Room11" || other.tag == "Room12" || other.tag == "Room13" || other.tag == "Room14" || other.tag == "Room15" || other.tag == "Room16" || other.tag == "Tutorial1" || other.tag == "Tutorial2" || other.tag == "Tutorial3")
        {
            playerCamHolder.SetActive(true); //this one
            Room1Cam.SetActive(false);
            Room2Cam.SetActive(false);
            Room3Cam.SetActive(false);
            Room4Cam.SetActive(false);
            Room5Cam.SetActive(false);
            Room6Cam.SetActive(false);
            Room7Cam.SetActive(false);
            Room8Cam.SetActive(false);
            Room9Cam.SetActive(false);
            Room10Cam.SetActive(false);
            Room11Cam.SetActive(false);
            Room12Cam.SetActive(false);
            Room13Cam.SetActive(false);
            Room14Cam.SetActive(false);
            Room15Cam.SetActive(false);
            Room16Cam.SetActive(false);
            tutorial1Cam.SetActive(false);
            tutorial2Cam.SetActive(false);
            tutorial3Cam.SetActive(false);
        }

        if (other.CompareTag("ShootingEnemyRange"))
        {
            enemyLookAt enemy = other.GetComponentInParent<enemyLookAt>(); 

            if (enemy != null)
            {
                enemy.isInShootingRange = false;
            }

        }

        if (other.CompareTag("GhostTrigger"))
        {
            ghostLookAt ghost = other.GetComponentInParent<ghostLookAt>();

            if (ghost != null)
            {
                ghost.isInGhostRange = false;
            }

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

    private IEnumerator FireSpellPopUp()
    {
        yield return new WaitForSeconds(0f);
        fireTextPopUp.SetActive(true);
        yield return new WaitForSeconds(2f);
        fireTextPopUp.SetActive(false);
    }

    private IEnumerator WaterSpellPopUp()
    {
        yield return new WaitForSeconds(0f);
        waterTextPopUp.SetActive(true);
        yield return new WaitForSeconds(2f);
        waterTextPopUp.SetActive(false);
    }

    private IEnumerator RockSpellPopUp()
    {
        yield return new WaitForSeconds(0f);
        rockTextPopUp.SetActive(true);
        yield return new WaitForSeconds(2f);
        rockTextPopUp.SetActive(false);
    }

    private IEnumerator WindSpellPopUp()
    {
        yield return new WaitForSeconds(0f);
        windTextPopUp.SetActive(true);
        yield return new WaitForSeconds(2f);
        windTextPopUp.SetActive(false);
    }

    private IEnumerator HealthPotionPopUp()
    {
        yield return new WaitForSeconds(0f);
        healthTextPopUp.SetActive(true);
        yield return new WaitForSeconds(2f);
        healthTextPopUp.SetActive(false);
    }

    private IEnumerator ManaPotionPopUp()
    {
        yield return new WaitForSeconds(0f);
        manaTextPopUp.SetActive(true);
        yield return new WaitForSeconds(2f);
        manaTextPopUp.SetActive(false);
    }

    private IEnumerator KeyPopUp()
    {
        yield return new WaitForSeconds(0f);
        keyTextPopUp.SetActive(true);
        yield return new WaitForSeconds(2f);
        keyTextPopUp.SetActive(false);
    }

    //spell switching coroutines

    private IEnumerator USOneToUSTwo()
    {
        yield return new WaitForSeconds(0f);
        unknownSpell1.SetActive(false);
        unknownSpellOneEquipped = false;
        yield return new WaitForSeconds(0.01f);
        unknownSpell2.SetActive(true);
        unknownSpellTwoEquipped = true;
        Debug.Log("changed to unknown spell 2");
    }

    private IEnumerator USOneToWaterSpell()
    {
        yield return new WaitForSeconds(0f);
        unknownSpell1.SetActive(false);
        unknownSpellOneEquipped = false;
        yield return new WaitForSeconds(0.01f);
        waterSpellUI.SetActive(true);
        waterSpellEquipped = true;
        Debug.Log("changed to water spell");
    }

    private IEnumerator USTwoToUSThree() 
    {  
        yield return new WaitForSeconds(0f);
        unknownSpell2.SetActive(false);
        unknownSpellTwoEquipped = false;
        yield return new WaitForSeconds(0.01f);
        unknownSpell3.SetActive(true);
        unknownSpellThreeEquipped = true;
        Debug.Log(" changed to unknown spell 3");
    }

    private IEnumerator USTwoToRockSpell()
    {
        yield return new WaitForSeconds(0f);
        unknownSpell2.SetActive(false);
        unknownSpellTwoEquipped = false;
        yield return new WaitForSeconds(0.01f);
        rockSpellUI.SetActive(true);
        rockSpellEquipped = true;
        Debug.Log(" changed to rock spell");
    }

    private IEnumerator USThreeToUSFour()
    {
        yield return new WaitForSeconds(0f);
        unknownSpell3.SetActive(false);
        unknownSpellThreeEquipped = false;
        yield return new WaitForSeconds(0.01f);
        unknownSpell4.SetActive(true);
        unknownSpellFourEquipped = true;
        Debug.Log(" changed to rock spell");
    }

    private IEnumerator USThreeToWindSpell()
    {
        yield return new WaitForSeconds(0f);
        unknownSpell3.SetActive(false);
        unknownSpellThreeEquipped = false;
        yield return new WaitForSeconds(0.01f);
        windSpellUI.SetActive(true);
        windSpellEquipped = true;
        Debug.Log(" changed to rock spell");
    }

    private IEnumerator USFourToUSOne()
    {
        yield return new WaitForSeconds(0f);
        unknownSpell4.SetActive(false);
        unknownSpellFourEquipped = false;
        yield return new WaitForSeconds(0.01f);
        unknownSpell1.SetActive(true);
        unknownSpellOneEquipped = true;
        Debug.Log(" changed to rock spell");
    }

    private IEnumerator USFourToFireSpell() 
    {
        yield return new WaitForSeconds(0f);
        unknownSpell4.SetActive(false);
        unknownSpellFourEquipped = false;
        yield return new WaitForSeconds(0.01f);
        fireSpellUI.SetActive(true);
        fireSpellEquipped = true;
        Debug.Log(" changed to rock spell");
    }

    private IEnumerator FireSpellToUSTwo()
    {
        yield return new WaitForSeconds(0f);
        fireSpellUI.SetActive(false);
        fireSpellEquipped = false;
        yield return new WaitForSeconds(0.01f);
        unknownSpell2.SetActive(true);
        unknownSpellTwoEquipped = true;
        Debug.Log("changed to unknown spell 2");
    }

    private IEnumerator FireSpellToWaterSpell() 
    {  
        yield return new WaitForSeconds(0f);
        fireSpellUI.SetActive(false);
        fireSpellEquipped = false;
        yield return new WaitForSeconds(0.01f);
        waterSpellUI.SetActive(true);
        waterSpellEquipped = true;
        Debug.Log("changed to water spell");
    }

    private IEnumerator WaterSpellToUSThree()
    {
        yield return new WaitForSeconds(0f);
        waterSpellUI.SetActive(false);
        waterSpellEquipped = false;
        yield return new WaitForSeconds(0.01f);
        unknownSpell3.SetActive(true);
        unknownSpellThreeEquipped = true;
        Debug.Log("changed to unknow spell 3");
    }

    private IEnumerator WaterSpellToRockSpell()
    {
        yield return new WaitForSeconds(0f);
        waterSpellUI.SetActive(false);
        waterSpellEquipped = false;
        yield return new WaitForSeconds(0.01f);
        rockSpellUI.SetActive(true);
        rockSpellEquipped = true;
        Debug.Log("changed to rock spell");
    }

    private IEnumerator RockSpellToUSFour()
    {
        yield return new WaitForSeconds(0f);
        rockSpellUI.SetActive(false);
        rockSpellEquipped = false;
        yield return new WaitForSeconds(0.01f);
        unknownSpell4.SetActive(true);
        unknownSpellFourEquipped = true;
        Debug.Log("changed to unknown spell 4");
    }

    private IEnumerator RockSpellToWindSpell()
    {
        yield return new WaitForSeconds(0f);
        rockSpellUI.SetActive(false);
        rockSpellEquipped = false;
        yield return new WaitForSeconds(0.01f);
        windSpellUI.SetActive(true);
        windSpellEquipped = true;
        Debug.Log("changed to wind spell");
    }

    private IEnumerator WindSpellToUSOne() 
    { 
        yield return new WaitForSeconds(0f);
        windSpellUI.SetActive(false);
        windSpellEquipped = false;
        yield return new WaitForSeconds(0.01f);
        unknownSpell1.SetActive(true);
        unknownSpellOneEquipped = true;
        Debug.Log("changed to unknown spell 1");
    }

    private IEnumerator WindSpellToFireSpell()
    {
        yield return new WaitForSeconds(0f);
        windSpellUI.SetActive(false);
        windSpellEquipped = false;
        yield return new WaitForSeconds(0.01f);
        fireSpellUI.SetActive(true);
        fireSpellEquipped = true;
        Debug.Log("changed to fire spell");
    }

    //water shield
    private IEnumerator WaterShield()
    {
        yield return new WaitForSeconds(0f);
        waterShield.SetActive(true);
        yield return new WaitForSeconds(5f);
        waterShield.SetActive(false);
        isUsingWaterShield = false;
    }

    //door locked text
    private IEnumerator DoorLocked()
    {
        yield return new WaitForSeconds(0f);
        unlockDoorText.SetActive(false);
        yield return new WaitForSeconds(2f);
        doorLockedText.SetActive(false);
    }

    //checkpoint set text
    private IEnumerator CheckpointSet()
    {
        yield return new WaitForSeconds(0f);
        prayText.SetActive(false);
        yield return new WaitForSeconds(2f);
        checkpointSetText.SetActive(false);
    }

    //got king coin
    private IEnumerator KingCoinAcquired()
    {
        yield return new WaitForSeconds(0f);
        kingCoinText.SetActive(true);
        yield return new WaitForSeconds(2f);
        kingCoinText.SetActive(false);
    }

    //got queen crystal
    private IEnumerator QueenCrystalAcquired()
    {
        yield return new WaitForSeconds(0f);
        queenCrystalText.SetActive(true);
        yield return new WaitForSeconds(2f);
        queenCrystalText.SetActive(false);
    }

    //got staff
    private IEnumerator StaffAcquired()
    {
        yield return new WaitForSeconds(0f);
        potionText.SetActive(false);
        staffText.SetActive(true);
        yield return new WaitForSeconds(5f);
        staffText.SetActive(false);
    }

    //got potion bag
    private IEnumerator PotionBagAcquired()
    {
        yield return new WaitForSeconds(0f);
        staffText.SetActive(false);
        potionText.SetActive(true);
        yield return new WaitForSeconds(5f);
        potionText.SetActive(false);
    }

    //double jump cap
    private IEnumerator CapJump()
    {
        yield return new WaitForSeconds(2f);
        isJumping = false;
    }

    //not strong enough

    private IEnumerator NotStrongEnough()
    {
        yield return new WaitForSeconds(0f);
        notStrongEnoughText.SetActive(true);
        yield return new WaitForSeconds(4f);
        notStrongEnoughText.SetActive(false);
    }

    //teleporter stuff
    private IEnumerator Teleport()
    {
        yield return new WaitForSeconds(0f);
        teleportScreen.SetActive(true);
        transform.position = teleportPoint.transform.position;
        _characterController.enabled = false;
        yield return new WaitForSeconds(2f);
        teleportScreen.SetActive(false);
        _characterController.enabled = true;
      //  tutorialLevel.SetActive(false);
    }

    public IEnumerator StartText()
    {
        yield return new WaitForSeconds(0f);
        moveText.SetActive(true);
        yield return new WaitForSeconds(3f);
        moveText.SetActive(false);
        aimText.SetActive(true);
        yield return new WaitForSeconds(3f);
        aimText.SetActive(false);
        jumpText.SetActive(true);
        yield return new WaitForSeconds(3f);
        jumpText.SetActive(false);
        dashText.SetActive(true);
        yield return new WaitForSeconds(3f);
        dashText.SetActive(false);
        pauseText.SetActive(true);
        yield return new WaitForSeconds(3f);
        pauseText.SetActive(false);
        startDoor.SetActive(false);
    }

    public IEnumerator KillBox()
    {
        yield return new WaitForSeconds(0f);
        _characterController.enabled = false;
        transform.position = killboxRespawnPoint.transform.position;
        playerCamHolder.SetActive(false);
        yield return new WaitForSeconds(0.5f);
        _characterController.enabled=true;
    }

    public IEnumerator KillBox2()
    {
        yield return new WaitForSeconds(0f);
        _characterController.enabled = false;
        transform.position = killbox2RespawnPoint.transform.position;
        playerCamHolder.SetActive(false);
        yield return new WaitForSeconds(0.5f);
        _characterController.enabled = true;
    }

    public IEnumerator KillBox3()
    {
        yield return new WaitForSeconds(0f);
        _characterController.enabled = false;
        transform.position = killbox3RespawnPoint.transform.position;
        playerCamHolder.SetActive(false);
        yield return new WaitForSeconds(0.5f);
        _characterController.enabled = true;
    }

    public IEnumerator KillBox4()
    {
        yield return new WaitForSeconds(0f);
        _characterController.enabled = false;
        transform.position = killbox4RespawnPoint.transform.position;
        playerCamHolder.SetActive(false);
        yield return new WaitForSeconds(0.5f);
        _characterController.enabled = true;
    }

    [ContextMenu("End game")]
    public void EndGame()
    {
        StartCoroutine(EndTheDamnGame());
    }

    public IEnumerator EndTheDamnGame()
    {
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene("End");
    }
}
