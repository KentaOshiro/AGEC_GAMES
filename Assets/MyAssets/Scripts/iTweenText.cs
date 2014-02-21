using UnityEngine;
using System.Collections;

public class iTweenText : MonoBehaviour {

    public Vector3 MoveToPosition;
    public Color ColorToValue;
    // Use this for initialization
    void Start()
    {
        gameObject.renderer.material.color = new Color(0, 0, 0, 0);
        iTween.MoveTo(gameObject, iTween.Hash("x", MoveToPosition.x, "y", MoveToPosition.y, "z", MoveToPosition.z, "time", 5.0f));
        iTween.ColorTo(gameObject, iTween.Hash("r", ColorToValue.r, "g", ColorToValue.g, "b", ColorToValue.b, "a", ColorToValue.a, "time", 3.0f));
    }

    // Update is called once per frame
    void Update()
    {

    }
}
