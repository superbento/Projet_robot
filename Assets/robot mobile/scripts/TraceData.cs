using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TraceData : MonoBehaviour {
	private static ILogger logger = Debug.logger;
	private static string kTAG = "MyGameTag";
	private MyFileLogHandler myFileLogHandler;

	void Start()
	{ 
		Debug.Log(Application.persistentDataPath);
		myFileLogHandler = new MyFileLogHandler();

		logger.Log(kTAG, "MyGameClass Start.");
	}
}
