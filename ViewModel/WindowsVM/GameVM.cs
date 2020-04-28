using ProjectB.Model.Board;
using ProjectB.Model.Help;
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


        #endregion





        #region Commands

        private ICommand butPrimaryAttackClick;
        public ICommand ButPrimaryAttackClick
        {
            get
            {
                return butPrimaryAttackClick ?? (butPrimaryAttackClick = new CommandHandler(() => PrimaryAttack(), () => { return PrimaryAttackEnable; }));
            }
        }

        private void PrimaryAttack()
        {
            Console.WriteLine("Primary Attack");
        }


        private ICommand butSkillAttackClick;
        public ICommand ButSkillAttackClick
        {
            get
            {
                return butSkillAttackClick ?? (butSkillAttackClick = new CommandHandler(() => SkillAttack(), () => { return SkillAttackEnable; }));
            }
        }

        private void SkillAttack()
        {
            Console.WriteLine("Skill Attack");
        }


        private ICommand mouseEnterAttackCommand;

        public ICommand MouseEnterAttackCommand
        {
            get
            {
                return mouseEnterAttackCommand ?? (mouseEnterAttackCommand = new CommandHandler(() => EnterAttack(), () => { return true; }));
            }
        }

        private void EnterAttack()
        {
            Console.WriteLine("Attack enetred");
        }


        #endregion







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

            GameState.UpdateUIEvent += UpdateField;
            GameState.StartAttackEvent += AttactEnable;

        }

    }
}
