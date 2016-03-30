using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class KeyTrigger : MonoBehaviour {


    public GameObject MyText;
    public int NumberKey;
    public Sprite Key;
    public GameObject KeyImage;
    private bool IsActivated;
	// Use this for initialization
	void Start ()
    {
        IsActivated = true;
        MyText.SetActive(false);
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    public void OnTriggerEnter(Collider collision)
    {
        if (IsActivated)
        {
            if (collision.gameObject.name == "Player")
            {
                MyText.SetActive(true);
				if (Input.GetKeyDown(KeyCode.A) || (PlayerController.wiimote !=null && PlayerController.wiimote.Button.a))
                {
                    if (PlayerPrefs.GetFloat("Key") < NumberKey)
                    {
                        PlayerPrefs.SetFloat("Key", NumberKey);
                        MyText.SetActive(false);
                        KeyImage.GetComponent<Image>().sprite = Key;
                        IsActivated = false;
                    }
                }
            }
        }
    }
    public void OnTriggerStay(Collider collision)
    {
        if (IsActivated)
        {
            if (collision.gameObject.name == "Player")
            {
                MyText.SetActive(true);
				if (Input.GetKeyDown(KeyCode.A) || (PlayerController.wiimote !=null && PlayerController.wiimote.Button.a))
                {
                    if (PlayerPrefs.GetFloat("Key") < NumberKey)
                    {
                        PlayerPrefs.SetFloat("Key", NumberKey);
                        MyText.SetActive(false);
                        KeyImage.GetComponent<Image>().sprite = Key;
                        IsActivated = false;
                    }
                }
            }
        }
    }
    void OnTriggerExit(Collider collision)
    {
        MyText.SetActive(false);
    }
}
