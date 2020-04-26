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



        public Archer(bool owner) : base(owner)
        {

        }




        #endregion
    }
}
