﻿using ProjectB.Model.Board;
using ProjectB.Model.Help;
using ProjectB.Model.Sklills;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO.Packaging;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;

namespace ProjectB.Model.Figures
{
    using R = Properties.Resources;

    public abstract class Pawn : IDisposable //To jest klasa bazowa czyli pionek
    {
        private static readonly Random random = new Random();
        private const int RENDER_MIN = 700;
        private const int RENDER_MAX = 1000;
        private const int RENDER_ATTACK = 150;
        private const byte MAX_FRAME_ATTACK = 5;
        private const byte MAX_FRAME_MOVE = 2;

        private int NextFrame => random.Next(RENDER_MIN, RENDER_MAX);

        private readonly System.Timers.Timer timer;
        private bool turn; // true left, false right
        private bool state = true; // true move, false attack
        private string Color => Owner ? App.blue : App.red;
        private string State => state ? App.idle : App.attack;
        private string Turn => turn ? App.left : App.right;
        private byte frameIdle = 1;
        private byte frameAttack = 1;
        private byte Frame => state ? frameIdle : frameAttack;

        #region properties

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

        public virtual string Title => string.Format(R.pawn_title, Class, (Owner ? R.enebul : R.marbang));
        public virtual string Desc => null;
        public virtual string Class => null;

        public virtual string PrimaryAttackName => null;
        public virtual string SkillAttackName => null;
        public virtual string PrimaryAttackDesc => null;
        public virtual string SkillAttackDesc => null;




        public string ImgPath => string.Format(App.pathToPawn, GetType().Name.ToLower(), Color, State, Turn, Frame); // 0 - class, 1 - color, 2 - attack/move, 3 - turn, 4 frame
        public string ImgBigPath => string.Format(App.pathToBigPawn, this.GetType().Name.ToLower(), (Owner ? "blue" : "red"));


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
            turn = Owner;

            timer = new System.Timers.Timer();
            timer.Elapsed += new ElapsedEventHandler(Animate);
            timer.Interval = NextFrame;
            timer.Enabled = true;

        }


        private void Animate(object source, ElapsedEventArgs e)
        {
            if (state) //move
            {
                frameIdle %= MAX_FRAME_MOVE;
                frameIdle++;
                timer.Interval = NextFrame;
            }
            else //attack
            {
                if (frameAttack == MAX_FRAME_ATTACK)
                {
                    state = true;
                }

                frameAttack %= MAX_FRAME_ATTACK;
                frameAttack++;

                timer.Interval = RENDER_ATTACK;

            }

        }





        #region methods

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

        public void TestHpDown()
        {
            HP--;
        }

        public virtual void NormalAttack(GameState gS, Cord defender, int bonus, Cord attacker)
        {

            TurnAttack(defender, attacker);
            state = false;
            Manna -= PrimaryAttackCost;
            Console.WriteLine("Atak primary, funkcja z klasy Pawn");
            gS.PAt(defender).Def(PrimaryAttackDmg + bonus + gS.At(attacker).AttackBonus, gS, defender, attacker);
        }

        public virtual void SkillAttack(GameState gS, Cord defender, int bonus, Cord attacker)
        {

            TurnAttack(defender, attacker);


            state = false;
            Manna -= SkillAttackCost;
            Console.WriteLine("Atak sklill, funkcja z klasy Pawn");
            gS.PAt(defender).Def(SkillAttackDmg + bonus + gS.At(attacker).AttackBonus, gS, defender, attacker);
        }

        protected void TurnDef(Cord defender, Cord attacker)
        {
            if (defender.Y > attacker.Y)
            {
                turn = true;
            }
            else if (defender.Y < attacker.Y)
            {
                turn = false;
            }
        }

        protected void TurnAttack(Cord defender, Cord attacker)
        {
            if (defender.Y > attacker.Y)
            {
                turn = false;
            }
            else if (defender.Y < attacker.Y)
            {
                turn = true;
            }
        }

        public virtual void Def(int dmg, GameState gS, Cord defender, Cord attacker)
        {
            TurnDef(defender, attacker);
            double reduction = (Convert.ToDouble(Armor) + gS.At(defender).DefBonus) / 10.0;
            int savedHP = (int)(reduction * dmg);
            HP -= (dmg - savedHP); //1 armor point reduce 10% of dmg
            if (HP <= 0)
            {
                Dead(gS, defender);
            }
        }

        public virtual void Dead(GameState gS, Cord C)
        {
            Console.WriteLine("DEAD");
            gS.KillPawn(C);

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


        public virtual bool IsSomeoneToAttack(Cord C, Arena A, bool attackType) // attackType - true primary, false extra
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




        #region Animmations




        #endregion



        #endregion

    }
}


