using ProjectB.Model.Board;
using ProjectB.Model.Help;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectB.Model.Figures
{
    using R = Properties.Resources;

    class King : Pawn
    {
        #region properties
        public override string Class => R.king;
        public override string Desc => R.king_desc;

        public override int PrimaryAttackRange => 1;
        public override int SkillAttackRange => 1;
        public override int Condition => 10;
        public override int BaseHp => 1;

        public override string PrimaryAttackDesc => string.Format(R.king_primary_desc, PrimaryAttackDmg);
        public override string SkillAttackDesc => string.Format(R.king_skill_desc, SkillAttackDmg);
        public override string PrimaryAttackName => string.Format(R.primary_attack_info, R.king_primary_name, PrimaryAttackRange, PrimaryAttackCost);
        public override string SkillAttackName => string.Format(R.skilll_attack_info, R.king_skill_name, SkillAttackRange, SkillAttackCost);

        public King(bool owner) : base(owner)
        {

        }

        #endregion

        public override void Dead(GameState gS, Cord C)
        {
            gS.KillPawn(C);
             gS.EndGame();
        }
    }
}
