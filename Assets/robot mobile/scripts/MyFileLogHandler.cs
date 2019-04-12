using System.Collections;
using UnityEngine;
using System.IO;
using System;

public class MyFileLogHandler : ILogHandler {

	private FileStream m_FileStream;
	private StreamWriter m_StreamWriter;
	private ILogHandler m_DefaultLogHandler = Debug.logger.logHandler;

	public MyFileLogHandler()
	{
		string filePath = Application.persistentDataPath + "/log.dat";

		m_FileStream = new FileStream(filePath, FileMode.OpenOrCreate, FileAccess.ReadWrite);
		m_StreamWriter = new StreamWriter(m_FileStream);

		// Replace the default debug log handler
		Debug.logger.logHandler = this;
	}

	public void LogFormat(LogType logType, UnityEngine.Object context, string format, params object[] args)
	{
		m_StreamWriter.WriteLine(String.Format(format, args));
		m_StreamWriter.Flush();
		m_DefaultLogHandler.LogFormat(logType, context, format, args);
	}

	public void LogException(Exception exception, UnityEngine.Object context)
	{
		m_DefaultLogHandler.LogException(exception, context);
	}
}
