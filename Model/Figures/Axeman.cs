using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectB.Model.Figures
{
    using R = Properties.Resources;

    class Axeman : Pawn
    {
        #region properties

        public override int Condition => 1;
        public override string Class => R.axeman;
        public override string Desc => R.axeman_desc;
        public override int BaseHp => 40;
        public override int PrimaryAttackDmg => 7;
        public override int SkillAttackDmg => 11;
        public override int Armor => 5;

        public override int PrimaryAttackRange => 1;
        public override int SkillAttackRange => 2;
        public override int SkillAttackCost => 5;


        public Axeman(bool owner) : base(owner)
        {
        }




        #endregion
    }
}
