using ProjectB.Model.Board;
using ProjectB.Model.Help;
using System.IO;
using System.Threading.Tasks;

namespace ProjectB.Model.Figures
{
    using R = Properties.Resources;

    class King : Pawn
    {

        #region Properties

        /// Stats
        public override int BaseHp => 35;
        public override int BaseManna => 10;
        public override int Condition => 2;
        public override int Armor => 2;
        public override int PrimaryAttackRange => 1;
        public override int PrimaryAttackCost => 0;
        public override int PrimaryAttackDmg => 5;
        public override int SkillAttackRange => 4;
        public override int SkillAttackCost => 10;
        public override int SkillAttackDmg => 10;
        public override int MannaRegeneration => 1;

        /// Strings
        public override string PrimaryAttackDesc => string.Format(R.king_primary_desc, PrimaryAttackDmg);
        public override string SkillAttackDesc => string.Format(R.king_skill_desc, SkillAttackDmg);
        public override string PrimaryAttackName => string.Format(R.primary_attack_info, R.king_primary_name, PrimaryAttackRange, PrimaryAttackCost);
        public override string SkillAttackName => string.Format(R.skilll_attack_info, R.king_skill_name, SkillAttackRange, SkillAttackCost);
        public override string Class => R.king;
        public override string Desc => R.king_desc;

        protected override UnmanagedMemoryStream GetSound()
        {
            return R.king_attack_0;
        }


        #endregion


        #region Methods

        public King(bool owner, Cord cord) : base(owner, cord) { }

        async public override void Dead(GameState gS)
        {
            await Task.Delay(RENDER_DEF * MAX_FRAME_DEF);
            gS.KillPawn(Cord);
            gS.EndGame();
        }

        #endregion

    }
}
