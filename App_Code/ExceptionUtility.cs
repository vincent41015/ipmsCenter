using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;

/// <summary>
/// ExceptionUtility 的摘要描述
/// </summary>
public class ExceptionUtility
{
	public ExceptionUtility()
	{
		//
		// TODO: 在此加入建構函式的程式碼
		//
	}

    public static void LogException(Exception exc, string source)
    {
        // Include enterprise logic for logging exceptions 
        // Get the absolute path to the log file 
        string logFile = "~/App_Data/ErrorLog_" + DateTime.Today.ToString("yyyyMMdd") + ".txt";
        logFile = HttpContext.Current.Server.MapPath(logFile);

        // Open the log file for append and write the log
        StreamWriter sw = new StreamWriter(logFile, true);
        sw.WriteLine("********** {0} **********", DateTime.Now);
        if (exc.InnerException != null)
        {
            sw.Write("Inner Exception Type: ");
            sw.WriteLine(exc.InnerException.GetType().ToString());
            sw.Write("Inner Exception: ");
            sw.WriteLine(exc.InnerException.Message);
            sw.Write("Inner Source: ");
            sw.WriteLine(exc.InnerException.Source);
            if (exc.InnerException.StackTrace != null)
            {
                sw.WriteLine("Inner Stack Trace: ");
                sw.WriteLine(exc.InnerException.StackTrace);
            }
        }
        sw.Write("Exception Type: ");
        sw.WriteLine(exc.GetType().ToString());
        sw.WriteLine("Exception: " + exc.Message);
        sw.WriteLine("Source: " + source);
        sw.WriteLine("Stack Trace: ");
        if (exc.StackTrace != null)
        {
            sw.WriteLine(exc.StackTrace);
            sw.WriteLine();
        }
        sw.Close();
    }
}