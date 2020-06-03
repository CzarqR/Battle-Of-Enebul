using ProjectB.Model.Board;
using ProjectB.Model.Help;
using ProjectB.Model.Render;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using System.Timers;

namespace ProjectB.Model.Figures
{
    using R = Properties.Resources;

    public abstract class Pawn : IDisposable
    {

        #region Animationts

        protected static readonly Random random = new Random();
        protected const int RENDER_MIN = 1000;
        protected const int RENDER_MAX = 1600;
        protected const int RENDER_ATTACK = 90;
        protected const int RENDER_DEF = RENDER_ATTACK * MAX_FRAME_ATTACK / MAX_FRAME_DEF;
        protected const byte MAX_FRAME_ATTACK = 5;
        protected const byte MAX_FRAME_MOVE = 2;
        protected const byte MAX_FRAME_DEF = 4;
        protected int NextFrame => random.Next(RENDER_MIN, RENDER_MAX);
        protected readonly Timer timer = new Timer();
        protected bool turn; // true left, false right
        protected string State = App.idle;
        protected string Color => Owner ? App.blue : App.red;
        protected string Turn => turn ? App.l : App.r;
        protected byte frameIdle = 1;
        protected byte frameAttack = 1;
        protected byte frameDef = 1;
        protected byte Frame => State == App.idle ? frameIdle : State == App.attack ? frameAttack : frameDef;

        protected void Animate(object source, ElapsedEventArgs e)
        {
            if (State == App.idle) //idle
            {
                frameIdle %= MAX_FRAME_MOVE;
                frameIdle++;
                timer.Interval = NextFrame;
            }
            else if (State == App.attack)  //attack
            {
                if (frameAttack == MAX_FRAME_ATTACK)
                {
                    State = App.idle;
                }

                frameAttack %= MAX_FRAME_ATTACK;
                frameAttack++;
                timer.Interval = RENDER_ATTACK;
            }
            else //def (move)
            {
                if (frameDef == MAX_FRAME_DEF)
                {
                    State = App.idle;
                }

                frameDef %= MAX_FRAME_DEF;
                frameDef++;
                timer.Interval = RENDER_DEF;
            }

            RenderEngine.TriggerUpdate(Cord);
        }

        protected void InitAnimation()
        {
            timer.Elapsed += new ElapsedEventHandler(Animate);
            timer.Interval = NextFrame;
            timer.Enabled = true;
        }

        #endregion


        #region Properties

        /// Stats
        public virtual int BaseHp => 50;
        public virtual int BaseManna => 10;
        public virtual int MannaRegeneration => 1;
        public virtual int Armor => 4;
        public virtual int Condition => 2;
        public virtual int PrimaryAttackDmg => 4;
        public virtual int SkillAttackDmg => 8;
        public virtual int PrimaryAttackRange => 3;
        public virtual int SkillAttackRange => 4;
        public virtual int PrimaryAttackCost => 0;
        public virtual int SkillAttackCost => 5;

        /// Strings
        public string Title => string.Format(R.pawn_title, Class, (Owner ? R.enebul : R.marbang));
        public virtual string Desc => null;
        public virtual string Class => null;
        public virtual string PrimaryAttackName => null;
        public virtual string SkillAttackName => null;
        public virtual string PrimaryAttackDesc => null;
        public virtual string SkillAttackDesc => null;
        public string ImgPath => string.Format(App.pathToPawn, GetType().Name.ToLower(), Color, State, Turn, Frame); // 0 - class, 1 - color, 2 - attack/move, 3 - turn, 4 frame
        public string ImgBigPath => string.Format(App.pathToBigPawn, this.GetType().Name.ToLower(), (Owner ? "blue" : "red"));

