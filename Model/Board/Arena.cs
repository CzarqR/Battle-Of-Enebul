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
        private readonly Field[,] B;


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

        public List<Pawn> BluePawns
        {
            get; private set;
        }


        public List<Pawn> RedPawns
        {
            get; private set;
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
            ///Never spend 6 minutes doing something by hand when you can spend 6 hours failing to automate it 

            BluePawns = new List<Pawn>();
            RedPawns = new List<Pawn>();

            Pawn defB = new Defender(false);
            Pawn magB = new Mag(false);
            Pawn kingB = new King(false);
            Pawn assassinB = new Assassin(false);
            Pawn archerB = new Archer(false);
            Pawn axemanB = new Axeman(false);

            BluePawns.Add(defB);
            BluePawns.Add(magB);
            BluePawns.Add(kingB);
            BluePawns.Add(assassinB);
            BluePawns.Add(archerB);
            BluePawns.Add(axemanB);

            B[4, 5].PawnOnField = defB;
            B[2, 3].PawnOnField = magB;
            B[0, 5].PawnOnField = kingB;
            B[3, 1].PawnOnField = assassinB;
            B[2, 7].PawnOnField = archerB;
            B[3, 9].PawnOnField = axemanB;

            Pawn defR = new Defender(true);
            Pawn magR = new Mag(true);
            Pawn kingR = new King(true);
            Pawn assassinR = new Assassin(true);
            Pawn archerR = new Archer(true);
            Pawn axemanR = new Axeman(true);

            RedPawns.Add(defR);
            RedPawns.Add(magR);
            RedPawns.Add(kingR);
            RedPawns.Add(assassinR);
            RedPawns.Add(archerR);
            RedPawns.Add(axemanR);

            B[6, 5].PawnOnField = defR;
            B[8, 7].PawnOnField = magR;
            B[10, 5].PawnOnField = kingR;
            B[7, 9].PawnOnField = assassinR;
            B[8, 3].PawnOnField = archerR;
            B[7, 1].PawnOnField = axemanR;
        }
    }
}
