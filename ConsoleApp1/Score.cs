using System;
using System.IO;
using Raylib;
using rl = Raylib.Raylib;

namespace ConsoleApp1
{
    class Score
    {
        private static int x = 25;
        private static string[] _value = new string[10];
        public static int[] value = new int[10] { 0,0,0,0,0,0,0,0,0,0};
        public static void add(int _score)
        {
            if (_score>value[9])
            {
                value[9] = _score;
            }
            sorter();
        }
        public static void sorter()
        {
            Array.Sort(value);
            Array.Reverse(value);
        }
        public static void print()
        {
            rl.DrawText("Highscore:",5,5,20,Color.RAYWHITE);
            foreach(int i in value)
            {
                if (i != 0)
                {
                    rl.DrawText($"{i} seconds left", 5, x, 20,Color.RAYWHITE);
                    x += 20;
                }
            }
            x = 25;
        }
        public static void textFile()
        { 
            StreamWriter writer;
            writer =new StreamWriter ("test.txt");
            for(int i =0; i<value.Length;i++)
            {
                writer.Write(value[i]);
                if (i != (value.Length-1)) writer.Write(",");
            }
            writer.Close();
        }
        public static void start()
        {
            StreamReader reader = new StreamReader("test.txt");
            string _read = reader.ReadLine();
            _value = _read.Split(",");
            for(int i =0; i<_value.Length; i++)
            {
                int.TryParse(_value[i], out value[i]);
            }
            reader.Close();
        }
        public static void reset()
        {
            StreamWriter writer;
            writer = new StreamWriter("test.txt");
            for (int i = 0; i < value.Length; i++)
            {
                value[i] = 0;
                writer.Write("0");
                if (i != (value.Length - 1)) writer.Write(",");
            }
            writer.Close();
        }
    }
}
