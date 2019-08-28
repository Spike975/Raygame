using System;
using Raylib;
using rl = Raylib.Raylib;

namespace ConsoleApp1
{
    static class Program
    {
        private static bool trial = true;
        public static bool done = false;
        public static bool CheckCollision(Player _pl, Pickup _pu,int val)
        {
                        //rl.DrawRectangleLines(pickup.pick.x+14,pickup.pick.y+17,38,30,Color.BLACK);
            bool rtn;
            Rectangle pickup = new Rectangle(_pu.pick.x+14, _pu.pick.y+17, 38, 30);
            Rectangle myPlayer = new Rectangle(_pl.vect.x, _pl.vect.y, 70, 48);
            rtn = rl.CheckCollisionRecs(pickup,myPlayer);
            if (rtn && val ==0)
            {
                _pu.Enabled = false;
            }
            return rtn;
        }
        public static bool CheckCollision(Pickup _pl, Pickup _pu)
        {
            bool rtn;
            Rectangle pickup = new Rectangle(_pu.pick.x + 14, _pu.pick.y + 17, 38, 30);
            Rectangle myPlayer = new Rectangle(_pl.pick.x + 14, _pl.pick.y + 17, 38, 30);
            rtn = rl.CheckCollisionRecs(pickup, myPlayer);
            return rtn;
        }
        public static bool CheckCollision(Player _pl, Enemy _pu)
        {
            bool rtn;
            Rectangle pickup = new Rectangle(_pu.x + 14, _pu.y + 17, 38, 30);
            Rectangle myPlayer = new Rectangle(_pl.vect.x + 14, _pl.vect.y + 17, 38, 30);
            rtn = rl.CheckCollisionRecs(pickup, myPlayer);
            return rtn;
        }
        public static int Main()
        {
            // Initialization
            //--------------------------------------------------------------------------------------
                int screenWidth = 800;
                int screenHeight = 450;
                int time = 20;
                int totalTime = time;
                int score = 0;
                Random rand = new Random();
                rl.InitWindow(screenWidth, screenHeight, "raylib [core] example - basic window");
                Pickup.myTexture = rl.LoadTexture("resources/platformPack_item007.png");
                Player.p1 = rl.LoadTexture("resources/bat.png");
                Player.p2 = rl.LoadTexture("resources/bat_fly.png");

            Score.start();
                //--------------------------------------------------------------------------------------

            while (trial) {

                Player MyPlayer = new Player();
                MyPlayer.vect.x = 50;
                MyPlayer.vect.y = 50;

                Enemy MyEnemy = new Enemy();

                rl.SetTargetFPS(60);

                Pickup[] pickups = new Pickup[10];
                for (int i = 0; i < 10; i++)
                {
                    if (pickups[i] == null) {
                        pickups[i] = new Pickup();
                    }
                    pickups[i].fav = Color.DARKGRAY;
                    pickups[i].pick.x = rand.Next(10, 750);
                    pickups[i].pick.y = rand.Next(10, 400);

                    for (int x = 0; x < i; x++) {
                        while (CheckCollision(pickups[x], pickups[i]) || CheckCollision(MyPlayer, pickups[i], 1)) {
                            Console.WriteLine($"Collision at {pickups[i].pick.x}, {pickups[i].pick.y}");
                            pickups[i].pick.x = rand.Next(10, 750);
                            pickups[i].pick.y = rand.Next(10, 400);
                            Console.WriteLine($"Moved to {pickups[i].pick.x}, {pickups[i].pick.y}");
                        }
                    }
                }


                // Main game loop
                while (!rl.WindowShouldClose())    // Detect window close button or ESC key
                {
                    // Update
                    rl.BeginDrawing(); 
                    rl.ClearBackground(MyPlayer.MyColor);
                    //----------------------------------------------------------------------------------
                    // TODO: Update your variables here
                    foreach (Pickup pickup in pickups)
                    {
                        if (pickup.Enabled)
                        {
                            pickup.Draw();
                            score += CheckCollision(MyPlayer, pickup, 0) ? 1 : 0;

                        }
                    }
                    if (rl.IsKeyPressed(KeyboardKey.KEY_R)&&rl.IsKeyDown(KeyboardKey.KEY_LEFT_CONTROL))
                    {
                        Score.reset();
                    }
                    //----------------------------------------------------------------------------------

                    // Draw
                    //----------------------------------------------------------------------------------

                    if (score != 10 && totalTime > 0&& MyPlayer.health !=0)
                    {
                        MyPlayer.Draw();
                        MyPlayer.RunUpdate();
                        totalTime = (int)(time - rl.GetTime());
                    }
                    else if (score == 10)
                    {
                        rl.DrawText("Congrats!", 350, 190, 20, Color.RAYWHITE);
                        rl.DrawText("Press Enter to play again. Press Escape to close.", 140, 210, 20, Color.RAYWHITE);
                        if (!done) {
                            Score.add(totalTime);
                            done = true;
                        }
                        Score.print();
                        if (rl.IsKeyPressed(KeyboardKey.KEY_ENTER))
                        {
                            time = (int)rl.GetTime()+20;
                            totalTime = time;
                            score = 0;
                            for (int k = 0; k < pickups.Length; k++)
                            {
                                pickups[k] = null;
                            }
                            done = false;
                            rl.EndDrawing();
                            break;
                        }
                    }
                    else if (MyPlayer.health == 0)
                    {
                        rl.DrawText("You have no move lives.", 270, 190, 20, Color.RAYWHITE);
                        rl.DrawText("Press Enter to play again. Press Escape to close.", 140, 210, 20, Color.RAYWHITE);
                        if (rl.IsKeyPressed(KeyboardKey.KEY_ENTER))
                        {
                            time = (int)(rl.GetTime() + 20);
                            totalTime = time;
                            score = 0;
                            for (int k = 0; k < pickups.Length; k++)
                            {
                                pickups[k] = null;
                            }
                            rl.EndDrawing();
                            Score.print();
                            break;
                        }
                    }
                    else if (totalTime <= 0)
                    {
                        rl.DrawText("You've ran out of time.", 300, 190, 20, Color.RAYWHITE);
                        rl.DrawText("Press Enter to play again. Press Escape to close.", 140, 210, 20 ,Color.RAYWHITE);
                        if (rl.IsKeyPressed(KeyboardKey.KEY_ENTER))
                        {
                            time = (int)(rl.GetTime()+20);
                            totalTime = time;
                            score = 0;
                            for(int k = 0; k <pickups.Length; k++)
                            {
                                pickups[k] = null;
                            }
                            rl.EndDrawing();
                            Score.print();
                            break;
                        }
                    }

                    rl.DrawText(score.ToString(), 775, 20, 20, Color.LIGHTGRAY);
                    rl.DrawText(totalTime.ToString(), 385, 20, 20, Color.LIGHTGRAY);
                    //----------------------------------------------------------------------------------
                    rl.EndDrawing();
                }
                if (rl.WindowShouldClose())
                {
                    trial = false;
                }

                // De-Initialization
                //--------------------------------------------------------------------------------------
                                         //--------------------------------------------------------------------------------------

            }
            
            rl.CloseWindow();        // Close window and OpenGL context

            Score.textFile();
            return 0;
            
        }
    }
}