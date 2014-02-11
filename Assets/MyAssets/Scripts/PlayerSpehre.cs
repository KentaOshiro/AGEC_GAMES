using UnityEngine;
using System.Collections;

public class PlayerSpehre : MonoBehaviour {

    GameObject[] SuctionObjects;
    bool m_bIsCollision = false;
	// Use this for initialization
	void Start () {
        SuctionObjects = GameObject.FindGameObjectsWithTag("SuctionObject");
	}
	
	// Update is called once per frame
	void Update () {
        if (!m_bIsCollision)
            return;
        foreach (GameObject obj in SuctionObjects)
        {
            obj.SendMessage("ToTarget", transform);
        }   
	}

    void OnCollisionEnter(Collision col)
    {
        m_bIsCollision = true;
        gameObject.rigidbody.constraints = RigidbodyConstraints.FreezeAll;
        gameObject.renderer.enabled = false;
    }
}
