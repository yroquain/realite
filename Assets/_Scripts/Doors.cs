using UnityEngine;
using System.Collections;

public class Doors : MonoBehaviour {


    public GameObject [] Door;
    private bool IsMoving;
    private int Key;
    // Use this for initialization
    void Start()
    {
        Door[0].SetActive(true);
        Door[1].SetActive(true);
        Door[2].SetActive(true);
    }
	
	// Update is called once per frame
	void Update () {
        if(IsMoving)
        {
            float step = 5 * Time.deltaTime;
            Door[Key].transform.position = Vector3.MoveTowards(Door[Key].transform.position, new Vector3(Door[Key].transform.position.x, 200, Door[Key].transform.position.z), step);

        }
	
	}

    public void OnTriggerEnter(Collider Coll)
    {
        if (Coll.gameObject.name == "Player")
        {
            Key = (int)PlayerPrefs.GetFloat("Key") - 1;
            if (Key >= 0)
            {
                IsMoving = true;
            }
        }
    }
    public void OnTriggerExit(Collider Coll)
    {
        if(Coll.gameObject.name=="Player")
        {
            IsMoving = false;
        }
    }
}
