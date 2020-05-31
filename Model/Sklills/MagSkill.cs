using ProjectB.Model.Board;
using ProjectB.Model.Help;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace ProjectB.Model.Sklills
{


    sealed class MagSkill : Skill, IDisposable
    {

        private const byte LOOPS = 3;
        private const byte MAX_FRAME = 1;
        private const int RENDER = 100;

        private byte loop = 0;
        private byte frame = 0;

        private readonly Timer timer = new Timer();



        public MagSkill(Cord attackPlace, bool attackOwner, int dmg, GameState gS, byte roundsToExec = 3) : base(attackPlace, attackOwner, dmg, gS, roundsToExec)
        {

        }

        public void Place()
        {
            gS.At(AttackPlace).CastingPath = CastingPath();
            gS.UpdateFieldsOnBoard(AttackPlace);
        }

        private void Execute()
        {
            MakeDmg();
            gS.At(AttackPlace).CastingPath = null;

            timer.Elapsed += new ElapsedEventHandler(Animate);
            timer.Interval = RENDER;
            timer.Enabled = true;

        }

        private void Animate(object source, ElapsedEventArgs e)
        {
            Console.WriteLine(loop + "   " + frame);

            if (loop < LOOPS)
            {
                if (frame > MAX_FRAME)
                {
                    frame = 0;
                    loop++;
                    ExecOneFrame();
                }
                else
                {
                    ExecOneFrame();
                    frame++;
                }
            }
            else
            {
                Clear();
            }
        }

        private void ExecOneFrame()
        {
            if (Arena.IsOK(AttackPlace, 1, 0)) //DOWN
            {
                gS.At(AttackPlace, 1, 0).SkillPath = SkillPath(4, frame);

            }
            if (Arena.IsOK(AttackPlace, -1, 0)) //UP
            {
                gS.At(AttackPlace, -1, 0).SkillPath = SkillPath(2, frame);
            }
            if (Arena.IsOK(AttackPlace, 0, 1)) //RIGHT
            {
                gS.At(AttackPlace, 0, 1).SkillPath = SkillPath(3, frame);
            }
            if (Arena.IsOK(AttackPlace, 0, -1)) //LEFT
            {
                gS.At(AttackPlace, 0, -1).SkillPath = SkillPath(1, frame);
            }
            //CENTER
            gS.At(AttackPlace).SkillPath = SkillPath(0, frame);
        }


        private void MakeDmg()
        {
            if (Arena.IsOK(AttackPlace, 1, 0)) //DOWN
            {
                gS.PAt(AttackPlace, 1, 0)?.Def(1, gS, new Cord(AttackPlace, 1, 0), AttackPlace);

            }
            if (Arena.IsOK(AttackPlace, -1, 0)) //UP
            {
                gS.PAt(AttackPlace, -1, 0)?.Def(1, gS, new Cord(AttackPlace, -1, 0), AttackPlace);
            }
            if (Arena.IsOK(AttackPlace, 0, 1)) //RIGHT
            {
                gS.PAt(AttackPlace, 0, 1)?.Def(1, gS, new Cord(AttackPlace, 0, 1), AttackPlace);
            }
            if (Arena.IsOK(AttackPlace, 0, -1)) //LEFT
            {
                gS.PAt(AttackPlace, 0, -1)?.Def(1, gS, new Cord(AttackPlace, 0, -1), AttackPlace);
            }
            //CENTER
            gS.PAt(AttackPlace)?.Def(1, gS, AttackPlace, AttackPlace);
        }

        private void Clear()
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

            timer.Dispose();

        }



        public override void Lifecycle()
        {
            RoundsToExec--;
            if (RoundsToExec == 1) //execute 
            {
                Execute();
            }
        }

        private string CastingPath() => string.Format(App.pathToMagCasting, AttackOwner ? '0' : '1');

        public string SkillPath(int place, byte id = 0)
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

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                Console.WriteLine("Disose mag skill");
                timer.Dispose();
            }
            base.Dispose(disposing);
        }


    }
}
