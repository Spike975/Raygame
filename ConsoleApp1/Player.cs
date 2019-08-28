using Raylib;
using System;
using rl = Raylib.Raylib;

namespace ConsoleApp1
{
    struct IntVector2
    {
        public int x;
        public int y;
    }
    class Player
    {
        public int health = 6;
        public IntVector2 vect = new IntVector2();
        public Color MyColor = Color.DARKPURPLE;
        public static Texture2D p1;
        public static Texture2D p2;
        private Texture2D current;
        private Texture2D h1 = rl.LoadTexture("resources/platformPack_item017.png");
        private Texture2D h2 = rl.LoadTexture("resources/platformPack_item017.png");
        private Texture2D h3 = rl.LoadTexture("resources/platformPack_item017.png");
        private double time = 0f;
        private int speed = 0;
        private int variable = 0;
        public void RunUpdate()
        {
            if (rl.IsKeyDown(KeyboardKey.KEY_LEFT_SHIFT))
            {
                speed = 3;
            }
            else
            {
                speed = 1;
            }

            if (rl.IsKeyDown(KeyboardKey.KEY_S) || rl.IsKeyDown(KeyboardKey.KEY_DOWN))
            {
               vect.y+= speed;
            }
            if (rl.IsKeyDown(KeyboardKey.KEY_W) || rl.IsKeyDown(KeyboardKey.KEY_UP))
            {
                vect.y-= speed;
            }
            if (rl.IsKeyDown(KeyboardKey.KEY_D) || rl.IsKeyDown(KeyboardKey.KEY_RIGHT))
            {
                vect.x+= speed;
            }
            if (rl.IsKeyDown(KeyboardKey.KEY_A) || rl.IsKeyDown(KeyboardKey.KEY_LEFT))
            {
                vect.x-= speed;
            }
        }
        public void Draw()
        {
            if (vect.x > 800) { vect.x = -70; }
            if (vect.x < -70) { vect.x = 800; }
            if (vect.y > 450) { vect.y = -40; }
            if (vect.y < -40) { vect.y = 450; }
            if ((rl.GetTime() - time) > 0.5f)
            {
                if (variable ==0) {
                    current = p1;
                    variable++;
                    time = rl.GetTime();
                }
                else
                {
                    current = p2;
                    variable--;
                    time = rl.GetTime();
                }
            }
            rl.DrawTexture(current, vect.x, vect.y, Color.BROWN);
            if (!Program.done) {
                rl.DrawTexture(h1, -10,-5,Color.RED);
                rl.DrawTexture(h2, 30,-5,Color.RED);
                rl.DrawTexture(h3, 70,-5,Color.RED);
            }
            //Console.WriteLine($"{vect.x}, {vect.y}");
            //rl.DrawRectangleLines(vect.x, vect.y, 70, 48, Color.RAYWHITE);
            //rl.DrawRectangle(vect.x, vect.y, 20, 40, Color.DARKGREEN);
            //rl.DrawCircle(vect.x+10,vect.y-9, 8, Color.DARKGREEN);
        }
        public void hitDamage()
        {
            health--;
            if (health == 5)
            {
                h3 = rl.LoadTexture("resources/platformPack_item011.png");
            }
            if (health == 4)
            {
                h3 = rl.LoadTexture("resources/platformPack_item005.png");
            }
            if (health == 3)
            {
                h2 = rl.LoadTexture("resources/platformPack_item011.png");
            }
            if (health == 2)
            {
                h2 = rl.LoadTexture("resources/platformPack_item005.png");
            }
            if (health == 1)
            {
                h1 = rl.LoadTexture("resources/platformPack_item011.png");
            }
            if (health == 0)
            {
                h1 = rl.LoadTexture("resources/platformPack_item005.png");
            }
        }
    }
}
