using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof(Controller2D))]
public class Player : MonoBehaviour {

	private float moveSpeed = 4;
	private float jumpSpeed = 7;
	private Vector3 velocity;
	private float gravity = -20;

	private float jetPackSpeed = 3;
	private bool jetPackActive = false;

	private Controller2D controller;
	public MoveInfo moveInfo;

	private void Start () {
		controller = GetComponent<Controller2D>();
		moveInfo.direction = 1;
	}

	private void Update () {

		if (controller.collisionInfo.below || controller.collisionInfo.above) {
			velocity.y = 0;
		}

		if (!controller.collisionInfo.below && jetPackActive == false) { Invoke ("ActivateJetpack",0.4f);}
	
		if (Input.GetKey("right")) {velocity.x = moveSpeed; moveInfo.direction = 1;} 
		else if (Input.GetKey("left")) { velocity.x = -moveSpeed; moveInfo.direction = -1;} 
		else {velocity.x = 0;}
			
		if (controller.collisionInfo.below && Input.GetKeyDown(KeyCode.Space)) {
			jetPackActive = false;
			velocity.y = jumpSpeed;
		}


		if (!controller.collisionInfo.below && Input.GetKey(KeyCode.Space) && jetPackActive) {
			velocity.y = jetPackSpeed;
			moveInfo.jetpack = true;
		} else if (!controller.collisionInfo.below && Input.GetKeyUp(KeyCode.Space)) {
			moveInfo.jetpack = false;
		}


		velocity.y +=gravity*Time.deltaTime;
		controller.Move(velocity*Time.deltaTime);

		//Animation states 
		if (!controller.collisionInfo.below) {

			moveInfo.run = false;
			if (velocity.y > 0) {moveInfo.jump = true;}
			else if (velocity.y < 0) {moveInfo.fall = true; moveInfo.jump = false;} 
		}

		if (controller.collisionInfo.below) {
			moveInfo.fall = false;

			if (velocity.x !=0) { moveInfo.run = true;} 
			if (velocity.x == 0) { moveInfo.run = false;}
		}
		
	}

	public void ActivateJetpack () {
		jetPackActive = true;
	}


	public struct MoveInfo {
		public bool jump, fall;
		public bool run;
		public bool jetpack;

		public int direction;

	}

}
