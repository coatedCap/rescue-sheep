using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CompleteLevelDone : MonoBehaviour
{

    public PauseUIScript completeLevel;

    Inventory inventory;

    // Start is called before the first frame update
    public void Start()
    {
        completeLevel = GameObject.FindGameObjectWithTag("GameController").GetComponent<PauseUIScript>();
        inventory = GameObject.FindGameObjectWithTag("Player").GetComponent<Inventory>();
    }

    // Update is called once per frame
    public void Update()
    {
        
    }

    public void OnTriggerEnter(Collider other) {
        if (other.gameObject.CompareTag("Player")) {
            if (inventory.sheepRescued < 2) {
                completeLevel.NeedSheep();
            }
            else {
                completeLevel.LevelComplete2();
            }
            
        }
    }

    public void OnTriggerExit(Collider other) {
        if (other.gameObject.CompareTag("Player")) {
            completeLevel.GetSheep();
        }
    }
}
