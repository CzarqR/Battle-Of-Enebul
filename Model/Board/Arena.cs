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


        private byte move = 0; // zaznacz| 1 porusz sie 

        private List<Cord> lastFields;
        private Cord lastCords;


        public List<Cord> HandleInput(Cord cord) //metoda zwraca kordy wszytkich pol na których sie cos zmieniło żeby okno moglo je zaktualizować
        {
            List<Cord> cordsToUpdate = new List<Cord>();
            Console.WriteLine("HandleInput dla pola; " + cord);

            if (move == 0)
            {
                if (GetFieldAt(cord).PawnOnField != null)
                {

                    int cond = board[cord.X, cord.Y].PawnOnField.BaseCondition();
                    int j = 0;


                    for (int i = -cond; i <= 0; i++)
                    {
                        j++;
                        for (int k = 0; k < j; k++)
                        {
                            if (cord.X + i >= 0 && cord.X + i <= 10)
                            {
                                if (cord.Y + k >= 0 && cord.Y + k <= 10 && board[cord.X + i, cord.Y + k].PawnOnField == null)
                                {
                                    board[cord.X + i, cord.Y + k].CanMove = true;
                                    cordsToUpdate.Add(new Cord(cord.X + i, cord.Y + k));
                                }
                                if (cord.Y - k >= 0 && cord.Y - k <= 10 && board[cord.X + i, cord.Y - k].PawnOnField == null)
                                {
                                    board[cord.X + i, cord.Y - k].CanMove = true;
                                    cordsToUpdate.Add(new Cord(cord.X + i, cord.Y - k));
                                }

                            }
                        }
                    }
                    j--;
                    for (int i = 1; i <= cond; i++)
                    {
                        j--;
                        for (int k = j; k >= 0; k--)
                        {
                            if (cord.X + i >= 0 && cord.X + i <= 10)
                            {
                                if (cord.Y + k >= 0 && cord.Y + k <= 10 && board[cord.X + i, cord.Y + k].PawnOnField == null)
                                {
                                    board[cord.X + i, cord.Y + k].CanMove = true;
                                    cordsToUpdate.Add(new Cord(cord.X + i, cord.Y + k));
                                }
                                if (cord.Y - k >= 0 && cord.Y - k <= 10 && board[cord.X + i, cord.Y - k].PawnOnField == null)
                                {
                                    board[cord.X + i, cord.Y - k].CanMove = true;
                                    cordsToUpdate.Add(new Cord(cord.X + i, cord.Y - k));
                                }

                            }
                        }
                    }
                    move = 1;
                    lastFields = new List<Cord>();
                    foreach (Cord item in cordsToUpdate)
                    {
                        lastFields.Add(item);
                    }

                }

            }


            else if (move == 1)
            {
                if (IsFieldInList(cord))
                {
                    Console.WriteLine("YES");
                    if (!lastCords.Equals(cord))
                    {
                        board[cord.X, cord.Y].PawnOnField = board[lastCords.X, lastCords.Y].PawnOnField;
                        board[lastCords.X, lastCords.Y].PawnOnField = null;
                        cordsToUpdate.Add(lastCords);
                        cordsToUpdate.Add(cord);
                        move = 0;
                        foreach (Cord cor in lastFields)
                        {
                            board[cor.X, cor.Y].CanMove = false;
                            cordsToUpdate.Add(cor);
                        }
                    }

                }
                else
                {
                    foreach (Cord cor in lastFields)
                    {
                        board[cor.X, cor.Y].CanMove = false;
                        cordsToUpdate.Add(cor);
                    }
                    move = 0;
                }
            }


            lastCords = cord;
            return cordsToUpdate;
        }

        private bool IsFieldInList(Cord cord)
        {
            foreach (Cord item in lastFields)
            {
                if (item.Equals(cord))
                {
                    return true;
                }
            }

            return false;

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
            Pawn pawn1 = new Defender(true);
            Pawn pawn2 = new Mag(false);
            board[8, 7].PawnOnField = pawn1;
            board[8, 8].PawnOnField = pawn2;

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
