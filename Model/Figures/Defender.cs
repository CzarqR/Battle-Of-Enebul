using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectB.Model.Figures
{
    class Defender : Pawn
    {
        #region properties
        public override int BaseHp => 1;



        #endregion


        #region methods  




        public Defender(bool owner) : base(owner)
        {

        }

        #endregion

    }
}
