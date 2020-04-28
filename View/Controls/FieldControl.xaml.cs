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

    public partial class FieldControl : UserControl
    {

        #region properties

        public string BackgroundPath
        {
            get
            {
                return (string)GetValue(BackgroundPathProperty);
            }
            set
            {
                SetValue(BackgroundPathProperty, value);
            }
        }

        public static readonly DependencyProperty BackgroundPathProperty =
            DependencyProperty.Register("BackgroundPath", typeof(string), typeof(FieldControl), new PropertyMetadata(null));



        public string SkillCastingPath
        {
            get
            {
                return (string)GetValue(SkillCastingPathProperty);
            }
            set
            {
                SetValue(SkillCastingPathProperty, value);
            }
        }

        public static readonly DependencyProperty SkillCastingPathProperty =
            DependencyProperty.Register("SkillCastingPath", typeof(string), typeof(FieldControl), new PropertyMetadata(null));



        public string SkillExecutingPath
        {
            get
            {
                return (string)GetValue(SkillExecutingPathProperty);
            }
            set
            {
                SetValue(SkillExecutingPathProperty, value);
            }
        }

        public static readonly DependencyProperty SkillExecutingPathProperty =
            DependencyProperty.Register("SkillExecutingPath", typeof(string), typeof(FieldControl), new PropertyMetadata(null));





        public string PawnImagePath
        {
            get
            {
                return (string)GetValue(PawnImagePathProperty);
            }
            set
            {
                SetValue(PawnImagePathProperty, value);
            }
        }

        public static readonly DependencyProperty PawnImagePathProperty =
            DependencyProperty.Register("PawnImagePath", typeof(string), typeof(FieldControl), new PropertyMetadata(null));





        public string PawnHP
        {
            get
            {
                return (string)GetValue(PawnHPProperty);
            }
            set
            {
                SetValue(PawnHPProperty, value);
            }
        }

        public static readonly DependencyProperty PawnHPProperty =
            DependencyProperty.Register("PawnHP", typeof(string), typeof(FieldControl), new PropertyMetadata(null));



        public string PawnManna
        {
            get
            {
                return (string)GetValue(PawnMannaProperty);
            }
            set
            {
                SetValue(PawnMannaProperty, value);
            }
        }

        public static readonly DependencyProperty PawnMannaProperty =
            DependencyProperty.Register("PawnManna", typeof(string), typeof(FieldControl), new PropertyMetadata(null));





        public ICommand PawnClick
        {
            get
            {
                return (ICommand)GetValue(PawnClickProperty);
            }
            set
            {
                SetValue(PawnClickProperty, value);
            }
        }

        public static readonly DependencyProperty PawnClickProperty =
            DependencyProperty.Register("PawnClick", typeof(ICommand), typeof(FieldControl), new PropertyMetadata(null));





        #endregion


        public FieldControl()
        {
            InitializeComponent();
        }




    }
}
