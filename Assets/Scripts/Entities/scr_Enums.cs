using UnityEngine;
using System.Collections;

// State of an action.
public enum ActionState { 
	Stand,
	MeleeAttack
}

// State of an animation.
public enum AnimationState {
	Melee,
	Walk,
	Stand
}