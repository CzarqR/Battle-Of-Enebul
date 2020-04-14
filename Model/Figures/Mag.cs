using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectB.Model.Figures
{
    class Mag : Pawn
    {
        #region properties

        public override int BaseHp => 8;

       


        #endregion


        #region methods  






        public Mag(bool owner) : base(owner)
        {

        }

        #endregion


    }
}
