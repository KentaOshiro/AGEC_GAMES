using UnityEngine;
using System.Collections;

public class StageObjectManager : MonoBehaviour
{

    public GameObject[] CreateObjects;
    public GUIStyle m_GUIStyle;
    private bool m_bDestroyPlayerSphere = false;
    private float m_fHogeHoge = 0.0f;
    // Use this for initialization
    void Start()
    {

        //for (int i = 0; i < 200; ++i)
        //{
        //    int index = Random.RandomRange(0, CreateObjects.Length);
        //    float xPos = (float)Random.RandomRange(-10, 10);
        //    float zPos = (float)Random.RandomRange(8, 15);
        //    CreateObjects[index].transform.position = new Vector3(xPos, 1.0f, zPos);
        //    CreateObjects[index].tag = "SuctionObject";
        //    if (m_fHogeHoge > 100)
        //        break;
        //    m_fHogeHoge += CreateObjects[index].GetComponent<StageObjects>().m_fDirtinessdegree;

        //    Instantiate(CreateObjects[index]);
        //}

        while (m_fHogeHoge < 200)
        {
            int index = Random.RandomRange(0, CreateObjects.Length);
            float xPos = (float)Random.RandomRange(-10, 10);
            float zPos = (float)Random.RandomRange(8, 15);
            CreateObjects[index].transform.position = new Vector3(xPos, 1.0f, zPos);
            CreateObjects[index].tag = "SuctionObject";
            m_fHogeHoge += CreateObjects[index].GetComponent<StageObjects>().m_fDirtinessdegree;
            Instantiate(CreateObjects[index]);
        }

        //GameObject.Find("StageObjectManager").SendMessage("Start");
    }

    // Update is called once per frame
    void Update()
    {
        if (!m_bDestroyPlayerSphere)
            return;

        float cleanliness = Cleanliness.m_fCleanliness;
        string text = "";
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
