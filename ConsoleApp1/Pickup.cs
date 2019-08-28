using System;
using System.Collections.Generic;
using System.Text;
using Raylib;
using rl = Raylib.Raylib;

namespace ConsoleApp1
{
    class Pickup
    {
        public IntVector2 pick;
        public Color fav = Color.GOLD;
        public bool Enabled = true;
        public static Texture2D myTexture;
        public void Draw()
        {
            if (Enabled) { rl.DrawTexture(Pickup.myTexture, pick.x,pick.y,Color.BLUE);}
        }
    }
}
