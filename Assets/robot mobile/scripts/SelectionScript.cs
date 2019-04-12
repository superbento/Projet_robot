using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectionScript : MonoBehaviour {
	public ManipulationScipt mani;
	public MaterielScript materielSCript=null;
	public NavigationScript navi = null;
	public Vector3 increment_IR;
	public Vector3 increment_VR;
    // Use this for initialization
    void Start () {
        increment_IR = new Vector3(0.0F, 0.0F, 0.0F);
		increment_VR = new Vector3(0.0F, 0.0F, 0.0F);
	}
	
	// Update is called once per frame
	void Update () {
		increment_IR = materielSCript.increment_IR;
		if (materielSCript.B1 == true) {
			

			mani.increment = increment_IR;
			//increment_IR=new Vector3(0.0F, 0.0F, 0.0F);	
		}else{
			navi.increment_rot_VR=increment_IR;
			navi.increment_trans_VR = increment_IR;
		}
	}
			
}
