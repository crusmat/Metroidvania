using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationController : MonoBehaviour {

	private Player player;
	private Animator animator;
	private Vector3 objectScale;
	// Use this for initialization
	void Start () {
		player = GetComponent<Player>();
		animator = GetComponent<Animator>();
		objectScale = transform.localScale;
	}

	// Update is called once per frame
	void Update () {

		//Animation Control
		if (player.moveInfo.direction == 1) {
			if (transform.localScale.x == 1) { } 
			else if (transform.localScale.x == -1) { objectScale.x += 2;}
		}

		if (player.moveInfo.direction == -1) {
			if (transform.localScale.x == -1) { } 
			else if (transform.localScale.x == 1) { objectScale.x -= 2;}
		}


		if (player.moveInfo.jump) {animator.SetBool ("Jump",true);} 
		if (!player.moveInfo.jump && player.moveInfo.fall) {animator.SetBool ("Fall",true); animator.SetBool ("Jump",false);}
		if (!player.moveInfo.fall) {animator.SetBool ("Fall",false);}
		if (player.moveInfo.run && player.moveInfo.jump) {animator.SetBool ("Jump",true);}
		if (player.moveInfo.run) { animator.SetBool("Run",true);} else if (!player.moveInfo.run) { animator.SetBool("Run",false);}
		if (player.moveInfo.jetpack) {animator.SetBool("Jetpack",true);} else if (!player.moveInfo.jetpack) { animator.SetBool("Jetpack",false);}

		transform.localScale = objectScale;
	}
}
