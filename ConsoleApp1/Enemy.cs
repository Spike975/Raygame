using System;
using Raylib;
using rl = Raylib.Raylib;

namespace ConsoleApp1
{
    class Enemy
    {
        public int x = 0;
        public int y = 0;
        private float time = 0f;
        private Random rand = new Random();
        private Random _rand = new Random();
        private static Texture2D e1 = rl.LoadTexture("resources/spider.png");
        private static Texture2D e2 = rl.LoadTexture("resources/spider_walk1.png");
        private static Texture2D e3 = rl.LoadTexture("resources/spider_walk2.png");
        private static Texture2D current;
        public void Draw()
        {

        }
        public void Move()
        {
            if (rl.GetTime()-time>5)
            {
                x = (rand.Next(-1, 1) * _rand.Next(1, 3));
                y = (rand.Next(-1, 1) * _rand.Next(1, 3));
                time += 5;
            }
        }
    }
}
