using UnityEngine;
using ZeroSignal.Engine;
using ZeroSignal.Engine.Interfaces;
using System.Collections;

// Registry class.
public class Registry : Singleton<Registry>, IInitializable {

#region Statics.

	public const string AxisHorizontal = "Horizontal";
	public const string AxisVertical = "Vertical";
	
	public static class Tags { 
	
		public const string MiniMapTag = "MiniMapCamera";
		public const string PlayerTag = "Player";
		public const string MainCameraTag = "MainCamera";
		public const string GameManager = "GameManager";
	}

	public static class Prefixes { 
		public const string AudioSoundPrefix = "audio_{0}";
	}

#endregion

#region Members.

	private GameObject _miniMap;
	private GameObject _currentPlayer;
	private GameObject _mainCamera;

#endregion

#region Properties.

	// Gets current player.
	public GameObject CurrentPlayer { 
		get { return _currentPlayer; }
	}

	// Gets main camera.
	public GameObject MainCamera { 
		get { return _mainCamera; }
	}

	public GameObject MiniMap { 
		get { return _miniMap; }
	}

#endregion

	public void Initialize() {
		_miniMap = (GameObject) GameObject.FindWithTag (Tags.MiniMapTag);
		_currentPlayer =  (GameObject) GameObject.FindWithTag (Tags.PlayerTag);
		_mainCamera =  (GameObject) GameObject.FindWithTag (Tags.MainCameraTag);
	}
	
}
