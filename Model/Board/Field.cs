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

        public Pawn PawnOnField
        {
            get;
            set;
        }

        public FloorType Floor
        {
            get; set;
        }
        public FloorStatus FloorStatus
        {
            get; set;
        }

        public string FloorPath()
        {
            return string.Format(App.pathToFloor, Floor, FloorStatus);
        }


        public string FloorBaseInfo => "to jest taka podloga, bl BLA, BLA, asdasdas fqwdqwd  qqkdbksf qwbdqks f qfbkahsfbk quifvbkan  qkfvk nb kqwufkad  eufhjdfh uyefva";
        public string FloorPrecInfo => "Własciwosci podlogi\nAtak - 1\nObrona - 1\nMove - 2";

        public Field(Pawn pawnOnField = null, FloorType floor = FloorType.Base, FloorStatus floorStatus = FloorStatus.Normal, int movementBonus = 0, double attackBonus = 1, double defBonus = 1) // default field without bonuses
        {
            MovementBonus = movementBonus;
            AttackBonus = attackBonus;
            DefBonus = defBonus;
            PawnOnField = pawnOnField;
            Floor = floor;
            FloorStatus = 0;
            FloorStatus = floorStatus;
        }


    }

    public enum FloorType
    {
        Base = 1,
        Attack = 2,
        Def = 3,
        Cond = 4
    }

    public enum FloorStatus
    {
        Normal = 0,
        Attack = 1,
        Move = 2
    }



}
