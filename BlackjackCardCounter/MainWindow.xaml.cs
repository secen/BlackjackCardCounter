using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

namespace BlackjackCardCounter
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static Stack<UndoCommand> undoCommands = new Stack<UndoCommand>();
        public static List<Card> Deck = new List<Card>();
        public static int noOfDecks = 8;
        public static ObservableCollection<Card> hand = new ObservableCollection<Card>();
        public static ObservableCollection<Card> otherPlayers = new ObservableCollection<Card>();
        public static ObservableCollection<Card> house = new ObservableCollection<Card>();
        public MainWindow()
        {
            InitializeComponent();
            loadCardData();
            HandDataGrid.AutoGenerateColumns = false;
            HandDataGrid.ItemsSource = hand;
            OtherPlayersDataGrid.AutoGenerateColumns = false;
            OtherPlayersDataGrid.ItemsSource = otherPlayers;
            HouseDataGrid.AutoGenerateColumns = false;
            HouseDataGrid.ItemsSource = house;
            undoButton.IsEnabled = false;
        }
        public void calculatePercentages()
        {
            int handSum = 0;
            int dealerSum = 0;
            foreach(Card card in hand)
            {
                if (card.Value <= 10)
                    handSum += card.Value;
                else
                    handSum += 10;
            }

            foreach (Card card in house)
            {
                if (card.Value <= 10)
                    dealerSum += card.Value;
                else
                    dealerSum += 10;
            }
            int noOfCardsGoingOver = 0;
            foreach(Card card in Deck)
            {
                if (card.Value + handSum > 21)
                    noOfCardsGoingOver++;
            }
            int noOfDealerCardsGoingOver = 0;
            foreach(Card card in Deck)
            {
                if (card.Value + dealerSum > 21)
                    noOfDealerCardsGoingOver++;
            }
            double chance = ((double)noOfCardsGoingOver / (double)Deck.Count());
            double chance2 = ((double)noOfDealerCardsGoingOver / (double)Deck.Count());
            HandChanceLabel.Content = "Your Chance of going over 21: " + (chance).ToString();
            DealerChanceLabel.Content = "Dealer's Chance of going over 21: " + chance2.ToString();
        }
        public void loadCardData()
        {
            for(int k = 1; k<=noOfDecks; k++)
                for (int i = 1; i <= 13; i++)
                    for (int j = 1; j <= 4; j++)
                        Deck.Add(new Card { Type = j, Value = i });
        }
        private void addCardButton_Click(object sender, RoutedEventArgs e)
        {
            var window = new AddCardWindow(1, this);
            window.Show();
        }

        private void addHandButton_Click(object sender, RoutedEventArgs e)
        {

            var window = new AddCardWindow(0, this);
            window.Show();
        }

        private void addDealerButton_Click(object sender, RoutedEventArgs e)
        {

            var window = new AddCardWindow(2, this);
            window.Show();
        }

        private void clearTableButton_Click(object sender, RoutedEventArgs e)
        {
            var wnd = new ClearTablePopup(this);
            wnd.Show();
        }

        public void ClearTable()
        {
            undoCommands.Clear();
            undoButton.IsEnabled = false;
            hand.Clear();
            otherPlayers.Clear();
            house.Clear();
            DealerChanceLabel.Content = "Dealer's Chance of going over 21: ";
            HandChanceLabel.Content = "Your Chance of going over 21: ";
        }

        private void reshuffleButton_Click(object sender, RoutedEventArgs e)
        {
            var wnd = new ReshufflePopup(this);
            wnd.Show();
        }

        public void ReshuffleDeck()
        {
            undoCommands.Clear();
            undoButton.IsEnabled = false;
            Deck.Clear();
            loadCardData();
        }

        private void undoButton_Click(object sender, RoutedEventArgs e)
        {
            var top = undoCommands.Pop();
            top.action(top.argument);
            if (undoCommands.Count == 0)
                undoButton.IsEnabled = false;
            calculatePercentages();
        }
    }
}
