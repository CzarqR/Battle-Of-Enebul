﻿using ProjectB.Model.Figures;
using ProjectB.Model.Help;
using System;

namespace ProjectB.Model.Board
{
    public sealed class Arena : IDisposable
    {

        #region Properties

        public static int HEIGHT = 11;
        public static int WIDTH = 11;
        private readonly Field[,] B;

        #endregion


        #region Help functions

        public static bool IsOK(Cord cord, int x = 0, int y = 0) => cord.X + x >= 0 && cord.X + x < WIDTH && cord.Y + y >= 0 && cord.Y + y < HEIGHT;
        public static bool IsOK(int x, int y) => x >= 0 && x < WIDTH && y >= 0 && y < HEIGHT;

        public static bool IsXOK(Cord cord) => cord.X >= 0 && cord.X < WIDTH;
        public static bool IsXOK(int x) => x >= 0 && x < WIDTH;

        public static bool IsYOK(Cord cord) => cord.Y >= 0 && cord.Y < HEIGHT;
        public static bool IsYOK(int y) => y >= 0 && y < HEIGHT;

        public Field At(Cord cord, int x = 0, int y = 0) => B[cord.X + x, cord.Y + y];
        public Field At(int x, int y) => B[x, y];

        public Pawn PAt(Cord cord, int x = 0, int y = 0) => B[cord.X + x, cord.Y + y].PawnOnField;
        public Pawn PAt(int x, int y) => B[x, y].PawnOnField;

        public Field this[Cord cord, int x = 0, int y = 0] => B[cord.X + x, cord.Y + y];
        public Field this[int x, int y] => B[x, y];

        #endregion


        #region Methods

        public Arena()
        {
            B = new Field[HEIGHT, WIDTH];
            for (int i = 0; i < HEIGHT; i++)
            {
                for (int j = 0; j < WIDTH; j++)
                {
                    B[i, j] = new Field
                    {
                        Cord = new Cord(i, j)
                    };
                }
            }

            FieldBonusInit();
            PlacePawns();
        }

        private void FieldBonusInit()
        {
            B[5, 3].Floor = FloorType.Attack;
            B[5, 4].Floor = FloorType.Attack;
            B[5, 5].Floor = FloorType.Attack;
            B[5, 6].Floor = FloorType.Attack;
            B[5, 7].Floor = FloorType.Attack;

            B[2, 1].Floor = FloorType.Def;
            B[2, 2].Floor = FloorType.Def;
            B[2, 8].Floor = FloorType.Def;
            B[2, 9].Floor = FloorType.Def;
            B[8, 1].Floor = FloorType.Def;
            B[8, 2].Floor = FloorType.Def;
            B[8, 8].Floor = FloorType.Def;
            B[8, 9].Floor = FloorType.Def;

            B[3, 4].Floor = FloorType.Cond;
            B[3, 6].Floor = FloorType.Cond;
            B[7, 4].Floor = FloorType.Cond;
            B[7, 6].Floor = FloorType.Cond;
            B[4, 0].Floor = FloorType.Cond;
            B[4, 10].Floor = FloorType.Cond;
            B[6, 0].Floor = FloorType.Cond;
            B[6, 10].Floor = FloorType.Cond;
        }

