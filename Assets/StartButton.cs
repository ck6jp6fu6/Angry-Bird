using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartButton : MonoBehaviour {

	void Update(){
		if(Input.GetMouseButtonDown(0))
			Application.LoadLevel (1);
	}

	/*void OnMouseUp(){
		Debug.Log ("Dray");
		Application.LoadLevel (1);
	}*/
}
