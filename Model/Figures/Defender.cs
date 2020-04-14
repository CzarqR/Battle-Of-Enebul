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

        public override int BaseHp => 15;



        #endregion


        #region methods  

        public override void Def(int dmg)
        {
            base.Def(dmg);
            //TODO logika obrony tank, jesli będzie miała cos więcej niż bazowa implementacja w klasie Pawn. Jak nie to można skasować. To samo tyczy się wszytkich metod override
        }







        public Defender(bool owner) : base(owner)
        {

        }

        #endregion

    }
}
