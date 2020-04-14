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


        private byte move = 0; //0 zaznacz| 1 porusz sie | 2 atak
        private bool turn = true; //czyja kolej
        private bool attackType; //true - primary, false - extra
        private bool attackChosen = false; //czy został wybrany atak

        private List<Cord> lastFields = new List<Cord>();
        private List<Cord> possibleAttackFields = new List<Cord>();
        private List<Cord> markedAttackFields = new List<Cord>();
        private Cord cordToMove;
        private Cord cordToAttack;




        public delegate void ShowPawnInfo(string imgPath, string floorPath, string baseInfo, string precInfo);
        public event ShowPawnInfo ShowPawnEvent;

        public delegate void OnAttackStart(bool primaryAttack, bool extraAttack);
        public event OnAttackStart StartAttack;

        public delegate void FieldToAttackSelected();
        public event FieldToAttackSelected SelectedFieldToAttack;

        public delegate void EndRoundD();
        public event EndRoundD EndRoundEvent;


        public List<Cord> HandleInput(Cord cord) //metoda zwraca kordy wszytkich pol na których sie cos zmieniło żeby okno moglo je zaktualizować
        {

            if (At(cord).PawnOnField != null) //pole z pionkeim
            {
                ShowPawnEvent(board[cord.X, cord.Y].PawnOnField.ImgPath, board[cord.X, cord.Y].FloorPath(), board[cord.X, cord.Y].PawnOnField.BaseInfo, board[cord.X, cord.Y].PawnOnField.PrecInfo);
            }
            else //sama podloga
            {
                ShowPawnEvent(null, board[cord.X, cord.Y].FloorPath(), board[cord.X, cord.Y].FloorBaseInfo, board[cord.X, cord.Y].FloorPrecInfo);
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
            else if (move == 2) //gracz wybiera pole które chce zaatakować
            {

                return AttackField(cord);


            }
            return null;
        }

        public List<Cord> AttackField(Cord cord) //wybor pionka do zaatakowania
        {

            if (board[cord.X, cord.Y].FloorStatus == FloorStatus.Attack)
            {
                cordToAttack = cord;
                SelectedFieldToAttack?.Invoke();


                foreach (Cord cor in markedAttackFields)
                {
                    if (!cor.Equals(cord))
                    {
                        At(cor).FloorStatus = FloorStatus.Normal;
                    }
                }
                return markedAttackFields;
            }
            return null;
        }

        public List<Cord> ExecuteAttack(int bonus1, int bonus2)
        {
            string x = attackType ? "Podstawowym" : "Extra";
            Console.WriteLine($"Pionek na polu {cordToMove} z bonusem {bonus1} atakuje atakiem {x} pionka na polu {cordToAttack} z bonusem {bonus2}");

            //tutaj wykonuje sie funckja ataku
            if (attackType)
            {
                return (board[cordToMove.X, cordToMove.Y].PawnOnField.NormalAttack(board, cordToAttack)).Concat(EndRound()).ToList();
            }
            else
            {
                return (board[cordToMove.X, cordToMove.Y].PawnOnField.SkillAttack(board, cordToAttack)).Concat(EndRound()).ToList();
            }
        }




        public List<Cord> ShowPossiblePrimaryAttack()
        {

            if (!attackChosen)
            {
                return possibleAttackFields = At(cordToMove).PawnOnField.ShowPossibleAttack(cordToMove, board, true);
            }
            else
            {
                return new List<Cord>();
            }
        }

        public List<Cord> ShowPossibleExtraAttack()
        {

            if (!attackChosen)
            {
                return possibleAttackFields = At(cordToMove).PawnOnField.ShowPossibleAttack(cordToMove, board, false);
            }
            else
            {
                return new List<Cord>();
            }
        }

        public List<Cord> HideAttackFields()
        {
            List<Cord> cordsToUpdate = new List<Cord>();

            if (!attackChosen)
            {
                foreach (Cord cord in possibleAttackFields)
                {
                    cordsToUpdate.Add(cord);
                    board[cord.X, cord.Y].FloorStatus = FloorStatus.Normal;
                }
            }
            return cordsToUpdate;
        }


        private List<Cord> MovePawnToField(Cord cord)
        {

            List<Cord> cordsToUpdate = new List<Cord>();

            if (board[cord.X, cord.Y].FloorStatus == FloorStatus.Move) // move field to cord
            {
                if (!cordToMove.Equals(cord)) //ruch na inne pole niz obecne
                {
                    board[cord.X, cord.Y].PawnOnField = board[cordToMove.X, cordToMove.Y].PawnOnField;
                    board[cordToMove.X, cordToMove.Y].PawnOnField = null;
                    cordToMove = cord;
                    move = 2;

                    StartAttack?.Invoke(At(cord).PawnOnField.IsSomeoneToAttack(cord, board, true), At(cord).PawnOnField.IsSomeoneToAttack(cord, board, false));

                }
                else // nacisniecie na pole na ktorym byl pionek, anulowanie ruchu
                {
                    move = 0;
                }
                foreach (Cord cor in lastFields)
                {
                    board[cor.X, cor.Y].FloorStatus = FloorStatus.Normal;
                    cordsToUpdate.Add(cor);
                }
                ShowPawnEvent(board[cord.X, cord.Y].PawnOnField.ImgPath, board[cord.X, cord.Y].FloorPath(), board[cord.X, cord.Y].PawnOnField.BaseInfo, board[cord.X, cord.Y].PawnOnField.PrecInfo);
            }
            return cordsToUpdate;
        }

        private List<Cord> ShowPossibleMove(Cord cord)
        {

            if (At(cord).PawnOnField != null && At(cord).PawnOnField.Owner == turn) //click on own pawn
            {
                move = 1;
                cordToMove = cord;
                return lastFields = At(cord).PawnOnField.ShowPossibleMove(cord, board);
            }
            else //click on empty field or enemy pawn
            {
                lastFields.Clear();
                return lastFields;
            }
        }

        public List<Cord> EndRound()
        {
            Console.WriteLine("end round");
            turn ^= true;
            attackChosen = false;
            EndRoundEvent?.Invoke();
            if (move == 0)
            {
                Console.WriteLine("0");
                return new List<Cord>();
            }
            else if (move == 1)
            {
                move = 0;
                Console.WriteLine("1");
                foreach (Cord cord in lastFields)
                {
                    board[cord.X, cord.Y].FloorStatus = FloorStatus.Normal;
                }
                return lastFields;
            }
            else if (move == 2)
            {
                move = 0;
                Console.WriteLine("2");
                foreach (Cord cord in markedAttackFields)
                {
                    board[cord.X, cord.Y].FloorStatus = FloorStatus.Normal;
                }
                return markedAttackFields;
            }
            throw new NotImplementedException();
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

        public List<Cord> MarkFieldsToAttack(bool attackType)
        {
            this.attackType = attackType;
            foreach (Cord cord in possibleAttackFields)
            {
                if (board[cord.X, cord.Y].PawnOnField == null || board[cord.X, cord.Y].PawnOnField.Owner == turn)
                {
                    board[cord.X, cord.Y].FloorStatus = FloorStatus.Normal;
                }
                else
                {
                    markedAttackFields.Add(cord);
                }
            }
            attackChosen = true;
            return possibleAttackFields;

        }

        public Field At(int x, int y)
        {
            return board[x, y];
        }

        public Field At(Cord cord)
        {
            return board[cord.X, cord.Y];
        }




    }
}



