using UnityEngine;
using System.Collections;

public class StageClearManager : MonoBehaviour {

    /// <summary>
    /// クリアするために最小限必要な清潔度.
    /// </summary>
    public float GoalBorderMin;
    /// <summary>
    /// クリアするために最大限必要な清潔度
    /// </summary>
    public float GoalBorderMax;
    /// <summary>
    /// 清潔度を表示するためのfont等を定義.
    /// </summary>
    public Texture2D m_FontBackgroundTexture;
    public GUIStyle m_GUIStyle;
    public GUIStyleState m_GUIStyleState;
    /// <summary>
    /// ステージクリアか否かを定義する.
    /// </summary>
    private bool m_bIsStageClear;
    private bool m_bIsStageFailure;
    /// <summary>
    /// 
    /// </summary>
    private bool m_bShowResult = false;

	// Use this for initialization
	void Start () {
        m_GUIStyleState.background = m_FontBackgroundTexture;
        m_GUIStyle.normal = m_GUIStyleState;

	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnGUI()
    {
        Rect ShowCleanliness = new Rect(10, 10, 100, 50);
        m_GUIStyle.fontSize = 50;
        m_GUIStyle.normal.textColor = Color.cyan;
        GUI.Label(ShowCleanliness, "清潔度:" + Cleanliness.m_fCleanliness.ToString() + "%", m_GUIStyle);

        Rect ShowClearBorder = new Rect(10, 70, 100, 50);
        m_GUIStyle.fontSize = 25;
        m_GUIStyle.normal.textColor = Color.red;
        GUI.Label(ShowClearBorder, "基準値:" + GoalBorderMax + "% ~" + GoalBorderMin + "%", m_GUIStyle);

        Rect ShowRemitGenerateCount = new Rect(10, Screen.height - 50, 100, 50);
        m_GUIStyle.fontSize = 25;
        m_GUIStyle.normal.textColor = Color.green;
        int GrabCount = GameObject.Find("Main Camera").GetComponent<GrabMotion>().m_iGrabCount;
        //int GrabCount = GameObject.Find("Main Camera").GetComponent<CameraMoveScripts>().m_iGrabCount;
        GUI.Label(ShowRemitGenerateCount, "残生成数:" + GrabCount, m_GUIStyle);


        if (m_bShowResult)
        {
            Rect Result = new Rect(Screen.width / 3.0f, Screen.height / 2.0f, 100, 100);
            m_GUIStyle.fontSize = 50;
            if (m_bIsStageClear)
            {
                m_GUIStyle.normal.textColor = Color.red;
                GUI.Label(Result, "Stage Clear!!!", m_GUIStyle);
                return;
            }

            if(m_bIsStageFailure)
            {
                m_GUIStyle.normal.textColor = Color.red;
                GUI.Label(Result, "Stage Failure...", m_GUIStyle);
                return;
            }
        }
    }

    /// <summary>
    /// このゲームをクリアしたかをチェックする.
    /// 清潔度がGoalBorderMin以上かつGoalBorderMax以下の値に収まっていれば true を返しクリアとなる.
    /// </summary>
    /// <returns>boolean</returns>
    void CheckGameClear()
    {
        if (Cleanliness.m_fCleanliness <= GoalBorderMax && 
            Cleanliness.m_fCleanliness >= GoalBorderMin)
        {
            m_bIsStageClear =  true;
            m_bIsStageFailure = false;
        }
            

        int GrabCount = GameObject.Find("Main Camera").GetComponent<GrabMotion>().m_iGrabCount;
        if (Cleanliness.m_fCleanliness > GoalBorderMax)
        {
            Debug.Log("クリア失敗：基準値を超えた");
            m_bIsStageClear = false;
            m_bIsStageFailure = true;
        }
        
        if (Cleanliness.m_fCleanliness < GoalBorderMin && GrabCount <= 0)
        {
            Debug.Log("クリア失敗：基準値に満たなかった＆弾を合うべて撃ち切った");
            m_bIsStageClear = false;
            m_bIsStageFailure = true;
        }
    }

    public void ShowResult()
    {
        m_bShowResult = true;
        CheckGameClear();
    }
}
