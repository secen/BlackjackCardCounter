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
using System.IO;

namespace BlackjackCardCounter
{
    /// <summary>
    /// Interaction logic for GraphicalView.xaml
    /// </summary>
    public partial class GraphicalView : Window
    {
        MainWindow wnd;
        public GraphicalView(MainWindow window)
        {
            wnd = window;
            InitializeComponent();
            ReDraw();
        }
        public void ReDraw()
        {
            foreach (Card card in MainWindow.house)
            {
                addImageToStackPanel(card,houseStackPanel);
            }
            foreach(Card card in MainWindow.hand)
            {
                addImageToStackPanel(card, handStackPanel);
            }
            foreach(Card card in MainWindow.otherPlayers)
            {
                addImageToStackPanel(card, otherPlayersStackPanel);
            }
        }

        private void addImageToStackPanel(Card card, StackPanel stackPanel)
        {
            string filename = getFileName(card);
            var uri = new Uri(System.IO.Path.Combine(Environment.CurrentDirectory, "Resources/" + filename));
            BitmapImage bimg = new BitmapImage(uri);
            Thickness thickness = new Thickness() { Bottom = 1, Top = 1, Right = 1, Left = 1 };
            Border border = new Border() { Width = 76, Height = 126, BorderThickness = thickness, BorderBrush = new SolidColorBrush() { Color = Color.FromRgb(0, 0, 0) } };
            Image image = new Image() { Source = bimg, Width = 75, Height = 125 };
            border.Child = image;
            stackPanel.Children.Add(border);
        }

        private string getFileName(Card card)
        {
            string filename = "";
            switch (card.Value)
            {
                case 13:
                    filename += "king";
                    break;
                case 12:
                    filename += "queen";
                    break;
                case 11:
                    filename += "jack";
                    break;
                case 1:
                    filename += "ace";
                    break;
                default:
                    filename += card.Value.ToString();
                    break;
            }
            filename += "_of_";
            Dictionary<int, string> dictionary = new Dictionary<int, string>() { { 1, "clubs" }, { 2, "hearts" }, { 3, "diamonds" }, { 4, "spades" } };
            filename += dictionary[card.Type];
            filename += ".png";
            return filename;
        }
    }
}
