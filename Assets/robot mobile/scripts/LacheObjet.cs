using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LacheObjet : MonoBehaviour {
	public PrendObjet prend;
	public GameObject opinceg;
	public GameObject Ocube;
	public GameObject opinced;
	public GameObject otool;
	public GameObject cube;
	public SelectionnableDistScript selectionalbe;
	public MaterielScript materiel;
	// Use this for initialization
	void Start () {
		//cube = GameObject.Find ("cube");
	}

	// Update is called once per frame
	void Update () {
		if (prend.iscatch == true) {
			if (selectionalbe.EstSelectionable == true && materiel.B3 == false) {
				opinced.transform.localPosition = new Vector3 (0.017F, 0.0F, 0.0F);
				opinceg.transform.localPosition = new Vector3 (-0.017F, 0.0F, 0.0F);
				//cube.transform.Translate (0.0F,0.0F,-cube.transform.position.y);
				//cube.transform.position=Ocube.transform.position;
				cube.transform.parent = null;
				cube.GetComponent<Rigidbody> ().useGravity = true;
				cube.GetComponent<Rigidbody> ().isKinematic = false;
				prend.iscatch = false;
			}
		}
	}
}
