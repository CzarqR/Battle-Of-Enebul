using ProjectB.Model.Figures;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectB.Model.Board
{

    public class Field
    {
        public int MovementBonus
        {
            get; set;
        }
        public double AttackBonus
        {
            get; set;
        }
        public double DefBonus
        {
            get; set;
        }
        private Pawn pawnOnField;

        public Pawn PawnOnField
        {
            get
            {
                return pawnOnField;
            }
            set
            {
                pawnOnField = value;
            }
        }

        public bool CanMove
        {
            get; set;
        }


        public Field(Pawn pawnOnField = null, int movementBonus = 0, double attackBonus = 1, double defBonus = 1) // default field without bonuses
        {
            MovementBonus = movementBonus;
            AttackBonus = attackBonus;
            DefBonus = defBonus;
            PawnOnField = pawnOnField;
        }


    }

}
