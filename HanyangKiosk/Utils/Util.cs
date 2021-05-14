using System;
using System.Diagnostics;
using System.Linq;

namespace HanyangKiosk.Utils
{
    /// <summary>
    /// 여러 기능들을 사용할 수 있습니다.
    /// </summary>
    public class Util
    {
        #region IsRunning
        /// <summary>
        /// 동일 프로그램의 실행 상태를 확인합니다. (중복 실행 감지)
        /// </summary>
        /// <returns>
        /// 동일 프로그램이 실행 중일 시 True, 아닐 시 False를 반환합니다.
        /// <para>Debug 모드에서는 중복 실행이여도 False를 반환합니다.</para>
        /// </returns>
        public static bool IsRunning()
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
                FileManager.WriteLog($"[Exception] {ex.Message}\n - {ex.StackTrace}");
                return true;
            }
        }
        #endregion

        #region GetWeather
        /// <summary>
        /// 날씨 정보를 가져옵니다. 
        /// </summary>
        /// <returns>
        /// 날씨 정보를 성공적으로 가져오면 True, 가져오지 못하면 False를 반환합니다.
        /// </returns>
        public static bool GetWeather()
        {
            string url = "";

            try
            {
                int weather = 0;

                // 날씨 가져오는 코드 작성

                if (App.MainWindowInstance.Infos.ContainsKey("Weather"))
                    App.MainWindowInstance.Infos.Remove("Weather");
                App.MainWindowInstance.Infos.Add("Weather", weather);
                FileManager.WriteLog($"[Weather] 날씨 정보를 가져왔습니다. ({url})");
                return true;
            }
            catch (Exception ex)
            {
                FileManager.WriteLog($"[Weather] 날씨 정보를 가져오지 못했습니다. ({url}) [{ex.Message}]\n - {ex.StackTrace}");
                return false;
            }
        }
        #endregion

        #region GetFineDust
        /// <summary>
        /// 미세먼지 정보를 가져옵니다. 
        /// </summary>
        /// <returns>
        /// 미세먼지 정보를 성공적으로 가져오면 True, 가져오지 못하면 False를 반환합니다.
        /// </returns>
        public static bool GetFineDust()
        {
            string url = "";

            try
            {
                int fineDust = 0;

                // 미세먼지 가져오는 코드 작성

                if (App.MainWindowInstance.Infos.ContainsKey("FineDust"))
                    App.MainWindowInstance.Infos.Remove("FineDust");
                App.MainWindowInstance.Infos.Add("FineDust", fineDust);
                FileManager.WriteLog($"[FineDust] 미세먼지 정보를 가져왔습니다. ({url})");
                return true;
            }
            catch (Exception ex)
            {
                FileManager.WriteLog($"[FineDust] 미세먼지 정보를 가져오지 못했습니다. ({url}) [{ex.Message}]\n - {ex.StackTrace}");
                return false;
            }
        }
        #endregion
    }
}
