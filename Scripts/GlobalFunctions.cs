using Godot;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BulletHellGD.Scripts
{
    public static class GlobalFunctions
    {
        public static Color RandomColor()
        {
            Random Rand = new Random();
            return new Color(Rand.Next(0, 100) / 100f, Rand.Next(0, 100) / 100f, Rand.Next(0, 100) / 100f);
        }

        public static UInt16 RandomId()
        {
            Random Rand = new Random();
            return (UInt16)Rand.Next(0, UInt16.MaxValue);
        }
    }
}
