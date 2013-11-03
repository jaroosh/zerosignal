using UnityEngine;
using UnityEditor;
using ZeroSignal.Engine;
using ZeroSignal.Engine.Interfaces;
using System.Collections;
using ZeroSignal.Engine.Interfaces.Logging;
using ZeroSignal.Engine.Logging;
using ZeroSignal.Utilities;

// Registry class.
public class Registry : Singleton<Registry>, IInitializable {

#region Statics.

	public const string AxisHorizontal = "Horizontal";
	public const string AxisVertical = "Vertical";
	private ILogger _logger = LoggerFactory.GetLogger(typeof(Registry));
	
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
	private Hashtable _appSettings;

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

	public Hashtable AppSettings { 
		get {
			return _appSettings;
		}
	}

#endregion

	private void InitializeStaticConfig() {
		var textConfig = (TextAsset)AssetDatabase.LoadAssetAtPath("Assets/Settings/appConfig.txt", typeof(TextAsset));
		if(textConfig == null)
		throw new System.IO.FileNotFoundException("app config not found.");

		// Decode object.
		try {
			Hashtable root = (Hashtable)MiniJSON.jsonDecode(textConfig.text);
		// App Settings.
			_appSettings = (Hashtable)root["appSettings"];
			_logger.Info("KEY : " + _appSettings["key"]); 
		}
		catch(System.Exception e) {
			throw;
		}
	}

	public void Initialize() {
		InitializeStaticConfig();
		_logger.Info("registry static configuration loaded successfuly!");

		_miniMap = (GameObject) GameObject.FindWithTag (Tags.MiniMapTag);
		_currentPlayer =  (GameObject) GameObject.FindWithTag (Tags.PlayerTag);
		_mainCamera =  (GameObject) GameObject.FindWithTag (Tags.MainCameraTag);
	}
	
}
