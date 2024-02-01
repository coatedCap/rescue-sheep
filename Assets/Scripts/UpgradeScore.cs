using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UpgradeScore : MonoBehaviour
{
    public TextMeshProUGUI score;
    public TextMeshProUGUI health;
    public TextMeshProUGUI stamina;
    public TextMeshProUGUI dashpow;
    public TextMeshProUGUI jumppow;


    // Update is called once per frame
    void Update()
    {
        score.text = "EXP: " + Manager.Instance.exp;
        health.text = "Max Health: " + Manager.Instance.maxHealth;
        stamina.text = "Max Stamina: " + Manager.Instance.maxStamina;
        dashpow.text = "Dash Power: " + Manager.Instance.dashModifier;
        jumppow.text = "Jump Power: " + Manager.Instance.jumpModifier;
    }
}
