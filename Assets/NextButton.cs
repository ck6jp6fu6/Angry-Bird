using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NextButton : MonoBehaviour {

	void Update(){
		if(Input.GetMouseButtonDown(0))
			Application.LoadLevel (4);
	}

}
