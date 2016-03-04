﻿using UnityEngine;
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

    // Use this for initialization
    void Start () 
	{
        Player=GameObject.FindWithTag("Player");
        lastShot = 0;
    }
	
	// Update is called once per frame
	void Update () 
	{
        if (Time.time > LastShoterRef.GetComponent<CannonBehavior>().lastShot + 1 && !hasShot)
        {
            if (Player.transform.position.z > 56 && LastShoterRef.GetComponent<CannonBehavior>().hasShot)
            {
                GameObject go = GameObject.Instantiate(m_shotPrefab, m_muzzle.position, m_muzzle.rotation) as GameObject;
                GameObject.Destroy(go, 3f);
                lastShot = Time.time;
                hasShot = true;
            }
        }
        if (NextShoterRef.GetComponent<CannonBehavior>().hasShot)
        {
            hasShot = false;
        }
	}
}
