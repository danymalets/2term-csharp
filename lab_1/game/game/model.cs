using System;

namespace game2048
{
    public class Model
    {
        int[,] map;
        static Random random = new Random();
        int size;
        int score;

        public int get(int i, int j)
        {
            return map[i, j];
        }

        public Model(int size)
        {
            score = 0;
            this.size = size;
            map = new int[size, size];
            for (int i = 0; i < size; i++)
                for (int j = 0; j < size; j++)
                    map[i, j] = -1;
            AddRandomNumber();
            AddRandomNumber();
        }

        public void Show()
        {
            Console.Clear();
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    Console.SetCursorPosition(j * 5 + 3, i * 2 + 1);
                    int num = map[i, j];
                    if (num == -1) Console.Write("."); else Console.Write(num);
                }
            }
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("Score - " + score);
            Console.WriteLine();
        }

        void AddRandomNumber()
        {
            int x, y;
            do
            {
                x = random.Next(size);
                y = random.Next(size);
            } while (map[x, y] != -1);
            if (random.Next(0, 10) == 0) map[x, y] = 4;
            else map[x, y] = 2;
        }

        public bool IsWin()
        {
            for (int i = 0; i < size; i++)
                for (int j = 0; j < size; j++)
                    if (map[i, j] == 2048) return true;
            return false;
        }

        public bool IsGameOver()
        {
            for (int i = 0; i < size; i++)
                for (int j = 0; j < size; j++)
                    if (map[i, j] == -1) return false;
            for (int i = 0; i < size; i++)
                for (int j = 0; j < size - 1; j++)
                    if (map[i, j] == map[i, j + 1]) return false;
            for (int i = 0; i < size - 1; i++)
                for (int j = 0; j < size; j++)
                    if (map[i, j] == map[i + 1, j]) return false;
            return true;
        }

        public void Left()
        {
            bool Changed = false;
            for (int i = 0; i < size; i++)
            {
                int tj = -1;
                bool split = false;
                for (int j = 0; j < size; j++)
                {
                    if (map[i, j] != -1)
                    {
                        if (tj != -1 && map[i, j] == map[i, tj] && !split)
                        {
                            map[i, tj] *= 2;
                            score += map[i, tj];
                            split = true;
                        }
                        else
                        {
                            map[i, ++tj] = map[i, j];
                            split = false;
                        }
                        if (j > tj)
                        {
                            map[i, j] = -1;
                            Changed = true;
                        }
                    }
                }
            }
            if (Changed) AddRandomNumber();
        }

        public void Rigth()
        {
            bool Changed = false;
            for (int i = size - 1; i >= 0; i--)
            {
                int tj = size;
                bool split = false;
                for (int j = size - 1; j >= 0; j--)
                {
                    if (map[i, j] != -1)
                    {
                        if (tj != size && map[i, j] == map[i, tj] && !split)
                        {
                            map[i, tj] *= 2;
                            score += map[i, tj];
                            split = true;
                        }
                        else
                        {
                            map[i, --tj] = map[i, j];
                            split = false;
                        }
                        if (j < tj)
                        {
                            map[i, j] = -1;
                            Changed = true;
                        }
                    }
                }
            }
            if (Changed) AddRandomNumber();
        }

        public void Up()
        {
            bool Changed = false;
            for (int j = 0; j < size; j++)
            {
                int ti = -1;
                bool split = false;
                for (int i = 0; i < size; i++)
                {
                    if (map[i, j] != -1)
                    {
                        if (ti != -1 && map[i, j] == map[ti, j] && !split)
                        {
                            map[ti, j] *= 2;
                            score += map[ti, j];
                            split = true;
                        }
                        else
                        {
                            map[++ti, j] = map[i, j];
                            split = false;
                        }
                        if (i > ti)
                        {
                            map[i, j] = -1;
                            Changed = true;
                        }
                    }
                }
            }
            if (Changed) AddRandomNumber();
        }

        public void Down()
        {
            bool Changed = false;
            for (int j = size - 1; j >= 0; j--)
            {
                int ti = size;
                bool split = false;
                for (int i = size - 1; i >= 0; i--)
                {
                    if (map[i, j] != -1)
                    {
                        if (ti != size && map[i, j] == map[ti, j] && !split)
                        {
                            map[ti, j] *= 2;
                            score += map[ti, j];
                            split = true;
                        }
                        else
                        {
                            map[--ti, j] = map[i, j];
                            split = false;
                        }
                        if (i < ti)
                        {
                            map[i, j] = -1;
                            Changed = true;
                        }
                    }
                }
            }
            if (Changed) AddRandomNumber();
        }
    }
}
