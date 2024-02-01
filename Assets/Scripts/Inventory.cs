using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public int health = 5; //Player health
    public int berryCount = 0; //Player berry count
    public int sheepRescued = 0; //Sheep player rescued
    public int wolvesDefeated = 0; //Wolves player has beaten
    PlayerCollider player;
    Movement movement;

    public GameObject deathRespawn;

    public AudioSource death;
    public AudioSource hit;

    public AudioSource eat;
    public AudioSource taken;

    Animator sheepAnim;

    void Start() {
        player = this.GetComponent<PlayerCollider>();
        movement = this.GetComponent<Movement>();
        sheepAnim = this.GetComponent<Animator>();
        //If instance of manager exists, update player to match upgrades:
        if (Manager.Instance != null) {
            UpgradeUpdate(Manager.Instance.maxHealth, Manager.Instance.maxStamina,
                          Manager.Instance.dashModifier, Manager.Instance.jumpModifier);
        }
    }

    //Update player values to match upgraded ones:
    public void UpgradeUpdate(int maxHealth, float maxStamina, float dashModifier, float jumpModifier) {
        health = maxHealth;
        movement.maxStamina = maxStamina;
        movement.stamina = maxStamina;
        movement.dashModifier = dashModifier;
        movement.jumpModifier = jumpModifier;
    }

    //Player collected a berry:
    public void BerryGet() {
        berryCount++;
        taken.Play();
    }

    //Player consumed a berry:
    public void BerryEat() {
        if (berryCount >= 1) {
            health++;
            berryCount--;
            eat.Play();
        }
    }

    //Player rescued a sheep:
    public void SheepRescued() {
        sheepRescued++;
    }

    //Player got hit:
    public void Hit() {
        health--;
        if (health >= 1) {
            hit.Play();
            sheepAnim.SetBool("Hit", true);
        }
        if (health == 0){
            death.Play();
            movement.moveModifier = 0f;
            sheepAnim.SetBool("Death", true);
        }
    }

    //The player can no longer be hit:
    public void Intangable() {
        player.tangable = false;
        sheepAnim.SetBool("Hit", false);
    }

    //The player can be hit:
    public void Tangable() {
        player.tangable = true;
    }

    //The player defeated a wolf:
    public void WolfDefeated() {
        wolvesDefeated++;
    }

    //The player has died:
    public void Death() {
        this.gameObject.transform.position = deathRespawn.transform.position;
        health = 5;
        movement.moveModifier = 5f;
        sheepAnim.SetBool("Death", false);
        Tangable();
    }
}
