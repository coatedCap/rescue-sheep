using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    public Enemy enemy;
    Inventory inventory;
    PlayerCollider pCollider;

    void OnTriggerStay(Collider other) {
        if (other.gameObject.CompareTag("Player")) {
            if (enemy.isAttacking == true) {
                inventory = other.gameObject.GetComponent<Inventory>();
                pCollider = other.gameObject.GetComponent<PlayerCollider>();
                if (pCollider.tangable == true) {
                    pCollider.tangable = false;
                    inventory.Hit();
                }
            } else {
                enemy.canAttack = true;
            }
        }
    }
}
