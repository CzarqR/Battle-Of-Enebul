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


        public King(bool owner) : base(owner)
        {

        }

        #endregion
    }
}
