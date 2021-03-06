﻿using ProjectB.Model.Board;
using ProjectB.Model.Help;
using System;

namespace ProjectB.Model.Sklills
{
#pragma warning disable CA1036 // Override methods on comparable types
    public abstract class Skill : IComparable<Skill>, IDisposable
#pragma warning restore CA1036 // Override methods on comparable types
    {
        public override string ToString()
        {
            return $"Skill at {AttackPlace} with dmg {Dmg}. From {AttackOwner}, Round To Finish: {RoundsToExec}";
        }

        public byte RoundsToExec
        {
            get; protected set;
        }

        public Cord AttackPlace
        {
            get; protected set;
        }

        public bool AttackOwner
        {
            get; protected set;
        }

        public int Dmg
        {
            get; protected set;
        }

        public bool Finished
        {
            get; protected set;
        }

        public int Bonus
        {
            get; private set;
        }

        protected GameState gS;

        protected Skill(Cord attackPlace, bool attackOwner, int dmg, int bonus, GameState gS, byte roundsToExec)
        {
            RoundsToExec = roundsToExec;
            AttackPlace = attackPlace;
            AttackOwner = attackOwner;
            Dmg = dmg;
            Finished = false;
            Bonus = bonus;
            this.gS = gS;
        }

        public abstract void Lifecycle();

        public int CompareTo(Skill other)
        {
            return (RoundsToExec.CompareTo(other.RoundsToExec));
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {

            }

        }
    }
}
