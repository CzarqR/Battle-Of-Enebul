using ProjectB.Model.Help;
using System.IO;

namespace ProjectB.Model.Figures
{
    using R = Properties.Resources;

    class Axeman : Pawn
    {

        #region Properties

        /// Stats
        public override int BaseHp => 40;
        public override int BaseManna => 10;
        public override int Condition => 2;
        public override int Armor => 3;
        public override int PrimaryAttackRange => 2;
        public override int PrimaryAttackCost => 2;
        public override int PrimaryAttackDmg => 7;
        public override int SkillAttackRange => 1;
        public override int SkillAttackCost => 5;
        public override int SkillAttackDmg => 13;
        public override int MannaRegeneration => 1;

        /// Strings
        public override string PrimaryAttackDesc => string.Format(R.axeman_primary_desc, PrimaryAttackDmg);
        public override string SkillAttackDesc => string.Format(R.axeman_skill_desc, SkillAttackDmg);
        public override string PrimaryAttackName => string.Format(R.primary_attack_info, R.axeman_primary_name, PrimaryAttackRange, PrimaryAttackCost);
        public override string SkillAttackName => string.Format(R.skilll_attack_info, R.axeman_skill_name, SkillAttackRange, SkillAttackCost);
        public override string Class => R.axeman;
        public override string Desc => R.axeman_desc;

        protected override UnmanagedMemoryStream GetSound()
        {
            return R.axeman_attack_0;
        }


        #endregion


        #region Methods  

        public Axeman(bool owner, Cord cord) : base(owner, cord) { }

        #endregion

    }
}
