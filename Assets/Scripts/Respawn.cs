using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Intended for "death trigger."
public class Respawn : MonoBehaviour
{
    public GameObject respawnPoint; //Respawm point
    public Inventory player; //Player

    public AudioClip death;

    public void OnTriggerEnter(Collider other) {
        //If player hits trigger
        if (other.gameObject.CompareTag("Player")) {
            //Reduce their health by one, and respawn them.
            player = other.gameObject.GetComponent<Inventory>();
            player.health--;
            other.gameObject.transform.position = respawnPoint.transform.position;
            GetComponent<AudioSource>().PlayOneShot(death);
        }
    }
}
