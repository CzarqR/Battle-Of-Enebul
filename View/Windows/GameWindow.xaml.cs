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
            bonus1 = bonus2 = 0;
        }




        public Arena Arena
        {
            get;
            private set;
        }

        private FieldControl[,] fields;
        private byte bonus1 = 0, bonus2 = 0;
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
            foreach (Cord cord in cords)
            {
                fields[cord.X, cord.Y].UpdateUI();
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
            Arena = new Arena();
            fields = new FieldControl[Arena.HEIGHT, Arena.WIDTH];

            for (int i = 0; i < Arena.HEIGHT; i++)
            {
                for (int j = 0; j < Arena.WIDTH; j++)
                {
                    fields[i, j] = new FieldControl(this, Arena.At(i, j), new Cord(i, j));
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
            if (bonus2 == 0)
            {
                bonus2 = (byte)random.Next(1, 7);
                Console.WriteLine(bonus1);
                imgDiceRoll2.Source = new BitmapImage(new Uri(string.Format(App.pathToDice, bonus2)));
                UpdateUI(Arena.ExecuteAttack(bonus1, bonus2));
            }
        }

        private void Image_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (bonus1 == 0)
            {
                bonus1 = (byte)random.Next(1, 7);
                Console.WriteLine(bonus1);
                imgDiceRoll1.Source = new BitmapImage(new Uri(string.Format(App.pathToDice, bonus1)));
                stcDiceRoll2.IsEnabled = true;
            }

        }


    }
}
