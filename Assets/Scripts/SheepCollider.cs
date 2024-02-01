using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SheepCollider : MonoBehaviour
{
    public bool airborn = false;
    sheepMovement movement; //SheepMovement Script
    Inventory inventory; //Inventory script
    Animator sheepAnim;
    NPSheep sheep;
    
    void Start(){
        movement = GetComponent<sheepMovement>();
        sheepAnim = GetComponent<Animator>();
        inventory = GetComponent<Inventory>();
    }

    public void OnCollisionEnter(Collision other) {
        if (other.gameObject.CompareTag("Floor")) {
            airborn = false;
            movement.extraJumping = false;
            sheepAnim.SetBool("Jumping", false);
            sheepAnim.SetBool("DoubleJump", false);
        }
    }

    void OnCollisionStay(Collision other) {
        //Otherwise, on collision with another sheep: 
        if (other.gameObject.CompareTag("Sheep")) {
            //If it hasn't already been rescued:
            sheep = other.gameObject.GetComponent<NPSheep>();
            if (sheep.rescued == false) {
                sheep.rescued = true;
                other.gameObject.SetActive(false); //The sheep dissapears,
                inventory.SheepRescued(); //and the player rescues it
            }
        }
    }
}
