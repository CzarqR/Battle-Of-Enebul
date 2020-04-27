using ProjectB.Model.Figures;
using ProjectB.Model.Help;
using ProjectB.Model.Sklills;
using System;
using System.Collections.Generic;
using System.IO.IsolatedStorage;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectB.Model.Board
{
    using R = Properties.Resources;

    public class GameState
    {


        public static int HEIGHT = 11;
        public static int WIDTH = 11;

        private readonly Arena A = new Arena();



        private byte move = 0; //0 zaznacz| 1 porusz sie | 2 atak
        private bool turn = true; //czyja kolej
        private bool attackType; //true - primary, false - extra
        private bool attackChosen = false; //czy został wybrany atak

        private List<Cord> lastFields = new List<Cord>();
        private List<Cord> possibleAttackFields = new List<Cord>();
        private List<Cord> markedAttackFields = new List<Cord>();
        private readonly List<Skill> skills = new List<Skill>();

        private Cord cordToMove;
        private Cord cordToAttack;




        public delegate void ShowPawnInfo(string imgPath, string floorPath, string baseInfo, string precInfo, string bonuses, string primary_name, string primary_desc, string skill_name, string skill_desc);
        public event ShowPawnInfo ShowPawnEvent;

        public delegate void OnAttackStart(bool primaryAttack, bool extraAttack);
        public event OnAttackStart StartAttack;

        public delegate void FieldToAttackSelected();
        public event FieldToAttackSelected SelectedFieldToAttack;

        public delegate void EndRoundD();
        public event EndRoundD EndRoundEvent;


        public List<Cord> HandleInput(Cord C) //metoda zwraca kordy wszytkich pol na których sie cos zmieniło żeby okno moglo je zaktualizować
        {
            if (PAt(C) != null) //pole z pionkeim
            {
                ShowPawnEvent(A.PAt(C).ImgPath, A[C].FloorPath, PAt(C).Title, PAt(C).Bonuses, PAt(C).Desc, PAt(C).PrimaryAttackName, PAt(C).PrimaryAttackDesc, PAt(C).SkillAttackName, PAt(C).SkillAttackDesc);
            }
            else //sama podloga
            {
                ShowPawnEvent(null, A[C].FloorPath, A[C].FloorTitle, A[C].FloorPrecInfo, A[C].FloorBonuses, null, null, null, null);//tutaj trzeba wymyslic co ma sie pokazac gdy naciska sie na pustą podloge
            }

            Console.WriteLine("HandleInput dla pola; " + C + ". Move = " + move);

            if (move == 0)//gracz wybiera pionka którym chce sie ruszyć
            {
                return ShowPossibleMove(C);
            }
            else if (move == 1) // gracz wybiera miejsce w które chce się ruszyć
            {
                return MovePawnToField(C);
            }
            else if (move == 2) //gracz wybiera pole które chce zaatakować
            {
                return AttackField(C);
            }
            return null;
        }

        internal void AddSkill(Skill skill)
        {
            skills.Add(skill);
        }

        public List<Cord> AttackField(Cord C) //wybor pionka do zaatakowania
        {

            if (A[C].FloorStatus == FloorStatus.Attack)
            {
                cordToAttack = C;
                SelectedFieldToAttack?.Invoke();

                foreach (Cord cor in markedAttackFields)
                {
                    A[cor].FloorStatus = FloorStatus.Normal;
                }

                A[C].FloorStatus = FloorStatus.Attack;
                move = 3;
                return markedAttackFields;
            }
            return null;
        }

        public List<Cord> ExecuteAttack(int bonus1)
        {
            string x = attackType ? "Podstawowym" : "Extra";
            Console.WriteLine($"Pionek na polu {cordToMove} z bonusem {bonus1} atakuje atakiem {x} pionka na polu {cordToAttack} z bonusem ");


            if (attackType)
            {
                return (PAt(cordToMove).NormalAttack(this, cordToAttack));
            }
            else
            {
                return (PAt(cordToMove).SkillAttack(this, cordToAttack));
            }
        }

        public List<Cord> MarkFieldsToAttack(bool attackType)
        {
            if (!attackChosen)
            {
                this.attackType = attackType;
                markedAttackFields = PAt(cordToMove).MarkFieldsToAttack(possibleAttackFields, A, attackType);
                attackChosen = true;
                return possibleAttackFields;
            }
            return null;
        }

        public List<Cord> SkipMovement(Cord C)
        {
            if (move == 1)
            {
                Console.WriteLine("Skipping movement");
                cordToMove = C;
                move = 2;
                foreach (Cord cord in lastFields)
                {
                    A[cord].FloorStatus = FloorStatus.Normal;
                }
                ShowPawnEvent(PAt(C).ImgPath, A[C].FloorPath, PAt(C).Title, PAt(C).Bonuses, PAt(C).Desc, PAt(C).PrimaryAttackName, PAt(C).PrimaryAttackDesc, PAt(C).SkillAttackName, PAt(C).SkillAttackDesc);
                StartAttack?.Invoke(PAt(C).IsSomeoneToAttack(C, A, true), PAt(C).IsSomeoneToAttack(C, A, false));
                return lastFields;
            }
            else
            {
                Console.WriteLine(R.cannot_skip_movemnt);
                return null;
            }

        }


        public List<Cord> ShowPossiblePrimaryAttack()
        {

            if (!attackChosen)
            {
                return possibleAttackFields = PAt(cordToMove).ShowPossibleAttack(cordToMove, A, true);
            }
            else
            {
                return null;
            }
        }

        public List<Cord> ShowPossibleExtraAttack()
        {

            if (!attackChosen)
            {
                return possibleAttackFields = PAt(cordToMove).ShowPossibleAttack(cordToMove, A, false);
            }
            else
            {
                return null;
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

        private List<Cord> MovePawnToField(Cord C)
        {

            List<Cord> cordsToUpdate = new List<Cord>();

            if (A[C].FloorStatus == FloorStatus.Move) // move field to cord
            {
                if (!cordToMove.Equals(C)) //ruch na inne pole niz obecne
                {
                    A[C].PawnOnField = PAt(cordToMove);

                    A[cordToMove].PawnOnField = null;
                    cordToMove = C;
                    move = 2;

                    StartAttack?.Invoke(PAt(C).IsSomeoneToAttack(C, A, true), PAt(C).IsSomeoneToAttack(C, A, false));

                }
                else // nacisniecie na pole na ktorym byl pionek, anulowanie ruchu
                {
                    move = 0;
                }
                foreach (Cord cord in lastFields)
                {
                    A[cord].FloorStatus = FloorStatus.Normal;
                    cordsToUpdate.Add(cord);
                }
                ShowPawnEvent(PAt(C).ImgPath, A[C].FloorPath, PAt(C).Title, PAt(C).Bonuses, PAt(C).Desc, PAt(C).PrimaryAttackName, PAt(C).PrimaryAttackDesc, PAt(C).SkillAttackName, PAt(C).SkillAttackDesc);
            }
            return cordsToUpdate;
        }

        private List<Cord> ShowPossibleMove(Cord C)
        {

            if (PAt(C) != null && PAt(C).Owner == turn) //click on own pawn
            {
                move = 1;
                cordToMove = C;
                return lastFields = PAt(C).ShowPossibleMove(C, A);
            }
            else //click on empty field or enemy pawn
            {
                lastFields.Clear();
                return lastFields;
            }
        }

        public List<Cord> EndRound()
        {
            Console.WriteLine($"End round, move = {move}");
            turn ^= true;
            attackChosen = false;

            EndRoundEvent?.Invoke();
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



