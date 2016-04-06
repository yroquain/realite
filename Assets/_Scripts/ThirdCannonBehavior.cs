using UnityEngine;
using System.Collections;

public class ThirdCannonBehavior : MonoBehaviour {

    public Transform m_cannonRot;
    public Transform m_muzzle;
    public GameObject m_shotPrefab;
    private GameObject Player;
    public float lastShot;
    public bool hasShot;
    public GameObject LastShoterRef;
    public GameObject NextShoterRef;
    private float dista;
    private float distb;
    private float distc;
    private Quaternion qto;

    // Use this for initialization
    void Start()
    {
        Player = GameObject.FindWithTag("Player");
        lastShot = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (PlayerPrefs.GetFloat("Area3") == 0)
        {
            if (Time.time > LastShoterRef.GetComponent<ThirdCannonBehavior>().lastShot + 0.5f && !hasShot)
            {
                if (Player.transform.position.x < -32 && LastShoterRef.GetComponent<ThirdCannonBehavior>().hasShot)
                {
                    GameObject go = GameObject.Instantiate(m_shotPrefab, m_muzzle.position, m_muzzle.rotation) as GameObject;
                    GameObject.Destroy(go, 3f);
                    lastShot = Time.time;
                    hasShot = true;
                    GetComponent<AudioSource>().Play();
                }
            }
            if (NextShoterRef.GetComponent<ThirdCannonBehavior>().hasShot)
            {
                hasShot = false;
            }
            dista = this.transform.position.z - Player.transform.position.z;
            distb = this.transform.position.x - Player.transform.position.x;
            distc = Mathf.Sqrt((dista * dista) + (distb * distb));
            if (dista > 0)
            {
                qto = Quaternion.Euler(0, -90 - Mathf.Acos(distb / distc) * Mathf.Rad2Deg, 0);
            }
            else
            {
                qto = Quaternion.Euler(0, -90 + Mathf.Acos(distb / distc) * Mathf.Rad2Deg, 0);
            }
            transform.position = new Vector3(transform.position.x, Player.transform.position.y+0.5f, transform.position.z);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, qto, Time.deltaTime * 100);
        }
        else
        {
            transform.position = new Vector3(transform.position.x, 4.07f, transform.position.z);            
        }
    }
}
