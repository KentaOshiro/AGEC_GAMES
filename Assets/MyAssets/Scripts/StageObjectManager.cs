using UnityEngine;
using System.Collections;

public class StageObjectManager : MonoBehaviour
{
    /// <summary>
    /// 生成するオブジェクトを格納する配列.
    /// Inspector上から指定出来るようにpublicとしている.
    /// </summary>
    public GameObject[] CreateObjects;
    /// <summary>
    /// 清潔度を表示するためのfont等を定義.
    /// </summary>
    public GUIStyle m_GUIStyle;
    /// <summary>
    /// Playerが生成したオブジェクトを削除するか否かを定義している.
    /// </summary>
    private bool m_bDestroyPlayerSphere = false;
    /// <summary>
    /// ステージ上にある全てのオブジェクトが持つ不潔度の合計値.
    /// </summary>
    private float m_fAllStageObjectDirtiness = 0.0f;

    // Use this for initialization
    void Start()
    {
        while (m_fAllStageObjectDirtiness < 200)
        {
            int index = Random.RandomRange(0, CreateObjects.Length);
            float xPos = (float)Random.RandomRange(-10, 10);
            float zPos = (float)Random.RandomRange(8, 15);
            CreateObjects[index].transform.position = new Vector3(xPos, 1.0f, zPos);
            CreateObjects[index].tag = "SuctionObject";
            m_fAllStageObjectDirtiness += CreateObjects[index].GetComponent<StageObjects>().m_fDirtinessdegree;
            Instantiate(CreateObjects[index]);
        }

    }

    // Update is called once per frame
    void Update()
    {
        if (!m_bDestroyPlayerSphere)
            return;

        float cleanliness = Cleanliness.m_fCleanliness;
        Rect rect = new Rect(50, 10, 400, 300);
        m_GUIStyle.fontSize = 50;
    }

    void OnGUI()
    {
        Rect rect = new Rect(10, 10, 400, 300);
        m_GUIStyle.fontSize = 50;
        GUI.Label(rect, Cleanliness.m_fCleanliness.ToString(), m_GUIStyle);
    }

    void RemoveThePlayerSphere()
    {
        m_bDestroyPlayerSphere = true;
    }
}
