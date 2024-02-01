using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hud : MonoBehaviour
{
    public int maxHealth = 5;
    public float maxStamina = 100f;
    public float dashModifier = 5f;
    public float jumpModifier = 300f;
    public GameObject player;
    Movement movement;
    Inventory inventory;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player");
        movement = player.GetComponent<Movement>();
        inventory = player.GetComponent<Inventory>();
        inventory.health = maxHealth;
        movement.maxStamina = maxStamina;
        movement.stamina = maxStamina;
        movement.dashModifier = dashModifier;
        movement.jumpModifier = jumpModifier;

        print("Start fn ran!");
    }
}
