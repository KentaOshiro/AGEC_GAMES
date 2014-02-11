using UnityEngine;
using System.Collections;
using Leap;

public class GrabMotion : MonoBehaviour
{

    public GameObject OriginalSphere;
    GameObject AddForceObject;
    Controller m_leapController;
    GameObject m_carriedObject;
    bool m_handOpenThisFrame = false;
    bool m_handOpenLastFrame = false;
    float m_fKeepGrabMotionTime = 0.0f;
    // Use this for initialization
    void Start()
    {
        m_leapController = new Controller();
        //UnityEngine.Screen.showCursor = false;
        //UnityEngine.Screen.lockCursor = true;
    }

    // gets the hand furthest away from the user (closest to the screen).
    Hand GetForeMostHand()
    {
        Frame f = m_leapController.Frame();
        Hand foremostHand = null;
        float zMax = -float.MaxValue;
        for (int i = 0; i < f.Hands.Count; ++i)
        {
            float palmZ = f.Hands[i].PalmPosition.ToUnityScaled().z;
            if (palmZ > zMax)
            {
                zMax = palmZ;
                foremostHand = f.Hands[i];
            }
        }

        return foremostHand;
    }

    // 手が開いている場合の処理.
    void OnHandOpen(Hand h)
    {
        if (AddForceObject == null)
            return;
        AddForceObject.rigidbody.AddForce(transform.forward * 1000.0f);
        m_carriedObject = null;
    }

    // 手が閉じている場合の処理.
    void OnHandClose(Hand h)
    {
        m_fKeepGrabMotionTime += Time.deltaTime;
        AddForceObject = Instantiate(OriginalSphere) as GameObject;
        AddForceObject.transform.position = transform.position + transform.forward;
        AddForceObject.transform.localScale = new Vector3(m_fKeepGrabMotionTime, m_fKeepGrabMotionTime, m_fKeepGrabMotionTime);
    }

    bool IsHandOpen(Hand h)
    {
        return h.Fingers.Count > 1;
    }

    // processes character camera look based on hand position.
    // 手の左右方向に合わせてカメラをY軸回転させる.
    void ProcessLook(Hand hand)
    {
        float rotThresholdX = 1.0f;
        float handX = hand.PalmPosition.ToUnityScaled().x;
        float handY = hand.PalmPosition.ToUnityScaled().y;
        Vector3 HandPosition = hand.PalmPosition.ToUnityScaled();
        Debug.Log(HandPosition.magnitude);
        if (Mathf.Abs(handX) > rotThresholdX)
        {
            Camera.main.transform.Rotate(Vector3.up, handX * 2.0f);
        }

        //if (Mathf.Abs(HandPosition.magnitude) < 10.0f)
        //{
        //    Camera.main.transform.rotation = Quaternion.Euler(new Vector3(-handY * 5.0f, handX * 50.0f, 0.0f));
        //}
    }


    // Determines if any of the hand open/close functions should be called.
    // 手が開いているか閉じているかに応じてのコールバック（反応）を定義.
    void HandCallbacks(Hand h)
    {
        if (m_handOpenThisFrame && m_handOpenLastFrame == false)
        {
            Debug.Log("Release Grab");
            OnHandOpen(h);
        }

        if (m_handOpenThisFrame == false && m_handOpenLastFrame == true)
        {
            Debug.Log("Is Grab");
            OnHandClose(h);
        }
    }

    void FixedUpdate()
    {
        Hand foremostHand = GetForeMostHand();
        if (foremostHand != null)
        {
            m_handOpenThisFrame = IsHandOpen(foremostHand);
            ProcessLook(foremostHand);
            HandCallbacks(foremostHand);
        }
        m_handOpenLastFrame = m_handOpenThisFrame;
    }
}
