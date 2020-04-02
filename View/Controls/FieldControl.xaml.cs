using ProjectB.Model.Board;
using ProjectB.Model.Figures;
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




        //public Field Field
        //{
        //    get
        //    {
        //        Console.WriteLine("IN");
        //        return (Field)GetValue(FieldProperty);
        //    }
        //    set
        //    {
        //        SetValue(FieldProperty, value);
        //    }
        //}

        //// Using a DependencyProperty as the backing store for Field.  This enables animation, styling, binding, etc...
        //public static readonly DependencyProperty FieldProperty =
        //    DependencyProperty.Register("Field", typeof(Field), typeof(FieldControl), new PropertyMetadata(null, SetValue));

        //private static void SetValue(DependencyObject d, DependencyPropertyChangedEventArgs e)
        //{
        //    Console.WriteLine("KKKKKK");
        //    FieldControl fieldControl = d as FieldControl;
        //    if (fieldControl != null)
        //    {
        //        Field field = e.NewValue as Field;
        //        if (field.PawnOnField != null)
        //        {
        //            fieldControl.txtHp.Text = (e.NewValue as Field).PawnOnField.HP.ToString();
        //            fieldControl.txtManna.Text = (e.NewValue as Field).PawnOnField.Manna.ToString();
        //            //fieldControl.imgPawn.Source = new BitmapImage(new Uri("Res/Images/witch64.png", UriKind.Relative));
        //            fieldControl.imgPawn.Source = new BitmapImage(new Uri("E:/C#/Projekty/ProjectB/Res/Images/witch64.png", UriKind.Absolute)); //todo hardcode
        //        }
        //        else
        //        {
        //            fieldControl.txtHp.Text = string.Empty;
        //            fieldControl.txtManna.Text = string.Empty;
        //            //todo kasowanie img
        //            //string packUri = "pack://application:,,,/AssemblyName;component/Res/Images/witch64.png"; //todo hardcode
        //            //fieldControl.imgPawn.Source = new ImageSourceConverter().ConvertFromString(packUri) as ImageSource;
        //        }
        //    }
        //}

        private ArenaControl ownerArena;

        public FieldControl(ArenaControl arenaControl, Field field, Cord cord)
        {
            InitializeComponent();
            Field = field;
            ownerArena = arenaControl;
            Cord = cord;
        }

        public void UpdateUI()
        {
            if (field.PawnOnField != null)
            {
                txtHp.Text = field.PawnOnField.HP.ToString();
                txtManna.Text = field.PawnOnField.Manna.ToString();
                imgPawn.Source = new BitmapImage(new Uri("pack://application:,,,/Res/Images/witch64.png"));
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
            Console.WriteLine("Pole Click");
            ownerArena.Cord = Cord;
        }
    }
}
