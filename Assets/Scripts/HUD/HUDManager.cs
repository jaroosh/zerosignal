using UnityEngine;
using System.Collections;
using ZeroSignal.Engine;

public class HUDManager : MonoBehaviour {

	public RenderTexture parMiniMapTexture;
	public Material parMiniMapMaterial;

	private float _offset;

	public void Awake() {
		_offset = 10;
	} 

	public void OnGUI() {
		if(Event.current.type == EventType.Repaint && GameManager.Instance.IsMiniMapVisible)
		Graphics.DrawTexture(new Rect(Screen.width - 256 - _offset, _offset, 256, 256), 
			parMiniMapTexture, parMiniMapMaterial);
	}
}
