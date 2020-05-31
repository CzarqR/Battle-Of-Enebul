using System.IO;

namespace ProjectB.Model.Figures
{
    using R = Properties.Resources;

    class Assassin : Pawn
    {

        #region Properties

        /// Stats
        public override int BaseHp => 15;
        public override int PrimaryAttackDmg => 10;
        public override int Condition => 4;
        public override int Armor => 3;
        public override int PrimaryAttackRange => 1;
        public override int SkillAttackRange => 1;
        public override int SkillAttackDmg => PrimaryAttackDmg + Condition;

        /// Strings
        public override string PrimaryAttackDesc => string.Format(R.assassin_primary_desc, PrimaryAttackDmg);
        public override string SkillAttackDesc => string.Format(R.assassin_skill_desc, SkillAttackDmg, Condition);
        public override string PrimaryAttackName => string.Format(R.primary_attack_info, R.assassin_primary_name, PrimaryAttackRange, PrimaryAttackCost);
        public override string SkillAttackName => string.Format(R.skilll_attack_info, R.assassin_skill_name, SkillAttackRange, SkillAttackCost);
        public override string Class => R.assassin;
        public override string Desc => R.assassin_desc;

        protected override UnmanagedMemoryStream GetSound()
        {
            return R.assasin_attack_0;
        }


        #endregion


        #region Methods  

        public Assassin(bool owner) : base(owner) { }

        #endregion

    }
}
