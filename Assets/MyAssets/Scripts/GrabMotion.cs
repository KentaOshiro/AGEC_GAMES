using UnityEngine;
using System.Collections;
using Leap;

public class GrabMotion : MonoBehaviour
{
    /// <summary>
    /// Grabモーションを行った際に生成するオブジェクトのオリジナル.
    /// </summary>
    public GameObject OriginalSphere;
    /// <summary>
    /// rigidbody の AddForce を適用するオブジェクト.
    /// </summary>
    GameObject AddForceObject;
    /// <summary>
    /// LeapMotionコントローラのインスタンス.
    /// </summary>
    Controller m_leapController;
    /// <summary>
    /// 現時点のフレームで手を開くモーションを行ったか.
    /// </summary>
    bool m_handOpenThisFrame = false;
    /// <summary>
    /// １フレーム前に手を開くモーションを行ったか.
    /// </summary>
    bool m_handOpenLastFrame = false;
    /// <summary>
    /// Grabモーションを行っているか?
    /// </summary>
    bool m_bIsGrab = false;
    /// <summary>
    /// プレイヤーが打ち出したブラックホールが GameScene に存在しているか.
    /// </summary>
    bool m_bIsBlackHallForGameScene = false;
    /// <summary>
    /// Grabモーションを継続している間カウントされるカウンタ.
    /// </summary>
    float m_fKeepGrabMotionTime = 0.0f;
    /// <summary>
    /// プレイヤーが打ち出せるブラックホールのカウント.
    /// </summary>
    public int m_iGrabCount = 3;

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
        m_bIsGrab = false;
    }

    // 手が閉じている場合の処理.
    void OnHandClose(Hand h)
    {
        m_bIsGrab = true;
        m_bIsBlackHallForGameScene = true;
        m_iGrabCount--;
        AddForceObject = Instantiate(OriginalSphere) as GameObject;
        AddForceObject.transform.localScale = new Vector3(0, 0, 0);
        m_fKeepGrabMotionTime = 0.0f;
        AddForceObject.transform.position = transform.position + (transform.forward * 2);
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

        if (Mathf.Abs(handX) > rotThresholdX)
            Camera.main.transform.Rotate(Vector3.up, handX * 2.0f);
    }

    void KeepGrabTimeToScale()
    {
        if (m_bIsGrab)
        {
            m_fKeepGrabMotionTime += Time.deltaTime;
            AddForceObject.transform.localScale = new Vector3(m_fKeepGrabMotionTime, m_fKeepGrabMotionTime, m_fKeepGrabMotionTime);
        }
    }


    // Determines if any of the hand open/close functions should be called.
    // 手が開いているか閉じているかに応じてのコールバック（反応）を定義.
    void HandCallbacks(Hand h)
    {
        if (m_handOpenThisFrame && m_handOpenLastFrame == false)
            OnHandOpen(h);

        if (!m_handOpenThisFrame && m_handOpenLastFrame && !m_bIsBlackHallForGameScene && m_iGrabCount > 0)
            OnHandClose(h);
    }

    public void DeleteBlackHall()
    {
        m_bIsBlackHallForGameScene = false;
    }

    void FixedUpdate()
    {
        Hand foremostHand = GetForeMostHand();
        if (foremostHand != null)
        {
            m_handOpenThisFrame = IsHandOpen(foremostHand);
            ProcessLook(foremostHand);
            KeepGrabTimeToScale();
            HandCallbacks(foremostHand);
        }
        m_handOpenLastFrame = m_handOpenThisFrame;
    }
}
