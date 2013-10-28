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
	
	// Ye start of all things.
	void Start () {	
		Initialize();
	}
	
	// Update is called once per frame
	void Update () {
		if(!IsAlive)  
			PwnEntity();		
		// Handles all inputs - interior and exterior.
		HandleInput();
		// Processes new states based on current state and inputs.
		ProcessState();					
		// After state is found - render.
		RenderState();
		// All post-processing.
		PostProcess();
	}
	
	#region Implementables.
	
	// Performs harakiri on an entity.
	protected virtual void PwnEntity() {
	}
		
	// Is entity alive.
	protected abstract bool IsAlive { get; }
	
	// Initializes entity.
	protected abstract void Initialize();
	
	// Handles all inputs - interior and exterior.
	protected virtual void HandleInput() {
		// For some entities like trees etc. we'll have no inputs.
	}

	// Processes new states based on current state and inputs.
	protected virtual void ProcessState() {
		// For some entities like trees etc. there is only one state.
	}

	// After state is found - render.
	protected virtual void RenderState() {
	}
	
	// Anything post-process related.
	protected virtual void PostProcess() {
	}	
	
	#endregion
}
