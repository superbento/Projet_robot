using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClavierScript : MonoBehaviour {
	public MaterielScript materielScript;
	//public gere_bras bras_robot;
	public float inc_step = 0.01F;
	public float followme;
	// Use this for initialization
	void Start()
	{
		followme = 1F;
	}

	// Update is called once per frame
	void Update()
	{
		string aux;
		Vector3 new_increment;
		//inc_step = followme * inc_step;
		//bool key_ok = false;
		if (Input.GetKey(KeyCode.UpArrow))
		{
			new_increment = new Vector3(followme *inc_step, 0.0F, 0.0F);
		}
		else if (Input.GetKey(KeyCode.DownArrow))
		{
			new_increment = new Vector3(-inc_step*followme , 0.0F, 0.0F);
		}
		else if (Input.GetKey(KeyCode.LeftArrow))
		{
			new_increment = new Vector3(0.0F, -inc_step*followme, 0.0F);

		}
		else if (Input.GetKey(KeyCode.RightArrow))
		{
			
			new_increment = new Vector3(0.0F, +inc_step*followme, 0.0F);
		}
		else if (Input.GetKey(KeyCode.A))
		{
			new_increment = new Vector3(0.0F, 0.0F, -inc_step*followme);
		}
		else if (Input.GetKey(KeyCode.Z))
		{
			new_increment = new Vector3(0.0F, 0.0F, inc_step*followme);
		}
		else
		{
			new_increment = new Vector3(0.0F, 0.0F, 0.0F);
			//Debug.Log("Touche incorrecte!");
		}
		if (new_increment.magnitude > 0.0F)
		{
			aux = "increment = [" + new_increment.x.ToString("0.0000") + "," + new_increment.y.ToString("0.0000") + "," + new_increment.z.ToString("0.0000") + "]";
			Debug.Log(aux);
			//bras_robot.increment = new_increment;
			materielScript.increment_IR = new_increment;
		}
	}
}
