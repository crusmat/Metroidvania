using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof(BoxCollider2D))]
public class Controller2D : MonoBehaviour {

	public LayerMask collisionMask;
	public CollisionInfo collisionInfo;


	private RayCastController rayCastController;

	private void Start() {
		rayCastController = GetComponent<RayCastController>();
	}

	public void Move (Vector3 velocity) {

		collisionInfo.Reset();

		if (velocity.y != 0) {
			VerticalCollision(ref velocity);
		}

		if (velocity.x != 0) {
			HorizontalCollision (ref velocity);
		}

		transform.Translate(velocity);
	}

	private void VerticalCollision(ref Vector3 velocity) {
		float directionY = Mathf.Sign(velocity.y);
		float RayLenght = Mathf.Abs(velocity.y) + rayCastController.skinWidth;
		
		for (int i=0; i < rayCastController.VerticalRayCount; i++) {
			Vector2 rayOrigin;
			rayOrigin = (directionY == -1)?rayCastController.rayCastOrigin.bottomLeft:rayCastController.rayCastOrigin.topLeft;
			rayOrigin += Vector2.right * rayCastController.VerticalRaySpacing * i;

			RaycastHit2D hit = Physics2D.Raycast(rayOrigin,Vector2.up * directionY, RayLenght,collisionMask);

			Debug.DrawRay (rayOrigin, Vector2.up * directionY * RayLenght,Color.red);

			if (hit) {

				velocity.y = (hit.distance - rayCastController.skinWidth) * directionY;
				RayLenght = hit.distance;

				if (directionY == 1) {collisionInfo.above = true;}
				if (directionY == -1) {collisionInfo.below = true;}
			}
		}
	}

	private void HorizontalCollision (ref Vector3 velocity) {
		float directionX = Mathf.Sign(velocity.x);
		float RayLenght = Mathf.Abs(velocity.x) + rayCastController.skinWidth;

		for (int i=0; i<rayCastController.HorizontalRayCount; i++) {
			Vector2 rayOrigin;
			rayOrigin = (directionX ==1)?rayCastController.rayCastOrigin.bottomRight:rayCastController.rayCastOrigin.bottomLeft;
			rayOrigin += Vector2.up * rayCastController.HorizontalRaySpacing * i;

			RaycastHit2D hit = Physics2D.Raycast (rayOrigin, Vector2.right * directionX, RayLenght, collisionMask);


			if (hit) {

				velocity.x = (hit.distance - rayCastController.skinWidth) * directionX;
				RayLenght = hit.distance;

				if (directionX == 1) {collisionInfo.right = true;}
				if (directionX == -1) {collisionInfo.left= true;}
			}
		}
	}



	public struct CollisionInfo {
		public bool above,below;
		public bool left,right;

		public void Reset() {
			above = below = false;
			left = right = false;
		}

	}




}
