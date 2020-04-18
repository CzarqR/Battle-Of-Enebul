using ProjectB.Model.Board;
using ProjectB.Model.Help;
using ProjectB.Model.Sklills;
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
        public override int ExtraAttackRange => 2;
        public override int Condition => 5;




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

        public override List<Cord> SkillAttack(GameState gameState, Cord defender)
        {
            gameState.At(defender).SkillOwner = Owner;
            gameState.AddSkill(new MagSkill(defender, Owner, SkillAttackDmg));
            return new List<Cord>
            {
                defender
            };

        }

        #endregion


    }


}
