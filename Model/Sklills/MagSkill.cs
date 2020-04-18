using ProjectB.Model.Board;
using ProjectB.Model.Help;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectB.Model.Sklills
{


    class MagSkill : Skill
    {

        public MagSkill(Cord attackPlace, bool attackOwner, int dmg, byte roundsToExec = 3) : base(attackPlace, attackOwner, dmg, roundsToExec)
        {

        }

        private List<Cord> Place(GameState gS)
        {
            gS.At(AttackPlace).CastingPath = CastingPath();

            return new List<Cord>
            {
                AttackPlace
            };
        }

        private List<Cord> Execute(GameState gS)
        {
            Console.WriteLine("Executing mag skill " + this);
            List<Cord> cordsToUpdate = new List<Cord>();
            gS.At(AttackPlace).CastingPath = null;

            if (Arena.IsOK(AttackPlace, 1, 0)) //DOWN
            {
                gS.At(AttackPlace, 1, 0).SkillPath = SkillPath(4);
                cordsToUpdate.Add(new Cord(AttackPlace, 1, 0));
            }
            if (Arena.IsOK(AttackPlace, -1, 0)) //UP
            {
                gS.At(AttackPlace, -1, 0).SkillPath = SkillPath(2);
                cordsToUpdate.Add(new Cord(AttackPlace, -1, 0));
            }
            if (Arena.IsOK(AttackPlace, 0, 1)) //RIGHT
            {
                gS.At(AttackPlace, 0, 1).SkillPath = SkillPath(3);
                cordsToUpdate.Add(new Cord(AttackPlace, 0, 1));
            }
            if (Arena.IsOK(AttackPlace, 0, -1)) //LEFT
            {
                gS.At(AttackPlace, 0, -1).SkillPath = SkillPath(1);
                cordsToUpdate.Add(new Cord(AttackPlace, 0, -1));
            }
            //CENTER
            gS.At(AttackPlace).SkillPath = SkillPath(0);
            cordsToUpdate.Add(new Cord(AttackPlace));


            foreach (Cord cord in cordsToUpdate)
            {
                if (gS.PAt(cord) != null) //make dmg if on field is pawn
                {
                    gS.PAt(cord).Def(Dmg, gS, cord);
                }
            }

            return cordsToUpdate;
        }

        private List<Cord> Clear(GameState gS)
        {
            Console.WriteLine("Clering mag skill " + this);
            List<Cord> cordsToUpdate = new List<Cord>();
            Finished = true;

            if (Arena.IsOK(AttackPlace, 1, 0)) //DOWN
            {
                gS.At(AttackPlace, 1, 0).SkillPath = null;
                gS.At(AttackPlace, 1, 0).SkillOwner = null;
                cordsToUpdate.Add(new Cord(AttackPlace, 1, 0));
            }
            if (Arena.IsOK(AttackPlace, -1, 0)) //UP
            {
                gS.At(AttackPlace, -1, 0).SkillPath = null;
                gS.At(AttackPlace, -1, 0).SkillOwner = null;
                cordsToUpdate.Add(new Cord(AttackPlace, -1, 0));
            }
            if (Arena.IsOK(AttackPlace, 0, 1)) //RIGHT
            {
                gS.At(AttackPlace, 0, 1).SkillPath = null;
                gS.At(AttackPlace, 0, 1).SkillOwner = null;
                cordsToUpdate.Add(new Cord(AttackPlace, 0, 1));
            }
            if (Arena.IsOK(AttackPlace, 0, -1)) //LEFT
            {
                gS.At(AttackPlace, 0, -1).SkillPath = null;
                gS.At(AttackPlace, 0, -1).SkillOwner = null;
                cordsToUpdate.Add(new Cord(AttackPlace, 0, -1));
            }
            //CENTER
            gS.At(AttackPlace).SkillPath = null;
            gS.At(AttackPlace).SkillOwner = null;
            cordsToUpdate.Add(new Cord(AttackPlace));


            return cordsToUpdate;
        }



        public override List<Cord> Lifecycle(GameState gS)
        {
            List<Cord> cordsToUpdate = new List<Cord>();
            RoundsToExec--;
            if (RoundsToExec == 2) //put
            {
                cordsToUpdate = cordsToUpdate.Concat(Place(gS)).ToList();
            }
            else if (RoundsToExec == 1) //execute 
            {
                cordsToUpdate = cordsToUpdate.Concat(Execute(gS)).ToList();
            }
            else if (RoundsToExec == 0) // clear
            {
                cordsToUpdate = cordsToUpdate.Concat(Clear(gS)).ToList();
            }
            return cordsToUpdate;

        }

        private string CastingPath() => string.Format(App.pathToMagCasting, AttackOwner ? '0' : '1');

        public string SkillPath(int place, int id = 0)
        {
            if (AttackOwner == true) // blue
            {
                return string.Format(App.pathToMagExec, '0', place, id);
            }
            else // red
            {
                if (place == 0) //center
                {
                    return string.Format(App.pathToMagExec, '1', '0', id);
                }
                else
                {
                    return string.Format(App.pathToMagExecRed, id);
                }
            }
        }


    }
}
