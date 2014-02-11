using UnityEngine;
using System.Collections;

public class StageObjects : MonoBehaviour {
    float timeToScale = 1.0f;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        //timeToScale += Time.deltaTime;
        //transform.localScale = new Vector3(timeToScale, timeToScale, timeToScale);
    }

    public void ToTarget(Transform toTarget)
    {
        Vector3 toPosition = toTarget.position - transform.position;
        gameObject.rigidbody.velocity += toPosition.normalized * (3.0f / Mathf.Max(1.0f, toPosition.magnitude));
    }

    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.tag == "PlayerBullet")
        {
            Destroy(gameObject);
        }
    }
}
