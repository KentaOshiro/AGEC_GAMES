using UnityEngine;
using System.Collections;

public class StageObjectManager : MonoBehaviour
{
    /// <summary>
    /// ステージオブジェクトを自動で生成するか否かを指定する.
    /// </summary>
    public bool m_bGenerateStageObject;
    /// <summary>
    /// 生成するオブジェクトを格納する配列.
    /// Inspector上から指定出来るようにpublicとしている.
    /// </summary>
    public GameObject[] CreateObjects;
    /// <summary>
    /// いくつのオブジェクト数を生成するのかを指定する.
    /// </summary>
    public int MaxGenerateObjects;
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
        if (!m_bGenerateStageObject)
            return;
        while (m_fAllStageObjectDirtiness < MaxGenerateObjects)
        {
            int index = Random.RandomRange(0, CreateObjects.Length);
            float xPos = (float)Random.RandomRange(-50, 50);
            float zPos = (float)Random.RandomRange(0, 30);
            CreateObjects[index].transform.position = new Vector3(xPos, 1.0f, zPos);
            CreateObjects[index].tag = "SuctionObject";
            m_fAllStageObjectDirtiness += CreateObjects[index].GetComponent<StageObjects>().m_fDirtinessdegree;
            Instantiate(CreateObjects[index]);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            Cleanliness.Initialize();
            Application.LoadLevel("MyRoom");
        }
    }

    void RemoveThePlayerSphere()
    {
        m_bDestroyPlayerSphere = true;
    }
}
