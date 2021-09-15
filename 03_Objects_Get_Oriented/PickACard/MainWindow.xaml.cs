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

namespace PickACard
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        string[] deck = new string[52];

        public MainWindow()
        {
            InitializeComponent();
            InitializeDeck();
        }

        void InitializeDeck()
        {
            int i = 0;
            foreach (var suit in new string[] { "♠", "♥", "♦", "♣" })
                foreach (var rank in new string[] { "2", "3", "4", "5", "6", "7", "8", "9", "10", "J", "Q", "K", "A" })
                    deck[i++] = rank + suit;
        }

        string[] PickRandomCards(int numberOfCards)
        {
            Random random = new Random();
            string[] returnCards = new string[numberOfCards];
            for (int i = 0; i < numberOfCards; i++)
                returnCards[i] = deck[random.Next(0, 52)];
            return returnCards;
        }

        void buttonPickCards_Click(object sender, RoutedEventArgs e)
        {
            listBoxListCards.Items.Clear();
            string[] chosenCards = PickRandomCards((int)sliderCardCount.Value);
            foreach (var card in chosenCards)
                listBoxListCards.Items.Add(card);
        }
    }
}
