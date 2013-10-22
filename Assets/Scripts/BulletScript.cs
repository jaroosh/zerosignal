using UnityEngine;
using System.Collections;

public class BulletScript : MonoBehaviour {
	private float moveSpeed = 30f; // how fast the bullet moves
	private float timeSpentAlive; // how long the bullet has stayed alive for
	private GameObject objPlayer;
	private VariableScript ptrScriptVariable;

	// Use this for initialization
	void Start () {
		objPlayer = (GameObject) GameObject.FindWithTag ("Player");
		ptrScriptVariable = (VariableScript) objPlayer.GetComponent( typeof(VariableScript) );
	}

	// Update is called once per frame
	void Update () {
		timeSpentAlive += Time.deltaTime;
		if (timeSpentAlive > 1) // if we have been travelling for more than one second remove the bullet
		{
			removeMe();
		}
		// move the bullet
			transform.Translate(0, 0, moveSpeed * Time.deltaTime);
			transform.position = new Vector3(transform.position.x,0,transform.position.z); // because the bullet has a rigid body we don't want it moving off it's Y axis
	}
	void removeMe ()
	{
		Instantiate(ptrScriptVariable.parBulletHit, transform.position, Quaternion.identity );
		Destroy(gameObject);
	}
	void OnCollisionEnter(Collision Other)
	{
		if ( Other.gameObject.GetComponent( typeof(AIscript) ) != null && Other.gameObject != objPlayer ) // if we have hit a character and it is not the player
		{
			AIscript ptrScriptAI = (AIscript) Other.gameObject.GetComponent( typeof(AIscript) );
			ptrScriptAI.health -= 10;
			Instantiate(ptrScriptVariable.parAlienHit, transform.position, Quaternion.identity );
			removeMe();
		}
		removeMe(); // remove the bullet if it has hit something else apart from an enemy character
	}
}
