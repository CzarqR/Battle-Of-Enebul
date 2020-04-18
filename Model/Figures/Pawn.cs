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
        public virtual int PrimaryAttackDmg => 3;
        public virtual int SkillAttackDmg => 5;
        public virtual int Armor => 1;
        public virtual int Condition => 4;
        public virtual int BaseManna => 10;
        public virtual int PrimaryAttackRange => 3;
        public virtual int ExtraAttackRange => 2;
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


        public virtual List<Cord> NormalAttack(GameState gS, Cord defender)
        {
            Console.WriteLine("Atak normal, funkcjia z klasy Pawn");
            List<Cord> cordsToUpdate = new List<Cord>
            {
                defender
            };
            gS.PAt(defender).Def(PrimaryAttackDmg, gS, defender);
            return cordsToUpdate;
        }

        public virtual List<Cord> SkillAttack(GameState gS, Cord defender)
        {
            Console.WriteLine("Atak sklill, funkcjia z klasy Pawn");
            List<Cord> cordsToUpdate = new List<Cord>
            {
                defender
            };
            gS.PAt(defender).Def(SkillAttackDmg, gS, defender);
            return cordsToUpdate;
        }


        public virtual void Def(int dmg, GameState gS, Cord C)
        {
            HP -= (dmg - Armor);
            if (HP <= 0)
            {
                Console.WriteLine("DEAD");
                gS.KillPawn(C);
            }
        }

        public virtual List<Cord> ShowPossibleMove(Cord C, Arena A)

        {
            List<Cord> cordsToUpdate = new List<Cord>();
            for (int i = 0; i <= Condition; i++)
            {
                for (int k = i; k >= -i; k--)
                {
                    if (Arena.IsOK(C, k, i - Condition) && A.PAt(C, k, i - Condition) == null)
                    {
                        A[C, k, i - Condition].FloorStatus = FloorStatus.Move;
                        cordsToUpdate.Add(new Cord(C, k, i - Condition));
                    }
                }
            }
            for (int i = 0; i < Condition; i++)
            {
                for (int k = i; k >= -i; k--)
                {
                    if (Arena.IsOK(C, k, Condition - i) && A.PAt(C, k, Condition - i) == null)
                    {
                        A[C, k, Condition - i].FloorStatus = FloorStatus.Move;
                        cordsToUpdate.Add(new Cord(C, k, Condition - i));
                    }
                }
            }
            A[C].FloorStatus = FloorStatus.Move;
            cordsToUpdate.Add(C);
            return cordsToUpdate;
        }


        public virtual bool IsSomeoneToAttack(Cord C, Arena A, bool attackType) // attackType - true primary, false extra
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


            for (int i = 0; i <= range; i++)
            {
                for (int k = i; k >= -i; k--)
                {
                    if (Arena.IsOK(C, k, i - range) && A.PAt(C, k, i - range) != null && A.PAt(C, k, i - range).Owner != Owner)
                    {
                        return true;
                    }
                }
            }
            for (int i = 0; i < range; i++)
            {
                for (int k = i; k >= -i; k--)
                {
                    if (Arena.IsOK(C, k, range - i) && A.PAt(C, k, range - i) != null && A.PAt(C, k, range - i).Owner != Owner)
                    {
                        return true;
                    }
                }
            }
            return false;
        }


        public virtual List<Cord> ShowPossibleAttack(Cord C, Arena A, bool attackType) // attackType - true primary, false extra
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


            for (int i = 0; i <= range; i++)
            {
                for (int k = i; k >= -i; k--)
                {
                    if (Arena.IsOK(C, k, i - range))
                    {
                        A[C, k, i - range].FloorStatus = FloorStatus.Attack;
                        cordsToUpdate.Add(new Cord(C, k, i - range));
                    }
                }
            }
            for (int i = 0; i < range; i++)
            {
                for (int k = i; k >= -i; k--)
                {
                    if (Arena.IsOK(C, k, range - i))
                    {
                        A[C, k, range - i].FloorStatus = FloorStatus.Attack;
                        cordsToUpdate.Add(new Cord(C, k, range - i));
                    }
                }
            }

            return cordsToUpdate;
        }

        public virtual List<Cord> MarkFieldsToAttack(List<Cord> possibleAttackFields, Arena A, bool attackType)
        {


            List<Cord> markedAttackFields = new List<Cord>();

            foreach (Cord cord in possibleAttackFields)
            {
                if (A.PAt(cord) == null || A.PAt(cord).Owner == Owner)
                {
                    A[cord].FloorStatus = FloorStatus.Normal;
                }
                else
                {
                    markedAttackFields.Add(cord);
                }
            }

            return markedAttackFields;
        }

        #endregion

    }
}


