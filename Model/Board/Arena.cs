using ProjectB.Model.Figures;
using ProjectB.Model.Help;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectB.Model.Board
{
    public class Arena
    {
        public static int HEIGHT = 11;
        public static int WIDTH = 11;
        public readonly Field[,] B;


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

        public Arena()
        {
            B = new Field[HEIGHT, WIDTH];
            for (int i = 0; i < HEIGHT; i++)
            {
                for (int j = 0; j < WIDTH; j++)
                {
                    B[i, j] = new Field();
                }
            }


            FieldBonusInit();
            PlacePawns();
        }

        private void FieldBonusInit()
        {
            B[5, 5].Floor = FloorType.Attack;
            B[1, 1].Floor = FloorType.Def;
            B[9, 9].Floor = FloorType.Def;
            B[1, 9].Floor = FloorType.Def;
            B[9, 1].Floor = FloorType.Def;
            B[5, 2].Floor = FloorType.Cond;
            B[5, 8].Floor = FloorType.Cond;
        }

        private void PlacePawns()
        {
            B[4, 5].PawnOnField = new Defender(false);
            B[2, 3].PawnOnField = new Mag(false);
            B[0, 5].PawnOnField = new King(false);
            B[3, 1].PawnOnField = new Assassin(false);
            B[2, 7].PawnOnField = new Archer(false);
            B[3, 9].PawnOnField = new Axeman(false);

            B[6, 5].PawnOnField = new Defender(true);
            B[8, 7].PawnOnField = new Mag(true);
            B[10, 5].PawnOnField = new King(true);
            B[7, 9].PawnOnField = new Assassin(true);
            B[8, 3].PawnOnField = new Archer(true);
            B[7, 1].PawnOnField = new Axeman(true);
        }
    }
}
