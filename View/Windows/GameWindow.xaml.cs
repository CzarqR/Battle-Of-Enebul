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
        private void ShowAttack()
        {
            Console.WriteLine("Show attack in game window");
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



        private Arena arena;
        public Arena Arena
        {
            get
            {
                return arena;
            }
        }

        public FieldControl[,] Fields
        {
            get;
            set;
        }


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
                Fields[cord.X, cord.Y].UpdateUI();
            }
        }


        public GameWindow()
        {
            InitializeComponent();
            InitArena();
            arena.StartAttack += ShowAttack;
            arena.ShowPawnEvent += ShowFieldInfo;
        }

        private void InitArena()
        {
            arena = new Arena();
            Fields = new FieldControl[Arena.HEIGHT, Arena.WIDTH];

            for (int i = 0; i < Arena.HEIGHT; i++)
            {
                for (int j = 0; j < Arena.WIDTH; j++)
                {
                    Fields[i, j] = new FieldControl(this, Arena.GetFieldAt(i, j), new Cord(i, j));
                    board.Children.Add(Fields[i, j]);
                    Grid.SetRow(Fields[i, j], i);
                    Grid.SetColumn(Fields[i, j], j);
                }
            }
        }


    }
}
