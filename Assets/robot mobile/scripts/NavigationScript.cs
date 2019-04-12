using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NavigationScript : MonoBehaviour {
    
    public BougeBaseMobile nav;
 
    public Vector3 increment_trans_VR;
    public Vector3 increment_rot_VR;
	// Use this for initialization
	void Start () {
		
	}
	
	

    // Update is called once per frame
    void Update()
    {

			nav.increment_rotation = increment_rot_VR;
			nav.increment_translation = increment_trans_VR;

    }
}
