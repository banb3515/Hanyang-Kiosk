using HanyangKiosk.Utils;

using System;
using System.Windows;
using System.Windows.Controls;

using PageType = HanyangKiosk.Models.PageModel.PageType;

namespace HanyangKiosk.Pages
{
    /// <summary>
    /// SplashPage.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class SplashPage : Page
    {
        #region 생성자
        public SplashPage()
        {
            try
            {
                InitializeComponent();
            }
            catch (Exception ex)
            {
                FileManager.WriteLog($"[Exception] {ex.Message}\n - {ex.StackTrace}");
            }
        }
        #endregion

        #region Page_Loaded
        /// <summary>
        /// 페이지가 로드 되었을 때 발생하는 이벤트입니다.
        /// </summary>
        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                Init();
            }
            catch (Exception ex)
            {
                FileManager.WriteLog($"[Exception] {ex.Message}\n - {ex.StackTrace}");
            }
        }
        #endregion

        #region Init
        /// <summary>
        /// 설정들을 초기화하는 함수입니다.
        /// </summary>
        private void Init()
        {
            try
            {
                Util.GetWeather();
                Util.GetFineDust();

                App.MainWindowInstance.NavigatePage(PageType.Main);
            }
            catch (Exception ex)
            {
                FileManager.WriteLog($"[Exception] {ex.Message}\n - {ex.StackTrace}");
            }
        }
        #endregion
    }
}
