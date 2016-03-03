using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

    private float movementSpeed = 3.0f;
    public GameObject PauseCanvas;
    //Rotation
    private float speed = 50.0f;
    private float rotate;
    private Quaternion qTo = Quaternion.Euler(0.0f, 0.0f, 0.0f);


    void Start()
    {
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //Reset
        GetComponent<Rigidbody>().velocity = new Vector3(GetComponent<Rigidbody>().velocity.x, GetComponent<Rigidbody>().velocity.y, 0); //Set X and Z velocity to 0
        
        //When Moving
        if (Input.GetAxis("Vertical") != 0)
        {
            
                transform.Translate(0, 0, Input.GetAxis("Vertical") * Time.deltaTime * movementSpeed);
               
            
        }
        

        //When Jumping
        if (GetComponent<Rigidbody>().velocity == new Vector3(0, 0, 0) && Input.GetKeyDown("space"))
        {
            GetComponent<Rigidbody>().AddForce(new Vector3(0, 400, 0), ForceMode.Force);
        }
        

        //Rotating
        if (Input.GetAxis("Horizontal") != 0 )
        {
            rotate += Input.GetAxis("Horizontal");
            qTo = Quaternion.Euler(0.0f, rotate , 0.0f);
        }

        transform.rotation = Quaternion.RotateTowards(transform.rotation, qTo, Time.deltaTime * speed);
        if (Input.GetKey(KeyCode.Escape))
        {
            PauseCanvas.SetActive(true);
            Time.timeScale = 0;
        }


    }
}


