using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    public Transform[] patrolPoints; //Patrol Points
    int destPoint = 0; //Index of current destination in above list
    public int health = 3; //Enemy health
    public GameObject player; //Player object
    public GameObject attacker;
    Inventory inventory;
    PlayerCollider pCollider;
    UnityEngine.AI.NavMeshAgent agent; //Navmesh agent
    Rigidbody wolf;
    bool stalking = false; //Hunting player?
    int limit = 10; //Sight limit
    float enemySpeed; //Stores speed value
    public bool isAttacking = false; //Enemy is currently attacking
    public bool canAttack = false; //Player entered attack range

    public AudioClip[] walkingGrass;
    public AudioClip[] walkingWood;
    public AudioClip[] walkingStone;

    public AudioSource growl;
    public AudioSource hurt;
    public AudioSource death;
    public AudioSource warning;

    public bool onGrass = true;
    public bool onWood;
    public bool onStone;

    bool foundPlayer = false;

    Animator wolfAnim;

    void Start() {
        agent = this.GetComponent<UnityEngine.AI.NavMeshAgent>();
        wolfAnim = this.GetComponent<Animator>();
        inventory = player.GetComponent<Inventory>();
        pCollider = player.GetComponent<PlayerCollider>();
        wolf = this.GetComponent<Rigidbody>();
        Patrol();
    }

    void Update() {
        wolfAnim.SetFloat("Speed", agent.velocity.sqrMagnitude);
        //If they can see the player, then hunt them
        if (stalking == true){
            if (!foundPlayer) {
                //GetComponent<AudioSource>().PlayOneShot(growl);
                growl.Play();
                foundPlayer = true;
            }
            Hunt();   
        } //else {
            //Patrol();
        //}

        //Otherwise, if the agent isn't already following a path and has less than
        //0.5 units to their destination, pick a new path.
        else if (!agent.pathPending && agent.remainingDistance < 0.5f) {
            Patrol();
        }


        //If they run out of health, deactivate them
        if (health <= 0) {
            //Insert death animation here w/ despawn trigger
            wolfAnim.SetBool("Dead", true);
        }

        if (canAttack == true) {
            wolfAnim.SetBool("WindUp", true);
            canAttack = false;
        }
    }

    //If hunting the player, set their destination as your next destination
    void Hunt() {
        agent.destination = player.transform.position;
    }

    //Sets next destination for patroller
    void Patrol() {
        //If there aren't any patrol points: stop.
        if (patrolPoints.Length == 0) {
            return;
        }

        //Sets agent's next destination.
        agent.destination = patrolPoints[destPoint].position;
        wolfAnim.SetFloat("Speed", agent.velocity.sqrMagnitude);
        //Sets next destination point, or cycles back to start.
        destPoint = (destPoint + 1) % (patrolPoints.Length);

        /*
        //Sets agent's next destination.
        if (!agent.pathPending)
        {
            if (agent.remainingDistance <= agent.stoppingDistance)
            {
                if (!agent.hasPath || agent.velocity.sqrMagnitude == 0f)
                {
                    // Done
                    agent.destination = patrolPoints[destPoint].position;
                    //Sets next destination point, or cycles back to start.
                    destPoint = (destPoint + 1) % (patrolPoints.Length);
                }
            }
        }
        */
    }

    //On command from animation, the wolf will dissapear.
    void Death() {
        inventory.WolfDefeated();
        this.gameObject.SetActive(false);
    }

    //Upon being hit, loose one health
    public void Hit() {
        health--;
        //GetComponent<AudioSource>().PlayOneShot(hurt);
        if (health >= 1) {
            hurt.Play();
            warning.Stop();
            wolfAnim.SetBool("Hit", true);
            //wolf.AddForce(transform.forward * knockback * -1.0f, ForceMode.Impulse);
            //wolf.AddForce(transform.up * knockback, ForceMode.Impulse);
        }
        if (health == 0) {
            warning.Stop();
            death.Play();
        }
    }

    public void WindUpComplete() {
        wolfAnim.SetBool("WindUp", false);
    }

    public void WindUp() {
        agent.destination = this.transform.position;
        enemySpeed = agent.speed;
        agent.speed = 0.0f;
        warning.Play();
    }

    public void Attack() {
        isAttacking = true;
        agent.speed = enemySpeed;
    }

    public void AttackComplete() {
        isAttacking = false;
        wolfAnim.SetBool("IsAttacking", false);
    }

     public void HitEffectOff() {
        wolfAnim.SetBool("Hit", false);
    }

    void OnTriggerStay(Collider other) {
        //If player is in trigger
        if (other.gameObject.CompareTag("Player")) {
            //When player enters sight trigger, raycast to see if they're visible
            RaycastHit hit;
            Vector3 aiToPlayer = player.transform.position - this.transform.position;

            //If player is in sight range, follow them:
            if (Physics.Raycast(this.transform.position + transform.up, aiToPlayer.normalized, out hit, limit)) {
                stalking = true;
            }
        }
    }

    void OnTriggerExit(Collider other) {
        //If player leaves sight, set stalking to false
        if (other.gameObject.CompareTag("Player")) {
            stalking = false;
            foundPlayer = false;
        }
    }

    public void OnCollisionEnter(Collision other) {
        if (other.gameObject.CompareTag("Floor")) {
            onGrass = true;
        }
    }

    //SFX for walking on surfaces.
    public void WolfWalkingGrass() {
        if (onGrass) {
            System.Random rand = new System.Random();
            int stepSound = rand.Next(0, walkingGrass.Length - 1);

            GetComponent<AudioSource>().PlayOneShot(walkingGrass[stepSound]);
        }
    }

    public void WolfWalkingWood() {
        if (onWood) {
            System.Random rand = new System.Random();
            int stepSound = rand.Next(0, walkingWood.Length - 1);

            GetComponent<AudioSource>().PlayOneShot(walkingWood[stepSound]);
        }
    }

    public void WolfWalkingStone() {
        if (onStone) {
            System.Random rand = new System.Random();
            int stepSound = rand.Next(0, walkingStone.Length - 1);

            GetComponent<AudioSource>().PlayOneShot(walkingStone[stepSound]);
        }
    }

}
