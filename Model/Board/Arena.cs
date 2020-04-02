using ProjectB.Model.Figures;
using ProjectB.Model.Help;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectB.Model.Board
{
    public class Arena
    {


        public static int HEIGHT = 11;
        public static int WIDTH = 11;

        private readonly Field[,] board;





        public List<Cord> HandleInput(Cord cord) //metoda zwraca kordy wszytkich pol na których sie cos zmieniło żeby okno moglo je zaktualizować
        {
            List<Cord> cords = new List<Cord>();
            Console.WriteLine("HandleInput dla pola; " + cord);

            ///TEST czy działa. jesli nacisniete zostalo pole z pionkiem to hp zmniejszy się o 1
            if (GetFieldAt(cord).PawnOnField!=null)
            {
                GetFieldAt(cord).PawnOnField.TestHPDown();
                cords.Add(cord);
            }
            else
            {
                Console.WriteLine("PUSTE pole");
            }

            return cords;
        }

        public Arena() //stworzenie domyslnej pustej szachownicy
        {
            board = new Field[HEIGHT, WIDTH];
            for (int i = 0; i < HEIGHT; i++)
            {
                for (int j = 0; j < WIDTH; j++)
                {
                    board[i, j] = new Field();
                }
            }
            PlacePawns();
        }

        private void PlacePawns()
        {
            //testowe rozlozenie figur. Pozniej treaba zrobic uniwersalna metode która bedzie rozkladac pionki wedlug jakeigos schmtu który mozna latwo zmieniac
            Pawn pawn1 = new Tank();
            Pawn pawn2 = new Tank();
            board[1, 2].PawnOnField = pawn1;
            board[10, 10].PawnOnField = pawn2;
        }


        public void Fight(Field attacker, Field defender) //walka, jakargument dwa pola szachownicy
        {
            //TODO Logika walki
        }

        public bool CanBeAttacked(Field attacker, Field defender)
        {
            //TODO sprawdzenie czy defender moze zostac zaatakowany
            throw new NotImplementedException();
        }

        public Field GetFieldAt(int x, int y)
        {
            return board[x, y];
        }

        public Field GetFieldAt(Cord cord)
        {
            return board[cord.X, cord.Y];
        }




    }
}
