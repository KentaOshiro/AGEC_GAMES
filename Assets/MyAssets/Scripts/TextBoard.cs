using UnityEngine;
using System.Collections;

public class TextBoard : MonoBehaviour {

    public Vector3 MoveToPosition;

	// Use this for initialization
	void Start () {
        iTween.MoveTo(gameObject, iTween.Hash("x", MoveToPosition.x, "y", MoveToPosition.y, "z", MoveToPosition.z, "time", 5.0f));
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
