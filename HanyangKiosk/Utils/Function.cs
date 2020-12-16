using System;
using System.Diagnostics;
using System.Linq;

namespace HanyangKiosk.Utils
{
    /// <summary>
    /// 여러 기능들을 사용할 수 있습니다.
    /// </summary>
    public class Function
    {
        #region IsAlreadyRunning
        /// <summary>
        ///  중복 실행 여부를 확인합니다.
        /// </summary>
        /// <returns>
        /// 중복 실행 시 True, 아닐 시 False를 반환합니다.
        /// <para>Debug 모드에서는 중복 실행이여도 False를 반환합니다.</para>
        /// </returns>
        public static bool IsAlreadyRunning()
        {
            try
            {
                int result = 0;

                Process.GetProcesses().ToList().ForEach(x =>
                {
                    if (x.ProcessName == Process.GetCurrentProcess().ProcessName)
                        result++;
                });
                return !Debugger.IsAttached && result > 1;
            }
            catch (Exception ex)
            {
                FileManager.WriteLog(string.Format("[예외] {0}\n - {1}", ex.Message, ex.StackTrace));
                return true;
            }
        }
        #endregion
    }
}
