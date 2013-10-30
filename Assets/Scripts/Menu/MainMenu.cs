using UnityEngine;
using System.Collections;

public class MainMenu : MonoBehaviour {

#region Members.

	public Texture2D parStartGameButton;

#endregion

	// TODO: texturki do spritesheeta.
	void OnGUI() 	{
		if ( GUI.Button( new Rect ( Screen.width  * 0.5f - 32, Screen.height  * 0.5f - 32, 64, 64 ), parStartGameButton,
			GUIStyle.none) ) {
			Application.LoadLevel("game");
		}
	}

}
