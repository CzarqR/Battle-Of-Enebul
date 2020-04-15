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

        public bool? SkillOwner
        {
            get; set;
        }


        public FloorType Floor
        {
            get; set;
        }
        public FloorStatus FloorStatus
        {
            get; set;
        }

        public MagSkillStatus MagSkill
        {
            get; set;
        }


        public string FloorPath()
        {
            return string.Format(App.pathToFloor, Floor, FloorStatus);
        }

        public string MagSkillPath(int id = 0)
        {
            if (SkillOwner == true) // blue
            {
                if ((int)MagSkill >= 0 && (int)MagSkill <= 4)
                {
                    return string.Format(App.pathToMagExec, '0', (int)MagSkill, id);
                }
                else if ((int)MagSkill == 5)
                {
                    return string.Format(App.pathToMagCasting, '0');
                }
                else if ((int)MagSkill == 6)
                {
                    return null;
                }
            }
            else if (SkillOwner == false) // red
            {
                if (MagSkill == 0)
                {
                    return string.Format(App.pathToMagExec, '1', '0', id);
                }
                if ((int)MagSkill >= 1 && (int)MagSkill <= 4)
                {
                    return string.Format(App.pathToMagExecRed, id);
                }
                else if ((int)MagSkill == 5)
                {
                    return string.Format(App.pathToMagCasting, '1');
                }
                else if ((int)MagSkill == 6)
                {
                    return null;
                }
            }
            return null;
        }

        public string FloorBaseInfo => "to jest taka podloga, bl BLA, BLA, asdasdas fqwdqwd  qqkdbksf qwbdqks f qfbkahsfbk quifvbkan  qkfvk nb kqwufkad  eufhjdfh uyefva";
        public string FloorPrecInfo => "Własciwosci podlogi\nAtak - 1\nObrona - 1\nMove - 2";

        public Field(Pawn pawnOnField = null, FloorType floor = FloorType.Base, FloorStatus floorStatus = FloorStatus.Normal, int movementBonus = 0, double attackBonus = 1, double defBonus = 1, MagSkillStatus magSkill = MagSkillStatus.None) // default field without bonuses
        {
            MovementBonus = movementBonus;
            AttackBonus = attackBonus;
            DefBonus = defBonus;
            PawnOnField = pawnOnField;
            Floor = floor;
            FloorStatus = floorStatus;
            MagSkill = magSkill;
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

    public enum MagSkillStatus
    {
        Center = 0,
        Left = 1,
        Up = 2,
        Right = 3,
        Down = 4,
        Casting = 5,
        None = 6
    }

}
