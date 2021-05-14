using HanyangKiosk.Properties;

using System;
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

                string folder = $@"{Settings.Default.DataPath}\logs\{now:yyyy}Y\{now.ToString("MM")}M";

                if (!Directory.Exists(folder)) Directory.CreateDirectory(folder);
                string fileName = $@"{folder}\{now:dd}D.log";

                log = $"[{now:HH:mm:ss}] {log}";

                StreamWriter sw = File.AppendText(fileName);
                sw.WriteLine(log);
                sw.Close();

                Console.WriteLine(log);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[Exception] {ex.Message}\n - {ex.StackTrace}");
            }
        }
        #endregion
    }
}
