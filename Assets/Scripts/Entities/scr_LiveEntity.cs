using UnityEngine;
using System.Collections;

// Represents a single live entity that can interact.
public abstract class LiveEntity : MonoBehaviour {

	#region Members.
	
	protected Vector3 _inputRotation;
	protected Vector3 _tempVector;
	protected Vector3 _tempVector2;
	protected ActionState _actionState;
	protected AnimationState _currentAnimation;
	protected float _animationTime = 0f; // time to pass before playing next animation
	protected float _animationCurrentFrame = 0; // current frame.		

	// Sprites.
	public float parSpriteSheetTotalRow = 5; // the total number of columns of the sprite sheet
	public float parSpriteSheetTotalHigh = 4; // the total number of rows of the sprite sheet
	protected Vector2 _spriteSheetOffset; // the offset value of the X, Y coordinate for the texture
	protected Vector2 _spriteSheetCount; // the X, Y position of the frame
	
	//Renderer
	public GameObject parObjSpriteRender;
	
	// Publics.
	public float parMoveSpeed = 100f;
	
	#endregion
	
	// Update is called once per frame
	void Update () {
		if(!IsAlive)  
			PwnEntity();		
		ProcessInput();
		ProcessMovement();
		ProcessAnimation();
		PostProcess();
	}
	
	#region Implementables.
	
	// Performs harakiri on an entity.
	protected virtual void PwnEntity() {
	}
		
	// Is entity alive.
	protected abstract bool IsAlive { get; }
	
	// Processes input / logic.
	protected abstract void ProcessInput();

	// Processes movement of an entity..
	protected abstract void ProcessMovement();

	// Processes animation
	protected virtual void ProcessAnimation() {
	}
	
	// Anything post-process related.
	protected virtual void PostProcess() {
	}
	
	#endregion
}
