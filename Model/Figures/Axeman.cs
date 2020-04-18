using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectB.Model.Figures
{
    class Axeman : Pawn
    {
        #region properties

        public override int Condition => 1;

        public Axeman(bool owner) : base(owner)
        {
        }




        #endregion
    }
}
