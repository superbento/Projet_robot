using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectionnableDistScript : MonoBehaviour {
	public bool EstSelectionable;
	public gere_bras gere_bras;
	public GameObject gameObject;
	public ToColor toColor;
	// Use this for initialization
	void Start () {
		EstSelectionable = false;
		//gameObject = GameObject.Find ("Ocube");
	}
	public void OnTriggerEnter(Collider other){
		if (other.name == "O7") {
			EstSelectionable = true;
			toColor.toColor (Color.yellow);
		}
		if (other.name == "depot") {
			
			toColor.toColor (Color.red);
		}
	}
	public void OnTriggerStay(Collider other){
		if (other.name == "O7") {
			EstSelectionable = true;
			toColor.toColor (Color.yellow);
		}
		if (other.name == "depot") {

			toColor.toColor (Color.red);
		}
	}
	public void OnTriggerExit(Collider other){
		if (other.name == "O7") {
			EstSelectionable = false;
			toColor.toColor (Color.gray);
		}
		if (other.name == "depot") {

			toColor.toColor (Color.red);
		}
	}
	// Update is called once per frame
	void Update () {
	/*	float dis = Vector3.Distance (gere_bras.Opince.transform.position,gameObject.transform.position);
		if (dis < 0.1) {
			EstSelectionable = true;
			toColor.toColor (Color.yellow);
		} else {
			EstSelectionable = false;
			toColor.toColor (Color.gray);
		}*/

	}
}
