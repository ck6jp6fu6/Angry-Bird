using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dragging : MonoBehaviour {

	public float maxStretch = 3.0f;
	public LineRenderer catapultLineFront;
	public LineRenderer catapultLineBack;
	public AudioSource shootmusic;
	public AudioSource hitmusic;

	private Transform catapult;
	private SpringJoint2D spring;
	private Ray rayToMouse;
	private Ray leftCatapultToProjectile;
	private float maxStretchSqr;
	private float circleRadius;
	private bool clickedOn;
	private Vector2 prevVelocity;
	private Rigidbody2D rigidbody2D;
	private CircleCollider2D circle;


	void Awake(){
		spring = GetComponent<SpringJoint2D> ();
		catapult = spring.connectedBody.transform;
		rigidbody2D = GetComponent<Rigidbody2D> ();
		circle = GetComponent<CircleCollider2D> ();
	}
	// Use this for initialization
	void Start () {
		LineRendererSetup ();
		rayToMouse = new Ray (catapult.position, Vector3.zero);
		leftCatapultToProjectile = new Ray (catapultLineFront.transform.position, Vector3.zero);
		maxStretchSqr = maxStretch * maxStretch;
		rigidbody2D.isKinematic = true;
		circleRadius = circle.radius;
	}
	
	// Update is called once per frame
	void Update () {
		if (spring != null) {
			if (!rigidbody2D.isKinematic && prevVelocity.sqrMagnitude > rigidbody2D.velocity.sqrMagnitude) {
				Destroy (spring);
				shootmusic.Play ();
				//rigidbody2D.velocity = prevVelocity;
			}
			if (clickedOn)
				DraggingAct ();
			else
				prevVelocity = rigidbody2D.velocity;

			LineRendererUpdate ();
		} else {
			catapultLineFront.enabled = false;
			catapultLineBack.enabled = false;
		}
	}

	void LineRendererSetup(){
		catapultLineFront.SetPosition (0, catapultLineFront.transform.position);
		catapultLineBack.SetPosition (0, catapultLineBack.transform.position);

		catapultLineFront.sortingLayerName = "ForeGround";
		catapultLineBack.sortingLayerName = "ForeGround";

		catapultLineFront.sortingOrder = 3;
		catapultLineBack.sortingOrder = 1;
	}

	void OnMouseDown(){
		spring.enabled = false;
		clickedOn = true;
	}

	void OnMouseUp(){
		spring.enabled = true;
		rigidbody2D.isKinematic = false;
		clickedOn = false;
	}

	void DraggingAct(){
		Vector3 mouseWorldPoint = Camera.main.ScreenToWorldPoint (Input.mousePosition);
		Vector2 catapultToMouse = mouseWorldPoint - catapult.position;

		if (catapultToMouse.sqrMagnitude > maxStretchSqr) {
			rayToMouse.direction = catapultToMouse;
			mouseWorldPoint = rayToMouse.GetPoint (maxStretch);
		}
		mouseWorldPoint.z = 0f;
		transform.position = mouseWorldPoint;
	}

	void LineRendererUpdate(){
		Vector2 catapultToProjectile = transform.position - catapultLineFront.transform.position;
		leftCatapultToProjectile.direction = catapultToProjectile;
		Vector3 holdPoint = leftCatapultToProjectile.GetPoint (catapultToProjectile.magnitude + circleRadius);
		catapultLineFront.SetPosition (1, holdPoint);
		catapultLineBack.SetPosition (1, holdPoint);
	}

	void OnCollisionEnter2D (Collision2D collision) {
		hitmusic.Play ();
	}
}		
