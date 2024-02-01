using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Berries : MonoBehaviour
{
    public bool collected = false; //Have these been collected?
    Interact interact;
    Inventory inventory;

    //On collision, if it was with the player:
    void OnTriggerStay (Collider other) {
        if (other.gameObject.CompareTag("Player")) {
            interact = other.gameObject.GetComponent<Interact>();
            inventory = other.gameObject.GetComponent<Inventory>();

            //If the berries haven't been collected and the player is interacting:
            if (collected == false && interact.interacting == true) {
                collected = true; //They have now been collected
                inventory.BerryGet(); //Player gets a berry
                this.gameObject.SetActive(false); //Berries no longer exist
            }
        }
    }
}
