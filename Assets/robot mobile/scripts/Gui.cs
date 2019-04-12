using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gui : MonoBehaviour {
	public MaterielScript ma;
	public EnZoneDepot en;
	// Use this for initialization
	void Start () {
		
	}
	void OnGUI(){
		GUI.Box (new Rect (10, 10, 100, 140), "robot etat");
		if (ma.B1 == false) {
			GUI.Button(new Rect(20,40,80,20),"move mode");
		}
		if (ma.B1 == true) {
			GUI.Button(new Rect(20,40,80,20),"bras mode");
		}
		if (ma.B3 == true) {
			GUI.Button(new Rect(20,70,80,20),"catching");
		}
		if (en.EstEnZoneDepot == true) {
			GUI.Button(new Rect(20,110,80,20),"en zone");
		}
		GUI.Box (new Rect (690, 10, 240, 100), "hot key");
		GUI.Button(new Rect(700,35,200,20),"'mouse1':changer de la mode");
		GUI.Button(new Rect(700,60,200,20),"'mouse2':changer de la camera");
		GUI.Button(new Rect(700,85,200,20),"'mouse3':prendre de l'objet");

	}
	// Update is called once per frame
	void Update () {
		
	}
}
