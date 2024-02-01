using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestCamera : MonoBehaviour
{
    public int speed;

    private float horiz = 0;
    private float vert = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
         // handle movement, get keys
        if (Input.GetKey(KeyCode.W)) {
            vert = 1;
        }
        if (Input.GetKey(KeyCode.S)) {
            vert = -1;
        }
        if (Input.GetKey(KeyCode.A)) {
            horiz = -1;
        }
        if (Input.GetKey(KeyCode.D)) {
            horiz = 1;
        }

        // move the boi
        Vector3 movement = new Vector3(horiz, 0f, vert);
        transform.Translate(movement*Time.deltaTime*speed);

        // handle restarting / go to start
        // if(Input.GetKey(KeyCode.R)) {
        //     transform.position = startPosition;
        // }

        horiz = 0;
        vert = 0; 
    }
}
