using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Windows.Media;
using System.Xml;

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

        #region HexToBrush
        public static Brush HexToBrush(string hex)
        {
            return (Brush)new BrushConverter().ConvertFrom(hex);
        }
        #endregion

        #region GetWeather
        /// <summary>
        /// 날씨 정보를 가져옵니다. 
        /// </summary>
        /// <returns>
        /// 날씨 정보를 성공적으로 가져오면 True, 가져오지 못하면 False를 반환합니다.
        /// </returns>
        public static string GetWeather()
        {
            string result = "";
            string url = "http://www.kma.go.kr/wid/queryDFSRSS.jsp?zone=1114061500"; // 서울특별시 중구 신당동

            try
            {
                var request = (HttpWebRequest)WebRequest.Create(url);
                request.Method = "GET";

                using (var response = request.GetResponse() as HttpWebResponse)
                {
                    var reader = new StreamReader(response.GetResponseStream());
                    var xmlStr = reader.ReadToEnd();
                    var xmlDoc = new XmlDocument();
                    xmlDoc.LoadXml(xmlStr);
                    var xmlList = xmlDoc.SelectNodes("/rss/channel/item/description/body/data");
                    var xml = xmlList[xmlList.Count - 1];
                    var temp = (int)Convert.ToDouble(xml["temp"].InnerText);
                    var state = xml["wfEn"].InnerText.Replace(' ', '_').Replace('/', '_');
                    result = $"{temp}:{state}";

                    FileManager.WriteLog($"[Weather] 날씨 정보를 가져왔습니다. {temp} ℃, {xml["wfKor"].InnerText}\n - ({url})");
                }
            }
            catch (Exception ex)
            {
                FileManager.WriteLog($"[Weather] 날씨 정보를 가져오지 못했습니다. ({url}) [{ex.Message}]\n - {ex.StackTrace}");
            }

            return result;
        }
        #endregion

        #region GetFineDust
        /// <summary>
        /// 미세먼지 정보를 가져옵니다. 
        /// </summary>
        /// <returns>
        /// 미세먼지 정보를 성공적으로 가져오면 True, 가져오지 못하면 False를 반환합니다.
        /// </returns>
        public static int GetFineDust()
        {
            int result = 0;
            string url = "http://openapi.seoul.go.kr:8088/526351415162616e3237706e4a4b6b/xml/RealtimeCityAir/1/5/도심권/중구"; // 서울특별시 중구 신당동

            try
            {
                var request = (HttpWebRequest)WebRequest.Create(url);
                request.Method = "GET";

                using (var response = request.GetResponse() as HttpWebResponse)
                {
                    var reader = new StreamReader(response.GetResponseStream());
                    var xmlStr = reader.ReadToEnd();
                    var xmlDoc = new XmlDocument();
                    xmlDoc.LoadXml(xmlStr);
                    var xmlList = xmlDoc.SelectNodes("/RealtimeCityAir/row");
                    var xml = xmlList[0];
                    result = Convert.ToInt32(xml["PM10"].InnerText);

                    FileManager.WriteLog($"[FineDust] 미세먼지 정보를 가져왔습니다. {result} ㎍/m³\n - ({url})");
                }
            }
            catch (Exception ex)
            {
                FileManager.WriteLog($"[FineDust] 미세먼지 정보를 가져오지 못했습니다. ({url}) [{ex.Message}]\n - {ex.StackTrace}");
            }

            return result;
        }
        #endregion
    }
}
