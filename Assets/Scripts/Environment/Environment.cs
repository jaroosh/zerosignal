using UnityEngine;
using System.Collections;

public class Environment : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	void OnGUI() {
		GUI.Box(new Rect(5,5, Screen.width / 3, 20),"Health");
		GUI.Box(new Rect(5,25, Screen.width / 3, 20), "Ammo");
	}
}

