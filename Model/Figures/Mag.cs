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

        public override void Def(int dmg)
        {
            base.Def(dmg);
            //TODO logika obrony tank, jesli będzie miała cos więcej niż bazowa implementacja w klasie Pawn. Jak nie to można skasować. To samo tyczy się wszytkich metod override
        }


        public override void Move()
        {
            base.Move();
        }





        public Mag(bool owner) : base(owner)
        {

        }

        #endregion


    }
}


class Parent
{
    virtual public int Hp
    {
        get
        {
            return 15;
        }
    }
}

class Child1: Parent
{
    public override int Hp
    {
        get
        {
            return 10;
        }
    }
}
