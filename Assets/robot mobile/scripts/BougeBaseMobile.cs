using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BougeBaseMobile : MonoBehaviour {
    public Vector3 increment_translation;
    public Vector3 increment_rotation;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        if(increment_translation.magnitude > 0)
        {
			//transform.position += transform.up * increment_translation.x;
			//transform.position+=new Vector3(increment_translation.x,0,0);
			transform.Translate (new Vector3(increment_translation.x,0,0),Space.Self);
            increment_translation = new Vector3(0.0F, 0.0F, 0.0F);
        }
        if(increment_rotation.magnitude > 0)
        {
			transform.Rotate (new Vector3(0,increment_rotation.y*100,0),Space.Self);
            increment_rotation = new Vector3(0.0F, 0.0F, 0.0F);
        }
        //if(increment_translation.magni)
	}
}
