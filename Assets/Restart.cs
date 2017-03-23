using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Restart : MonoBehaviour {

	public Rigidbody2D projectile;
	public float resetSpeed = 0.25f;
	public Component[] life;
	public Damage damage;

	private float resetSpeedSqr;
	private SpringJoint2D spring;
	private Rigidbody2D collider;

	static int times = 3;

	// Use this for initialization
	void Start () {
		resetSpeedSqr = resetSpeed * resetSpeed;
		spring = projectile.GetComponent<SpringJoint2D> ();
		if (times == 2)
			life [2].gameObject.SetActive (false);
		else if (times == 1) {
			life [2].gameObject.SetActive (false);
			life [1].gameObject.SetActive (false);
		}
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.R)) {
			Reset ();
		}

		if (spring == null && projectile.velocity.sqrMagnitude < resetSpeedSqr) {
			Reset ();
		}
	}

	void OnTriggerExit2D (Collider2D other) {
		collider = other.GetComponent<Rigidbody2D> ();
		if (collider == projectile) {
			Reset ();
		}
	}
		
	void Reset(){
		times--;
		int level = Application.loadedLevel;
		if (damage.currentHitPoints <= 0) {
			times++;
			if (level == 1)
				Application.LoadLevel (3); //Next Level
			else if (level == 4)
				Application.LoadLevel (5); //All Done
			//times = 3;
		} else if (times > 0) {
			if (level == 1)
				Application.LoadLevel (1); //restart
			else if (level == 4)
				Application.LoadLevel (4);
		}
		else {
			Application.LoadLevel (2); //gameover
			times = 3;
		}
	}
}

