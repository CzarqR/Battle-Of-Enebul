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
        public virtual int BaseAttack => 2;
        public virtual int ExtraAttack => 4;
        public virtual int BaseDef => 1;
        public virtual int BaseCondition => 2;
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

        public virtual void Move()
        {
            //TODO jak bedzie znana logika gry
        }




        #endregion



    }
}


