using UnityEngine;
using System.Collections;

public class SaberColor : MonoBehaviour
{
    public Material C_Yellow;
    public Material C_Blue;
    public Material C_Red;
    public Material C_Green;
    public Material C_Orange;
    public Material C_Magenta;
    public GameObject Light;
    // Use this for initialization
    void Start ()
    {
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
	void Update () {
    }
}
