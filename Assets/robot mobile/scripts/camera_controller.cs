using UnityEngine;
using System.Collections;

public class camera_controller : MonoBehaviour {
	
	public Camera exocentrique1;
	public Camera exocentrique2;
	public Camera egocentrique;
	private Camera[] liste;
	private int courante;
	public MaterielScript ms;
	// Use this for initialization
	void Start () {
		exocentrique1.enabled = true;
		exocentrique2.enabled = false;
		egocentrique.enabled = false;
		liste = new Camera[3];
		liste[0] = exocentrique1;
		liste[1] = exocentrique2;
		liste[2] = egocentrique;
		courante = 0;
	}
	
	void Switch()
	{
		liste[courante%3].enabled = false;
		courante++;
		liste[courante%3].enabled = true;
	}
	
	// Update is called once per frame
	void Update () {
		if(ms.B2==true)
		{
		   Switch();
		}
		ms.B2 = false;
	}
}
