using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    Animator sheepAnim;
    Enemy target;
    bool isAttacking = false;
    bool swinging = false;

    public AudioClip swing;
    public AudioClip sheepNoise;

    //public AudioClip hit;

    // Start is called before the first frame update
    void Start()
    {
        sheepAnim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        //Attack animation:
        if (Input.GetKeyDown(KeyCode.V) ) {
            isAttacking = true;
            sheepAnim.SetBool("IsAttacking", isAttacking);
        }
        if (Input.GetKeyUp(KeyCode.V)) {
            isAttacking = false;
            sheepAnim.SetBool("IsAttacking", isAttacking);
        }
    }

    //If sword can hit target;
    public void Swinging() {
        swinging = true;
        GetComponent<AudioSource>().PlayOneShot(swing, 1.0f);
        
    }

    public void SwingNoise() {
        GetComponent<AudioSource>().PlayOneShot(sheepNoise);
    }

    //If sword can no longer hit target
    public void StopSwinging() {
        swinging = false;
    }

    void OnTriggerEnter(Collider other) {
        //Upon player or sword's collision with enemy, if the sword is swinging:
        if (other.gameObject.CompareTag("Enemy") && swinging == true) {
            //Target is hit
            target = other.gameObject.GetComponent<Enemy>();
            target.Hit();
        }
    }
}
