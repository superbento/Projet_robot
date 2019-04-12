using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaterielScript : MonoBehaviour {
    public Vector3 increment_IR;

    public bool B1;
    public bool B2;
    public bool B3;
    // Use this for initialization
    void Start () {
        increment_IR = new Vector3(0.0F, 0.0F, 0.0F);
        B1 = false;
        B2 = false;
        B3 = false;
    }
	
	// Update is called once per frame
	void Update () {
		increment_IR = new Vector3(0.0F, 0.0F, 0.0F);
	}

}