using UnityEngine;
using System.Collections;

public class CannonBehavior : MonoBehaviour {

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
    void Start () 
	{
        Player=GameObject.FindWithTag("Player");
        lastShot = 0;
    }
	
	// Update is called once per frame
	void Update () 
	{
        if (PlayerPrefs.GetFloat("Area1") == 0)
        {
            if (Time.time > LastShoterRef.GetComponent<CannonBehavior>().lastShot + 1 && !hasShot)
            {
                if (Player.transform.position.z > 56 && LastShoterRef.GetComponent<CannonBehavior>().hasShot)
                {
                    GameObject go = GameObject.Instantiate(m_shotPrefab, m_muzzle.position, m_muzzle.rotation) as GameObject;
                    GameObject.Destroy(go, 3f);
                    lastShot = Time.time;
                    hasShot = true;
                    GetComponent<AudioSource>().Play();
                }
            }
            if (NextShoterRef.GetComponent<CannonBehavior>().hasShot)
            {
                hasShot = false;
            }
            dista = this.transform.position.z - Player.transform.position.z;
            distb = this.transform.position.x - Player.transform.position.x;
            distc = Mathf.Sqrt((dista * dista) + (distb * distb));
            if (distb > 0)
            {
                qto = Quaternion.Euler(0, 180 + Mathf.Acos(dista / distc) * Mathf.Rad2Deg, 0);
            }
            else
            {
                qto = Quaternion.Euler(0, 180 - Mathf.Acos(dista / distc) * Mathf.Rad2Deg, 0);
            }
            transform.rotation = Quaternion.RotateTowards(transform.rotation, qto, Time.deltaTime * 100);
        }
    }
}
