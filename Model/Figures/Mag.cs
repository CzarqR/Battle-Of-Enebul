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
        public override int ExtraAttackRange => 1;




        #endregion


        #region methods  
        public Mag(bool owner) : base(owner)
        {

        }


        public override bool IsSomeoneToAttack(Cord cord, Arena A, bool attackType)
        {
            if (attackType) //primary attack
            {
                return base.IsSomeoneToAttack(cord, A, attackType);
            }
            else
            {
                return true;
            }
        }

        public override List<Cord> ShowPossibleAttack(Cord C, Arena A, bool attackType)
        {

            if (attackType) //primary attack
            {
                return base.ShowPossibleAttack(C, A, attackType);
            }
            else
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
                        if (Arena.IsXOK(C.X + i))
                        {
                            if (Arena.IsYOK(C.Y + k))
                            {
                                A[C, i, k].FloorStatus = FloorStatus.Attack;
                                cordsToUpdate.Add(new Cord(C, i, k));
                            }
                            if (Arena.IsYOK(C.Y - k))
                            {
                                A[C, i, -k].FloorStatus = FloorStatus.Attack;
                                cordsToUpdate.Add(new Cord(C, i, -k));
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
                        if (Arena.IsXOK(C.X + i))
                        {
                            if (Arena.IsYOK(C.Y + k))
                            {
                                A[C, i, k].FloorStatus = FloorStatus.Attack;
                                cordsToUpdate.Add(new Cord(C.X + i, C.Y + k));
                            }
                            if (Arena.IsYOK(C.Y - k))
                            {
                                A[C, i, -k].FloorStatus = FloorStatus.Attack;
                                cordsToUpdate.Add(new Cord(C.X + i, C.Y - k));
                            }

                        }
                    }
                }

                A[C].FloorStatus = FloorStatus.Attack;
                cordsToUpdate.Add(C);


                return cordsToUpdate;
            }
        }

        //todo reapair mag skill
        public override List<Cord> MarkFieldsToAttack(List<Cord> possibleAttackFields, Arena A, bool attackType)
        {


            if (attackType) //primary attack
            {
                return base.MarkFieldsToAttack(possibleAttackFields, A, attackType);
            }
            else
            {
                return possibleAttackFields;
            }


        }

        public override List<Cord> SkillAttack(GameState arena, Cord defender)
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
        public override string ToString()
        {
            return $"Mag Skill at {AttackPlace} with dmg {Dmg}. From {AttackOwner}, Round To Finish: {RoundsToExec}";
        }
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

        public List<Cord> Execute(Arena A)
        {
            Console.WriteLine("Executing mag skill " + this);
            List<Cord> cordsToUpdate = new List<Cord>();

            A[AttackPlace].MagSkill = MagSkillStatus.Center;
            A[AttackPlace, -1, 0].MagSkill = MagSkillStatus.Up;
            A[AttackPlace, 0, 1].MagSkill = MagSkillStatus.Right;
            A[AttackPlace, 1, 0].MagSkill = MagSkillStatus.Down;
            A[AttackPlace, 0, -1].MagSkill = MagSkillStatus.Left;

            A[AttackPlace].SkillOwner = AttackOwner;
            A[AttackPlace, -1, 0].SkillOwner = AttackOwner;
            A[AttackPlace, 0, 1].SkillOwner = AttackOwner;
            A[AttackPlace, 1, 0].SkillOwner = AttackOwner;
            A[AttackPlace, 0, -1].SkillOwner = AttackOwner;

            cordsToUpdate.Add(new Cord(AttackPlace));
            cordsToUpdate.Add(new Cord(AttackPlace, -1, 0));
            cordsToUpdate.Add(new Cord(AttackPlace, 0, 1));
            cordsToUpdate.Add(new Cord(AttackPlace, 1, 0));
            cordsToUpdate.Add(new Cord(AttackPlace, 0, -1));

            foreach (Cord cord in cordsToUpdate)
            {
                if (A.PAt(cord) != null) //make dmg if on field is pawn
                {
                    A.PAt(cord).Def(Dmg);
                }
            }

            return cordsToUpdate;
        }

        public List<Cord> Clear(Arena A)
        {
            Console.WriteLine("Clering mag skill " + this);
            List<Cord> cordsToUpdate = new List<Cord>();


            A[AttackPlace].MagSkill = MagSkillStatus.None;
            A[AttackPlace, -1, 0].MagSkill = MagSkillStatus.None;
            A[AttackPlace, 0, 1].MagSkill = MagSkillStatus.None;
            A[AttackPlace, 1, 0].MagSkill = MagSkillStatus.None;
            A[AttackPlace, 0, -1].MagSkill = MagSkillStatus.None;

            A[AttackPlace].SkillOwner = null;
            A[AttackPlace, -1, 0].SkillOwner = null;
            A[AttackPlace, 0, 1].SkillOwner = null;
            A[AttackPlace, 1, 0].SkillOwner = null;
            A[AttackPlace, 0, -1].SkillOwner = null;

            cordsToUpdate.Add(new Cord(AttackPlace));
            cordsToUpdate.Add(new Cord(AttackPlace, -1, 0));
            cordsToUpdate.Add(new Cord(AttackPlace, 0, 1));
            cordsToUpdate.Add(new Cord(AttackPlace, 1, 0));
            cordsToUpdate.Add(new Cord(AttackPlace, 0, -1));


            return cordsToUpdate;
        }

    }
}
