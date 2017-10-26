using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayCastController : MonoBehaviour {

	public int HorizontalRayCount = 4;
	public int VerticalRayCount = 4;

	public float HorizontalRaySpacing, VerticalRaySpacing;
	public RayCastOrigins rayCastOrigin;

	public float skinWidth = 0.015f;
	private BoxCollider2D myCollider;

	// Use this for initialization
	void Start () {
		myCollider = GetComponent<BoxCollider2D>();
		CalculateRaySpacing();
	}
	
	// Update is called once per frame
	void Update () {
		UpdateRayCastOrigins();
	}


	private void UpdateRayCastOrigins() {

		Bounds bounds = myCollider.bounds;
		bounds.Expand(skinWidth * -2);

		rayCastOrigin.topLeft = new Vector2 (bounds.min.x,bounds.max.y);
		rayCastOrigin.topRight = new Vector2 (bounds.max.x,bounds.max.y);
		rayCastOrigin.bottomLeft = new Vector2 (bounds.min.x,bounds.min.y);
		rayCastOrigin.bottomRight = new Vector2 (bounds.max.x, bounds.min.y);

	}

	private void CalculateRaySpacing() {
		Bounds bounds = myCollider.bounds;
		bounds.Expand(skinWidth* -2);

		HorizontalRaySpacing = bounds.size.y /(HorizontalRayCount -1);
		VerticalRaySpacing = bounds.size.x/ (VerticalRayCount -1);
	}

	public struct RayCastOrigins {

		public Vector2 topLeft,topRight;
		public Vector2 bottomLeft,bottomRight;

	}
}
