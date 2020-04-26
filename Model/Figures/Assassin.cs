using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectB.Model.Figures
{
    using R = Properties.Resources;

    class Assassin : Pawn
    {
        #region properties
        public override string Class => R.assassin;
        public override string Desc => R.assassin_desc;
        public override int BaseHp => 15;
        public override int PrimaryAttackDmg => 10;
        public override int Condition => 4;
        public override int Armor => 3;
        public override int PrimaryAttackRange => 1;
        public override int SkillAttackRange => 1;
        public override int SkillAttackDmg => PrimaryAttackDmg + Condition;


        public Assassin(bool owner) : base(owner)
        {
        }




        #endregion
    }
}
