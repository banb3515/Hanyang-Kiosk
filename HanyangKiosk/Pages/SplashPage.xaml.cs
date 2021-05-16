using HanyangKiosk.Utils;

using System;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;

using PageType = HanyangKiosk.Models.PageModel.PageType;

namespace HanyangKiosk.Pages
{
    /// <summary>
    /// SplashPage.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class SplashPage : Page
    {
        #region 생성자
        public SplashPage(Window mainWindow)
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
                new Thread(() =>
                {
                    Dispatcher.Invoke(DispatcherPriority.Normal, new Action(delegate
                    {
                        var weather = Util.GetWeather().Split(':');
                        var fineDust = Util.GetFineDust();

                        App.MainWindowInstance.SetWeather(Convert.ToInt32(weather[0]), weather[1]);
                        App.MainWindowInstance.SetFineDust(fineDust);

                        App.MainWindowInstance.NavigatePage(PageType.Main);
                    }));
                }).Start();
            }
            catch (Exception ex)
            {
                FileManager.WriteLog($"[Exception] {ex.Message}\n - {ex.StackTrace}");
            }
        }
        #endregion
    }
}
