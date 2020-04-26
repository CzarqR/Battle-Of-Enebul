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
    using R = Properties.Resources;

    class Mag : Pawn
    {
        #region properties
        public override string Class => R.mag;
        public override string Desc => R.mag_desc;

        public override int BaseHp => 25;
        public override int PrimaryAttackRange => 1;
        public override int PrimaryAttackDmg => 1;
        public override int Armor => 3;
        public override int SkillAttackRange => 4;
        public override int SkillAttackCost => 8;
        public override int Condition => 5;
        public override int SkillAttackDmg => 10; //skill dmg center
        public int SkillAttackOutside => 3; //skill dmg outside center

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
