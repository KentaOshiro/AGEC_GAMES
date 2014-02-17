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
    public GUIStyle m_GUIStyle;


	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnGUI()
    {
        Rect rect = new Rect(10, 10, 100, 50);
        m_GUIStyle.fontSize = 50;
        m_GUIStyle.normal.textColor = Color.cyan;
        GUI.Label(rect, Cleanliness.m_fCleanliness.ToString() + "%", m_GUIStyle);

        Rect rect2 = new Rect(10, 70, 100, 50);
        m_GUIStyle.fontSize = 25;
        m_GUIStyle.normal.textColor = Color.red;
        GUI.Label(rect2, GoalBorderMax + "% ~" + GoalBorderMin + "%", m_GUIStyle);
    }

    /// <summary>
    /// このゲームをクリアしたかをチェックする.
    /// 清潔度がGoalBorderMin以上かつGoalBorderMax以下の値に収まっていれば true を返しクリアとなる.
    /// </summary>
    /// <returns>boolean</returns>
    bool IsGameClear()
    {
        if (Cleanliness.m_fCleanliness <= GoalBorderMax && Cleanliness.m_fCleanliness >= GoalBorderMin)
        {
            return true;    
        }
        return false;
    }

    public void ShowResult()
    {
        if (IsGameClear())
            Debug.Log("Stage Clear!!");
        else
            Debug.Log("Stage Failure...");
    }
}
