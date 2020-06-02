using ProjectB.Model.Help;
using System.IO;

namespace ProjectB.Model.Figures
{
    using R = Properties.Resources;

    class Archer : Pawn
    {

        #region Properties

        /// Stats
        public override int BaseHp => 25;
        public override int BaseManna => 10;
        public override int Condition => 2;
        public override int Armor => 1;
        public override int PrimaryAttackRange => 4;
        public override int PrimaryAttackCost => 0;
        public override int PrimaryAttackDmg => 4;
        public override int SkillAttackRange => 5;
        public override int SkillAttackCost => 6;
        public override int SkillAttackDmg => 7;
        public override int MannaRegeneration => 1;

        /// Strings
        public override string PrimaryAttackDesc => string.Format(R.archer_primary_desc, PrimaryAttackDmg);
        public override string PrimaryAttackName => string.Format(R.primary_attack_info, R.archer_primary_name, PrimaryAttackRange, PrimaryAttackCost);
        public override string SkillAttackDesc => string.Format(R.archer_skill_desc, SkillAttackDmg);
        public override string SkillAttackName => string.Format(R.skilll_attack_info, R.archer_skill_name, SkillAttackRange, SkillAttackCost);
        public override string Class => R.archer;
        public override string Desc => R.archer_desc;

        protected override UnmanagedMemoryStream GetSound()
        {
            return R.archer_attack_0;
        }

        #endregion


        #region Methods  

        public Archer(bool owner, Cord cord) : base(owner, cord) { }

        #endregion

    }
}
