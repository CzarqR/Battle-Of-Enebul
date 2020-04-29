using ProjectB.Model.Board;
using ProjectB.Model.Help;
using ProjectB.ViewModel.Commands;
using ProjectB.ViewModel.ControlsVM;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ProjectB.ViewModel.WindowsVM
{


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





        #endregion


        #region event bindings


        public void UpdateField(string[] fieldValues, int index)
        {
            FieldsVM.ElementAt(index).BackgroundPath = fieldValues[0];
            FieldsVM.ElementAt(index).SkillCastingPath = fieldValues[1];
            FieldsVM.ElementAt(index).SkillExecutingPath = fieldValues[2];
            FieldsVM.ElementAt(index).PawnImagePath = fieldValues[3];
            FieldsVM.ElementAt(index).PawnHP = fieldValues[4];
            FieldsVM.ElementAt(index).PawnManna = fieldValues[5];
        }

        public void AttactEnable(bool primaryAttack, bool skillAttack)
        {
            PrimaryAttackEnable = primaryAttack;
            SkillAttackEnable = skillAttack;
        }

        public void StartAttack()
        {
            DiceRollEnable = true;
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
            DicePath = "/Res/Images/Dices/dice_0.png";

            GameState.UpdateUIEvent += UpdateField;
            GameState.StartAttackEvent += AttactEnable;
            GameState.FieldToAttackSelectedEvent += StartAttack;

        }

    }
}
