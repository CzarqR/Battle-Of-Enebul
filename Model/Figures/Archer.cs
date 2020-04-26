using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectB.Model.Figures
{
    using R = Properties.Resources;

    class Archer : Pawn
    {
        #region properties
        public override string Class => R.archer;
        public override string Desc => R.archer_desc;
        public override int BaseHp => 20;
        public override int Armor => 3;
        public override int PrimaryAttackRange => 4;
        public override int SkillAttackRange => 5;
        public override int SkillAttackDmg => 7;

        public override string PrimaryAttackDesc => string.Format(R.archer_primary_desc, PrimaryAttackDmg);
        public override string PrimaryAttackName => string.Format(R.primary_attack_info, R.archer_skill_name, PrimaryAttackRange, PrimaryAttackCost);
        public override string SkillAttackDesc => string.Format(R.archer_skill_desc, SkillAttackDmg);
        public override string SkillAttackName => string.Format(R.skilll_attack_info, R.archer_skill_name, SkillAttackRange, SkillAttackCost);


        public Archer(bool owner) : base(owner)
        {

        }




        #endregion
    }
}
