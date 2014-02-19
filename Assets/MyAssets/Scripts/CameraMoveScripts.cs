using UnityEngine;
using System.Collections;

public class CameraMoveScripts : MonoBehaviour {

    public GameObject OriginalSphere;
    public int m_iGrabCount = 3;

    private GameObject AddForceObject;
    private bool m_bIsGrab;
    private float m_fKeepGrabMotionTime = 0.0f;

	// Use this for initialization
	void Start () {
        ObjectInit();
	}
	
	// Update is called once per frame
	void Update () {

        transform.Rotate(new Vector3(0, Input.GetAxis("Horizontal"), 0));

        if (Input.GetKey(KeyCode.Space) && m_iGrabCount == 2)
            KeepGrabTimeToScale();
        if (Input.GetKeyUp(KeyCode.Space))
            Shot();

	}

    void Shot()
    {
        if (AddForceObject == null)
            return;
        AddForceObject.rigidbody.AddForce(transform.forward * 1000.0f);
        m_bIsGrab = false;
    }

    void ObjectInit()
    {
        m_bIsGrab = true;
        m_iGrabCount--;
        AddForceObject = Instantiate(OriginalSphere) as GameObject;
        AddForceObject.transform.localScale = Vector3.zero;
        m_fKeepGrabMotionTime = 0.0f;
        AddForceObject.transform.position = transform.position + (transform.forward * 2);
    }

    void KeepGrabTimeToScale()
    {
        if (!m_bIsGrab)
        {
            ObjectInit();
            return;
        }
            
        m_fKeepGrabMotionTime += Time.deltaTime;
        AddForceObject.transform.localScale = new Vector3(m_fKeepGrabMotionTime, m_fKeepGrabMotionTime, m_fKeepGrabMotionTime);
    }
}
