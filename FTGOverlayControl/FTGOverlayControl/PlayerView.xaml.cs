using FTGOverlayControl.Model;
using Microsoft.Win32;
using System.Windows;
using System.Windows.Controls;

namespace FTGOverlayControl
{
    /// <summary>
    /// PlayerView.xaml の相互作用ロジック
    /// </summary>
    public partial class PlayerView : UserControl
    {
        public PlayerView()
        {
            InitializeComponent();
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            PlayerModel item = (PlayerModel)PlayerComboBox.SelectedValue;
            Player1Name.Text = item.Name;
            Player1Copy.Text = item.Copy;
            FilePath.Text = item.ImagePath;
            Attr1.Text = item.Attributes[0];
            Attr2.Text = item.Attributes[1];
            Attr3.Text = item.Attributes[2];
        }

        private void ChooseImage_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new OpenFileDialog();
            dialog.FileName = "sample.png";
            dialog.Filter = "image file|*.png";
            bool? result = dialog.ShowDialog();
            if (result != true)
            {
                return;
            }

            FilePath.Text = dialog.FileName;
        }
    }
}
