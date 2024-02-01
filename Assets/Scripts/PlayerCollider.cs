using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollider : MonoBehaviour
{
    public bool tangable = true; //Can player be hit?
    public bool airborn = false; //Is player in the air?
    public float time = 0.0f;
    Movement movement; //Movement script
    Inventory inventory; //Inventory script
    Animator sheepAnim;
    NPSheep sheep; //Sheep being rescued
    public AudioSource sheepCollected;
    
    void Start() {
        movement = GetComponent<Movement>();
        inventory = GetComponent<Inventory>();
        sheepAnim = GetComponent<Animator>();
    }

    void OnCollisionEnter(Collision other) {
        //On collision with floor:
        if (other.gameObject.CompareTag("Floor")) {
            airborn = false; //Player is no longer airborn,
            movement.extraJumping = false; //and they aren't double/triple jumping
            sheepAnim.SetBool("Jumping", false);
            sheepAnim.SetBool("DoubleJump", false);
            sheepAnim.SetBool("Dashing", false);
            movement.onGrass = true;
        }
    }

    void OnCollisionStay(Collision other) {
        //Otherwise, on collision with another sheep: 
        if (other.gameObject.CompareTag("Sheep")) {
            sheep = other.gameObject.GetComponent<NPSheep>();
            if (sheep.rescued == false) {
                sheep.rescued = true;
                other.gameObject.SetActive(false); //The sheep dissapears,
                inventory.SheepRescued(); //and the player rescues it
                sheepCollected.Play();
            }
        }
    }

    void Update() {
        time += Time.deltaTime; //Increment timer
    }
}
