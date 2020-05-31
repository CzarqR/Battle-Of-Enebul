using System.IO;

namespace ProjectB.Model.Figures
{
    using R = Properties.Resources;

    class Defender : Pawn
    {

        #region properties

        /// Stats
        public override int BaseHp => 10;
        public override int SkillAttackDmg => 50;
        public override int PrimaryAttackRange => 1;
        public override int SkillAttackRange => 2;
        public override int SkillAttackCost => 3;
        public override int Condition => 10;

        /// Strings
        public override string PrimaryAttackDesc => string.Format(R.defender_primary_desc, PrimaryAttackDmg);
        public override string SkillAttackDesc => string.Format(R.defender_skill_desc, SkillAttackDmg);
        public override string PrimaryAttackName => string.Format(R.primary_attack_info, R.defender_primary_name, PrimaryAttackRange, PrimaryAttackCost);
        public override string SkillAttackName => string.Format(R.skilll_attack_info, R.defender_skill_name, SkillAttackRange, SkillAttackCost);
        public override string Class => R.defender;
        public override string Desc => R.defender_desc;

        protected override UnmanagedMemoryStream GetSound()
        {
            return R.defender_attack_0;
        }

        #endregion


        #region methods  

        public Defender(bool owner) : base(owner) { }

        #endregion

    }
}
