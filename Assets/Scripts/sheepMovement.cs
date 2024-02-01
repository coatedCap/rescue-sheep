using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sheepMovement : MonoBehaviour
{
    public float moveModifier = 5f; //Movement speed
    public float baseMoveModifier = 5f;
    public float moveVelocity = 0f; //Current velocity
    public bool jumping = false;
    public bool extraJumping = false;
    public float jumpModifier = 50f; //Jump height
    public float lookModifier = 3f; //Camera speed
    public Vector2 rotation = new Vector2(0, 0); //Rotation vector for mouse look

    public float stamina = 100f; //Stamina
    float maxStamina = 100f; //Maximum possible stamina

    Rigidbody player;
    SheepCollider statuses;

    Animator sheepAnim;

    // Start is called before the first frame update
    void Start()
    {
        //Sets up variables for mouse look.
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = false;

        //Initializes rigidbody for player.
        player = GetComponent<Rigidbody>();
        statuses = GetComponent<SheepCollider>();
        sheepAnim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {   

        //Simple attacking code animation, move to attack script!
        if (Input.GetKeyDown(KeyCode.V) ) {
            sheepAnim.SetBool("IsAttacking", true);
        }
        if (Input.GetKeyUp(KeyCode.V)) {
            sheepAnim.SetBool("IsAttacking", false);
        }

        //Sprint function:
        //When shift is pressed down, movement speed is doubled,
        //but the turning speed is divided by a factor of 5.
        if (Input.GetKeyDown(KeyCode.LeftShift) ) {
            if (stamina > 0) {
                moveModifier = 10f;
                lookModifier = 2f;
                sheepAnim.SetFloat("Sprinting", moveModifier);
            } else {
                moveModifier = 3f;
            }
        }
        //Once the player let
        if (Input.GetKeyUp(KeyCode.LeftShift)) {
            moveModifier = 5f;
            lookModifier = 10f;
            sheepAnim.SetFloat("Walking", moveModifier);
        }

        if (Input.GetKeyDown(KeyCode.Space)) {
            jumping = true;
            if (statuses.airborn == true) {
                extraJumping = true;
            }
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
        if (jumping == true) {
            //If they're grounded, they can jump
            if (statuses.airborn == false){
                player.AddForce(transform.up * jumpModifier);
                statuses.airborn = true;
                sheepAnim.SetBool("Jumping", true);
            //Otherwise, they can spend 75 stamina to double jump,
            //but only if they have the stamina to do it (at least 75 above 0)
            } else if (extraJumping == true && stamina - 75f > 0f) {
                player.AddForce(transform.up * jumpModifier * 2);
                sheepAnim.SetBool("DoubleJump", true);
                stamina -= 75;
            }
        }
    
    } 
}
