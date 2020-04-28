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


        public GameState GameState
        {
            get; private set;
        }


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



        private void UpdateGame(Cord cord)
        {
            GameState.HandleInput(cord);
        }

        public void UpdateField(string[] fieldValues, int index)
        {
            FieldsVM.ElementAt(index).BackgroundPath = fieldValues[0];
            FieldsVM.ElementAt(index).SkillCastingPath = fieldValues[1];
            FieldsVM.ElementAt(index).SkillExecutingPath = fieldValues[2];
            FieldsVM.ElementAt(index).PawnImagePath = fieldValues[3];
            FieldsVM.ElementAt(index).PawnHP = fieldValues[4];
            FieldsVM.ElementAt(index).PawnManna = fieldValues[5];

        }


        public GameVM()
        {
            GameState = new GameState();

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
                        PawnClick = new CommandHandler(() => UpdateGame(cord), () => { return true; })

                    });
                }
            }

            GameState.UpdateUIEvent += UpdateField;
        }

    }
}
