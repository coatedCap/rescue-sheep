using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Upgrade : MonoBehaviour
{
    int healthUpgradeCost = 5000; //Cost to upgrade health
    int staminaUpgradeCost = 4000; //Cost to upgrade stamina
    int dashUpgradeCost = 5000; //Cost to upgrade dash modifier
    int jumpUpgradeCost = 3000; //Cost to upgrade jump modifier
    public Button healthButton;
    public Button staminaButton;
    public Button dashButton;
    public Button jumpButton;
    public AudioSource selected;

    void Update()
    {
        // this must be altered if the cost of dash or health changes.
        if (Manager.Instance.exp < 5000)
        {
            healthButton.interactable = false;
            dashButton.interactable = false;
        }
        else
        {
            healthButton.interactable = true;
            dashButton.interactable = true;
        }
        if (Manager.Instance.exp < staminaUpgradeCost)
        {
            staminaButton.interactable = false;
        }
        else
        {
            staminaButton.interactable = true;
        }
        if (Manager.Instance.exp < jumpUpgradeCost)
        { 
            jumpButton.interactable = false;
        }

    }
    //Upgrades health by 1:
    public void HealthUpgrade() {
        if (Manager.Instance != null) {
            if (Manager.Instance.exp >= healthUpgradeCost)
            {
                selected.Play();
                Manager.Instance.maxHealth++;
                Manager.Instance.exp -= healthUpgradeCost;
            }
        }
    }

    //Upgrades max stamina by 10:
    public void StaminaUpgrade()
    {
        if (Manager.Instance != null) {
            if (Manager.Instance.exp >= staminaUpgradeCost)
            {
                selected.Play();
                Manager.Instance.maxStamina += 10.0f;
                Manager.Instance.exp -= staminaUpgradeCost;
            }
        }
    }

    //Upgrades dash modifier by 0.5:
    public void DashUpgrade()
    {
        if (Manager.Instance != null) {
            if (Manager.Instance.exp >= dashUpgradeCost)
            {
                selected.Play();
                Manager.Instance.dashModifier += 0.5f;
                Manager.Instance.exp -= dashUpgradeCost;
            }
        }
    }

    //Upgrades jump modifier by 20:
    public void JumpUpgrade()
    {
        if (Manager.Instance != null) {
            if (Manager.Instance.exp >= jumpUpgradeCost)
            {
                selected.Play();
                Manager.Instance.jumpModifier += 20.0f;
                Manager.Instance.exp -= jumpUpgradeCost;
            }
        }
    }
}
