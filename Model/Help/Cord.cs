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
        private int x;

        public int X
        {
            get
            {
                return x;
            }
            set
            {
                if (value < 0 || value > Arena.WIDTH)
                {
                    throw new IndexOutOfRangeException();
                }
                x = value;
            }
        }

        private int y;

        public int Y
        {
            get
            {
                return y;
            }
            set
            {
                if (value < 0 || value > Arena.HEIGHT)
                {
                    throw new IndexOutOfRangeException();
                }
                y = value;
            }
        }

        public Cord(int x, int y)
        {
            X = x;
            Y = y;
        }

        public override string ToString()
        {
            return $"[{x}, {y}]";
        }

        public override bool Equals(object obj)
        {
            if ((obj == null) || !this.GetType().Equals(obj.GetType()))
            {
                return false;
            }
            else
            {
                Cord p = (Cord)obj;
                return (X == p.X) && (y == p.Y);
            }
        }
    }
}
