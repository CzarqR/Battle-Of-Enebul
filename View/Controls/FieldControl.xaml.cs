using ProjectB.Model.Board;
using ProjectB.Model.Figures;
using ProjectB.Model.Help;
using ProjectB.View.Windows;
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
    /// Interaction logic for FieldControl.xaml
    /// </summary>
    public partial class FieldControl : UserControl
    {

        private Field field;

        public Field Field
        {
            get
            {
                return field;
            }
            set
            {
                field = value;
                UpdateUI();
            }
        }

        public Cord Cord
        {
            get;
        }





        private readonly GameWindow gameWindow;

        public FieldControl(GameWindow gameWindow, Field field, Cord cord)
        {
            InitializeComponent();
            Field = field;
            this.gameWindow = gameWindow;
            Cord = cord;

        }




        public void UpdateUI()
        {
            string magSkill = Field.MagSkillPath();
            if (magSkill != null)
            {
                canMag.Background = new ImageBrush(new BitmapImage(new Uri(magSkill)));
            }
            else
            {
                canMag.Background = null;
            }

            grdBack.Background = new ImageBrush(new BitmapImage(new Uri(Field.FloorPath())));

            if (field.PawnOnField != null)
            {
                txtHp.Text = field.PawnOnField.HP.ToString();
                txtManna.Text = field.PawnOnField.Manna.ToString();
                imgPawn.Source = new BitmapImage(new Uri(Field.PawnOnField.ImgPath));
            }
            else
            {
                txtHp.Text = string.Empty;
                txtManna.Text = string.Empty;
                imgPawn.Source = null;
            }
        }

        private void UserControl_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            gameWindow.Cord = Cord;
        }

    }
}
