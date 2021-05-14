using HanyangKiosk.Properties;
using HanyangKiosk.Utils;

using System;
using System.Windows;
using System.Windows.Forms;

namespace HanyangKiosk.Popup
{
    /// <summary>
    /// 프로그램 데이터 경로를 선택하는 팝업창입니다.
    /// </summary>
    public partial class SelectDataPathPopup : Window
    {
        #region 생성자
        public SelectDataPathPopup()
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

        #region DefaultFolder_Click
        /// <summary>
        /// 기본 폴더 클릭 시 발생하는 이벤트입니다.
        /// </summary>
        private void DefaultFolder_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Settings.Default.DataPath = @"C:\Hanyang";
                DialogResult = true;
            }
            catch (Exception ex)
            {
                FileManager.WriteLog($"[Exception] {ex.Message}\n - {ex.StackTrace}");
            }
        }
        #endregion

        #region FindFolder_Click
        /// <summary>
        /// 폴더 찾기 클릭 시 발생하는 이벤트입니다.
        /// </summary>
        private void FindFolder_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var dlg = new FolderBrowserDialog()
                {
                    ShowNewFolderButton = true,
                    Description = "프로그램 데이터 폴더 경로를 선택해주세요."
                };

                if (dlg.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    Settings.Default.DataPath = dlg.SelectedPath;
                    DialogResult = true;
                }
            }
            catch (Exception ex)
            {
                FileManager.WriteLog($"[Exception] {ex.Message}\n - {ex.StackTrace}");
            }
        }
        #endregion
    }
}
