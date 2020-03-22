using System;
namespace game2048
{
    public class Bot
    {
        int size;
        int[,] map;

        public Bot(int size)
        {
            this.size = size;
        }

        bool IsPossibleVertical()
        {
            for (int i = 0; i < size - 1; i++)
                for (int j = 0; j < size; j++)
                    if (map[i, j] == map[i + 1, j] && map[i, j] != -1) return true;
            return false;
        }

        bool IsPossibleGorizontal()
        {
            for (int i = 0; i < size; i++)
                for (int j = 0; j < size - 1; j++)
                    if (map[i, j] == map[i, j + 1] && map[i, j] != -1) return true;
            return false;
        }

        bool IsPossibleLeft()
        {
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size - 1; j++)
                {
                    if (map[i, j] == -1 && map[i, j + 1] != -1) return true;
                }
            }
            return IsPossibleGorizontal();
        }

        bool IsPossibleRigth()
        {
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size - 1; j++)
                {
                    if (map[i, j] != -1 && map[i, j + 1] == -1) return true;
                }
            }
            return IsPossibleGorizontal();
        }

        bool IsPossibleDown()
        {
            for (int i = 0; i < size - 1; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    if (map[i, j] != -1 && map[i + 1, j] == -1) return true;
                }
            }
            return IsPossibleVertical();
        }

        bool IsPossibleLine(int k)
        {
            for (int j = 0; j < size - 1; j++)
                if (map[k, j] == map[k, j + 1] && map[k, j] != -1) return true;
            return false;
        }

        bool IsFreeLine(int k)
        {
            for (int j = 0; j < size; j++)
            {
                if (map[k, j] == -1) return true;
            }
            return false;
        }

        bool NeedRigth(int k)
        {
            for (int j = 0; j < size - 1; j++)
            {
                if (map[k, j] != -1 && map[k, j + 1] == -1) return true;
            }
            return false;
        }

        bool NeedLeft(int k)
        {
            for (int j = 0; j < size - 1; j++)
            {
                if (map[k, j] == -1 && map[k, j + 1] != -1) return true;
            }
            return false;
        }

        int CntLeft(int i, int j)
        {
            int res = 0;
            for (int k = 0; k < j; k++)
            {
                if (map[i, k] != -1) res++;
            }
            return res;
        }

        int CntRigth(int i, int j)
        {
            int res = 0;
            for (int k = j + 1; k < size; k++)
            {
                if (map[i, k] != -1) res++;
            }
            return res;
        }

        public enum Dir
        {
            Left = 0,
            Rigth,
            Up,
            Down
        }

        public Dir Ask(Model model)
        {
            map = new int[size, size];
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    this.map[i, j] = model.get(i, j); 
                }
            }
            for (int i = size - 1; i >= 0; i--)
            {
                if (i != size - 1)
                {
                    for (int t = 2; t <= 1024; t *= 2)
                    {
                        for (int j = 0; j < size; j++)
                        {
                            if (map[i, j] == t)
                            {
                                for (int k = size - CntRigth(i, j) - 1; k >= CntLeft(i, j); k--)
                                {
                                    if (map[i, j] == map[i + 1, k])
                                    {
                                        if (j == k) return Dir.Down;
                                        else if (j < k) return Dir.Rigth;
                                        else return Dir.Left;
                                    }
                                }
                            }
                        }
                    }
                }
                if (i % 2 == 1)
                {

                    if (IsPossibleLine(i)) return Dir.Rigth;
                    if (IsFreeLine(i))
                    {
                        if (NeedRigth(i)) return Dir.Rigth;
                        if (IsPossibleDown()) return Dir.Down;
                        if (IsPossibleRigth()) return Dir.Rigth;
                        if (IsPossibleLeft()) return Dir.Left;
                        return Dir.Up;
                    }

                }
                else
                {
                    if (IsPossibleLine(i)) return Dir.Left;
                    if (IsFreeLine(i))
                    {
                        if (NeedLeft(i)) return Dir.Left;
                        if (IsPossibleDown()) return Dir.Down;
                        if (IsPossibleLeft()) return Dir.Left;
                        if (IsPossibleRigth()) return Dir.Rigth;
                        return Dir.Up;
                    }

                }
            }
            return Dir.Up;
        }

    }
}
