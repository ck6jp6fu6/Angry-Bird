using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitSound : MonoBehaviour {
	
	public AudioSource hitmusic;

	void OnCollisionEnter2D (Collision2D collision) {
		//hitmusic.Play ();
	}
}
