using HanyangKiosk.Pages;
using HanyangKiosk.Utils;

using System;
using System.Globalization;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Threading;

namespace HanyangKiosk
{
    /// <summary>
    /// MainWindow.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class MainWindow : Window
    {
        #region PageType
        public enum PageType
        {
            Splash,
            Main
        }
        #endregion

        /// <summary>
        /// 현재 페이지의 타입을 확인하는 변수입니다.
        /// </summary>
        PageType NowPage { get; set; }

        /// <summary>
        /// 시간 텍스트를 새로고침하는 타이머입니다.
        /// </summary>
        DispatcherTimer timeTimer;

        #region 생성자
        public MainWindow(PageType type)
        {
            try
            {
                InitializeComponent();

                NavigatePage(type);

                timeTimer = new DispatcherTimer();
                timeTimer.Interval = TimeSpan.FromSeconds(1);
                timeTimer.Tick += TimeTimer_Tick;
                timeTimer.Start();
            }
            catch (Exception ex)
            {
                FileManager.WriteLog(string.Format("[예외] {0}\n - {1}", ex.Message, ex.StackTrace));
            }
        }
        #endregion

        #region TimeTimer_Tick
        /// <summary>
        /// Time 타이머 틱 마다 실행되는 이벤트입니다.
        /// </summary>
        private void TimeTimer_Tick(object sender, EventArgs e)
        {
            try
            {
                TimeText.Text = DateTime.Now.ToString("dddd, tt h시 m분", new CultureInfo("ko-KR"));
            }
            catch (Exception ex)
            {
                FileManager.WriteLog(string.Format("[예외] {0}\n - {1}", ex.Message, ex.StackTrace));
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
            try
            {
                if (e.Key == Key.System && e.SystemKey == Key.F4) e.Handled = true;
            }
            catch (Exception ex)
            {
                FileManager.WriteLog(string.Format("[예외] {0}\n - {1}", ex.Message, ex.StackTrace));
            }
        }
        #endregion

        #region NavigatePage
        /// <summary>
        /// 페이지를 이동합니다.
        /// </summary>
        /// <param name="type">이동할 페이지의 타입입니다.</param>
        public void NavigatePage(PageType type)
        {
            try
            {
                NowPage = type;

                switch (type)
                {
                    case PageType.Splash:
                        Background = new ImageBrush(GetBitmapImage("Resources/Image/SplashPage/Background.png"));
                        MainFrame.Navigate(new SplashPage());
                        
                        SetSidebar(false);
                        break;
                    case PageType.Main:
                        Background = new ImageBrush(GetBitmapImage("Resources/Image/SplashPage/Background.png"));
                        MainFrame.NavigationService.RemoveBackEntry();
                        MainFrame.Navigate(new MainPage());

                        SetSidebar(true, false);
                        SpreadSidebarAnimation(LeftPanel);
                        SpreadSidebarAnimation(RightPanel);
                        break;
                }
            }
            catch (Exception ex)
            {
                FileManager.WriteLog(string.Format("[예외] {0}\n - {1}", ex.Message, ex.StackTrace));
            }
        }
        #endregion

        #region SetSidebar
        /// <summary>
        /// 사이드바를 사용 또는 사용 하지 않습니다.
        /// </summary>
        /// <param name="value">사이드바 사용 여부입니다.</param>
        /// <param name="button">사이드바 접기/펼치기 버튼 사용 여부입니다.</param>
        private void SetSidebar(bool value, bool button = true)
        {
            try
            {
                if (value)
                {
                    if (SidebarPanel.Visibility == Visibility.Hidden) SidebarPanel.Visibility = Visibility.Visible;
                    if (!SidebarPanel.IsEnabled) SidebarPanel.IsEnabled = true;

                    if(button)
                    {
                        if (LeftSidebarButton.Visibility == Visibility.Hidden) LeftSidebarButton.Visibility = Visibility.Visible;
                        if (RightSidebarButton.Visibility == Visibility.Hidden) RightSidebarButton.Visibility = Visibility.Visible;
                        if (!LeftSidebarButton.IsEnabled) LeftSidebarButton.IsEnabled = true;
                        if (!RightSidebarButton.IsEnabled) RightSidebarButton.IsEnabled = true;
                    }
                    else
                    {
                        if (LeftSidebarButton.Visibility == Visibility.Visible) LeftSidebarButton.Visibility = Visibility.Hidden;
                        if (RightSidebarButton.Visibility == Visibility.Visible) RightSidebarButton.Visibility = Visibility.Hidden;
                        if (LeftSidebarButton.IsEnabled) LeftSidebarButton.IsEnabled = false;
                        if (RightSidebarButton.IsEnabled) RightSidebarButton.IsEnabled = false;
                    }
                }
                else
                {
                    if (SidebarPanel.Visibility == Visibility.Visible) SidebarPanel.Visibility = Visibility.Hidden;
                    if (SidebarPanel.IsEnabled) SidebarPanel.IsEnabled = false;
                }
            }
            catch (Exception ex)
            {
                FileManager.WriteLog(string.Format("[예외] {0}\n - {1}", ex.Message, ex.StackTrace));
            }
        }
        #endregion

        #region BackPage
        /// <summary>
        /// 이전 페이지로 이동합니다.
        /// <para>이전 페이지가 Splash 페이지일 경우 이동하지 않습니다.</para>
        /// </summary>
        public void BackPage()
        {
            try
            {
                JournalEntry last = null;
                foreach (JournalEntry item in MainFrame.BackStack) last = item;

                if (last.Name != "SplashPage") MainFrame.GoBack();
            }
            catch (Exception ex)
            {
                FileManager.WriteLog(string.Format("[예외] {0}\n - {1}", ex.Message, ex.StackTrace));
            }
        }
        #endregion

        #region GetBitmapImage
        /// <summary>
        /// 이미지의 BitmapImage를 가져옵니다.
        /// </summary>
        /// <param name="path">가져올 이미지의 경로입니다.</param>
        private BitmapImage GetBitmapImage(string path)
        {
            try
            {
                return new BitmapImage(new Uri(BaseUriHelper.GetBaseUri(this), path));
            }
            catch (Exception ex)
            {
                FileManager.WriteLog(string.Format("[예외] {0}\n - {1}", ex.Message, ex.StackTrace));
                return null;
            }
        }
        #endregion

        #region SidebarAnimation
        /// <summary>
        /// 사이드바 접기 애니메이션입니다.
        /// </summary>
        private void FoldingSidebarAnimation(Grid panel)
        {
            try
            {
                TranslateTransform trans = new TranslateTransform();
                panel.RenderTransform = trans;

                var animation = new DoubleAnimation()
                {
                    From = 0,
                    To = panel.Name == "LeftPanel" ? -415 : 415,
                    Duration = TimeSpan.FromMilliseconds(750),
                    EasingFunction = new QuadraticEase()
                };

                trans.BeginAnimation(TranslateTransform.XProperty, animation);
            }
            catch (Exception ex)
            {
                FileManager.WriteLog(string.Format("[예외] {0}\n - {1}", ex.Message, ex.StackTrace));
            }
        }

        /// <summary>
        /// 사이드바 펼치기 애니메이션입니다.
        /// </summary>
        private void SpreadSidebarAnimation(Grid panel)
        {
            try
            {
                TranslateTransform trans = new TranslateTransform();
                panel.RenderTransform = trans;

                var animation = new DoubleAnimation()
                {
                    From = panel.Name == "LeftPanel" ? -415 : 415,
                    To = 0,
                    Duration = TimeSpan.FromMilliseconds(750),
                    EasingFunction = new QuadraticEase()
                };

                trans.BeginAnimation(TranslateTransform.XProperty, animation);
            }
            catch (Exception ex)
            {
                FileManager.WriteLog(string.Format("[예외] {0}\n - {1}", ex.Message, ex.StackTrace));
            }
        }
        #endregion

        #region LeftSidebarButton_Click
        /// <summary>
        /// 왼쪽 사이드바 화살표 버튼 클릭 시 실행되는 이벤트입니다.
        /// <para>왼쪽 사이드바를 접거나 펼칩니다.</para>
        /// <para>Tag가 1일 경우 펼친 상태, 0일 경우 접힌 상태</para>
        /// </summary>
        private void LeftSidebarButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                int tag = Convert.ToInt32(LeftSidebarButton.Tag);

                if (tag == 1)
                {
                    LeftSidebarButton_Image.Source = GetBitmapImage("Resources/Icon/Right_Button.png");
                    FoldingSidebarAnimation(LeftPanel);
                    LeftSidebarButton.Tag = 0;
                }
                else if(tag == 0)
                {
                    LeftSidebarButton_Image.Source = GetBitmapImage("Resources/Icon/Left_Button.png");
                    SpreadSidebarAnimation(LeftPanel);
                    LeftSidebarButton.Tag = 1;
                }
            }
            catch (Exception ex)
            {
                FileManager.WriteLog(string.Format("[예외] {0}\n - {1}", ex.Message, ex.StackTrace));
            }
        }
        #endregion

