using UnityEngine;
using System.Collections;
using WiimoteApi;

public class PlayerController : MonoBehaviour
{
    //Canvas
    public GameObject PauseCanvas;
    public GameObject DeathCanvas;

    //Rotation
    private float speed = 50.0f;
    private float rotate;
    private Quaternion qTo = Quaternion.Euler(0.0f, 0.0f, 0.0f);

    //Light Saber
    private bool IsLightsaberActive;
    public GameObject lightsaber;
    public float waitTime;
    public AudioClip opensound;
    public AudioClip closesound;

    //Health
    private int HP;
    public GameObject[] Health;
    public GameObject[] Health2;
    public GameObject HealthJ;
    public GameObject HealthS;

    private float movementSpeed = 10.0f;

	//Wiimote & mouse controller
	private Wiimote wiimote;
	private float horizontalspeed = 2f;
	private float verticalspeed = 2f;
	private float audiowait;
	public AudioClip movesound;


	void toggleSaber(){
		waitTime = Time.time;
		if (IsLightsaberActive)
		{
			AudioSource.PlayClipAtPoint(closesound, transform.position, 0.1f);
			lightsaber.SetActive(false);
		}
		else if (!IsLightsaberActive)
		{
			AudioSource.PlayClipAtPoint(opensound, transform.position, 0.1f);
			lightsaber.SetActive(true);
		}
		IsLightsaberActive = !IsLightsaberActive;
	}

    void Start()
    {
        #region a_supprimer_avant_build_final

        PlayerPrefs.SetFloat("Area1", 0);
        PlayerPrefs.SetFloat("Area2", 0);
        PlayerPrefs.SetFloat("Key", 0);

        #endregion
        if (PlayerPrefs.GetFloat("Type") == 0)
        {
            HealthJ.SetActive(false);
        }
        if (PlayerPrefs.GetFloat("Type") == 1)
        {
            HealthS.SetActive(false);
        }
        HP = 3;
        IsLightsaberActive = true;
        DeathCanvas.SetActive(false);
        waitTime = 0;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //Death 
        if(transform.position.y<-5)
        {
            DeathCanvas.SetActive(true);
            Time.timeScale = 0;
        }
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
        if (Input.GetAxis("Horizontal") != 0)
        {
            rotate += Input.GetAxis("Horizontal");
            qTo = Quaternion.Euler(0.0f, rotate, 0.0f);
        }

        transform.rotation = Quaternion.RotateTowards(transform.rotation, qTo, Time.deltaTime * speed);
        if (Input.GetKey(KeyCode.Escape))
        {
            PauseCanvas.SetActive(true);
            Time.timeScale = 0;
        }

        if (Input.GetKeyDown(KeyCode.E) && Time.time > waitTime + 0.3f)
        {
			toggleSaber ();

        }

		float h = horizontalspeed * Input.GetAxis("Mouse X");
		float v = verticalspeed * Input.GetAxis("Mouse Y");
		v = -v;
		if (Time.timeScale != 0)
		{
			lightsaber.transform.Rotate(v, h, 0);
		}
		if((v > 2 || h>2 || v<-2 || h<-2) && Time.time> audiowait+1f && Time.time>waitTime+1f)
		{
			AudioSource.PlayClipAtPoint(movesound, transform.position, 0.2f);
			audiowait = Time.time;
		}


        if(transform.position.z> 143)
        {
            PlayerPrefs.SetFloat("Area1", 1);
        }
        if(transform.position.x>115)
        {
            PlayerPrefs.SetFloat("Area2", 1);
        }

		//Wiimote 
		if (!WiimoteManager.HasWiimote()) {
			return;
		}
		wiimote = WiimoteManager.Wiimotes[0];


		int ret = 0;
		do
		{
			ret = wiimote.ReadWiimoteData();
		} while (ret > 0);				


		if (wiimote.Button.b && Time.time > waitTime + 0.3f)
		{
			toggleSaber ();
		}
    }
    void OnCollisionEnter(Collision coll)
    {
        if (coll.gameObject.tag == "Shot")
        {
            if (HP > 0)
            {
                HP = HP - 1;
                if (PlayerPrefs.GetFloat("Type") == 0)
                {
                    Health[HP].SetActive(false);
                }
                if (PlayerPrefs.GetFloat("Type") == 1)
                {
                    Health2[HP].SetActive(false);
                }

            }
            if(HP==0)
            {
                DeathCanvas.SetActive(true);
                Time.timeScale = 0;
            }
        }
    }
}


