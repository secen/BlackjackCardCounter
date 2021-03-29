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
using System.Windows.Shapes;

namespace BlackjackCardCounter
{
    /// <summary>
    /// Interaction logic for ClearTablePopup.xaml
    /// </summary>
    public partial class ClearTablePopup : Window
    {
        MainWindow window;
        public ClearTablePopup(MainWindow wnd)
        {
            window = wnd;
            InitializeComponent();
        }

        private void confirmButton_Click(object sender, RoutedEventArgs e)
        {
            window.ClearTable();
            this.Close();
        }

        private void cancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
