using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damage : MonoBehaviour {

	public int hitPoints = 2;
	public Sprite damagedSprite;
	public float damageImpactSpeed;
	public AudioSource diemusic;
	public ParticleSystem kill;

	public int currentHitPoints;
	private float damageImpactSpeedSqr;
	private SpriteRenderer spriteRenderer;
	private Collider2D collider2D;
	private Rigidbody2D rigidbody2D;

	//void Awake(){
	//	DontDestroyOnLoad (this);
	//}
	// Use this for initialization
	void Start () {
		spriteRenderer = GetComponent<SpriteRenderer> ();
		collider2D = GetComponent<Collider2D> ();
		rigidbody2D = GetComponent<Rigidbody2D> ();
		currentHitPoints = hitPoints;
		damageImpactSpeedSqr = damageImpactSpeed * damageImpactSpeed;
	}
	
	// Update is called once per frame
	void OnCollisionEnter2D (Collision2D collision) {
		if (collision.collider.tag != "Damager")
			return;
		if (collision.relativeVelocity.sqrMagnitude < damageImpactSpeedSqr)
			return;

		spriteRenderer.sprite = damagedSprite;
		currentHitPoints--;

		if (currentHitPoints <= 0) {
			Kill ();

		}
	}

	void Kill(){
		kill.transform.position = this.transform.position;
		kill.gameObject.SetActive (true);
		diemusic.Play ();
		spriteRenderer.enabled = false;
		collider2D.enabled = false;
		rigidbody2D.isKinematic = true;
		//Application.LoadLevel (3);
	}
}
