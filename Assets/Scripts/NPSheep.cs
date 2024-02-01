using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPSheep : MonoBehaviour
{
    public Transform[] patrolPoints; //Patrol Points
    public GameObject player; //Player Object
    public bool rescued = false; //Has sheep been saved?
    int destPoint = 0; //Index of current destination in above list
    bool following = false; //Following player?
    int limit = 10; //Sight limit
    UnityEngine.AI.NavMeshAgent agent; //NavMesh Agent

    public AudioClip[] walkingGrass;

    public AudioClip found;
    bool isFound = false;

    Animator sheepAnim;

    
    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        agent.autoBraking = false; //Makes sure patroller doesn't slow down at patrol points
        sheepAnim = this.GetComponent<Animator>();
        Patrol();
    }

    //Sets next destination for patroller
    void Patrol() {
        //If there aren't any patrol points: stop.
        if (patrolPoints.Length == 0) {
            return;
        }
        //Sets agent's next destination.
        agent.destination = patrolPoints[destPoint].position;
        sheepAnim.SetFloat("Speed", agent.velocity.sqrMagnitude);
        //Sets next destination point, or cycles back to start.
        destPoint = (destPoint + 1) % (patrolPoints.Length);
    }

    //If following the player, set their destination to be yours.
    void Follow() {
        if (!isFound) {
            GetComponent<AudioSource>().PlayOneShot(found);
            isFound = true;
        }
        
        agent.destination = player.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        sheepAnim.SetFloat("Speed", agent.velocity.sqrMagnitude);
        //If following is true, follow the player
        if (following == true) {
            Follow();

        //Otherwise, if the agent isn't already following a path and has less than
        //0.5 units to their destination, pick a new path.
        } else if (!agent.pathPending && agent.remainingDistance < 0.5f) {
            Patrol();
        }

    }

    void OnTriggerStay(Collider other) {
        //If player enters trigger, raycast to see if they are visible
        if (other.gameObject.CompareTag("Player")) {
            RaycastHit hit;
            Vector3 aiToPlayer = player.transform.position - this.transform.position;

            if (Physics.Raycast(this.transform.position + transform.up, aiToPlayer.normalized, out hit, limit)) {
                //If they are, follow them by setting follow to true
                following = true;
            }
        }
    }

    void OnTriggerExit(Collider other) {
        //If player leaves trigger, set follow to false
        if (other.gameObject.CompareTag("Player")) {
            following = false;
        }
    }

    public void WalkingGrass() {
        System.Random rand = new System.Random();
        int stepSound = rand.Next(0, walkingGrass.Length - 1);

        GetComponent<AudioSource>().PlayOneShot(walkingGrass[stepSound]);
    }

}
