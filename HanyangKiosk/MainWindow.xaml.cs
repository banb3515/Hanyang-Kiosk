using System;
using System.Windows;
using System.Windows.Input;

namespace HanyangKiosk
{
    /// <summary>
    /// MainWindow.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class MainWindow : Window
    {
        #region 생성자
        public MainWindow()
        {
            InitializeComponent();

            if(!App.IsSplash)
            {
                MainFrame.Source = new Uri("Pages/SplashPage.xaml", UriKind.Relative);
                App.IsSplash = true;
            }
        }
        #endregion

        #region Window_KeyDown
        /// <summary>
        /// 키가 눌렸을 때 실행되는 이벤트입니다.
        /// <para>Alt + F4 키가 눌렸을 때 창이 닫히지 않도록 이벤트를 취소합니다.</para>
        /// </summary>
        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.System && e.SystemKey == Key.F4) e.Handled = true;
        }
        #endregion
    }
}
