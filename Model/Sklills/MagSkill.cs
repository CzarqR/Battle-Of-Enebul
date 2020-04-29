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

        public void Place(GameState gS)
        {
            gS.At(AttackPlace).CastingPath = CastingPath();
            gS.UpdateFieldsOnBoard(AttackPlace);
        }

        private void Execute(GameState gS)
        {
            gS.At(AttackPlace).CastingPath = null;

            if (Arena.IsOK(AttackPlace, 1, 0)) //DOWN
            {
                gS.At(AttackPlace, 1, 0).SkillPath = SkillPath(4);
                gS.PAt(AttackPlace, 1, 0)?.Def(1, gS, new Cord(AttackPlace, 1, 0));
                Console.WriteLine(SkillPath(4));
                Console.WriteLine(gS.At(AttackPlace, 1, 0).SkillPath);

            }
            if (Arena.IsOK(AttackPlace, -1, 0)) //UP
            {
                gS.At(AttackPlace, -1, 0).SkillPath = SkillPath(2);
                gS.PAt(AttackPlace, -1, 0)?.Def(1, gS, new Cord(AttackPlace, -1, 0));
            }
            if (Arena.IsOK(AttackPlace, 0, 1)) //RIGHT
            {
                gS.At(AttackPlace, 0, 1).SkillPath = SkillPath(3);
                gS.PAt(AttackPlace, 0, 1)?.Def(1, gS, new Cord(AttackPlace, 0, 1));
            }
            if (Arena.IsOK(AttackPlace, 0, -1)) //LEFT
            {
                gS.At(AttackPlace, 0, -1).SkillPath = SkillPath(1);
                gS.PAt(AttackPlace, 0, -1)?.Def(1, gS, new Cord(AttackPlace, 0, -1));
            }
            //CENTER
            gS.At(AttackPlace).SkillPath = SkillPath(0);
            gS.PAt(AttackPlace)?.Def(1, gS, AttackPlace);

        }

        private void Clear(GameState gS)
        {
            Console.WriteLine("Clering mag skill " + this);
            Finished = true;

            if (Arena.IsOK(AttackPlace, 1, 0)) //DOWN
            {
                gS.At(AttackPlace, 1, 0).SkillPath = null;
                gS.At(AttackPlace, 1, 0).SkillOwner = null;
            }
            if (Arena.IsOK(AttackPlace, -1, 0)) //UP
            {
                gS.At(AttackPlace, -1, 0).SkillPath = null;
                gS.At(AttackPlace, -1, 0).SkillOwner = null;
            }
            if (Arena.IsOK(AttackPlace, 0, 1)) //RIGHT
            {
                gS.At(AttackPlace, 0, 1).SkillPath = null;
                gS.At(AttackPlace, 0, 1).SkillOwner = null;
            }
            if (Arena.IsOK(AttackPlace, 0, -1)) //LEFT
            {
                gS.At(AttackPlace, 0, -1).SkillPath = null;
                gS.At(AttackPlace, 0, -1).SkillOwner = null;
            }
            //CENTER
            gS.At(AttackPlace).SkillPath = null;
            gS.At(AttackPlace).SkillOwner = null;


        }



        public override void Lifecycle(GameState gS)
        {
            RoundsToExec--;
            if (RoundsToExec == 1) //execute 
            {
                Execute(gS);
            }
            else if (RoundsToExec == 0) // clear
            {
                Clear(gS);
            }

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
