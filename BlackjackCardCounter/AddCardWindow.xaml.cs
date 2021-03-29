using System;
using System.Collections.Generic;
using System.Diagnostics;
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
    /// Interaction logic for AddCardWindow.xaml
    /// </summary>
    public partial class AddCardWindow : Window
    {
        List<Card> ds = new List<Card>();
        int cnt = 0;
        MainWindow mainWindow;
        public AddCardWindow()
        {
            InitializeComponent();
            initList();
            addCardComboBox.ItemsSource = ds;
        }
        public AddCardWindow(int i, MainWindow window)
        {
            cnt = i;
            mainWindow = window;
            InitializeComponent();
            initList();
            addCardComboBox.ItemsSource = ds;
        }
        private void initList()
        {
            for (int i = 1; i <= 13; i++)
                for (int j = 1; j <= 4; j++)
                    ds.Add(new Card { Type = j, Value = i });
        }
        private void closeCardButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void addCardButton_Click(object sender, RoutedEventArgs e)
        {
            Card card = (Card)addCardComboBox.SelectedItem;
            for (int i = 0; i < MainWindow.Deck.Count(); i++)
            {
                if (MainWindow.Deck[i].Type == card.Type && MainWindow.Deck[i].Value == card.Value)
                {
                    MainWindow.Deck.Remove(MainWindow.Deck[i]);
                    break;
                }
            }
            switch (cnt)
            {
                case 0:
                    MainWindow.undoCommands.Push(new UndoCommand { action = (argument) => { MainWindow.Deck.Add(argument); MainWindow.hand.Remove(argument); }, argument = (Card)addCardComboBox.SelectedItem });
                    mainWindow.undoButton.IsEnabled = true;
                    MainWindow.hand.Add((Card)addCardComboBox.SelectedItem);
                    break;
                case 1:
                    MainWindow.undoCommands.Push(new UndoCommand { action = (argument) => { MainWindow.Deck.Add(argument); MainWindow.otherPlayers.Remove(argument); }, argument = (Card)addCardComboBox.SelectedItem });
                    mainWindow.undoButton.IsEnabled = true;
                    MainWindow.otherPlayers.Add((Card)addCardComboBox.SelectedItem);
                    break;
                case 2:
                    MainWindow.undoCommands.Push(new UndoCommand { action = (argument) => { MainWindow.Deck.Add(argument); MainWindow.house.Remove(argument); }, argument = (Card)addCardComboBox.SelectedItem });
                    mainWindow.undoButton.IsEnabled = true;
                    MainWindow.house.Add((Card)addCardComboBox.SelectedItem);
                    break;
            }
            mainWindow.calculatePercentages();
        }
    }
}
