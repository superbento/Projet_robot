using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GéraCameras : MonoBehaviour {

	public float distanceAway=1.7f;			
	public float distanceUp=1.3f;			
	public float smooth=2f;				
	public Camera exo;
	public Camera ego1;
	public Camera ego2;
	private Vector3 m_TargetPosition;		
	private Vector3 m_TargetPosition2;
	Transform follow;        
	Transform follow2;
	void Start(){
		follow = GameObject.Find ("Otool").transform;
		follow2 = GameObject.Find ("Otool").transform;


	}

	void LateUpdate ()
	{
		
		m_TargetPosition = follow.position + Vector3.up * distanceUp + follow.forward * distanceAway;

	
		exo.transform.position = Vector3.Lerp(exo.transform.position, m_TargetPosition, Time.deltaTime * smooth);

	
		exo.transform.LookAt(follow);

		//-------------------------------------------------------------------------------
		m_TargetPosition2 = follow2.position+Vector3.up*0.4F+ follow.forward *0.3F;

	
		ego1.transform.position = Vector3.Lerp(ego1.transform.position, m_TargetPosition2, Time.deltaTime * smooth);

	
		ego1.transform.LookAt(follow2);

		ego2.transform.position = Vector3.Lerp(ego1.transform.position, m_TargetPosition2, Time.deltaTime * smooth);

		ego2.transform.LookAt(follow2);


	}

		
}
