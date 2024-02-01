using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public float moveModifier = 5f; //Movement speed
    public float baseMoveModifier = 5f;
    public float moveVelocity = 0f; //Current velocity
    public bool dashing = false;
    public bool jumping = false;
    public bool extraJumping = false;
    public float jumpTime = 0.0f;
    public float jumpModifier = 300f; //Jump height
    public float dashModifier = 5f;
    public float lookModifier = 3f; //Camera speed
    public Vector2 rotation = new Vector2(0, 0); //Rotation vector for mouse look

    public AudioClip jumpNoise;

    public AudioSource roll;

    public AudioClip[] walkingGrass;
    public AudioClip[] walkingWood;
    public AudioClip[] walkingStone;
    public AudioClip[] runningGrass;
    public AudioClip[] runningWood;
    public AudioClip[] runningStone;

    public bool onGrass;
    public bool onWood;
    public bool onStone;

    public float stamina = 100f; //Stamina
    public float maxStamina = 100f; //Maximum possible stamina

    Rigidbody player;
    PlayerCollider statuses;
    Animator sheepAnim;

    // Start is called before the first frame update
    void Start()
    {
        //Sets up variables for mouse look.
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = false;

        //Initializes rigidbody for player.
        player = GetComponent<Rigidbody>();
        statuses = GetComponent<PlayerCollider>();
        sheepAnim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        //Sprint function:
        //When shift is pressed down, movement speed is doubled,
        //but the turning speed is divided by a factor of 5.
        if (Input.GetKeyDown(KeyCode.LeftShift)) {
            if (stamina > 0) {
                if (statuses.airborn == false) {
                    moveModifier = 10f;
                    lookModifier = 2f;
                    sheepAnim.SetFloat("Sprinting", moveModifier);
                } else {
                    dashing = true;
                }
            } else {
                moveModifier = 3f;
            }
        }
        //Once the player lets go of shift, they slow down.
        if (Input.GetKeyUp(KeyCode.LeftShift)) {
            moveModifier = 5f;
            lookModifier = 10f;
            if (statuses.airborn == false){
                sheepAnim.SetFloat("Walking", moveModifier);
            } else {
                dashing = false;
            }
        }

        //If the player presses space, they are jumping:
        if (Input.GetKeyDown(KeyCode.Space)) {
            jumping = true;
            //If they are already airborn,
            //they are double or "extra jumping"
            if (statuses.airborn == true) {
                extraJumping = true;
            }

        //Once they let go of space, they are no longer jumping:
        } else if (Input.GetKeyUp(KeyCode.Space)) {
            jumping = false;
        }

        //Horizantal and vertical movement.
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");
        //Movement vector is set with new values, and the player's
        //position is modified by it, the change in time since the last frame,
        //and the move speed variable.
        Vector3 movement = new Vector3(moveHorizontal, 0f, moveVertical);
        moveVelocity = moveVertical * moveModifier;
        transform.Translate(movement * moveModifier * Time.deltaTime);

        sheepAnim.SetFloat("Walking", moveVelocity);

        //Code that tracks mouse movement and modifies camera accordingly.
        this.rotation.y += Input.GetAxis("Mouse X") * lookModifier;
        transform.eulerAngles = rotation;


        //If the player isn't sprinting, is grounded and have less than max stamina, increase it.
        if (moveModifier <= baseMoveModifier && statuses.airborn == false && stamina < maxStamina) {
            stamina += 20f * Time.deltaTime;
        }

        //If the player's stamina is ever below 0 or above the maximum,
        //set it to be within the range 0 <= stamina <= maxStamina
        if (stamina > maxStamina) {
            stamina = maxStamina;
        } else if (stamina < 0f) {
            stamina = 0f;
        }

        if (jumpTime <= 1.0f) {
            jumpTime += Time.deltaTime;
        }

        //If the game is paused and the lookModifer isn't 0, 
        //set it to 0.
        if (Time.timeScale == 0 && lookModifier > 0) {
            lookModifier = 0f;
        
        //Otherwise, if the game is unpaused and the lookModifier is 0,
        //set it to its proper value
        } else if (Time.timeScale > 0 && lookModifier == 0) {
            lookModifier = 3f;
        }
    }

    void FixedUpdate(){
        //Jump
        if (jumping == true && jumpTime >= 0.7f) {
            //If they're grounded, they can jump:
            if (statuses.airborn == false){
                player.AddForce(transform.up * jumpModifier);
                statuses.airborn = true;
                sheepAnim.SetBool("Jumping", true);
                GetComponent<AudioSource>().PlayOneShot(jumpNoise);
                jumping = false;
            //Otherwise, they can spend 50 stamina to double jump,
            //but only if they have the stamina to do it:
            } else if (extraJumping == true && stamina >= 50f) {
                //sheepAnim.SetBool("DoubleJump", false);
                player.AddForce(transform.up * jumpModifier);
                //sheepAnim.SetBool("DoubleJump", true);
                sheepAnim.Play("Jump", -1, 0f);
                GetComponent<AudioSource>().PlayOneShot(jumpNoise);
                sheepAnim.SetBool("Dashing", false);
                stamina -= 50;
                jumping = false;
                //extraJumping = false;
            }
            jumpTime = 0f;
        }

        //Airdash:
        //If the player is dashing and they have at least 30 stamina,
        //they will perform a midair dash:
        if (dashing == true && stamina >= 30f) {
            player.AddForce(transform.forward * dashModifier, ForceMode.Impulse);
            stamina -= 30;
            sheepAnim.SetBool("Dashing", true);
            sheepAnim.SetBool("DoubleJump", false);
            roll.Play();
            dashing = false;
        }
    }

    public void WalkingGrass() {
        if (onGrass) {
            System.Random rand = new System.Random();
            int stepSound = rand.Next(0, walkingGrass.Length - 1);

            GetComponent<AudioSource>().PlayOneShot(walkingGrass[stepSound]);
        }
    }

    public void RunningGrass() {
        if (onGrass) {
            System.Random rand = new System.Random();
            int stepSound = rand.Next(0, runningGrass.Length - 1);
            
            GetComponent<AudioSource>().PlayOneShot(runningGrass[stepSound]);
        }
        
        
    }

    public void WalkingWood() {
        if (onWood) {
            System.Random rand = new System.Random();
            int stepSound = rand.Next(0, walkingWood.Length - 1);

            GetComponent<AudioSource>().PlayOneShot(walkingWood[stepSound]);
        }
    }

    public void RunningWood() {
        if (onWood) {
            System.Random rand = new System.Random();
            int stepSound = rand.Next(0, runningWood.Length - 1);
            
            GetComponent<AudioSource>().PlayOneShot(runningWood[stepSound]);
        }
        
        
    }

    public void WalkingStone() {
        if (onStone) {
            System.Random rand = new System.Random();
            int stepSound = rand.Next(0, walkingStone.Length - 1);

            GetComponent<AudioSource>().PlayOneShot(walkingStone[stepSound]);
        }
    }

    public void RunningStone() {
        if (onStone) {
            System.Random rand = new System.Random();
            int stepSound = rand.Next(0, runningStone.Length - 1);
            
            GetComponent<AudioSource>().PlayOneShot(runningStone[stepSound]);
        }
        
        
    }

}
