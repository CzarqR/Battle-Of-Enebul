using ProjectB.Model.Figures;
using ProjectB.Model.Help;
using ProjectB.Model.Sklills;
using ProjectB.ViewModel;
using System;
using System.Collections.Generic;
using System.IO.IsolatedStorage;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectB.Model.Board
{
    using R = Properties.Resources;

    public class GameState : BaseVM
    {


        public static int HEIGHT = 11;
        public static int WIDTH = 11;

        private readonly Arena A = new Arena();




        private byte move = 0; //0 zaznacz| 1 porusz sie | 2 atak
        private bool turn = true; //czyja kolej
        private bool attackType; //true - primary, false - extra
        private bool attackChosen = false; //czy został wybrany atak

        private List<Cord> cordsMarkedToMove; //list of fields marked as green in move = 0
        private Cord movedPawn;
        private Cord attackedPlace;


        private List<Cord> lastFields = new List<Cord>();
        private List<Cord> possibleAttackFields = new List<Cord>();
        private List<Cord> markedAttackFields = new List<Cord>();
        private readonly List<Skill> skills = new List<Skill>();







        //public delegate void ShowPawnInfo(string imgPath, string floorPath, string baseInfo, string precInfo, string bonuses, string primary_name, string primary_desc, string skill_name, string skill_desc);
        //public event ShowPawnInfo ShowPawnEvent;

        //public delegate void OnAttackStart(bool primaryAttack, bool extraAttack);
        //public event OnAttackStart StartAttack;

        //public delegate void FieldToAttackSelected();
        //public event FieldToAttackSelected SelectedFieldToAttack;

        //public delegate void EndRoundD();
        //public event EndRoundD EndRoundEvent;















        #region New'Clean'Code


        public void HandleInput(Cord C) //metoda zwraca kordy wszytkich pol na których sie cos zmieniło żeby okno moglo je zaktualizować
        {
            //if (PAt(C) != null) //pole z pionkeim
            //{
            //    ShowPawnEvent(A.PAt(C).ImgPath, A[C].FloorPath, PAt(C).Title, PAt(C).Bonuses, PAt(C).Desc, PAt(C).PrimaryAttackName, PAt(C).PrimaryAttackDesc, PAt(C).SkillAttackName, PAt(C).SkillAttackDesc);
            //}
            //else //sama podloga
            //{
            //    ShowPawnEvent(null, A[C].FloorPath, A[C].FloorTitle, A[C].FloorPrecInfo, A[C].FloorBonuses, null, null, null, null);//tutaj trzeba wymyslic co ma sie pokazac gdy naciska sie na pustą podloge
            //}

            Console.WriteLine("HandleInput dla pola; " + C + ". Move = " + move);

            if (move == 0)//gracz wybiera pionka którym chce sie ruszyć
            {
                ShowPossibleMove(C);
            }
            else if (move == 1) // gracz wybiera miejsce w które chce się ruszyć
            {
                MovePawnToField(C);
            }

            //else if (move == 2) //gracz wybiera pole które chce zaatakować
            //{
            //    UpdateWholeBoard();
            //    return AttackField(C);
            //}

            UpdateWholeBoard();

        }



        // 0
        private void ShowPossibleMove(Cord C)
        {

            if (PAt(C) != null && PAt(C).Owner == turn) //click on own pawn
            {
                Console.WriteLine($"{PAt(C).Class } was clicked");
                move = 1;
                movedPawn = C;
                cordsMarkedToMove = PAt(C).ShowPossibleMove(C, A);
            }
            else
            {
                Console.WriteLine("Empty field or enemy pawn was clicked");
            }
        }


        //1
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

                    //StartAttack?.Invoke(PAt(C).IsSomeoneToAttack(C, A, true), PAt(C).IsSomeoneToAttack(C, A, false));
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
                //ShowPawnEvent(PAt(C).ImgPath, A[C].FloorPath, PAt(C).Title, PAt(C).Bonuses, PAt(C).Desc, PAt(C).PrimaryAttackName, PAt(C).PrimaryAttackDesc, PAt(C).SkillAttackName, PAt(C).SkillAttackDesc);
            }
            else
            {
                Console.WriteLine($"Cannot move pawn to this field {C}");
            }

        }






        #endregion



























        public delegate void UpdateUIDelegate(string[] fieldItems, int index);
        public event UpdateUIDelegate UpdateUIEvent;


        public void UpdateWholeBoard()
        {
            for (int i = 0; i < Arena.HEIGHT; i++)
            {
                for (int j = 0; j < Arena.WIDTH; j++)
                {
                    UpdateUIEvent(GetFieldView(i, j), i * Arena.HEIGHT + j);
                }
            }
        }

        //Return array of values which field control has
        public string[] GetFieldView(Cord C)
        {
            string[] r = new string[6];
            r[0] = A[C].FloorPath;
            r[1] = A[C].CastingPath;
            r[2] = A[C].SkillPath;
            if (A[C].PawnOnField != null)
            {
                r[3] = PAt(C).ImgPath;
                r[4] = PAt(C).HP.ToString();
                r[5] = PAt(C).Manna.ToString();
            }
            else
            {
                r[3] = null;
                r[4] = null;
                r[5] = null;
            }
            return r;
        }

        public string[] GetFieldView(int x, int y)
        {
            string[] r = new string[6];
            r[0] = A[x, y].FloorPath;
            r[1] = A[x, y].CastingPath;
            r[2] = A[x, y].SkillPath;
            if (A[x, y].PawnOnField != null)
            {
                r[3] = PAt(x, y).ImgPath;
                r[4] = PAt(x, y).HP.ToString();
                r[5] = PAt(x, y).Manna.ToString();
            }
            else
            {
                r[3] = null;
                r[4] = null;
                r[5] = null;
            }
            return r;
        }







        internal void AddSkill(Skill skill)
        {
            skills.Add(skill);
        }

        public void AttackField(Cord C) //wybor pionka do zaatakowania
        {

            if (A[C].FloorStatus == FloorStatus.Attack)
            {
                attackedPlace = C;
                //SelectedFieldToAttack?.Invoke();

                foreach (Cord cor in markedAttackFields)
                {
                    A[cor].FloorStatus = FloorStatus.Normal;
                }

                A[C].FloorStatus = FloorStatus.Attack;
                move = 3;
            }

        }

        public void ExecuteAttack(int bonus1)
        {
            string x = attackType ? "Podstawowym" : "Extra";
            Console.WriteLine($"Pionek na polu {movedPawn} z bonusem {bonus1} atakuje atakiem {x} pionka na polu {attackedPlace} z bonusem ");


            if (attackType)
            {
                PAt(movedPawn).NormalAttack(this, attackedPlace);
            }
            else
            {
                PAt(movedPawn).SkillAttack(this, attackedPlace);
            }
        }

        public void MarkFieldsToAttack(bool attackType)
        {
            if (!attackChosen)
            {
                this.attackType = attackType;
                PAt(movedPawn).MarkFieldsToAttack(possibleAttackFields, A, attackType);
                attackChosen = true;
                //return possibleAttackFields;
            }
            //return null;
        }

        public List<Cord> SkipMovement(Cord C)
        {
            if (move == 1)
            {
                Console.WriteLine("Skipping movement");
                movedPawn = C;
                move = 2;
                foreach (Cord cord in lastFields)
                {
                    A[cord].FloorStatus = FloorStatus.Normal;
                }
                //ShowPawnEvent(PAt(C).ImgPath, A[C].FloorPath, PAt(C).Title, PAt(C).Bonuses, PAt(C).Desc, PAt(C).PrimaryAttackName, PAt(C).PrimaryAttackDesc, PAt(C).SkillAttackName, PAt(C).SkillAttackDesc);
                //StartAttack?.Invoke(PAt(C).IsSomeoneToAttack(C, A, true), PAt(C).IsSomeoneToAttack(C, A, false));
                return lastFields;
            }
            else
            {
                Console.WriteLine(R.cannot_skip_movemnt);
                return null;
            }

        }


        public void ShowPossiblePrimaryAttack()
        {

            if (!attackChosen)
            {
                PAt(movedPawn).ShowPossibleAttack(movedPawn, A, true);
            }
            else
            {
                //return null;
            }
        }

        public void ShowPossibleExtraAttack()
        {

            if (!attackChosen)
            {
                PAt(movedPawn).ShowPossibleAttack(movedPawn, A, false);
            }
            else
            {
                //return null;
            }
        }


        public List<Cord> HideAttackFields()
        {
            List<Cord> cordsToUpdate = new List<Cord>();

            if (!attackChosen)
            {
                foreach (Cord C in possibleAttackFields)
                {
                    cordsToUpdate.Add(C);
                    A[C].FloorStatus = FloorStatus.Normal;
                }
            }
            return cordsToUpdate;
        }

        public void KillPawn(Cord C)
        {
            A[C].PawnOnField = null;
        }

       



        public List<Cord> EndRound()
        {
            Console.WriteLine($"End round, move = {move}");
            turn ^= true;
            attackChosen = false;

            //EndRoundEvent?.Invoke();
            if (move == 0)
            {
                return SkillLifecycle();
            }
            else if (move == 1)
            {
                move = 0;
                foreach (Cord C in lastFields)
                {
                    A[C].FloorStatus = FloorStatus.Normal;
                }
                return SkillLifecycle().Concat(lastFields).ToList();
            }
            else if (move == 2 || move == 3)
            {
                move = 0;
                foreach (Cord C in markedAttackFields)
                {
                    A[C].FloorStatus = FloorStatus.Normal;
                }
                return SkillLifecycle().Concat(markedAttackFields).ToList();
            }

            throw new NotImplementedException();
        }


        public Pawn PAt(Cord cord, int x = 0, int y = 0) => A.PAt(cord, x, y);
        public Field At(Cord cord, int x = 0, int y = 0) => A[cord, x, y];

        public Pawn PAt(int x = 0, int y = 0) => A.PAt(x, y);
        public Field At(int x = 0, int y = 0) => A[x, y];




        private List<Cord> SkillLifecycle()
        {

            Console.WriteLine($"Executing skills. Skills count {skills.Count}");
            List<Cord> cordsToUpdate = new List<Cord>();
            skills.Sort();
            foreach (Skill skill in skills)
            {
                Console.WriteLine(skill);
                cordsToUpdate = cordsToUpdate.Concat(skill.Lifecycle(this)).ToList();
            }

            for (int i = 0; i < skills.Count;)
            {
                if (skills[i].Finished)
                {
                    skills.RemoveAt(i);
                }
                else
                {
                    i++;
                }
            }


            Console.WriteLine("End skills count : " + skills.Count);

            return cordsToUpdate;
        }


    }
}



