using ProjectB.Model.Board;
using ProjectB.Model.Help;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace ProjectB.Model.Figures
{
    using R = Properties.Resources;

    class King : Pawn
    {

        #region Properties

        /// Stats
        public override int BaseHp => 35;
        public override int BaseManna => 10;
        public override int Condition => 2;
        public override int Armor => 2;
        public override int PrimaryAttackRange => 1;
        public override int PrimaryAttackCost => 2;
        public override int PrimaryAttackDmg => 5;
        public override int SkillAttackRange => 3;
        public override int SkillAttackCost => 10;
        public override int SkillAttackDmg => 13;
        public override int MannaRegeneration => 1;

        /// Strings
        public override string PrimaryAttackDesc => string.Format(R.king_primary_desc, PrimaryAttackDmg);
        public override string SkillAttackDesc => string.Format(R.king_skill_desc, SkillAttackDmg);
        public override string PrimaryAttackName => string.Format(R.primary_attack_info, R.king_primary_name, PrimaryAttackRange, PrimaryAttackCost);
        public override string SkillAttackName => string.Format(R.skilll_attack_info, R.king_skill_name, SkillAttackRange, SkillAttackCost);
        public override string Class => R.king;
        public override string Desc => R.king_desc;

        protected override UnmanagedMemoryStream GetSound()
        {
            return R.king_attack_0;
        }


        #endregion


        #region Methods

        public King(bool owner, Cord cord) : base(owner, cord) { }

        async public override void Dead(GameState gS)
        {
            await Task.Delay(RENDER_DEF * MAX_FRAME_DEF);
            gS.KillPawn(Cord);
            gS.EndGame();
        }

        public override List<Cord> ShowPossibleAttack(Cord C, Arena A, bool attackType)
        {
            if (attackType)
            {
                List<Cord> cordsToUpdate = new List<Cord>();
                int size = PrimaryAttackRange + 1;

                for (int i = 1; i < size; i++)
                {
                    for (int k = i; k >= -i; k--)
                    {
                        if (Arena.IsOK(C, k, i - size))
                        {
                            A[C, k, i - size].FloorStatus = FloorStatus.Attack;
                            cordsToUpdate.Add(new Cord(C, k, i - size));
                        }
                    }
                }

                for (int i = 1; i < size; i++)
                {
                    for (int k = i; k >= -i; k--)
                    {
                        if (Arena.IsOK(C, k, size - i))
                        {
                            A[C, k, size - i].FloorStatus = FloorStatus.Attack;
                            cordsToUpdate.Add(new Cord(C, k, size - i));
                        }
                    }
                }

                for (int k = 1 - size; k <= size - 1; k++)
                {
                    if (Arena.IsOK(C, k, 0))
                    {
                        A[C, k, 0].FloorStatus = FloorStatus.Attack;
                        cordsToUpdate.Add(new Cord(C, k, 0));
                    }
                }


                A[C].FloorStatus = FloorStatus.Attack;
                cordsToUpdate.Add(C);
                return cordsToUpdate;

            }
            else
            {
                return base.ShowPossibleAttack(C, A, attackType);
            }
        }

        public override bool IsSomeoneToAttack(Cord C, Arena A, bool attackType)
        {
            if (attackType)
            {
                int size = PrimaryAttackRange + 1;

                if (Manna < PrimaryAttackCost)
                {
                    return false;
                }

                for (int i = 1; i < size; i++)
                {
                    for (int k = i; k >= -i; k--)
                    {
                        if (Arena.IsOK(C, k, i - size) && A.PAt(C, k, i - size) != null && A.PAt(C, k, i - size).Owner != Owner)
                        {
                            return true;
                        }
                    }
                }

                for (int i = 1; i < size; i++)
                {
                    for (int k = i; k >= -i; k--)
                    {
                        if (Arena.IsOK(C, k, size - i) && A.PAt(C, k, size - i) != null && A.PAt(C, k, size - i).Owner != Owner)
                        {
                            return true;
                        }
                    }
                }

                for (int k = 1 - size; k <= size - 1; k++)
                {
                    if (Arena.IsOK(C, k, 0) && A.PAt(C, k, 0) != null && A.PAt(C, k, 0).Owner != Owner)
                    {
                        return true;
                    }
                }

                return false;
            }
            else
            {
                return base.IsSomeoneToAttack(C, A, attackType);
            }
        }

        public override List<Cord> ShowPossibleMove(Cord C, Arena A)
        {
            List<Cord> cordsToUpdate = new List<Cord>();

            int size = Condition + A[C].MovementBonus + 1;
            for (int i = 1; i < size; i++)
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

            for (int i = 1; i < size; i++)
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

            for (int k = 1 - size; k <= size - 1; k++)
            {
                if (Arena.IsOK(C, k, 0) && A.PAt(C, k, 0) == null)
                {
                    A[C, k, 0].FloorStatus = FloorStatus.Move;
                    cordsToUpdate.Add(new Cord(C, k, 0));
                }
            }


            A[C].FloorStatus = FloorStatus.Move;
            cordsToUpdate.Add(C);
            return cordsToUpdate;


        }

        #endregion

    }
}
