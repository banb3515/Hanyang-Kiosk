using HanyangKiosk.Properties;

using System;
using System.Diagnostics;
using System.IO;

namespace HanyangKiosk.Utils
{
    /// <summary>
    /// 파일 입출력에 관한 기능을 사용할 수 있습니다.
    /// </summary>
    public class FileManager
    {
        #region WriteLog
        /// <summary>
        /// 파일에 로그를 기록합니다.
        /// <para>파일 경로: Settings.DataPath\logs\현재 년도Y\현재 달M</para>
        /// <para>파일명: 현재 일D.log</para>
        /// </summary>
        /// <param name="log">기록할 로그 내용 변수입니다.</param>
        public static void WriteLog(string log)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(Settings.Default.DataPath)) return;

                DateTime now = DateTime.Now;

                string folder = string.Format(@"{0}\logs\{1}Y\{2}M", Settings.Default.DataPath, now.ToString("yyyy"), now.ToString("MM"));

                if (!Directory.Exists(folder)) Directory.CreateDirectory(folder);
                string fileName = string.Format(@"{0}\{1}D.log", folder, now.ToString("dd"));

                log = string.Format("[{0}] {1}", now.ToString("HH:mm:ss"), log);

                StreamWriter sw = File.AppendText(fileName);
                sw.WriteLine(log);
                sw.Close();

                Debug.WriteLine(log);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(string.Format("[예외] IsRunning\n - {0}\n - {1}", ex.Message, ex.StackTrace));
            }
        }
        #endregion
    }
}
