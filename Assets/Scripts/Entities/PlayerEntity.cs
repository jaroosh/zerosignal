using UnityEngine;
using System.Collections;
using ZeroSignal.Engine;

public class PlayerEntity : LiveEntity {
	#region Members.

	private VariableScript _ptrScriptVariable;
	
	// FOOD and stuff.
	
	
	// Movement
	private Vector3 _inputMovement;

	// Calc.	
	private int i;
	private bool meleeDamageState;
	private bool _isShooting;
	
	// Animation.
	public float parAnimationFrameRate = 11f; // how many frames to play per second
	public float parMeleeAnimationMin = 1; // the first frame of the melee animation
	public float parMeleeAnimationMax = 1; // the last frame of the melee animation
	public float parWalkAnimationMin = 1; // the first frame of the walk animation
	public float parWalkAnimationMax = 5.9f; // the last frame of the walk animation
	public float parStandAnimationMin = 1; // the first frame of the stand animation
	public float parStandAnimationMax = 1; // the last frame of the stand animation

	private bool _meleeDamageState;
	
	#endregion


	// Use this for initialization
	protected override void Initialize () {	
		// Find the gos.
		_ptrScriptVariable = (VariableScript) Registry.Instance.CurrentPlayer.GetComponent( typeof(VariableScript) );
		_actionState = ActionState.Stand;
	}
	
	protected override void HandleInput ()
	{		
		// find vector to move
		_inputMovement = new Vector3( Input.GetAxis(Registry.AxisHorizontal),0,Input.GetAxis(Registry.AxisVertical) );
		
		// Find the position where we're facing.
		_tempVector2 = new Vector3(Screen.width * 0.5f,0,Screen.height * 0.5f); // the position of the middle of the screen
		_tempVector = Input.mousePosition; // find the position of the moue on screen
		_tempVector.z = _tempVector.y; // input mouse position gives us 2D coordinates, I am moving the Y coordinate to the Z coorindate in temp Vector and setting the Y coordinate to 0, so that the Vector will read the input along the X (left and right of screen) and Z (up and down screen) axis, and not the X and Y (in and out of screen) axis
		_tempVector.y = 0;
		_inputRotation = _tempVector - _tempVector2; // the direction we want face/aim/shoot is from the middle of the screen to where the mouse is pointing

		// Do the shooting.
		_isShooting = Input.GetMouseButtonDown(0);

		// Handle minimap.
		if (Input.GetKeyDown ("tab"))
			GameManager.Instance.ToggleMiniMap();
	}
	
	protected void HandleBullets ()
	{
		_tempVector = Quaternion.AngleAxis(8f, Vector3.up) * _inputRotation;
		_tempVector = (transform.position + (_tempVector.normalized * 0.8f));
		GameObject objCreatedBullet = (GameObject) Instantiate(_ptrScriptVariable.objBullet, _tempVector, Quaternion.LookRotation(_inputRotation) ); // create a bullet, and rotate it based on the vector inputRotation
		Physics.IgnoreCollision(objCreatedBullet.collider, collider);
	}

	// Processes movement of an entity..
	protected override void ProcessState() {
		// First of all - if were shooting - were motherfucking shooting!
		if(_isShooting)
			HandleBullets();
	
		// Move.
		_tempVector = rigidbody.GetPointVelocity(transform.position) * Time.deltaTime * 1000;		
		rigidbody.AddForce (-_tempVector.x, -_tempVector.y, -_tempVector.z);
		rigidbody.AddForce (_inputMovement.normalized * parMoveSpeed * Time.deltaTime);
		transform.rotation = Quaternion.LookRotation(_inputRotation);
		transform.eulerAngles = new Vector3(0, transform.eulerAngles.y + 180,0);
		transform.position = new Vector3(transform.position.x,0,transform.position.z);		
	}
	
	// All things related to post-movement stuff - like animation, camera etc.
	protected override void RenderState() {
		UpdateAnimation();
		RenderAnimation();
	}
	
	private void UpdateAnimation() {		
		// Find animation.
		if (_actionState == ActionState.MeleeAttack || 
			_currentAnimation == AnimationState.Melee) {					
			_currentAnimation = AnimationState.Melee;
			return;
		}
		if (_inputMovement.magnitude > 0) {			
			_currentAnimation = AnimationState.Walk;
		} else {
			_currentAnimation = AnimationState.Stand;
		}
	}
	
	private void RenderAnimation() {
		// Tune to fps.
		_animationTime -= Time.deltaTime; 
		if (_animationTime <= 0) { // animate another frame
			_animationCurrentFrame += 1;
			// one play animations (play from start to finish)
				if (_currentAnimation == AnimationState.Melee) {
					// Move this to an animation matrix.
					// Or use dictionary / hashmap with Animation , min/max
					_animationCurrentFrame = Mathf.Clamp(_animationCurrentFrame, parMeleeAnimationMin, parMeleeAnimationMax+ 1 );
					if (_animationCurrentFrame > parMeleeAnimationMax) {
						_meleeDamageState = false; // Once we have finished playing our melee animation, we can detect if we have hit the player again
						if (_actionState == ActionState.MeleeAttack) { // yup were still attacking.
							_animationCurrentFrame = parMeleeAnimationMin;
						} else {
							_currentAnimation = AnimationState.Walk;
							_animationCurrentFrame = parWalkAnimationMin;
						}
					}				
				}
				// cyclic animations (cycle through the animation)
				if (_currentAnimation == AnimationState.Stand)
				{
					_animationCurrentFrame = Mathf.Clamp(_animationCurrentFrame, parStandAnimationMin, parStandAnimationMax+1);
					if (_animationCurrentFrame > parStandAnimationMax) {
						_animationCurrentFrame = parStandAnimationMin;
					}
				}
				if (_currentAnimation == AnimationState.Walk) {                        
					_animationCurrentFrame = Mathf.Clamp(_animationCurrentFrame, parWalkAnimationMin, parWalkAnimationMax+1);
					if (_animationCurrentFrame > parWalkAnimationMax) {
						_animationCurrentFrame = parWalkAnimationMin;
					}
				}
				_animationTime += (1 / parAnimationFrameRate); // if the animationFrameRate is 11, 1/11 is one eleventh of a second, that is the time we are waiting before we play the next frame.
			}
			
			_spriteSheetCount.y = 0;				
			for (i=(int)_animationCurrentFrame; i > 5; i-=5) { // find the number of frames down the animation is and set the y coordinate accordingly 
				_spriteSheetCount.y += 1;
			}
			_spriteSheetCount.x = i - 1; // find the X coordinate of the frame to play
			_spriteSheetOffset = new Vector2(1 - (_spriteSheetCount.x/parSpriteSheetTotalRow),1 - (_spriteSheetCount.y/parSpriteSheetTotalHigh));  // find the X and Y coordinate of the frame to display
			parObjSpriteRender.renderer.material.SetTextureOffset ("_MainTex", _spriteSheetOffset); // offset the texture to display the correct frame	
	}
	
	// Handles camera.
	protected override void PostProcess() {
		Registry.Instance.MainCamera.transform.position = new Vector3(transform.position.x,15,transform.position.z);
		Registry.Instance.MainCamera.transform.eulerAngles = new Vector3(90,0,0);
	}
	
	protected override bool IsAlive { get { return true; }}
		
}
