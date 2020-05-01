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
        public override int SkillAttackDmg => 10; //skill dmg center
        public int SkillAttackOutside => 3; //skill dmg outside center

        public override string PrimaryAttackDesc => string.Format(R.mag_primary_desc, PrimaryAttackDmg, BaseManna);
        public override string PrimaryAttackName => string.Format(R.primary_attack_info, R.mag_skill_name, PrimaryAttackRange, PrimaryAttackCost);
        public override string SkillAttackDesc => string.Format(R.mag_skill_desc, SkillAttackDmg, SkillAttackOutside);
        public override string SkillAttackName => string.Format(R.skilll_attack_info, R.mag_skill_name, SkillAttackRange, SkillAttackCost);


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
                if (Manna < SkillAttackCost)
                {
                    return false;
                }
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

        public override void SkillAttack(GameState gameState, Cord defender, int bonus)
        {
            gameState.At(defender).SkillOwner = Owner;
            MagSkill skill = new MagSkill(defender, Owner, SkillAttackDmg);
            gameState.AddSkill(skill);
            skill.Place(gameState);

        }

        #endregion


    }


}
