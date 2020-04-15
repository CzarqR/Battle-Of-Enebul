using ProjectB.Model.Board;
using ProjectB.Model.Help;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Documents;

namespace ProjectB.Model.Figures
{
    class Mag : Pawn
    {
        #region properties

        public override int BaseHp => 8;
        public override int ExtraAttackRange => 5;




        #endregion


        #region methods  
        public Mag(bool owner) : base(owner)
        {

        }


        public override bool IsSomeoneToAttack(Cord cord, Field[,] board, bool attackType)
        {
            if (attackType) //primary attack
            {
                return base.IsSomeoneToAttack(cord, board, attackType);
            }
            else
            {
                return true;
            }
        }

        public override List<Cord> ShowPossibleAttack(Cord cord, Field[,] board, bool attackType)
        {

            if (attackType) //primary attack
            {
                return base.ShowPossibleAttack(cord, board, attackType);
            }
            else
            {
                List<Cord> cordsToUpdate = new List<Cord>();
                int j = 0;
                for (int i = -ExtraAttackRange; i <= 0; i++)
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
                for (int i = 1; i <= ExtraAttackRange; i++)
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
                return cordsToUpdate;
            }
        }

        public override List<Cord> MarkFieldsToAttack(List<Cord> possibleAttackFields, Field[,] board, bool attackType)
        {
            if (attackType) //primary attack
            {
                return base.MarkFieldsToAttack(possibleAttackFields, board, attackType);
            }
            else
            {
                return possibleAttackFields;
            }
        }

        public override List<Cord> SkillAttack(Arena arena, Cord defender)
        {
            arena.At(defender).MagSkill = MagSkillStatus.Casting;
            arena.At(defender).SkillOwner = Owner;
            arena.AddMagSkillAttack(defender, Owner, 3, 2);
            return new List<Cord>
            {
                defender
            };

        }

        #endregion


    }

    class MagSkill
    {
        public Cord AttackPlace
        {
            get; set;
        }

        public bool AttackOwner
        {
            get; set;
        }

        public byte RoundsToExec
        {
            get; set;
        }

        public int Dmg
        {
            get; set;
        }

        public MagSkill(Cord attackPlace, bool attackOwner, int dmg, byte execNextRound = 3)
        {
            AttackPlace = attackPlace;
            AttackOwner = attackOwner;
            RoundsToExec = execNextRound;
            Dmg = dmg;
        }

        public List<Cord> Execute(Field[,] board)
        {
            List<Cord> cordsToUpdate = new List<Cord>();

            board[AttackPlace.X, AttackPlace.Y].MagSkill = MagSkillStatus.Center;
            board[AttackPlace.X - 1, AttackPlace.Y].MagSkill = MagSkillStatus.Up;
            board[AttackPlace.X, AttackPlace.Y + 1].MagSkill = MagSkillStatus.Right;
            board[AttackPlace.X + 1, AttackPlace.Y].MagSkill = MagSkillStatus.Down;
            board[AttackPlace.X, AttackPlace.Y - 1].MagSkill = MagSkillStatus.Left;  
            
            board[AttackPlace.X, AttackPlace.Y].SkillOwner = AttackOwner;
            board[AttackPlace.X - 1, AttackPlace.Y].SkillOwner = AttackOwner;
            board[AttackPlace.X, AttackPlace.Y + 1].SkillOwner = AttackOwner;
            board[AttackPlace.X + 1, AttackPlace.Y].SkillOwner = AttackOwner;
            board[AttackPlace.X, AttackPlace.Y - 1].SkillOwner = AttackOwner;

            cordsToUpdate.Add(new Cord(AttackPlace.X, AttackPlace.Y));
            cordsToUpdate.Add(new Cord(AttackPlace.X - 1, AttackPlace.Y));
            cordsToUpdate.Add(new Cord(AttackPlace.X, AttackPlace.Y + 1));
            cordsToUpdate.Add(new Cord(AttackPlace.X + 1, AttackPlace.Y));
            cordsToUpdate.Add(new Cord(AttackPlace.X, AttackPlace.Y - 1));

            foreach (Cord cord in cordsToUpdate)
            {
                Console.WriteLine("IN");
                if (board[cord.X, cord.Y].PawnOnField != null) //make dmg if on field is pawn
                {
                    board[cord.X, cord.Y].PawnOnField.Def(Dmg);
                }
            }

            return cordsToUpdate;
        }

        public List<Cord> Clear(Field[,] board)
        {
            List<Cord> cordsToUpdate = new List<Cord>();

            board[AttackPlace.X, AttackPlace.Y].MagSkill = MagSkillStatus.None;
            board[AttackPlace.X - 1, AttackPlace.Y].MagSkill = MagSkillStatus.None;
            board[AttackPlace.X, AttackPlace.Y + 1].MagSkill = MagSkillStatus.None;
            board[AttackPlace.X + 1, AttackPlace.Y].MagSkill = MagSkillStatus.None;
            board[AttackPlace.X, AttackPlace.Y - 1].MagSkill = MagSkillStatus.None;

            board[AttackPlace.X, AttackPlace.Y].SkillOwner = null;
            board[AttackPlace.X - 1, AttackPlace.Y].SkillOwner = null;
            board[AttackPlace.X, AttackPlace.Y + 1].SkillOwner = null;
            board[AttackPlace.X + 1, AttackPlace.Y].SkillOwner = null;
            board[AttackPlace.X, AttackPlace.Y - 1].SkillOwner = null;

            cordsToUpdate.Add(new Cord(AttackPlace.X, AttackPlace.Y));
            cordsToUpdate.Add(new Cord(AttackPlace.X - 1, AttackPlace.Y));
            cordsToUpdate.Add(new Cord(AttackPlace.X, AttackPlace.Y + 1));
            cordsToUpdate.Add(new Cord(AttackPlace.X + 1, AttackPlace.Y));
            cordsToUpdate.Add(new Cord(AttackPlace.X, AttackPlace.Y - 1));


            return cordsToUpdate;
        }

    }
}
