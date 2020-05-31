using ProjectB.Model.Board;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

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




        public FloorStatus FloorStatus
        {
            get
            {
                return (FloorStatus)GetValue(FloorStatusProperty);
            }
            set
            {
                SetValue(FloorStatusProperty, value);
            }
        }

        public static readonly DependencyProperty FloorStatusProperty =
            DependencyProperty.Register("FloorStatus", typeof(FloorStatus), typeof(FieldControl), new PropertyMetadata(null));



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



        public string InfoTT
        {
            get
            {
                return (string)GetValue(InfoTTProperty);
            }
            set
            {
                SetValue(InfoTTProperty, value);
            }
        }

        public static readonly DependencyProperty InfoTTProperty =
            DependencyProperty.Register("InfoTT", typeof(string), typeof(FieldControl), new PropertyMetadata(null));



        #endregion


        public FieldControl()
        {
            InitializeComponent();
        }

        private void MouseEnterUpdateCursor(object sender, MouseEventArgs e)
        {
            if (FloorStatus == FloorStatus.Move)
            {
                Cursor = new Cursor(Application.GetResourceStream(new Uri(string.Format(App.cursorPath, GameState.Turn ? "blue" : "red", App.move), UriKind.Relative)).Stream);
            }
            else if (FloorStatus == FloorStatus.Attack)
            {
                Cursor = new Cursor(Application.GetResourceStream(new Uri(string.Format(App.cursorPath, GameState.Turn ? "blue" : "red", App.attack), UriKind.Relative)).Stream);
            }
            else
            {
                Cursor = new Cursor(Application.GetResourceStream(new Uri(string.Format(App.cursorPath, GameState.Turn ? "blue" : "red", App.defauLt), UriKind.Relative)).Stream);
            }
        }

    }
}