        public UnmanagedMemoryStream AttackSound => GetSound();
        protected virtual UnmanagedMemoryStream GetSound()
        {
            return null;
        }


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
                if (Cord != null)
                {
                    RenderEngine.TriggerUpdate(Cord);
                }
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
                if (Cord != null)
                {
                    RenderEngine.TriggerUpdate(Cord);
                }
            }
        }

        public bool Owner // true = blue, false = red
        {
            get;
            protected set;
        }

        public Cord Cord
        {
            get;
            set;
        }


        #endregion


        #region Methods

        protected Pawn(bool owner, Cord cord)
        {
            turn = Owner = owner;
            HP = BaseHp;
            Manna = BaseManna;
            Cord = cord;

            InitAnimation();
        }

        public virtual string Bonuses(FloorType floorType)
        {
            string attack = string.Empty;
            string cond = string.Empty;
            string def = string.Empty;

            if (floorType == FloorType.Attack)
            {
                attack = $"+1";
            }
            else if (floorType == FloorType.Cond)
            {
                cond = $"+1";
            }
            if (floorType == FloorType.Def)
            {
                def = $"+1";
            }
            return string.Format(R.stats_info, PrimaryAttackDmg, SkillAttackDmg, PrimaryAttackRange, SkillAttackRange, Condition, Armor, attack, cond, def);
        }

        public virtual void NormalAttack(GameState gS, Cord defender, int bonus)
        {

            TurnAttack(defender);
            State = App.attack;
            timer.Interval = RENDER_ATTACK;

            Manna -= PrimaryAttackCost;
            gS.PAt(defender).Def(PrimaryAttackDmg + bonus + gS.At(Cord).AttackBonus, gS, Cord);
        }

        public virtual void SkillAttack(GameState gS, Cord defender, int bonus)
        {

            TurnAttack(defender);
            State = App.attack;
            timer.Interval = RENDER_ATTACK;

            Manna -= SkillAttackCost;
            gS.PAt(defender).Def(SkillAttackDmg + bonus + gS.At(Cord).AttackBonus, gS, Cord);
        }

        protected void TurnDef(Cord attacker)
        {
            if (Cord.Y > attacker.Y)
            {
                turn = true;
            }
            else if (Cord.Y < attacker.Y)
            {
                turn = false;
            }
        }

        protected void TurnAttack(Cord attacker)
        {
            if (Cord.Y > attacker.Y)
            {
                turn = true;
            }
            else if (Cord.Y < attacker.Y)
            {
                turn = false;
            }
        }

        public virtual void Def(int dmg, GameState gS, Cord attacker)
        {
            TurnDef(attacker);
            State = App.move; //def
            timer.Interval = RENDER_DEF;

            double reduction = (Convert.ToDouble(Armor) + gS.At(Cord).DefBonus) / 10.0;
            int savedHP = (int)(reduction * dmg);
            HP -= (dmg - savedHP); //1 armor point reduce 10% of dmg
            if (HP <= 0)
            {
                Dead(gS);
            }
        }

        async public virtual void Dead(GameState gS)
        {
            Console.WriteLine("Dead");
            await Task.Delay(RENDER_DEF * MAX_FRAME_DEF);
            gS.KillPawn(Cord);

        }

        public virtual List<Cord> ShowPossibleMove(Cord C, Arena A)

        {
            List<Cord> cordsToUpdate = new List<Cord>();
            int size = Condition + A[C].MovementBonus;
            for (int i = 0; i <= size; i++)
            {
                for (int k = i; k >= -i; k--)
                {
                    if (Arena.IsOK(C, k, i - size) && A.PAt(C, k, i - size) == null)
                    {
                        A[C, k, i - size].FloorStatus = FloorStatus.Move;
                        cordsToUpdate.Add(new Cord(C, k, i - size));
                    }
                }
            }
            for (int i = 0; i < size; i++)
            {
                for (int k = i; k >= -i; k--)
                {
                    if (Arena.IsOK(C, k, size - i) && A.PAt(C, k, size - i) == null)
                    {
                        A[C, k, size - i].FloorStatus = FloorStatus.Move;
                        cordsToUpdate.Add(new Cord(C, k, size - i));
                    }
                }
            }
            A[C].FloorStatus = FloorStatus.Move;
            cordsToUpdate.Add(C);
            return cordsToUpdate;
        }

        public virtual bool IsSomeoneToAttack(Cord C, Arena A, bool attackType) // attackType - true primary, false skill
        {
            int range;
            if (attackType) //primary attack
            {
                if (Manna < PrimaryAttackCost)
                {
                    return false;
                }
                range = PrimaryAttackRange;
            }
            else // skill attack
            {
                if (Manna < SkillAttackCost)
                {
                    return false;
                }
                range = SkillAttackRange;
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

        public virtual void MannaRegenerationAtNewRound()
        {
            if (Manna < BaseManna)
            {
                Manna += MannaRegeneration;
            }
            if (Manna > BaseManna)
            {
                Manna = BaseManna;
            }
        }

        public virtual List<Cord> ShowPossibleAttack(Cord C, Arena A, bool attackType) // attackType - true primary, false skill
        {

            List<Cord> cordsToUpdate = new List<Cord>();
            int range;
            if (attackType)
            {
                range = PrimaryAttackRange;
            }
            else
            {
                range = SkillAttackRange;
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

        public virtual void HPRegeneration(int hp, Cord healer)
        {
            Console.WriteLine($"Pawn at {Cord} healed by {healer} with {hp} hp");
            TurnDef(healer);
            State = App.move; //def
            timer.Interval = RENDER_DEF;
            if (HP + hp > BaseHp)
            {
                HP = BaseHp;
            }
            else
            {
                HP += hp;
            }
        }


        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            Console.WriteLine("Dispose Pawn");
            if (disposing)
            {
                timer.Dispose();
            }
        }

        #endregion

    }

}