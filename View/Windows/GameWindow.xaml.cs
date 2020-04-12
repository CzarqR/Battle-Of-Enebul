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
            grdAttack.IsEnabled = true;
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

        public Arena Arena { get;
            private set;
        }

        private FieldControl[,] fields;



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
        }

        private void InitArena()
        {
            Arena = new Arena();
            fields = new FieldControl[Arena.HEIGHT, Arena.WIDTH];

            for (int i = 0; i < Arena.HEIGHT; i++)
            {
                for (int j = 0; j < Arena.WIDTH; j++)
                {
                    fields[i, j] = new FieldControl(this, Arena.GetFieldAt(i, j), new Cord(i, j));
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
            UpdateUI(Arena.MarkFieldsToAttack());
        }

        private void ButExtraAttackClick(object sender, RoutedEventArgs e)
        {
            UpdateUI(Arena.MarkFieldsToAttack());
        }
    }
}
