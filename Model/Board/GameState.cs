using ProjectB.Model.Figures;
using ProjectB.Model.Help;
using ProjectB.Model.Render;
using ProjectB.Model.Sklills;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Timers;
using System.Windows.Threading;

namespace ProjectB.Model.Board
{
    using R = Properties.Resources;

    public sealed class GameState : IDisposable
    {

        #region Properties

        private readonly Arena A = new Arena();
        private byte attackBonus;
        private byte move = 0; //0 wybor pionka | 1 porusz sie pionkeim  | 2 wybor ataku| 3 wybor kogo zaatakowac | 4 rzut koscią 
        private bool attackType; //true - primary, false - extra
        private bool isGameEnded = false;
        private Cord movedPawn;
        private Cord attackPlace;
        private List<Cord> cordsMarkedToMove; //list of fields marked as green in move = 0
        private List<Cord> cordsMarkedToAttackRange; //list of all fields marked as purple to show attack range
        private List<Cord> cordsMarkedToPossibleAttack; // list of cords which player can execute attack
        private readonly List<Skill> skills = new List<Skill>(); //list of all active skills on arena

        public bool CanSkip
        {
            get; private set;
        } = false;
        public static bool Turn
        {
            get; private set;
        }

        #endregion


        #region Events

        public event Action<string, string, string, string> ShowCustomPanelEvent;
        public event Action<string, string, string, string, string, string, string, string> ShowPawnInfoEvent;
        public event Action<string, string, string, string> ShowFloorInfoEvent;
        public event Action<string[], int, FloorStatus> UpdateUIEvent;
        public event Action<bool, bool> StartAttackEvent;
        public event Action FieldToAttackSelectedEvent;
        public event Action<string> CursosUpdateEvent;
        public event Action<bool> OnlyCanEnd;
        public event Action<UnmanagedMemoryStream> PlaySound;
        public event Action EndGameEvent;

        #endregion


        #region Game routine


        public void HandleInput(Cord C)
        {
            if (!isGameEnded)
            {
                Console.WriteLine($"HandleInput for field: {C}. Move = {move}");

                if (move == 0) // selecting pwan
                {
                    ShowPossibleMove(C);
                }
                else if (move == 1) // selecting field to move
                {
                    MovePawnToField(C);
                }
                else if (move == 2) //selecting attack
                {
                    Console.WriteLine("First you have to chose attack type");
                }
                else if (move == 3) // selecting pawn to attack
                {
                    ChoseFieldToAttack(C);
                }
                else if (move == 4) // dice roll
                {
                    Console.WriteLine("First you have to roll the dice");
                }
                else if (move == 5) // end tour
                {
                    Console.WriteLine("Turn is finished, click end round");
                    if (PAt(C) != null) // clicking pawn
                    {
                        ShowPawnInfo(C);
                    }
                    else // clicking floor
                    {
                        ShowFloorInfo(C);
                    }
                }
            }
            else
            {
                Console.WriteLine("Game is finished!");
            }
        }

        // 0
        private void ShowPossibleMove(Cord C)
        {

            if (PAt(C) != null) //click on pawn
            {
                if (PAt(C).Owner == Turn) //own pawn
                {
                    move = 1;
                    movedPawn = C;
                    cordsMarkedToMove = PAt(C).ShowPossibleMove(C, A);
                    ShowPawnInfo(C);
                    CanSkip = true;
                }
                else //enemy pawn
                {
                    ShowPawnInfo(C);
                    CanSkip = false;
                }

            }
            else
            {
                CanSkip = false;
                ShowFloorInfo(C);
                Console.WriteLine("Empty field or enemy pawn was clicked");
            }
        }


        // 1
        private void MovePawnToField(Cord C)
        {

            if (A[C].FloorStatus == FloorStatus.Move) // move pawn to cord
            {
                if (!movedPawn.Equals(C)) //ruch na inne pole niz obecne
                {
                    Console.WriteLine($"Pawn from {movedPawn} can be moved to {C}");

                    A[C].PawnOnField = PAt(movedPawn);
                    A[movedPawn].PawnOnField = null;
                    movedPawn = C;
                    move = 2;
                    A[C].PawnOnField.Cord = C;
                    ShowAtttack(C);
                    ShowPawnInfo(C);

                }
                else // nacisniecie na pole na ktorym byl pionek, anulowanie ruchu
                {
                    Console.WriteLine($"Canceling moving pawn from {movedPawn}, same field was clicked ({C})");
                    move = 0;
                }
                foreach (Cord cord in cordsMarkedToMove)
                {
                    A[cord].FloorStatus = FloorStatus.Normal;
                }
                CanSkip = false;
            }
            else //selecting field on which pawn cannot move
            {
                if (PAt(C) != null)
                {
                    if (PAt(C)?.Owner == Turn)//clicking at current player pawn
                    {
                        foreach (Cord cord in cordsMarkedToMove)
                        {
                            A[cord].FloorStatus = FloorStatus.Normal;
                        }
                        ShowPossibleMove(C);
                        ShowPawnInfo(C);
                    }
                    else //clicking at enemy pawn
                    {
                        ShowPawnInfo(C);
                        CanSkip = false;
                    }
                }
                else //empty field
                {
                    ShowFloorInfo(C);
                    CanSkip = false;
                }

            }

        }

