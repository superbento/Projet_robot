using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToColor : MonoBehaviour {
	public GameObject gameObect;
	// Use this for initialization
	void Start () {
		gameObect = GameObject.Find("cube");
		toColor(Color.gray);
	}
	public void toColor(Color color){
		gameObject.GetComponent<Renderer>().material.color = color;

	}
	// Update is called once per frame
	void Update () {
		
	}
}
