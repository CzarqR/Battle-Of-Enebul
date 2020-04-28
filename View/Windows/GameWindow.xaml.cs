using ProjectB.Model.Board;
using ProjectB.Model.Help;
using ProjectB.View.Controls;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Imaging;
namespace ProjectB.View.Windows
{


    public partial class GameWindow : Window
    {

        public GameWindow()
        {
            InitializeComponent();
        //    InitArena();
        //    GS.StartAttack += ShowAttack;
        //    GS.ShowPawnEvent += ShowFieldInfo;
        //    GS.SelectedFieldToAttack += SelectedFieldToAttack;
        //    GS.EndRoundEvent += EndRoundEvent;
        //    random = new Random();
        }


        //private void ShowAttack(bool primaryAttack, bool extraAttack)
        //{
        //    butExtraAttack.IsEnabled = extraAttack;
        //    butPrimaryAttack.IsEnabled = primaryAttack;
        //}

        //private void ShowFieldInfo(string imgPawn, string imgFloor, string baseInfo, string precInfo, string bonus, string primary_name, string primary_desc, string skill_name, string skill_desc)
        //{
        //    if (imgPawn != null)
        //    {
        //        imgInfoPawn.Source = new BitmapImage(new Uri(string.Format(imgPawn, 0)));
        //    }
        //    else
        //    {
        //        imgInfoPawn.Source = null;
        //    }

        //    txtTitle.Content = baseInfo;
        //    txtDesc.Text = precInfo;
        //    txtInfoBonuses.Text = bonus;


        //    txtPrimaryDesc.Text = primary_desc;
        //    txtSkillDesc.Text = skill_desc;
        //    txtPrimaryName.Text = primary_name;
        //    txtSkillName.Text = skill_name;
        //}

        //private void SelectedFieldToAttack()
        //{
        //    imgDiceRoll.IsEnabled = true;
        //}

        //private void EndRoundEvent()
        //{
        //    imgDiceRoll.Source = new BitmapImage(new Uri(string.Format(App.pathToDice, 0)));
        //    butExtraAttack.IsEnabled = false;
        //    butPrimaryAttack.IsEnabled = false;
        //    bonusDice = 0;
        //}




        //public GameState GS
        //{
        //    get;
        //    private set;
        //}

        //private FieldControl[,] fields;
        //private byte bonusDice = 0;
        //private readonly Random random;

        //private Cord cord;

        //public Cord Cord
        //{
        //    get
        //    {
        //        return cord;
        //    }
        //    set
        //    {
        //        cord = value;
        //        UpdateUI(GS.HandleInput(Cord));
        //    }
        //}

        //private void UpdateUI(List<Cord> cords)
        //{
        //    if (cords != null)
        //    {
        //        foreach (Cord cord in cords)
        //        {
        //            fields[cord.X, cord.Y].UpdateUI();
        //        }
        //    }

        //}




        //private void InitArena()
        //{
        //    GS = new GameState();
        //    fields = new FieldControl[GameState.HEIGHT, GameState.WIDTH];

        //    for (int i = 0; i < GameState.HEIGHT; i++)
        //    {
        //        for (int j = 0; j < GameState.WIDTH; j++)
        //        {
        //            Cord cord = new Cord(i, j);
        //            fields[i, j] = new FieldControl(this, GS.At(cord), cord);
        //            board.Children.Add(fields[i, j]);
        //            Grid.SetRow(fields[i, j], i);
        //            Grid.SetColumn(fields[i, j], j);
        //        }
        //    }
        //}

        //private void MouseEnterPrimaryAttackButton(object sender, MouseEventArgs e)
        //{
        //    UpdateUI(GS.ShowPossiblePrimaryAttack());
        //}

        //private void MouseEnterExtraAttackButton(object sender, MouseEventArgs e)
        //{
        //    UpdateUI(GS.ShowPossibleExtraAttack());
        //}

        //private void MouseLeaveAttackButton(object sender, MouseEventArgs e)
        //{
        //    UpdateUI(GS.HideAttackFields());
        //}

        //private void ButEndRoundClik(object sender, RoutedEventArgs e)
        //{
        //    UpdateUI(GS.EndRound());
        //}

        //private void ButPrimaryAttackClick(object sender, RoutedEventArgs e)
        //{
        //    UpdateUI(GS.MarkFieldsToAttack(true));
        //}

        //private void ButExtraAttackClick(object sender, RoutedEventArgs e)
        //{
        //    UpdateUI(GS.MarkFieldsToAttack(false));
        //}



        //private void ButMovementSkipClick(object sender, RoutedEventArgs e)
        //{
        //    UpdateUI(GS.SkipMovement(Cord));
        //}

        //private void ImgDiceRollClick(object sender, MouseButtonEventArgs e)
        //{
        //    if (bonusDice == 0)
        //    {
        //        bonusDice = (byte)random.Next(1, 7);
        //        Console.WriteLine(bonusDice);
        //        imgDiceRoll.Source = new BitmapImage(new Uri(string.Format(App.pathToDice, bonusDice)));
        //        UpdateUI(GS.ExecuteAttack(bonusDice));

        //    }

        //}


    }
}