        private void ShowAtttack(Cord C)
        {
            bool primaryAttackEnable = PAt(C).IsSomeoneToAttack(C, A, true);
            bool skillAttackEnable = PAt(C).IsSomeoneToAttack(C, A, false);


            if (primaryAttackEnable || skillAttackEnable) //can attack
            {
                Console.WriteLine("Moved pawn can attack");
            }
            else //cannot attack
            {
                Console.WriteLine("Moved pawn cannot attack anyone");
                OnlyCanEnd?.Invoke(true);
            }

            StartAttackEvent?.Invoke(primaryAttackEnable, skillAttackEnable);
        }


        //2
        public void ShowPossibleAttack(bool attackType)
        {
            if (move == 2)
            {
                cordsMarkedToAttackRange = PAt(movedPawn).ShowPossibleAttack(movedPawn, A, attackType);
            }
        }

        public void HidePossibleAttack()
        {
            if (move == 2)
            {
                foreach (Cord cord in cordsMarkedToAttackRange)
                {
                    A[cord].FloorStatus = FloorStatus.Normal;
                }
            }
        }

        public void MarkFieldsToAttack(bool attackType)
        {
            StartAttackEvent?.Invoke(attackType ? true : false, attackType ? false : true);
            if (move == 2)
            {
                Console.WriteLine("Marking fields");
                this.attackType = attackType;
                move = 3;
                cordsMarkedToPossibleAttack = PAt(movedPawn).MarkFieldsToAttack(cordsMarkedToAttackRange, A, attackType);
            }
        }


        //3
        public void ChoseFieldToAttack(Cord C)
        {

            if (A[C].FloorStatus == FloorStatus.Attack)
            {
                Console.WriteLine($"Marking pawn at {C} to attack");
                attackPlace = C;

                foreach (Cord cor in cordsMarkedToPossibleAttack)
                {
                    A[cor].FloorStatus = FloorStatus.Normal;
                }

                A[C].FloorStatus = FloorStatus.Attack;
                move = 4;
                FieldToAttackSelectedEvent?.Invoke();
            }
            else
            {
                Console.WriteLine("You have to chose pawn marked in purple");
            }

        }


        //4
        public void RollDice(byte bonus)
        {
            if (move == 4)
            {
                attackBonus = bonus;
                ExecuteAttack();
            }
        }

        public void ExecuteAttack()
        {
            string x = attackType ? "Attack" : "Skill";
            Console.WriteLine($"Pawn at {movedPawn} with bonus {attackBonus}. {x} on pawn at {attackPlace}");


            if (attackType)
            {
                PAt(movedPawn).NormalAttack(this, attackPlace, attackBonus);
                PlaySound?.Invoke(PAt(movedPawn).AttackSound);
            }
            else
            {
                PAt(movedPawn).SkillAttack(this, attackPlace, attackBonus);
                PlaySound?.Invoke(PAt(movedPawn).AttackSound);
            }
            A[attackPlace].FloorStatus = FloorStatus.Normal;
            move = 5;
            OnlyCanEnd?.Invoke(true);
        }


        //5

        public void EndRound()
        {
            Console.WriteLine($"End round, move = {move}");
            Turn ^= true;
            OnlyCanEnd?.Invoke(false);
            CanSkip = false;


            if (move == 0)
            {
            }
            else if (move == 1)
            {
                move = 0;
                foreach (Cord C in cordsMarkedToMove)
                {
                    A[C].FloorStatus = FloorStatus.Normal;
                }
            }
            else if (move == 2)
            {
                move = 0;
            }
            else if (move == 3)
            {
                move = 0;
                foreach (Cord C in cordsMarkedToPossibleAttack)
                {
                    A[C].FloorStatus = FloorStatus.Normal;
                }
            }
            else if (move == 4)
            {
                move = 0;
                A[attackPlace].FloorStatus = FloorStatus.Normal;
            }
            else if (move == 5)
            {
                move = 0;
            }
            else
            {
                throw new NotImplementedException();
            }
            SkillLifecycle();
            A.EndRound(Turn);
            UpdateCursor(App.defauLt);
        }

