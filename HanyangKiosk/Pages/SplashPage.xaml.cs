using HanyangKiosk.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace HanyangKiosk.Pages
{
    /// <summary>
    /// SplashPage.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class SplashPage : Page
    {
        /// <summary>
        /// 로딩 중 입니다 . . . 애니메이션 타이머입니다.
        /// </summary>
        DispatcherTimer textTimer;
        int count = 0;
        string text = "로딩 중 입니다 . . .";

        #region 생성자
        public SplashPage()
        {
            InitializeComponent();
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
                textTimer = new DispatcherTimer();
                textTimer.Interval = TimeSpan.FromMilliseconds(100);
                textTimer.Tick += TextTimer_Tick;
                textTimer.Start();

                Init();
            }
            catch (Exception ex)
            {
                FileManager.WriteLog(string.Format("[예외] {0}\n - {1}", ex.Message, ex.StackTrace));
            }
        }
        #endregion

        #region TextTimer_Tick
        /// <summary>
        /// 타이머 0.1초가 지날 때마다 실행되는 이벤트입니다.
        /// </summary>
        private void TextTimer_Tick(object sender, EventArgs e)
        {
            try
            {
                if (count > 13) textTimer.Stop();
                else LoadingText.Text += text[count++];
            }
            catch (Exception ex)
            {
                FileManager.WriteLog(string.Format("[예외] {0}\n - {1}", ex.Message, ex.StackTrace));
            }
        }
        #endregion

        #region Init
        /// <summary>
        /// 설정들을 초기화하는 함수입니다.
        /// </summary>
        private void Init()
        {
            // 초기화 코드 작성
        }
        #endregion
    }
}
