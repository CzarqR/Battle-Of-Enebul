using ProjectB.Model.Board;
using ProjectB.Model.Help;
using ProjectB.View.Controls;
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

namespace ProjectB.View.Windows
{
    /// <summary>
    /// Interaction logic for GameWindow.xaml
    /// </summary>
    public partial class GameWindow : Window
    {
        private void ShowAttack(bool primaryAttack, bool extraAttack)
        {
            butExtraAttack.IsEnabled = extraAttack;
            butPrimaryAttack.IsEnabled = primaryAttack;
        }

        private void ShowFieldInfo(string imgPawn, string imgFloor, string baseInfo, string precInfo)
        {
            if (imgPawn != null)
            {
                imgInfoPawn.Background = new ImageBrush(new BitmapImage(new Uri(imgPawn)));
            }
            else
            {
                imgInfoPawn.Background = null;
            }
            imgInfoFloor.Background = new ImageBrush(new BitmapImage(new Uri(imgFloor)));

            txtBase.Text = baseInfo;
            txtPrec.Text = precInfo;
        }

        private void SelectedFieldToAttack()
        {
            stcDiceRoll1.IsEnabled = true;
        }

        private void EndRoundEvent()
        {
            imgDiceRoll1.Source = new BitmapImage(new Uri(string.Format(App.pathToDice, 0)));
            imgDiceRoll2.Source = new BitmapImage(new Uri(string.Format(App.pathToDice, 0)));
            stcDiceRoll1.IsEnabled = false;
            stcDiceRoll2.IsEnabled = false;
            butExtraAttack.IsEnabled = false;
            butPrimaryAttack.IsEnabled = false;
            bonusBlue = bonusRed = 0;
        }




        public GameState Arena
        {
            get;
            private set;
        }

        private FieldControl[,] fields;
        private byte bonusBlue = 0, bonusRed = 0;
        private readonly Random random;

        private Cord cord;

        public Cord Cord
        {
            get
            {
                return cord;
            }
            set
            {
                cord = value;
                UpdateUI(Arena.HandleInput(Cord));
            }
        }

        private void UpdateUI(List<Cord> cords)
        {
            if (cords != null)
            {
                foreach (Cord cord in cords)
                {
                    fields[cord.X, cord.Y].UpdateUI();
                }
            }

        }


        public GameWindow()
        {
            InitializeComponent();
            InitArena();
            Arena.StartAttack += ShowAttack;
            Arena.ShowPawnEvent += ShowFieldInfo;
            Arena.SelectedFieldToAttack += SelectedFieldToAttack;
            Arena.EndRoundEvent += EndRoundEvent;
            random = new Random();
        }

        private void InitArena()
        {
            Arena = new GameState();
            fields = new FieldControl[GameState.HEIGHT, GameState.WIDTH];

            for (int i = 0; i < GameState.HEIGHT; i++)
            {
                for (int j = 0; j < GameState.WIDTH; j++)
                {
                    Cord cord = new Cord(i, j);
                    fields[i, j] = new FieldControl(this, Arena.At(cord), cord);
                    board.Children.Add(fields[i, j]);
                    Grid.SetRow(fields[i, j], i);
                    Grid.SetColumn(fields[i, j], j);
                }
            }
        }

        private void MouseEnterPrimaryAttackButton(object sender, MouseEventArgs e)
        {
            UpdateUI(Arena.ShowPossiblePrimaryAttack());
        }

        private void MouseEnterExtraAttackButton(object sender, MouseEventArgs e)
        {
            UpdateUI(Arena.ShowPossibleExtraAttack());
        }

        private void MouseLeaveAttackButton(object sender, MouseEventArgs e)
        {
            UpdateUI(Arena.HideAttackFields());
        }

        private void ButEndRoundClik(object sender, RoutedEventArgs e)
        {
            UpdateUI(Arena.EndRound());
        }

        private void ButPrimaryAttackClick(object sender, RoutedEventArgs e)
        {
            UpdateUI(Arena.MarkFieldsToAttack(true));
        }

        private void ButExtraAttackClick(object sender, RoutedEventArgs e)
        {
            UpdateUI(Arena.MarkFieldsToAttack(false));
        }

        private void ImgDiceRoll2_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (bonusRed == 0)
            {
                bonusRed = (byte)random.Next(1, 7);
                Console.WriteLine(bonusBlue);
                imgDiceRoll2.Source = new BitmapImage(new Uri(string.Format(App.pathToDice, bonusRed)));
                UpdateUI(Arena.ExecuteAttack(bonusBlue, bonusRed));
            }
        }

        private void Image_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (bonusBlue == 0)
            {
                bonusBlue = (byte)random.Next(1, 7);
                Console.WriteLine(bonusBlue);
                imgDiceRoll1.Source = new BitmapImage(new Uri(string.Format(App.pathToDice, bonusBlue)));
                stcDiceRoll2.IsEnabled = true;
            }

        }


    }
}
