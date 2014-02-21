using UnityEngine;
using System.Collections;

public class HelloString : MonoBehaviour {

    public GUIStyle m_GUIStyle;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnGUI()
    {
        Rect ShowHello = new Rect(Screen.width / 3.0f, Screen.height / 2.0f, 100, 50);
        m_GUIStyle.fontSize = 50;
        m_GUIStyle.normal.textColor = Color.cyan;
        GUI.Label(ShowHello, "Hello iTween", m_GUIStyle);
    }
}
