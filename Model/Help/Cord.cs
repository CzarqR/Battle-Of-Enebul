using System;

namespace ProjectB.Model.Help
{
#pragma warning disable CS0659 // Type overrides Object.Equals(object o) but does not override Object.GetHashCode()
    public class Cord
#pragma warning restore CS0659 // Type overrides Object.Equals(object o) but does not override Object.GetHashCode()
    {
        public int X
        {
            get;
            set;
        }

        public int Y
        {
            get;
            set;
        }

        public Cord(int x, int y)
        {
            X = x;
            Y = y;
        }


        public Cord(Cord cord, int x = 0, int y = 0)
        {
            X = cord.X + x;
            Y = cord.Y + y;
        }


        public override string ToString()
        {
            return $"[{X}, {Y}]";
        }

        public override bool Equals(object obj)
        {
            if ((obj == null) || !GetType().Equals(obj.GetType()))
            {
                return false;
            }
            else
            {
                Cord p = (Cord)obj;
                return (X == p.X) && (Y == p.Y);
            }
        }
    }
}
