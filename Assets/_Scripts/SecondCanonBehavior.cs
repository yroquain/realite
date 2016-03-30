using UnityEngine;
using System.Collections;

public class SecondCanonBehavior : MonoBehaviour {

    public Transform m_cannonRot;
    public Transform m_muzzle;
    public GameObject m_shotPrefab;
    private GameObject Player;
    public float lastShot;
    public bool hasShot;
    public GameObject LastShoterRef;
    public GameObject NextShoterRef;

    // Use this for initialization
    void Start()
    {
        Player = GameObject.FindWithTag("Player");
        lastShot = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (PlayerPrefs.GetFloat("Area2") == 0)
        {
            if (Time.time > LastShoterRef.GetComponent<SecondCanonBehavior>().lastShot + 0.2f && !hasShot)
            {
                if (Player.transform.position.x > 40 && LastShoterRef.GetComponent<SecondCanonBehavior>().hasShot && PlayerPrefs.GetFloat("Key")>0)
                {
                    GameObject go = GameObject.Instantiate(m_shotPrefab, m_muzzle.position, m_muzzle.rotation) as GameObject;
                    GameObject.Destroy(go, 3f);
                    lastShot = Time.time;
                    hasShot = true;
                    GetComponent<AudioSource>().Play();
                }
            }
            if (Time.time > LastShoterRef.GetComponent<SecondCanonBehavior>().lastShot  && !hasShot && gameObject.name== "Cannon (10)")
            {
                if (Player.transform.position.x > 40 && LastShoterRef.GetComponent<SecondCanonBehavior>().hasShot && PlayerPrefs.GetFloat("Key") > 0)
                {
                    GameObject go = GameObject.Instantiate(m_shotPrefab, m_muzzle.position, m_muzzle.rotation) as GameObject;
                    GameObject.Destroy(go, 3f);
                    lastShot = Time.time;
                    hasShot = true;
                    GetComponent<AudioSource>().Play();
                }
            }
            if (NextShoterRef.GetComponent<SecondCanonBehavior>().hasShot)
            {
                hasShot = false;
            }
        }
    }
}

