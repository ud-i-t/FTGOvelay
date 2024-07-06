using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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

namespace FTGOverlayControl
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void ResetScore1_Click(object sender, RoutedEventArgs e)
        {
            string msg = "本当にスコアをリセットしますか？\n（個人スコア・チームスコアともに0にします）";

            MessageBoxResult res = MessageBox.Show(msg, "Confirmation", MessageBoxButton.OKCancel,
                MessageBoxImage.Question, MessageBoxResult.Cancel);
            switch (res)
            {
                case MessageBoxResult.OK:
                    // ここからVMの処理を呼ぶのは汚いが簡単のため一旦こうする…。
                    var vm = (MainVm)DataContext;
                    vm.ResetScore.Execute(null);
                    break;
            }
        }
    }
}