        private void PlacePawns()
        {
            ///Never spend 6 minutes doing something by hand when you can spend 6 hours failing to automate it 

            //red
            Pawn defR0 = new Defender(false, new Cord(0, 3));
            Pawn defR1 = new Defender(false, new Cord(0, 7));
            Pawn defR2 = new Defender(false, new Cord(1, 4));
            Pawn defR3 = new Defender(false, new Cord(1, 5));
            Pawn defR4 = new Defender(false, new Cord(1, 6));
            Pawn defR5 = new Defender(false, new Cord(2, 5));

            Pawn magR0 = new Mag(false, new Cord(0, 4));
            Pawn magR1 = new Mag(false, new Cord(0, 6));

            Pawn kingR = new King(false, new Cord(0, 5));

            Pawn assassinR0 = new Assassin(false, new Cord(0, 0));
            Pawn assassinR1 = new Assassin(false, new Cord(0, 10));

            Pawn archerR0 = new Archer(false, new Cord(0, 1));
            Pawn archerR1 = new Archer(false, new Cord(0, 9));

            Pawn axemanR0 = new Axeman(false, new Cord(0, 2));
            Pawn axemanR1 = new Axeman(false, new Cord(0, 8));
            Pawn axemanR2 = new Axeman(false, new Cord(1, 2));
            Pawn axemanR3 = new Axeman(false, new Cord(1, 8));


            B[0, 3].PawnOnField = defR0;
            B[0, 7].PawnOnField = defR1;
            B[1, 4].PawnOnField = defR2;
            B[1, 5].PawnOnField = defR3;
            B[1, 6].PawnOnField = defR4;
            B[2, 5].PawnOnField = defR5;

            B[0, 4].PawnOnField = magR0;
            B[0, 6].PawnOnField = magR1;

            B[0, 5].PawnOnField = kingR;

            B[0, 0].PawnOnField = assassinR0;
            B[0, 10].PawnOnField = assassinR1;

            B[0, 1].PawnOnField = archerR0;
            B[0, 9].PawnOnField = archerR1;

            B[0, 2].PawnOnField = axemanR0;
            B[0, 8].PawnOnField = axemanR1;
            B[1, 2].PawnOnField = axemanR2;
            B[1, 8].PawnOnField = axemanR3;

            //blue
            Pawn defB0 = new Defender(true, new Cord(10, 3));
            Pawn defB1 = new Defender(true, new Cord(10, 7));
            Pawn defB2 = new Defender(true, new Cord(9, 4));
            Pawn defB3 = new Defender(true, new Cord(9, 5));
            Pawn defB4 = new Defender(true, new Cord(9, 6));
            Pawn defB5 = new Defender(true, new Cord(8, 5));

            Pawn magB0 = new Mag(true, new Cord(10, 4));
            Pawn magB1 = new Mag(true, new Cord(10, 6));

            Pawn kingB = new King(true, new Cord(10, 5));

            Pawn assassinB0 = new Assassin(true, new Cord(10, 0));
            Pawn assassinB1 = new Assassin(true, new Cord(10, 10));

            Pawn archerB0 = new Archer(true, new Cord(10, 1));
            Pawn archerB1 = new Archer(true, new Cord(10, 9));

            Pawn axemanB0 = new Axeman(true, new Cord(10, 2));
            Pawn axemanB1 = new Axeman(true, new Cord(10, 8));
            Pawn axemanB2 = new Axeman(true, new Cord(9, 2));
            Pawn axemanB3 = new Axeman(true, new Cord(9, 8));



            B[10, 3].PawnOnField = defB0;
            B[10, 7].PawnOnField = defB1;
            B[9, 4].PawnOnField = defB2;
            B[9, 5].PawnOnField = defB3;
            B[9, 6].PawnOnField = defB4;
            B[8, 5].PawnOnField = defB5;

            B[10, 4].PawnOnField = magB0;
            B[10, 6].PawnOnField = magB1;

            B[10, 5].PawnOnField = kingB;

            B[10, 0].PawnOnField = assassinB0;
            B[10, 10].PawnOnField = assassinB1;

            B[10, 1].PawnOnField = archerB0;
            B[10, 9].PawnOnField = archerB1;

            B[10, 2].PawnOnField = axemanB0;
            B[10, 8].PawnOnField = axemanB1;
            B[9, 2].PawnOnField = axemanB2;
            B[9, 8].PawnOnField = axemanB3;
        }

        public void Dispose()
        {
            Console.WriteLine("Dispose Arena");
            foreach (Field field in B)
            {
                field.PawnOnField?.Dispose();
            }
        }

        public void EndRound(bool turn)
        {
            foreach (Field field in B)
            {
                if (field.PawnOnField != null && field.PawnOnField?.Owner == turn)
                {
                    field.PawnOnField?.MannaRegenerationAtNewRound();
                }
            }
        }

        #endregion

    }
}
