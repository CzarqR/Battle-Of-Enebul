using ProjectB.Model.Board;
using ProjectB.Model.Figures;
using ProjectB.Model.Help;
using ProjectB.Model.Render;
using System;
using System.Timers;

namespace ProjectB.Model.Sklills
{
    using R = Properties.Resources;

    sealed class MagSkill : Skill, IDisposable
    {
        private const byte LOOPS = 5;
        private const byte MAX_FRAME_EXECUTING = 1;
        private const byte MAX_FRAME_MARKING = 25;
        private const int RENDER_EXECUTING = 100;
        private const int RENDER_MARKING = 40;
        private byte loop = 0;
        private byte frame = 0;
        private Timer timer = new Timer();

        public MagSkill(Cord attackPlace, bool attackOwner, int dmg, int bonus, GameState gS, byte roundsToExec = 3) : base(attackPlace, attackOwner, dmg, bonus, gS, roundsToExec)
        {

        }

        public void Place()
        {
            gS.At(AttackPlace).SkillDesc = Desc();

            timer.Elapsed += new ElapsedEventHandler(AnimateMarking);
            timer.Interval = RENDER_MARKING;
            timer.Enabled = true;
        }

        private void Execute()
        {
            Console.WriteLine($"Executing mag skill at {AttackPlace}");
            gS.Play(AttackOwner ? R.mag_skill_blue_0 : R.mag_skill_red_0);
            MakeDmg();
            gS.At(AttackPlace).CastingPath = null;

            timer = new Timer();
            timer.Elapsed += new ElapsedEventHandler(AnimateExecuting);
            timer.Interval = RENDER_EXECUTING;
            timer.Enabled = true;

        }

        private void AnimateMarking(object source, ElapsedEventArgs e)
        {
            if (frame < MAX_FRAME_MARKING)
            {
                gS.At(AttackPlace).CastingPath = CastingPath(frame);
                gS.At(AttackPlace).SkillDesc = Desc();
                frame++;
            }
            else
            {
                timer.Dispose();
                frame = 0;
            }
        }


        private void AnimateExecuting(object source, ElapsedEventArgs e)
        {
            if (loop < LOOPS)
            {
                if (frame > MAX_FRAME_EXECUTING)
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
                gS.PAt(AttackPlace, 1, 0)?.Def(Mag.SKILL_ATTACK_OUTSIDE + Bonus, gS, AttackPlace);

            }
            if (Arena.IsOK(AttackPlace, -1, 0)) //UP
            {
                gS.PAt(AttackPlace, -1, 0)?.Def(Mag.SKILL_ATTACK_OUTSIDE + Bonus, gS, AttackPlace);
            }
            if (Arena.IsOK(AttackPlace, 0, 1)) //RIGHT
            {
                gS.PAt(AttackPlace, 0, 1)?.Def(Mag.SKILL_ATTACK_OUTSIDE + Bonus, gS, AttackPlace);
            }
            if (Arena.IsOK(AttackPlace, 0, -1)) //LEFT
            {
                gS.PAt(AttackPlace, 0, -1)?.Def(Mag.SKILL_ATTACK_OUTSIDE + Bonus, gS, AttackPlace);
            }
            //CENTER
            gS.PAt(AttackPlace)?.Def(Dmg + Bonus, gS, AttackPlace);
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

        private string CastingPath(int frame) => string.Format(App.pathToMagMarking, AttackOwner ? App.blue : App.red, frame);

        public string SkillPath(int place, byte frame = 0)
        {
            if (AttackOwner == true) // blue
            {
                return string.Format(App.pathToMagExecute, App.blue, PlaceBlue(place), frame);
            }
            else // red
            {
                return string.Format(App.pathToMagExecute, App.red, PlaceRed(place), frame);
            }
        }

        private string PlaceBlue(int place)
        {
            if (place == 0)
            {
                return App.center;
            }
            else if (place == 1)
            {
                return App.left;
            }
            else if (place == 2)
            {
                return App.up;
            }
            else if (place == 3)
            {
                return App.right;
            }
            else if (place == 4)
            {
                return App.down;
            }
            else
            {
                throw new Exception($"Cannot find position with index {place}");
            }
        }

        private string PlaceRed(int place)
        {
            if (place == 0)
            {
                return App.center;
            }
            else if (place > 0 && place < 5)
            {
                return App.side;
            }
            else
            {
                throw new Exception($"Cannot find position with index {place}");
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                Console.WriteLine("Dispose mag skill");
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
