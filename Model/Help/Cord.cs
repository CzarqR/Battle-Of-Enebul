using ProjectB.Model.Board;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectB.Model.Help
{
    public class Cord
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

        public override int GetHashCode()
        {
            throw new NotImplementedException();
        }
    }
}
