using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectB.Model.Figures
{
    class King : Pawn
    {
        #region properties

        public const int BASE_HP = 20;
        public const int ATTACK = 2;
        public const int DEF = 10;
        public const int CONDITION = 1;
        public const int BASE_MANNA = 7;

        public King(bool owner) : base(owner)
        {
        }

        public override int BaseAttack()
        {
            return ATTACK;
        }

        public override int BaseCondition()
        {
            return CONDITION;
        }

        public override int BaseDef()
        {
            return DEF;
        }

        public override int BaseHealth()
        {
            return BASE_HP;
        }

        public override int BaseManna()
        {
            return BASE_MANNA;
        }

        public override string ImagePath()
        {
            return ImagePath();
        }

        #endregion
    }
}
