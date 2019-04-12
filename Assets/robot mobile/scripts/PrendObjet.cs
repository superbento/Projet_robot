using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrendObjet : MonoBehaviour {
	public bool iscatch;
	public GameObject opinceg;
	public GameObject opinced;
	public GameObject otool;
	public GameObject cube;
	public SelectionnableDistScript selectionalbe;
	public MaterielScript materiel;
	// Use this for initialization
	void Start () {
		//cube = GameObject.Find ("cube");
		iscatch=false;
	}
	
	// Update is called once per frame
	void Update () {
		if (selectionalbe.EstSelectionable == true && materiel.B3==true) {
			opinced.transform.localPosition = new Vector3 (0.0F,0.0F,0.0F);
			opinceg.transform.localPosition = new Vector3 (0.0F, 0.0F, 0.0F);
			//selectionalbe.gameObject.transform.parent = otool.transform;
			//selectionalbe.gameObject.GetComponent<Rigidbody> ().useGravity = false;
			cube.transform.parent = otool.transform;
			cube.GetComponent<Rigidbody> ().useGravity = false;
		//	cube.GetComponent<Rigidbody> ().isKinematic = true;
			iscatch = true;
		}
		if (selectionalbe.EstSelectionable == false && materiel.B3==true) {
			cube.transform.parent = null;
			iscatch = false;
	//sss		materiel.B3 = false;
			cube.GetComponent<Rigidbody> ().useGravity = true;
		//	cube.GetComponent<Rigidbody> ().isKinematic = false;
		}
	}
}
