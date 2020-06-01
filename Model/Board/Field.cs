using ProjectB.Model.Figures;
using System;

namespace ProjectB.Model.Board
{
    using R = Properties.Resources;

    public class Field
    {

        #region Properties 

        public const int DEFAULT_ATTACK_BONUS = 1;
        public const int DEFAULT_DEF_BONUS = 1;
        public const int DEFAULT_MOVEMENT_BONUS = 1;

        public int MovementBonus
        {
            get; set;
        }
        public int AttackBonus
        {
            get; set;
        }
        public int DefBonus
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

        private string castingPath;
        public string CastingPath
        {
            get
            {
                return castingPath;
            }
            set
            {
                Console.WriteLine(value);
                castingPath = value;

            }
        }
        public string SkillPath
        {
            get; set;
        }
        public string SkillDesc
        {
            get; set;
        }
        public string FloorPath => string.Format(App.pathToFloor, Floor, FloorStatus);

        private FloorType floor;
        public FloorType Floor
        {
            get
            {
                return floor;
            }

            set
            {
                if (value == FloorType.Attack)
                {
                    AttackBonus = DEFAULT_ATTACK_BONUS;
                }
                else if (value == FloorType.Def)
                {
                    DefBonus = DEFAULT_DEF_BONUS;
                }
                else if (value == FloorType.Cond)
                {
                    MovementBonus = DEFAULT_MOVEMENT_BONUS;
                }
                floor = value;
            }
        }
        public FloorStatus FloorStatus
        {
            get; set;
        }



        #endregion


        #region Methods

        public Field(Pawn pawnOnField = null, FloorType floor = FloorType.Base, FloorStatus floorStatus = FloorStatus.Normal) // default field without bonuses
        {
            PawnOnField = pawnOnField;
            Floor = floor;
            FloorStatus = floorStatus;
        }

        public string GetTitle()
        {
            if (Floor == FloorType.Base)
            {
                return R.floor_base_title;
            }
            else if (Floor == FloorType.Attack)
            {
                return string.Format(R.floor_blessing, R.feith);
            }
            else if (Floor == FloorType.Def)
            {
                return string.Format(R.floor_blessing, R.rhea);

            }
            else if (Floor == FloorType.Cond)
            {
                return string.Format(R.floor_blessing, R.saula);
            }
            throw new Exception("Udefined floor");
        }

        public string GetDesc()
        {
            if (Floor == FloorType.Base)
            {
                return R.floor_base_desc;
            }
            else if (Floor == FloorType.Attack)
            {
                return R.floor_attack_desc;
            }
            else if (Floor == FloorType.Def)
            {
                return R.floor_def_desc;
            }
            else if (Floor == FloorType.Cond)
            {
                return R.floor_move_desc;
            }
            throw new Exception("Udefined floor");
        }

        public string GetBonuses()
        {
            if (Floor == FloorType.Base)
            {
                return R.floor_base_bonuses;
            }
            else if (Floor == FloorType.Attack)
            {
                return string.Format(R.floor_bonus, AttackBonus, R.attack);
            }
            else if (Floor == FloorType.Def)
            {
                return string.Format(R.floor_bonus, DefBonus, R.defense);
            }
            else if (Floor == FloorType.Cond)
            {
                return string.Format(R.floor_bonus, MovementBonus, R.condition);
            }
            throw new Exception("Udefined floor");
        }

        public string GetLegend()
        {
            if (Floor == FloorType.Base)
            {
                return R.floor_base_legeng;
            }
            else if (Floor == FloorType.Attack)
            {
                return R.floor_attack_legend;
            }
            else if (Floor == FloorType.Def)
            {
                return R.floor_def_legend;

            }
            else if (Floor == FloorType.Cond)
            {
                return R.floor_move_legend;
            }
            throw new Exception("Udefined floor");
        }

        public string GetToolTip()
        {
            if (PawnOnField != null)
            {
                return PawnOnField.Bonuses(Floor) + SkillDesc;
            }
            else
            {
                return GetBonuses() + SkillDesc;
            }
        }

        #endregion

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