using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manager : MonoBehaviour
{
    public static Manager Instance; //This instance of the manager
    public int maxHealth = 5; //Upgraded health
    public float maxStamina = 100f; //Upgraded stamina
    public float dashModifier = 5f; //Upgraded dash mod
    public float jumpModifier = 300f; //Upgraded jump mod
    public int exp = 0; //Total experience (score) earned
    public int sceneIndex = 1;

    private void Awake() {

        //If an instance of this object already exists, destroy this object:
        //This way, only one instance of the manager will exist
        if (Instance != null) {
            Destroy(gameObject);
            return;
        }

        //Otherwise, set the instance:
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }
}
