using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interact : MonoBehaviour
{
    Inventory inventory;
    public bool interacting = false; //Can player interact?

    // Start is called before the first frame update
    void Start()
    {
        inventory = this.GetComponent<Inventory>();
    }

    // Update is called once per frame
    void Update()
    {
        //If player presses Q, they eat a berry
        if (Input.GetKeyDown(KeyCode.Q)) {
            inventory.BerryEat();
        }

        //If player is holding down E, they can interact
        if (Input.GetKeyDown(KeyCode.E)) {
            interacting = true;
        }

        //Once player lets go of E, they can't
        if (Input.GetKeyUp(KeyCode.E)) {
            interacting = false;
        }
    }
}
