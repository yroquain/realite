using UnityEngine;
using System.Collections;

public class PlateformeCorridor3 : MonoBehaviour {

    public GameObject Ref;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	    if(PlayerPrefs.GetFloat("Area3")==0)
        {
            transform.position = new Vector3(transform.position.x, Ref.transform.position.y-1f, transform.position.z);
        }
        else
        {
            transform.position = new Vector3(transform.position.x, 2.59994f, transform.position.z);
        }
	}
}
