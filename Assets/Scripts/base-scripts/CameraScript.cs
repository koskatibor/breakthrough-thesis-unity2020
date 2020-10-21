using UnityEngine;

public class CameraScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        this.transform.LookAt(CenterObject.transform);
    }

    public GameObject CenterObject;
    public Camera Kamera;
    public float Velocity = 2.0f;

		void Awake()
		{
				//QualitySettings.vSyncCount = 1;
				Application.targetFrameRate = 30;
		}

		// Update is called once per frame
		void Update()
    {				
				if (Input.GetKey(KeyCode.D))
        {
            //this.transform.LookAt(CenterObject.transform);
						transform.RotateAround(CenterObject.transform.position, Vector3.down, Velocity * Time.deltaTime);
						/*this.transform.Translate(Vector3.right * Time.deltaTime * Velocity);
            this.transform.LookAt(CenterObject.transform);*/
        }
				else
        if (Input.GetKey(KeyCode.A))
        {
            //this.transform.LookAt(CenterObject.transform);
						transform.RotateAround(CenterObject.transform.position, Vector3.up, Velocity * Time.deltaTime);
						/*this.transform.Translate(Vector3.left * Time.deltaTime * Velocity);
            this.transform.LookAt(CenterObject.transform);*/
        }

				if (Input.GetAxis("Mouse ScrollWheel") > 0f)
				{
						if (Kamera.fieldOfView > 25.0f)
								Kamera.fieldOfView -= Input.GetAxis("Mouse ScrollWheel") * 2.0f;

						
				}
				if (Input.GetAxis("Mouse ScrollWheel") < 0f)
				{
						if (Kamera.fieldOfView < 55.0f)
								Kamera.fieldOfView -= Input.GetAxis("Mouse ScrollWheel") * 2.0f;
				}
		}
}
