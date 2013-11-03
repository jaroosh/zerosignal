using UnityEngine;
using System.Collections;
using ZeroSignal.Engine.Interfaces;

namespace ZeroSignal.Engine { 

	public class GameManager : Singleton<GameManager>, IInitializable {

		private Camera _miniMap;

		public bool IsMiniMapVisible { 
			get { 
				return _miniMap.enabled;
			}
		}

		public void Initialize() {
			_miniMap = Registry.Instance.MiniMap.GetComponent<Camera>();
		}

		public void ToggleMiniMap() {
			_miniMap.enabled = !_miniMap.enabled;
		}
	}
}