using ProjectB.Model.Board;
using ProjectB.Model.Help;
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

namespace ProjectB.View.Controls
{
    /// <summary>
    /// Interaction logic for ArenaControl.xaml
    /// </summary>
    public partial class ArenaControl : UserControl
    {
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


        public ArenaControl()
        {
            InitializeComponent();
            InitArena();
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
