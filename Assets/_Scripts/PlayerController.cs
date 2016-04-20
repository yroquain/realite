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

	private float rotx;
	private float roty;
	private float rotz;

    //Health
    private int HP;
    public GameObject[] Health;
    public GameObject[] Health2;
    public GameObject HealthJ;
    public GameObject HealthS;
    public bool IsDead;

    private float movementSpeed = 10.0f;

	//Wiimote & mouse controller
	public static Wiimote wiimote =null;
	private float horizontalspeed = 2f;
	private float verticalspeed = 2f;
	private float audiowait;
	public AudioClip movesound;
	public Quaternion initialRotation;


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
        PlayerPrefs.SetFloat("Area3", 0);
        PlayerPrefs.SetFloat("Key", 0);

		#endregion
		rotx = transform.position.x + lightsaber.transform.position.x;
		roty = transform.position.y + lightsaber.transform.position.y;
		rotz = transform.position.z + lightsaber.transform.position.z;
        IsDead = false;
        if (PlayerPrefs.GetFloat("Type") == 0)
        {
            HealthJ.SetActive(false);
        }
        if (PlayerPrefs.GetFloat("Type") == 1)
        {
            HealthS.SetActive(false);
        }
        HP = 3;
        IsLightsaberActive = false;
        DeathCanvas.SetActive(false);
		waitTime = 0;


		WiimoteManager.FindWiimotes();
		if (WiimoteManager.HasWiimote ()) {
			wiimote = WiimoteManager.Wiimotes [0];
			wiimote.RequestIdentifyWiiMotionPlus();
			wiimote.SendDataReportMode(InputDataType.REPORT_BUTTONS_ACCEL);
			wiimote.Accel.CalibrateAccel(AccelCalibrationStep.A_BUTTON_UP);
		}

		initialRotation = lightsaber.transform.localRotation;
    }

	void OnApplicationQuit() {
		if (wiimote != null) {
			WiimoteManager.Cleanup(wiimote);
			wiimote = null;
		}
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

		//Wiimote 
		if (wiimote != null) {
			int ret = 0;
			do
			{
				ret = wiimote.ReadWiimoteData();
			} while (ret > 0);
		}


        //When Moving
        if (!IsDead)
        {
            if (wiimote != null && (wiimote.Button.d_up || wiimote.Button.d_down))
            {
                transform.Translate(0, 0, (wiimote.Button.d_up ? 1 : -1) * Time.deltaTime * movementSpeed);
            }
            else if (Input.GetAxis("Vertical") != 0)
            {
                transform.Translate(0, 0, Input.GetAxis("Vertical") * Time.deltaTime * movementSpeed);
            }


            //When Jumping
            if (GetComponent<Rigidbody>().velocity == new Vector3(0, 0, 0) && (Input.GetKeyDown("space") || (wiimote != null && wiimote.Button.b)))
            {
                GetComponent<Rigidbody>().AddForce(new Vector3(0, 400, 0), ForceMode.Force);
            }


            //Rotating
            if (wiimote != null && (wiimote.Button.d_left || wiimote.Button.d_right))
            {
                rotate += (wiimote.Button.d_right ? 1 : -1);
                qTo = Quaternion.Euler(0.0f, rotate, 0.0f);
            }
            else if (Input.GetAxis("Horizontal") != 0)
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

            if ((Input.GetKeyDown(KeyCode.E) || (wiimote != null && wiimote.Button.one)) && Time.time > waitTime + 0.3f)
            {
                toggleSaber();

            }

            float h = horizontalspeed * Input.GetAxis("Mouse X");
            float v = verticalspeed * Input.GetAxis("Mouse Y");
            v = -v;
			if (wiimote == null && Time.timeScale != 0) {
				lightsaber.transform.Rotate (v, h, 0);
			} else {
				Vector3 accel = GetAccelVector ();
				Debug.Log ("x" + accel.x);
				Debug.Log ("y" + accel.y);
				Debug.Log ("z" + accel.z);
				accel.x = accel.x ;
				accel.y = accel.y ;
				accel.z = accel.z ;


				float h1 = Mathf.Sqrt (accel.x * accel.x + accel.z * accel.z);
				float ry = Mathf.Atan (accel.y / h1);
				float h2 = Mathf.Sqrt (accel.x * accel.x + accel.y * accel.y);
				float rz = Mathf.Atan (h2 / accel.z);
				float h3 = Mathf.Sqrt (accel.y * accel.y + accel.z * accel.z);
				float rx = Mathf.Atan (h3 / accel.x);

				lightsaber.transform.localRotation = Quaternion.Euler (rx* (rotx)*45, ry* (roty)*45, rz* (rotz)*45);
			}
            if ((v > 2 || h > 2 || v < -2 || h < -2) && Time.time > audiowait + 1f && Time.time > waitTime + 1f)
            {
                AudioSource.PlayClipAtPoint(movesound, transform.position, 0.2f);
                audiowait = Time.time;
            }


            if (transform.position.z > 143)
            {
                PlayerPrefs.SetFloat("Area1", 1);
            }
            if (transform.position.x > 115)
            {
                PlayerPrefs.SetFloat("Area2", 1);
            }
            if(transform.position.x<-110)
            {
                PlayerPrefs.SetFloat("Area3", 1);
            }
        }
    }

	void OnDrawGizmos(){
		if (wiimote != null) {
			Gizmos.color = Color.red;
			Gizmos.DrawLine(lightsaber.transform.position, lightsaber.transform.position + initialRotation*GetAccelVector()*2);
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
                IsDead = true;
                DeathCanvas.SetActive(true);
                Time.timeScale = 0;
				if (wiimote != null) {
					WiimoteManager.Cleanup(wiimote);
					wiimote = null;
				}
            }
        }
    }
	private Vector3 GetAccelVector()
	{
		float accel_x;
		float accel_y;
		float accel_z;

		float[] accel = wiimote.Accel.GetCalibratedAccelData();
		accel_x = accel[0];
		accel_y = accel[1];
		accel_z = accel[2];

		return new Vector3(accel_x, accel_y, accel_z).normalized;
	}
}


