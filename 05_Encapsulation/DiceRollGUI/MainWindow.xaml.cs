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
using System.Diagnostics;

namespace DiceRollGUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        static Random random = new();
        SwordDamage swordDamage;

        public MainWindow()
        {
            InitializeComponent();
            swordDamage = new(RollDice());
            DisplayDamage();
        }

        public static int RollDice()
        {
            return random.Next(1, 7) + random.Next(1, 7) + random.Next(1, 7);
        }

        void DisplayDamage()
        {
            damage.Text = $"Rolled {swordDamage.Roll} for {swordDamage.Damage} HP";
        }

        private void Flaming_Checked(object sender, RoutedEventArgs e)
        {
            swordDamage.Flaming = true;
            DisplayDamage();
        }

        private void Flaming_Unchecked(object sender, RoutedEventArgs e)
        {
            swordDamage.Flaming = false;
            DisplayDamage();
        }

        private void Magic_Checked(object sender, RoutedEventArgs e)
        {
            swordDamage.Magic = true;
            DisplayDamage();
        }

        private void Magic_Unchecked(object sender, RoutedEventArgs e)
        {
            swordDamage.Magic = false;
            DisplayDamage();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            swordDamage.Roll = RollDice();
            DisplayDamage();
        }
    }
}



class SwordDamage
{
    public const int BaseDamage = 3;
    public const int FlameDamage = 2;

    int roll;
    bool flaming;
    bool magic;

    /// <summary>
    /// Calculates damage without any magic or flaming using
    /// the initial roll.
    /// </summary>
    /// <param name="initialRoll">Initial 3d6 roll</param>
    public SwordDamage(int initialRoll)
    {
        Roll = initialRoll;
        CalculateDamage();
    }

    /// <summary>
    /// Contains the calculated damage.
    /// </summary>
    public int Damage { get; private set; }

    /// <summary>
    /// Contains the results of a 3d6 roll.
    /// </summary>
    public int Roll
    {
        get => roll;
        set
        {
            roll = value;
            CalculateDamage();
        }
    }

    /// <summary>
    /// True if sword is flaming.
    /// </summary>
    public bool Flaming
    {
        get => flaming;
        set
        {
            flaming = value;
            CalculateDamage();
        }
    }

    /// <summary>
    /// True if sword is magic.
    /// </summary>
    public bool Magic
    {
        get => magic;
        set
        {
            magic = value;
            CalculateDamage();
        }
    }

    /// <summary>
    /// Updates damage property using the current sword properties.
    /// </summary>
    void CalculateDamage()
    {
        var magicMultiplier = Magic ? 1.75f : 1f;
        var flameDamage = Flaming ? FlameDamage : 0;
        Damage = (int)(Roll * magicMultiplier) + BaseDamage + flameDamage;
    }
}