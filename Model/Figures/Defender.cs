using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectB.Model.Figures
{
    using R = Properties.Resources;

    class Defender : Pawn
    {
        #region properties

        public override string Class => R.defender;
        public override string Desc => R.defender_desc;
        public override int BaseHp => 10;
        public override int PrimaryAttackRange => 1;
        public override int SkillAttackRange => 1;
        public override int SkillAttackCost => 1;


        #endregion


        #region methods  




        public Defender(bool owner) : base(owner)
        {

        }

        #endregion

    }
}
