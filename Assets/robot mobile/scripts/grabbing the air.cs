using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class grabbingtheair : MonoBehaviour {
	public gere_bras gere_bras = null;
	public Vector3 increment;
	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update () {
		gere_bras.increment = increment;
	}
}
