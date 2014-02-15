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

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
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
