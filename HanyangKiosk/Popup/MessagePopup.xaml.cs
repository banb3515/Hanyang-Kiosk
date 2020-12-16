using HanyangKiosk.Utils;

using System;
using System.Windows;
using System.Windows.Media.Imaging;

namespace HanyangKiosk.Popup
{
    /// <summary>
    /// 메시지 팝업창입니다.
    /// </summary>
    public partial class MessagePopup : Window
    {
        #region MessagePopupIcon
        /// <summary>
        /// 아이콘 타입입니다.
        /// </summary>
        public enum MessagePopupIcon
        {
            Default,
            Warning
        }
        #endregion

        #region MessagePopupType
        /// <summary>
        /// 팝업창 타입입니다.
        /// </summary>
        public enum MessagePopupType
        {
            Yes, // 예
            YesNo, // 예, 아니요
            Ok, // 확인
            OkCancel // 확인, 취소
        }
        #endregion

        #region 생성자
        /// <summary>
        /// 메시지 상자를 표시합니다.
        /// </summary>
        /// <param name="content">표시할 내용입니다. (필수 입력)</param>
        /// <param name="caption">표시할 제목입니다. (기본 값: null)</param>
        /// <param name="icon">아이콘 타입입니다. (기본 값: Default)</param>
        /// <param name="type">메시지 팝업 타입입니다. (기본 값: Ok)</param>
        public MessagePopup(string content, string caption = null, MessagePopupIcon icon = MessagePopupIcon.Default, MessagePopupType type = MessagePopupType.Ok)
        {
            try
            {
                InitializeComponent();

                MessageContent.Text = content;

                if (caption == null) Caption.Text = App.ProgramName;
                else Caption.Text = caption;

                switch (icon)
                {
                    case MessagePopupIcon.Default:
                        MessageIcon.Source = new BitmapImage(new Uri(@"/Hanyang Kiosk;component/Resources/Icon/Information.png", UriKind.Relative));
                        break;
                    case MessagePopupIcon.Warning:
                        MessageIcon.Source = new BitmapImage(new Uri(@"/Hanyang Kiosk;component/Resources/Icon/Warning.png", UriKind.Relative));
                        break;
                }

                switch (type)
                {
                    case MessagePopupType.Yes:
                        Button1_Border.Visibility = Visibility.Hidden;
                        Button2.Content = "예";
                        break;
                    case MessagePopupType.YesNo:
                        Button1.Content = "아니요";
                        Button2.Content = "예";
                        break;
                    case MessagePopupType.Ok:
                        Button1_Border.Visibility = Visibility.Hidden;
                        Button2.Content = "확인";
                        break;
                    case MessagePopupType.OkCancel:
                        Button1.Content = "취소";
                        Button2.Content = "확인";
                        break;
                }
            }
            catch (Exception ex)
            {
                FileManager.WriteLog(string.Format("[예외] {0}\n - {1}", ex.Message, ex.StackTrace));
            }
        }
        #endregion

        #region Button1_Click
        /// <summary>
        /// 아니요, 취소 버튼 클릭 시 발생하는 이벤트입니다.
        /// </summary>
        private void Button1_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }
        #endregion

        #region Button2_Click
        /// <summary>
        /// 예, 확인 버튼 클릭 시 발생하는 이벤트입니다.
        /// </summary>
        private void Button2_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
        }
        #endregion
    }
}