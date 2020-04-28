using ProjectB.Model.Board;
using ProjectB.Model.Help;
using ProjectB.ViewModel.ControlsVM;
using System;
using System.Collections.Generic;
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
         
        public FieldVM[] FieldsVM
        {
            get; set;
        }

        private void UpdateGame(Cord cord)
        {
            GameState.PAt(cord).TestHpDown();
            Console.WriteLine(GameState.PAt(cord).HP); 
            Console.WriteLine($"Cords clicked: {cord}");
            Console.WriteLine("GAME UPDATED!!!!!");
        }






        public GameVM()
        {
            GameState = new GameState();

            FieldsVM = new FieldVM[Arena.WIDTH * Arena.HEIGHT];

            for (int i = 0; i < Arena.HEIGHT; i++)
            {
                for (int j = 0; j < Arena.WIDTH; j++)
                {
                    Cord cord = new Cord(i, j);
                    var x = GameState.GetFieldView(cord);

                    FieldsVM[i * Arena.HEIGHT + j] = new FieldVM
                    {
                        BackgroundPath = x[0],
                        SkillCastingPath = x[1],
                        SkillExecutingPath = x[2],
                        PawnImagePath = x[3],
                        PawnHP = x[4],
                        PawnManna = x[5],
                        PawnClick = new CommandHandler(() => UpdateGame(cord), () => { return true; })

                    };
                }
            }


        }

    }
}
