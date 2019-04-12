using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenereObjetsScript : MonoBehaviour {
	
	public GameObject Ocube;
	public List<GameObject> cubes;
	// Use this for initialization
	void Start () {
		Debug.Log (Random.Range(-7F,7F));
	    Vector3 o0v3 = new Vector3 (Random.Range(-7F,7F),0F,Random.Range(-7F,7F));
		Vector3 depotv3 = new Vector3 (Random.Range(-7F,7F),0F,Random.Range(-7F,7F));
		Vector3 ocv3 = new Vector3 (Random.Range(-7F,7F),0F,Random.Range(-7F,7F));
		/*O0.transform.Translate (o0v3);
		Ocube.transform.Translate (ocv3);
		Depot.transform.Translate (depotv3);*/
	
		for (int i = 0; i < 10; i++) {
			Vector3 v0 = new Vector3(Random.Range(-7F,7F),0F,Random.Range(-7F,7F));
			GameObject g1 = GameObject.Instantiate (Ocube,v0,Quaternion.identity);
			cubes.Add (g1);
		}

		cubes.Add (Ocube);
	}

	// Update is called once per frame
	void Update () {
		
	}
}
