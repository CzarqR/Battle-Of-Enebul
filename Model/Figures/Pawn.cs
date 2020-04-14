using ProjectB.Model.Board;
using ProjectB.Model.Help;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectB.Model.Figures
{
    public abstract class Pawn //To jest klasa bazowa czyli pionek
    {


        #region properties

        public virtual int BaseHp => 10;
        public virtual int BaseAttack => 3;
        public virtual int ExtraAttack => 5;
        public virtual int BaseDef => 1;
        public virtual int BaseCondition => 3;
        public virtual int BaseManna => 10;
        public virtual int PrimaryAttackRange => 2;
        public virtual int ExtraAttackRange => 1;
        public virtual string BaseInfo => "To jest przykładowy tekst podstawoego info o pionku";
        public virtual string PrecInfo => "To jest specyzownay opi pionka. BLa bla lnasd asdasfasf gsd fsd f sdf sdg sd gss\nSiła ataku : duzo\nObrona : też";

        public string ImgPath => string.Format(App.pathToPawn, this.GetType().Name.ToLower(), (Owner ? "blue" : "red"));


        protected int hp;
        public int HP
        {
            get
            {
                return hp;
            }
            protected set
            {
                hp = value;
            }
        }

        protected int manna;
        public int Manna
        {
            get
            {
                return manna;
            }
            protected set
            {
                manna = value;
            }
        }

        public bool Owner // true = blue, false = red
        {
            get;
            protected set;
        }




        #endregion


        protected Pawn(bool owner)
        {
            Owner = owner;
            HP = BaseHp;
            Manna = BaseManna;
        }

        #region methods


        public virtual List<Cord> NormalAttack(Field[,] board, Cord defender)
        {
            Console.WriteLine("Atak normal, funkcjia z klasy Pawn");
            //TODO jesli pionek ma innym atak to trzeba zmienic
            List<Cord> cordsToUpdate = new List<Cord>
            {
                defender
            };
            board[defender.X, defender.Y].PawnOnField.Def(BaseAttack);
            return cordsToUpdate;
        }

        public virtual List<Cord> SkillAttack(Field[,] board, Cord defender)
        {
            Console.WriteLine("Atak sklill, funkcjia z klasy Pawn");
            //TODO jesli pionek ma innym atak to trzeba zmienic
            List<Cord> cordsToUpdate = new List<Cord>
            {
                defender
            };
            board[defender.X, defender.Y].PawnOnField.Def(ExtraAttack);
            return cordsToUpdate;
        }


        public virtual void Def(int dmg)
        {
            HP -= (dmg - BaseDef);
            //TODO jak bedzie znana logika gry
        }

        public virtual List<Cord> ShowPossibleMove(Cord cord, Field[,] board)
        {
            List<Cord> cordsToUpdate = new List<Cord>();

            int j = 0;

            for (int i = -BaseCondition; i <= 0; i++)
            {
                j++;
                for (int k = 0; k < j; k++)
                {
                    if (cord.X + i >= 0 && cord.X + i <= 10)
                    {
                        if (cord.Y + k >= 0 && cord.Y + k <= 10 && board[cord.X + i, cord.Y + k].PawnOnField == null)
                        {
                            board[cord.X + i, cord.Y + k].FloorStatus = FloorStatus.Move;
                            cordsToUpdate.Add(new Cord(cord.X + i, cord.Y + k));
                        }
                        if (cord.Y - k >= 0 && cord.Y - k <= 10 && board[cord.X + i, cord.Y - k].PawnOnField == null)
                        {
                            board[cord.X + i, cord.Y - k].FloorStatus = FloorStatus.Move;
                            cordsToUpdate.Add(new Cord(cord.X + i, cord.Y - k));
                        }

                    }
                }
            }
            j--;
            for (int i = 1; i <= BaseCondition; i++)
            {
                j--;
                for (int k = j; k >= 0; k--)
                {
                    if (cord.X + i >= 0 && cord.X + i <= 10)
                    {
                        if (cord.Y + k >= 0 && cord.Y + k <= 10 && board[cord.X + i, cord.Y + k].PawnOnField == null)
                        {
                            board[cord.X + i, cord.Y + k].FloorStatus = FloorStatus.Move;
                            cordsToUpdate.Add(new Cord(cord.X + i, cord.Y + k));
                        }
                        if (cord.Y - k >= 0 && cord.Y - k <= 10 && board[cord.X + i, cord.Y - k].PawnOnField == null)
                        {
                            board[cord.X + i, cord.Y - k].FloorStatus = FloorStatus.Move;
                            cordsToUpdate.Add(new Cord(cord.X + i, cord.Y - k));
                        }

                    }
                }
            }

            board[cord.X, cord.Y].FloorStatus = FloorStatus.Move;
            cordsToUpdate.Add(cord);

            return cordsToUpdate;
        }


        public virtual bool IsSomeoneToAttack(Cord cord, Field[,] board, bool attackType) // attackType - true primary, false extra
        {
            int range;
            if (attackType)
            {
                range = PrimaryAttackRange;
            }
            else
            {
                range = ExtraAttackRange;
            }

            int j = 0;
            for (int i = -range; i <= 0; i++)
            {
                j++;
                for (int k = 0; k < j; k++)
                {
                    if (cord.X + i >= 0 && cord.X + i <= 10)
                    {
                        if (cord.Y + k >= 0 && cord.Y + k <= 10 && board[cord.X + i, cord.Y + k].PawnOnField != null && board[cord.X + i, cord.Y + k].PawnOnField.Owner != Owner)
                        {
                            return true;
                        }

                        if (cord.Y - k >= 0 && cord.Y - k <= 10 && board[cord.X + i, cord.Y - k].PawnOnField != null && board[cord.X + i, cord.Y - k].PawnOnField.Owner != Owner)
                        {
                            return true;
                        }

                    }
                }
            }
            j--;
            for (int i = 1; i <= range; i++)
            {
                j--;
                for (int k = j; k >= 0; k--)
                {
                    if (cord.X + i >= 0 && cord.X + i <= 10)
                    {
                        if (cord.Y + k >= 0 && cord.Y + k <= 10 && board[cord.X + i, cord.Y + k].PawnOnField != null && board[cord.X + i, cord.Y + k].PawnOnField.Owner != Owner)
                        {
                            return true;
                        }
                        if (cord.Y - k >= 0 && cord.Y - k <= 10 && board[cord.X + i, cord.Y - k].PawnOnField != null && board[cord.X + i, cord.Y - k].PawnOnField.Owner != Owner)
                        {
                            return true;
                        }

                    }
                }
            }
            return false;
        }


        public List<Cord> ShowPossibleAttack(Cord cord, Field[,] board, bool attackType)
        {

            List<Cord> cordsToUpdate = new List<Cord>();
            int range;
            if (attackType)
            {
                range = PrimaryAttackRange;
            }
            else
            {
                range = ExtraAttackRange;
            }


            int j = 0;

            for (int i = -range; i <= 0; i++)
            {
                j++;
                for (int k = 0; k < j; k++)
                {
                    if (cord.X + i >= 0 && cord.X + i <= 10)
                    {
                        if (cord.Y + k >= 0 && cord.Y + k <= 10)
                        {
                            board[cord.X + i, cord.Y + k].FloorStatus = FloorStatus.Attack;
                            cordsToUpdate.Add(new Cord(cord.X + i, cord.Y + k));
                        }
                        if (cord.Y - k >= 0 && cord.Y - k <= 10)
                        {
                            board[cord.X + i, cord.Y - k].FloorStatus = FloorStatus.Attack;
                            cordsToUpdate.Add(new Cord(cord.X + i, cord.Y - k));
                        }

                    }
                }
            }
            j--;
            for (int i = 1; i <= range; i++)
            {
                j--;
                for (int k = j; k >= 0; k--)
                {
                    if (cord.X + i >= 0 && cord.X + i <= 10)
                    {
                        if (cord.Y + k >= 0 && cord.Y + k <= 10)
                        {
                            board[cord.X + i, cord.Y + k].FloorStatus = FloorStatus.Attack;
                            cordsToUpdate.Add(new Cord(cord.X + i, cord.Y + k));
                        }
                        if (cord.Y - k >= 0 && cord.Y - k <= 10)
                        {
                            board[cord.X + i, cord.Y - k].FloorStatus = FloorStatus.Attack;
                            cordsToUpdate.Add(new Cord(cord.X + i, cord.Y - k));
                        }

                    }
                }
            }

            board[cord.X, cord.Y].FloorStatus = FloorStatus.Attack; //dodanie pola na którym znajduje sie dany pionek
            cordsToUpdate.Add(cord);

            return cordsToUpdate;
        }

        #endregion

    }
}


