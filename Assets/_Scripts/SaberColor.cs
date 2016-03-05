using UnityEngine;
using System.Collections;

public class SaberColor : MonoBehaviour
{
    private float horizontalspeed = 1f;
    private float verticalspeed = 1f;
    private GameObject Player;
    public Material C_Yellow;
    public Material C_Blue;
    public Material C_Red;
    public Material C_Green;
    public Material C_Orange;
    public Material C_Magenta;
    public GameObject Light;
    private float audiowait;
    // Use this for initialization
    void Start ()
    {
        audiowait = 0;
        Player = GameObject.FindWithTag("Player");
        if (PlayerPrefs.GetFloat("SaberColor") == 0)
        {
            GetComponent<Renderer>().material.color = C_Red.color;
            Light.GetComponent<Light>().color= C_Red.color;
        }
        if (PlayerPrefs.GetFloat("SaberColor") == 1)
        {
            GetComponent<Renderer>().material.color = C_Blue.color;
            Light.GetComponent<Light>().color = C_Blue.color;
        }
        if (PlayerPrefs.GetFloat("SaberColor") == 2)
        {
            GetComponent<Renderer>().material.color = C_Green.color;
            Light.GetComponent<Light>().color = C_Green.color;
        }
        if (PlayerPrefs.GetFloat("SaberColor") == 3)
        {
            GetComponent<Renderer>().material.color = C_Yellow.color;
            Light.GetComponent<Light>().color = C_Yellow.color;
        }
        if (PlayerPrefs.GetFloat("SaberColor") == 4)
        {
            GetComponent<Renderer>().material.color = C_Orange.color;
            Light.GetComponent<Light>().color = C_Orange.color;
        }
        if (PlayerPrefs.GetFloat("SaberColor") == 5)
        {
            GetComponent<Renderer>().material.color = C_Magenta.color;
            Light.GetComponent<Light>().color = C_Magenta.color;
        }

    }
	
	// Update is called once per frame
	void Update ()
    {
        float h = horizontalspeed * Input.GetAxis("Mouse X");
        float v = verticalspeed * Input.GetAxis("Mouse Y");
        transform.Rotate(v, h, 0);
        if((v > 2 || h>2 || v<-2 || h<-2) && Time.time> audiowait+1f && Time.time>Player.GetComponent<PlayerController>().waitTime+1f)
        {
            GetComponent<AudioSource>().Play();
            audiowait = Time.time;
        }
    }
    
    void OnTriggerEnter(Collider coll)
    {
        if(coll.tag=="Shot")
        {
            Destroy(coll.gameObject);
        }
    }
}
