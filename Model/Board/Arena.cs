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


        private byte move = 0; //0 zaznacz| 1 porusz sie , 2 atak
        private bool turn = true; //czyja kolej

        private readonly List<Cord> lastFields = new List<Cord>();
        private Cord lastCords;


        public delegate void ShowPawnInfo(string imgPath, string floorPath, string baseInfo, string precInfo);
        public event ShowPawnInfo ShowPawnEvent;

        public delegate void OnAttackStart();
        public event OnAttackStart StartAttack;


        public List<Cord> HandleInput(Cord cord) //metoda zwraca kordy wszytkich pol na których sie cos zmieniło żeby okno moglo je zaktualizować
        {

            if (GetFieldAt(cord).PawnOnField != null) //pole z pionkeim
            {
                ShowPawnEvent(board[cord.X, cord.Y].PawnOnField.ImgPath, board[cord.X, cord.Y].FloorPath(), board[cord.X, cord.Y].PawnOnField.BaseInfo(), board[cord.X, cord.Y].PawnOnField.PrecInfo());
            }
            else //sama podloga
            {
                ShowPawnEvent(null, board[cord.X, cord.Y].FloorPath(), board[cord.X, cord.Y].FloorBaseInfo(), board[cord.X, cord.Y].FloorPrecInfo());
            }

            Console.WriteLine("HandleInput dla pola; " + cord);

            if (move == 0)//gracz wybiera pionka którym chce sie ruszyć
            {
                return ShowPossibleMove(cord);
            }
            else if (move == 1) // gracz wybiera miejsce w które chce się ruszyć
            {
                return MovePawnToField(cord);
            }
            else
            {
                throw new NotImplementedException("Not implemented yet");
            }


        }

        private List<Cord> MovePawnToField(Cord cord)
        {

            List<Cord> cordsToUpdate = new List<Cord>();

            if (IsFieldInList(cord)) // move field to cord
            {
                if (!lastCords.Equals(cord)) //ruch na inne pole niz obecne
                {
                    board[cord.X, cord.Y].PawnOnField = board[lastCords.X, lastCords.Y].PawnOnField;
                    board[lastCords.X, lastCords.Y].PawnOnField = null;
                    lastCords = cord;
                    turn ^= true; //zmiana tury
                    ShowPawnEvent(board[cord.X, cord.Y].PawnOnField.ImgPath, board[cord.X, cord.Y].FloorPath(), board[cord.X, cord.Y].PawnOnField.BaseInfo(), board[cord.X, cord.Y].PawnOnField.PrecInfo());
                }

                move = 0;
                foreach (Cord cor in lastFields)
                {
                    board[cor.X, cor.Y].CanMove = false;
                    cordsToUpdate.Add(cor);
                    board[cor.X, cor.Y].FloorStatus = FloorStatus.Normal;
                }

                if (StartAttack != null)
                {
                    Console.WriteLine("Not NULL");
                    StartAttack();
                }

                return cordsToUpdate;
            }
            else // cannot move pawn to cord, cord out of trange
            {
                return cordsToUpdate;
            }


        }

        private List<Cord> ShowPossibleMove(Cord cord)
        {
            List<Cord> cordsToUpdate = new List<Cord>();

            if (GetFieldAt(cord).PawnOnField != null && GetFieldAt(cord).PawnOnField.Owner == turn)
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
                lastFields.Clear();

                board[cord.X, cord.Y].CanMove = true;//dodanie pola na którym znajduje sie dany pionek
                cordsToUpdate.Add(cord);

                foreach (Cord cor in cordsToUpdate)
                {
                    lastFields.Add(cor);
                    board[cor.X, cor.Y].FloorStatus = FloorStatus.Move;
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

            FieldBonusInit();
            PlacePawns();
        }

        private void FieldBonusInit()
        {
            board[5, 5].Floor = FloorType.Attack;

            board[1, 1].Floor = FloorType.Def;
            board[9, 9].Floor = FloorType.Def;
            board[1, 9].Floor = FloorType.Def;
            board[9, 1].Floor = FloorType.Def;

            board[5, 2].Floor = FloorType.Cond;
            board[5, 8].Floor = FloorType.Cond;

        }

        private void PlacePawns()
        {
            //testowe rozlozenie figur. Pozniej treaba zrobic uniwersalna metode która bedzie rozkladac pionki wedlug jakeigos schmtu który mozna latwo zmieniac
            Pawn def = new Defender(false);
            Pawn mag = new Mag(false);
            Pawn king = new King(false);
            Pawn rouge = new Rouge(false);
            Pawn archer = new Archer(false);
            Pawn axeman = new Axeman(false);
            board[4, 5].PawnOnField = def;
            board[2, 3].PawnOnField = mag;
            board[0, 5].PawnOnField = king;
            board[3, 1].PawnOnField = rouge;
            board[2, 7].PawnOnField = archer;
            board[3, 9].PawnOnField = axeman;
            Pawn edef = new Defender(true);
            Pawn emag = new Mag(true);
            Pawn eking = new King(true);
            Pawn erouge = new Rouge(true);
            Pawn earcher = new Archer(true);
            Pawn eaxeman = new Axeman(true);
            board[6, 5].PawnOnField = edef;
            board[8, 7].PawnOnField = emag;
            board[10, 5].PawnOnField = eking;
            board[7, 9].PawnOnField = erouge;
            board[8, 3].PawnOnField = earcher;
            board[7, 1].PawnOnField = eaxeman;

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