        public void SkipMovement()
        {
            if (move == 1)
            {
                Console.WriteLine("Skipping movement");
                move = 2;
                foreach (Cord cord in cordsMarkedToMove)
                {
                    A[cord].FloorStatus = FloorStatus.Normal;
                }
                ShowAtttack(movedPawn);
                //UpdateFieldsOnBoard(cordsMarkedToMove);
                CanSkip = false;
            }
            else
            {
                Console.WriteLine("To skip movement pawn has to be selected");
            }

        }

        private void SkillLifecycle()
        {
            Console.WriteLine($"Executing skills. Skills count {skills.Count}");
            skills.Sort();
            foreach (Skill skill in skills)
            {
                skill.Lifecycle();
            }

            //removing finished skills
            for (int i = 0; i < skills.Count;)
            {
                if (skills[i].Finished)
                {
                    skills[i].Dispose();
                    skills.RemoveAt(i);
                }
                else
                {
                    i++;
                }
            }

            Console.WriteLine("End, skills count : " + skills.Count);
        }


        #endregion


        #region UI



        public void UpdateFieldOnBoard(Cord cord)
        {
            UpdateUIEvent?.Invoke(GetFieldView(cord), cord.X * Arena.HEIGHT + cord.Y, A[cord].FloorStatus);
        }

        //Return array of values which field control has
        public string[] GetFieldView(Cord C)
        {
            string[] r = new string[7];
            r[0] = A[C].FloorPath;
            r[1] = A[C].CastingPath;
            r[2] = A[C].SkillPath;
            r[3] = PAt(C)?.ImgPath;
            r[4] = PAt(C)?.HP.ToString();
            r[5] = PAt(C)?.Manna.ToString();

            r[6] = A[C].GetToolTip();

            return r;
        }

        public string[] GetFieldView(int x, int y)
        {
            return GetFieldView(new Cord(x, y));
        }

        private void ShowPawnInfo(Cord C)
        {
            ShowPawnInfoEvent?.Invoke(PAt(C).Title, PAt(C).ImgBigPath, PAt(C).Desc, PAt(C).Bonuses(A[C].Floor), PAt(C).PrimaryAttackName, PAt(C).PrimaryAttackDesc, PAt(C).SkillAttackName, PAt(C).SkillAttackDesc);
        }

        private void ShowFloorInfo(Cord C)
        {
            ShowFloorInfoEvent?.Invoke(A[C].GetTitle(), A[C].FloorPath, A[C].GetDesc(), A[C].GetLegend());
        }

        private void UpdateCursor(string type)
        {
            CursosUpdateEvent?.Invoke(string.Format(App.cursorPath, Turn ? "blue" : "red", type));
        }

        #endregion


        #region Help functions

        public Pawn PAt(Cord cord, int x = 0, int y = 0) => A.PAt(cord, x, y);

        public Field At(Cord cord, int x = 0, int y = 0) => A[cord, x, y];

        public Pawn PAt(int x = 0, int y = 0) => A.PAt(x, y);

        public Field At(int x = 0, int y = 0) => A[x, y];

        #endregion


        #region Methods

        public GameState()
        {
            Console.WriteLine("Ctor GameState");
            Turn = true;
            RenderEngine.UpdateField += UpdateFieldOnBoard;
        }



        public void AddSkill(Skill skill)
        {
            skills.Add(skill);
        }

        public void KillPawn(Cord C)
        {
            A[C].PawnOnField.Dispose();
            A[C].PawnOnField = null;

        }

        public void EndGame()
        {
            Console.WriteLine($"Game has ended, {Turn} Won");
            isGameEnded = true;
            ShowCustomPanelEvent?.Invoke(R.end_game_title, Turn ? App.pathToCustomImageEndBlue : App.pathToCustomImageEndRed, R.end_game_legend, R.end_game_bottom_title);
            EndGameEvent?.Invoke();


        }

        public void StartGame()
        {
            ShowCustomPanelEvent?.Invoke(R.starting_title, App.pathToCustomImageStart, R.starting_legend, R.start_game_bottom_title);
            UpdateCursor(App.defauLt);


        }

        public void Play(UnmanagedMemoryStream sound)
        {
            PlaySound?.Invoke(sound);
        }

        public void Dispose()
        {
            Console.WriteLine("Dispose GameState");
            A.Dispose();
            foreach (Skill skill in skills)
            {
                skill.Dispose();
            }
        }

        #endregion

    }
}