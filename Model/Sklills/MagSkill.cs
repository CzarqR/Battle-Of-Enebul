using ProjectB.Model.Board;
using ProjectB.Model.Help;
using System;
using System.Timers;

namespace ProjectB.Model.Sklills
{
    using R = Properties.Resources;

    sealed class MagSkill : Skill, IDisposable
    {
        public const int SIDE_DMG = 8;
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
            gS.At(AttackPlace).SkillDesc = Desc();
            gS.UpdateFieldsOnBoard(AttackPlace);
        }

        private void Execute()
        {
            gS.Play(AttackOwner ? R.mag_skill_blue_0 : R.mag_skill_red_0);
            MakeDmg();
            gS.At(AttackPlace).CastingPath = null;

            timer.Elapsed += new ElapsedEventHandler(Animate);
            timer.Interval = RENDER;
            timer.Enabled = true;

        }

        private void Animate(object source, ElapsedEventArgs e)
        {
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
                gS.PAt(AttackPlace, 1, 0)?.Def(SIDE_DMG, gS, new Cord(AttackPlace, 1, 0), AttackPlace);

            }
            if (Arena.IsOK(AttackPlace, -1, 0)) //UP
            {
                gS.PAt(AttackPlace, -1, 0)?.Def(SIDE_DMG, gS, new Cord(AttackPlace, -1, 0), AttackPlace);
            }
            if (Arena.IsOK(AttackPlace, 0, 1)) //RIGHT
            {
                gS.PAt(AttackPlace, 0, 1)?.Def(SIDE_DMG, gS, new Cord(AttackPlace, 0, 1), AttackPlace);
            }
            if (Arena.IsOK(AttackPlace, 0, -1)) //LEFT
            {
                gS.PAt(AttackPlace, 0, -1)?.Def(SIDE_DMG, gS, new Cord(AttackPlace, 0, -1), AttackPlace);
            }
            //CENTER
            gS.PAt(AttackPlace)?.Def(Dmg, gS, AttackPlace, AttackPlace);
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
            gS.At(AttackPlace).SkillDesc = null;


            timer.Dispose();

        }

        public override void Lifecycle()
        {
            RoundsToExec--;
            gS.At(AttackPlace).SkillDesc = Desc();
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

        private string Desc()
        {
            if (RoundsToExec > 1)
            {
                return string.Format(R.magskill_desc, RoundsToExec - 1, Dmg);
            }
            else
            {
                return string.Empty;
            }
        }


    }
}
