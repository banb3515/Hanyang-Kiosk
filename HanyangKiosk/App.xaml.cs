using HanyangKiosk.Popup;
using HanyangKiosk.Properties;
using HanyangKiosk.Utils;

using System;
using System.Reflection;
using System.Windows;
using static HanyangKiosk.MainWindow;

namespace HanyangKiosk
{
    /// <summary>
    /// App.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class App : Application
    {
        public static MainWindow MainWindowInstance { get; set; }

        /// <summary>
        /// 현재 프로그램 이름입니다.
        /// <para>프로그램 이름 변경: Properties > 어셈블리 이름</para>
        /// </summary>
        public static string ProgramName { get; } = Assembly.GetExecutingAssembly().GetName().Name;

        #region Application_Startup
        /// <summary>
        /// 프로그램이 시작될 때 실행되는 이벤트입니다.
        /// </summary>
        private void Application_Startup(object sender, StartupEventArgs e)
        {
            try
            {
                #region 중복 실행 감지, 중복 실행 시 프로그램 실행 안함
                if (Function.IsAlreadyRunning())
                {
                    MessageBox.Show("프로그램이 이미 실행 중입니다.", ProgramName, MessageBoxButton.OK, MessageBoxImage.Warning);
                    Environment.Exit(0);
                }
                #endregion

                #region 데이터 폴더 지정
                if (Settings.Default.DataPath == "")
                {
                    SelectDataPathPopup dataPathPopup = new SelectDataPathPopup();

                    if (dataPathPopup.ShowDialog().Value)
                    {
                        if (dataPathPopup != null) dataPathPopup.Close();

                        FileManager.WriteLog(string.Format("데이터 폴더 경로를 지정하였습니다. ({0})", Settings.Default.DataPath));
                    }
                    else
                    {
                        if (dataPathPopup != null) dataPathPopup.Close();

                        MessageBox.Show("데이터 폴더 경로를 지정하지 않으면 프로그램을 실행할 수 없습니다.", ProgramName, MessageBoxButton.OK, MessageBoxImage.Warning);

                        Environment.Exit(0);
                    }

                    Settings.Default.Save();
                }
                #endregion

                Current.ShutdownMode = ShutdownMode.OnMainWindowClose;

                MainWindowInstance = new MainWindow(PageType.Splash);
                Current.MainWindow = MainWindowInstance;
                Current.MainWindow.Show();

                FileManager.WriteLog(string.Format("{0} 실행", ProgramName));
            }
            catch (Exception ex)
            {
                FileManager.WriteLog(string.Format("[예외] {0}\n - {1}", ex.Message, ex.StackTrace));
            }
        }
        #endregion
    }
}
