using UnityEngine;
using System.Collections;
using Leap;

public class GrabMotion : MonoBehaviour
{

    public GameObject OriginalSphere;
    GameObject AddForceObject;
    Controller m_leapController;
    float m_lastBlastTime = 0.0f;

    GameObject m_carriedObject;
    bool m_handOpenThisFrame = false;
    bool m_handOpenLastFrame = false;

    // Use this for initialization
    void Start()
    {
        m_leapController = new Controller();
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
        AddForceObject = Instantiate(OriginalSphere) as GameObject;
        AddForceObject.transform.position = transform.position + transform.forward;
        
        // look for an object to pick up.
        // Rayを飛ばしHitしたオブジェクトを「移動させるオブジェクト」として記録する
        //RaycastHit hit;
        //if (Physics.SphereCast(new Ray(transform.position + transform.forward * 2.0f, transform.forward), 2.0f, out hit))
        //{
        //    m_carriedObject = hit.collider.gameObject;
        //}
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
        float rotThresholdY = 3.0f;
        float handX = hand.PalmPosition.ToUnityScaled().x;
        float handY = hand.PalmPosition.ToUnityScaled().y;
        //Debug.Log(Mathf.Abs(handY));
        //Debug.Log(handY);
        if (Mathf.Abs(handX) > rotThresholdX)
        {
            Camera.main.transform.Rotate(Vector3.up, handX * 2.0f);
        }
    }

    void MoveCharacter(Hand hand)
    {
        if (hand.PalmPosition.ToUnityScaled().z > 0)
        {
            Camera.main.transform.position += Camera.main.transform.forward * 0.1f;
        }

        if (hand.PalmPosition.ToUnityScaled().z < -1.0f)
        {
            Camera.main.transform.position -= Camera.main.transform.forward * 0.04f;
        }
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

    // if we're carrying an object, perform the logic needed to move the object
    // with us as we walk (or pull it toward us if it's far away).
    // 掴んだオブジェクトを移動（運ぶ）させる.
    void MoveCarriedObject()
    {
        if (m_carriedObject != null)
        {
            Vector3 targetPos = transform.position + new Vector3(transform.forward.x, 0, transform.forward.z) * 5.0f;
            Vector3 deltaVec = targetPos - m_carriedObject.transform.position;
            if (deltaVec.magnitude > 0.1f)
            {
                m_carriedObject.rigidbody.velocity = (deltaVec) * 10.0f;
            }
            else
            {
                m_carriedObject.rigidbody.velocity = Vector3.zero;
            }
        }
    }

    void FixedUpdate()
    {
        Hand foremostHand = GetForeMostHand();
        if (foremostHand != null)
        {
            m_handOpenThisFrame = IsHandOpen(foremostHand);
            ProcessLook(foremostHand);
            //MoveCharacter(foremostHand);
            HandCallbacks(foremostHand);
            MoveCarriedObject();
        }
        m_handOpenLastFrame = m_handOpenThisFrame;
    }
}
