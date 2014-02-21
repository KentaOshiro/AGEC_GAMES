using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ObjectAbsorbManager : MonoBehaviour {

    [SerializeField]
    private List<string> m_CollectObjectName;
    [SerializeField]
    private List<float> m_CollectObjectDirtinessdegree;

	// Use this for initialization
	void Start () {
        m_CollectObjectName = new List<string>();
        m_CollectObjectDirtinessdegree = new List<float>();
	}
	
	// Update is called once per frame
	void Update () {
	}

    public void CollectObject(GameObject obj)
    {
        m_CollectObjectName.Add(obj.name);
        float objectDirtinessdegree = obj.GetComponent<StageObjects>().m_fDirtinessdegree;
        m_CollectObjectDirtinessdegree.Add(objectDirtinessdegree);
    }
}
