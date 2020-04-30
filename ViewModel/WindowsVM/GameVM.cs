using ProjectB.Model.Board;
using ProjectB.Model.Help;
using ProjectB.Model.Sklills;
using ProjectB.ViewModel.Commands;
using ProjectB.ViewModel.ControlsVM;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace ProjectB.ViewModel.WindowsVM
{
    using R = Properties.Resources;

    public class GameVM : BaseVM
    {

        #region Properties

        private readonly GameState GameState;

        private readonly Random random = new Random();


        private ObservableCollection<FieldVM> fieldsVM;

        public ObservableCollection<FieldVM> FieldsVM
        {
            get
            {
                return fieldsVM;
            }
            set
            {
                fieldsVM = value;
                OnPropertyChanged(nameof(FieldsVM));
            }
        }

        public bool PrimaryAttackEnable
        {
            get; private set;
        }

        public bool SkillAttackEnable
        {
            get; private set;
        }

        public bool DiceRollEnable
        {
            get; private set;
        }

        private string dicePath;

        public string DicePath
        {
            get
            {
                return dicePath;
            }
            set
            {
                dicePath = value;
                OnPropertyChanged(nameof(DicePath));
            }
        }

        private string title;

        public string Title
        {
            get
            {
                return title;
            }
            set
            {
                title = value;
                OnPropertyChanged(nameof(Title));
            }
        }

        private string pawnImagePath;

        public string PawnImagePath
        {
            get
            {
                return pawnImagePath;
            }
            set
            {
                pawnImagePath = value;
                OnPropertyChanged(nameof(PawnImagePath));
            }
        }

        private string descPawn;

        public string DescPawn
        {
            get
            {
                return descPawn;
            }
            set
            {
                descPawn = value;
                OnPropertyChanged(nameof(DescPawn));
            }
        }

        private string stats;

        public string Stats
        {
            get
            {
                return stats;
            }
            set
            {
                stats = value;
                OnPropertyChanged(nameof(Stats));
            }
        }

        private string primaryAttackName;

        public string PrimaryAttackName
        {
            get
            {
                return primaryAttackName;
            }
            set
            {
                primaryAttackName = value;
                OnPropertyChanged(nameof(PrimaryAttackName));
            }
        }

        private string primaryAttackDesc;

        public string PrimaryAttackDesc
        {
            get
            {
                return primaryAttackDesc;
            }
            set
            {
                primaryAttackDesc = value;
                OnPropertyChanged(nameof(PrimaryAttackDesc));
            }
        }

        private string skillAttackName;

        public string SkillAttackName
        {
            get
            {
                return skillAttackName;
            }
            set
            {
                skillAttackName = value;
                OnPropertyChanged(nameof(SkillAttackName));
            }
        }

        private string skillAttackDesc;

        public string SkillAttackDesc
        {
            get
            {
                return skillAttackDesc;
            }
            set
            {
                skillAttackDesc = value;
                OnPropertyChanged(nameof(SkillAttackDesc));
            }
        }

        private string floorImagePath;

        public string FloorImagePath
        {
            get
            {
                return floorImagePath;
            }
            set
            {
                floorImagePath = value;
                OnPropertyChanged(nameof(FloorImagePath));
            }
        }

        private string floorDesc;

        public string FloorDesc
        {
            get
            {
                return floorDesc;
            }
            set
            {
                floorDesc = value;
                OnPropertyChanged(nameof(FloorDesc));
            }
        }

        private string floorLegend;

        public string FloorLegend
        {
            get
            {
                return floorLegend;
            }
            set
            {
                floorLegend = value;
                OnPropertyChanged(nameof(FloorLegend));
            }
        }

        private Visibility pawnPanelVisibility;

        public Visibility PawnPanelVisibility
        {
            get
            {
                return pawnPanelVisibility;
            }
            set
            {
                pawnPanelVisibility = value;
                OnPropertyChanged(nameof(PawnPanelVisibility));
            }
        }

        private Visibility floorPanelVisibility;

        public Visibility FloorPanelVisibility
        {
            get
            {
                return floorPanelVisibility;
            }
            set
            {
                floorPanelVisibility = value;
                OnPropertyChanged(nameof(FloorPanelVisibility));
            }
        }

        private Visibility customPanelVisibility;

        public Visibility CustomPanelVisibility
        {
            get
            {
                return customPanelVisibility;
            }
            set
            {
                customPanelVisibility = value;
                OnPropertyChanged(nameof(CustomPanelVisibility));
            }
        }

        private string customLegend;

        public string CustomLegend
        {
            get
            {
                return customLegend;
            }
            set
            {
                customLegend = value;
                OnPropertyChanged(nameof(CustomLegend));
            }
        }

        private string customImagePath;

        public string CustomImagePath
        {
            get
            {
                return customImagePath;
            }
            set
            {
                customImagePath = value;
                OnPropertyChanged(nameof(CustomImagePath));
            }
        }







        #endregion


        #region Commands

        private ICommand butAttackCLickCommand;
        public ICommand ButAttackClickCommand
        {
            get
            {
                return butAttackCLickCommand ?? (butAttackCLickCommand = new CommandHandlerExecParameter(AttackClick, GetAttack));
            }
        }

        private bool GetAttack(bool attackType)
        {
            if (attackType)
            {
                return PrimaryAttackEnable;
            }
            else
            {
                return SkillAttackEnable;
            }
        }

        private void AttackClick(bool attackType)
        {
            GameState.MarkFieldsToAttack(attackType);
        }


        private ICommand mouseEnterAttackCommand;
        public ICommand MouseEnterAttackCommand
        {
            get
            {
                return mouseEnterAttackCommand ?? (mouseEnterAttackCommand = new CommandHandlerParameter(EnterAttack, () => { return true; }));
            }
        }

        private void EnterAttack(bool attackType)
        {
            GameState.ShowPossibleAttack(attackType);
        }


        private ICommand mouseLeaveAttackCommand;
        public ICommand MouseLeaveAttackCommand
        {
            get
            {
                return mouseLeaveAttackCommand ?? (mouseLeaveAttackCommand = new CommandHandler(LeaveAttack, () => { return true; }));
            }
        }

        private void LeaveAttack()
        {
            GameState.HidePossibleAttack();
        }


        private ICommand mouseInterractDiceCommand;

        public ICommand MouseInterractDiceCommand
        {
            get
            {
                return mouseInterractDiceCommand ?? (mouseInterractDiceCommand = new CommandHandlerParameter(SetDiceImage, () => { return DiceRollEnable; }));
            }

        }

        private void SetDiceImage(bool inOff)
        {
            if (inOff) //enter dice image
            {
                DicePath = string.Format(App.pathToDice, "00");
            }
            else
            {
                DicePath = string.Format(App.pathToDice, "0");

            }
        }



        private ICommand diceRollCommand;

        public ICommand DiceRollCommand
        {
            get
            {
                return diceRollCommand ?? (diceRollCommand = new CommandHandler(RollDice, () => { return DiceRollEnable; }));
            }

        }

        private void RollDice()
        {
            DiceRollEnable = false;
            int bonus = random.Next(1, 7);
            DicePath = string.Format(App.pathToDice, bonus);
            GameState.RollDice(Convert.ToByte(bonus));
        }


        private ICommand endRoundCommand;

        public ICommand EndRoundCommand
        {
            get
            {
                return endRoundCommand ?? (endRoundCommand = new CommandHandler(EndRound, () => { return true; }));
            }
        }

        private void EndRound()
        {
            GameState.EndRound();
            DicePath = string.Format(App.pathToDice, 0);
            DiceRollEnable = false;
            PrimaryAttackEnable = false;
            SkillAttackEnable = false;
        }


        private ICommand skipMovementCommand;

        public ICommand SkipMovementCommand
        {
            get
            {
                return skipMovementCommand ?? (skipMovementCommand = new CommandHandler(GameState.SkipMovement, () => { return true; }));
            }
        }




        #endregion


        #region event bindings


        private void UpdateField(string[] fieldValues, int index)
        {
            FieldsVM.ElementAt(index).BackgroundPath = fieldValues[0];
            FieldsVM.ElementAt(index).SkillCastingPath = fieldValues[1];
            FieldsVM.ElementAt(index).SkillExecutingPath = fieldValues[2];
            FieldsVM.ElementAt(index).PawnImagePath = fieldValues[3];
            FieldsVM.ElementAt(index).PawnHP = fieldValues[4];
            FieldsVM.ElementAt(index).PawnManna = fieldValues[5];
        }

        private void AttactEnable(bool primaryAttack, bool skillAttack)
        {
            PrimaryAttackEnable = primaryAttack;
            SkillAttackEnable = skillAttack;
        }

        private void StartAttack()
        {
            DiceRollEnable = true;
        }

        private void UpdatePanelPawn(string title, string pawnImagePath, string descPawn, string stats, string primaryAttackName, string primaryAttackDesc, string skillAttackName, string skillAttackDesc)
        {
            Title = title;
            PawnImagePath = pawnImagePath;
            DescPawn = descPawn;
            Stats = stats;
            PrimaryAttackDesc = primaryAttackDesc;
            PrimaryAttackName = primaryAttackName;
            SkillAttackDesc = skillAttackDesc;
            SkillAttackName = SkillAttackName;
            PawnPanelVisibility = Visibility.Visible;
            FloorPanelVisibility = Visibility.Collapsed;
            CustomPanelVisibility = Visibility.Collapsed;
        }

        private void UpdatePanelFloor(string title, string floorImagePath, string floorDesc, string legend)
        {
            Console.WriteLine("XX");
            Title = title;
            FloorImagePath = floorImagePath;
            FloorLegend = legend;
            FloorDesc = floorDesc;
            PawnPanelVisibility = Visibility.Collapsed;
            FloorPanelVisibility = Visibility.Visible;
            CustomPanelVisibility = Visibility.Collapsed;

        }


        #endregion


        /// <summary>
        /// ctor
        /// </summary>
        public GameVM()
        {
            GameState = new GameState();

            /// Init fields
            FieldsVM = new ObservableCollection<FieldVM>();
            for (int i = 0; i < Arena.HEIGHT; i++)
            {
                for (int j = 0; j < Arena.WIDTH; j++)
                {
                    Cord cord = new Cord(i, j);
                    var x = GameState.GetFieldView(i, j);

                    FieldsVM.Add(new FieldVM
                    {
                        BackgroundPath = x[0],
                        SkillCastingPath = x[1],
                        SkillExecutingPath = x[2],
                        PawnImagePath = x[3],
                        PawnHP = x[4],
                        PawnManna = x[5],
                        PawnClick = new CommandHandler(() => { GameState.HandleInput(cord); }, () => { return true; })

                    });
                }
            }

            /// seting startup values
            PrimaryAttackEnable = false;
            SkillAttackEnable = false;
            DiceRollEnable = false;
            DicePath = string.Format(App.pathToDice, 0);
            PawnPanelVisibility = Visibility.Collapsed;
            FloorPanelVisibility = Visibility.Collapsed;
            CustomPanelVisibility = Visibility.Visible;
            Title = R.starting_title;
            CustomLegend = R.starting_legend;
            CustomImagePath = App.pathToCustomImageStart;

            GameState.UpdateUIEvent += UpdateField;
            GameState.StartAttackEvent += AttactEnable;
            GameState.FieldToAttackSelectedEvent += StartAttack;
            GameState.ShowPawnInfoEvent += UpdatePanelPawn;
            GameState.ShowFloorInfoEvent += UpdatePanelFloor;
        }

    }
}