        #region RightSidebarButton_Click
        /// <summary>
        /// 오른쪽 사이드바 화살표 버튼 클릭 시 실행되는 이벤트입니다.
        /// <para>오른쪽 사이드바를 접거나 펼칩니다.</para>
        /// <para>Tag가 1일 경우 펼친 상태, 0일 경우 접힌 상태</para>
        /// </summary>
        private void RightSidebarButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                int tag = Convert.ToInt32(RightSidebarButton.Tag);

                if (tag == 1)
                {
                    RightSidebarButton_Image.Source = GetBitmapImage("Resources/Icon/Left_Button.png");
                    FoldingSidebarAnimation(RightPanel);
                    RightSidebarButton.Tag = 0;
                }
                else if (tag == 0)
                {
                    RightSidebarButton_Image.Source = GetBitmapImage("Resources/Icon/Right_Button.png");
                    SpreadSidebarAnimation(RightPanel);
                    RightSidebarButton.Tag = 1;
                }
            }
            catch (Exception ex)
            {
                FileManager.WriteLog(string.Format("[예외] {0}\n - {1}", ex.Message, ex.StackTrace));
            }
        }
        #endregion

        #region Window_MouseDown
        /// <summary>
        /// 창을 클릭했을 때 실행되는 이벤트입니다.
        /// <para>사이드바를 클릭했을 땐 실행되지 않습니다.</para>
        /// </summary>
        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            try
            {
                if (NowPage == PageType.Main || Convert.ToInt32(LeftPanel.Tag) == 1 || Convert.ToInt32(RightPanel.Tag) == 1) return;

                if (Convert.ToInt32(LeftSidebarButton.Tag) == 1)
                {
                    LeftSidebarButton_Image.Source = GetBitmapImage("Resources/Icon/Right_Button.png");
                    FoldingSidebarAnimation(LeftPanel);
                    LeftSidebarButton.Tag = 0;
                }

                if (Convert.ToInt32(RightSidebarButton.Tag) == 1)
                {
                    RightSidebarButton_Image.Source = GetBitmapImage("Resources/Icon/Left_Button.png");
                    FoldingSidebarAnimation(RightPanel);
                    RightSidebarButton.Tag = 0;
                }
            }
            catch (Exception ex)
            {
                FileManager.WriteLog(string.Format("[예외] {0}\n - {1}", ex.Message, ex.StackTrace));
            }
        }
        #endregion

        #region LeftPanel_Mouse Enter/Leave
        private void LeftPanel_MouseEnter(object sender, MouseEventArgs e) { LeftPanel.Tag = 1; }

        private void LeftPanel_MouseLeave(object sender, MouseEventArgs e) { LeftPanel.Tag = 0; }
        #endregion

        #region RightPanel_Mouse Enter/Leave
        private void RightPanel_MouseEnter(object sender, MouseEventArgs e) { RightPanel.Tag = 1; }

        private void RightPanel_MouseLeave(object sender, MouseEventArgs e) { RightPanel.Tag = 0; }
        #endregion
    }
}