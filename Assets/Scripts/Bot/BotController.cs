using UnityEngine;

public class BotController
{
    public BotController()
    {

    }

    private void turn(bool botTurn, GameModel model, int x, int y)
    {
        if (botTurn)
        {
            model.test = 1;
            model.Symbol.text = "X";
            model.Btn.enabled = false;
        }
        else
        {
            model.test = 2;
            model.Symbol.text = "O";
            model.Btn.enabled = false;
        }
    }

    public int[] makeTurn(int x, int y, GameModel[,] model, int size, bool botTurn)
    {
        int value = model[x, y].test;
        int checkwin = 0;
        int[] indexes = new int[] { 1, 1 };

        for (int j = 0; j < size; j++)
        {
            if (model[x, j].test == value)
            {
                checkwin++;
            }
            else
            {
                checkwin = 0;
            }

            if (checkwin == 2 || checkwin == 4)
            {
                if (j + 1 < size)
                {
                    if (model[x, j + 1].test == 0)
                    {
                        turn(botTurn, model[x, j + 1], x, j + 1);
                        indexes = new int[] { x, j + 1 };
                        return indexes;
                    }
                }
            }
        }

        checkwin = 0;
        for (int j = size - 1; j >= 0; j--)
        {
            if (model[x, j].test == value)
            {
                checkwin++;
            }
            else
            {
                checkwin = 0;
            }

            if (checkwin == 2 || checkwin == 4)
            {
                if (j - 1 >= 0)
                {
                    if (model[x, j - 1].test == 0)
                    {
                        turn(botTurn, model[x, j - 1], x, j - 1);
                        indexes = new int[] { x, j - 1 };
                        return indexes;
                    }
                }
            }
        }

        checkwin = 0;
        for (int j = 0; j < size; j++)
        {
            if (model[j, y].test == value)
            {
                checkwin++;
            }
            else
            {
                checkwin = 0;
            }

            if (checkwin == 2 || checkwin == 4)
            {
                if (j + 1 < size)
                {
                    if (model[j + 1, y].test == 0)
                    {
                        turn(botTurn, model[j + 1, y], j + 1, y);
                        indexes = new int[] { j + 1, y };
                        return indexes;
                    }
                }
            }
        }

        checkwin = 0;
        for (int j = size - 1; j >= 0; j--)
        {
            if (model[j, y].test == value)
            {
                checkwin++;
            }
            else
            {
                checkwin = 0;
            }

            if (checkwin == 2 || checkwin == 4)
            {
                if (j - 1 >= 0)
                {
                    if (model[j - 1, y].test == 0)
                    {
                        turn(botTurn, model[j - 1, y], j - 1, y);
                        indexes = new int[] { j - 1, y };
                        return indexes;
                    }
                }
            }
        }
        int row = x;
        int col = y;

        int g = y;

        for (int i = x; i >= 0 && g < size; i--, g++)
        {
            row = i;
            col = g;
        }

        checkwin = 0;
        g = col;
        for (int i = row; i < size && g >= 0; i++, g--)
        {
            if (model[i, g].test == value)
            {
                checkwin++;
            }
            else
            {
                checkwin = 0;
            }

            if (checkwin == 2 || checkwin == 4)
            {
                if(i + 1 < size && g - 1 >= 0)
                {
                    if (model[i + 1, g - 1].test == 0)
                    {
                        turn(botTurn, model[i + 1, g - 1], i + 1, g - 1);
                        indexes = new int[] { i + 1, g - 1 };
                        return indexes;
                    }
                }
            }
            col++;
        }


        if (x > y)
        {
            row = x - y;
            col = 0;
        }
        else if (x < y)
        {
            col = y - x;
            row = 0;
        }
        else
        {
            row = 0;
            col = 0;
        }
        checkwin = 0;
        for (int i = row; i < size && col < size; i++, col++)
        {
            if (model[i, col].test == value)
            {
                checkwin++;
            }
            else
            {
                checkwin = 0;
            }

            if (checkwin == 2 || checkwin == 4)
            {
                if (i + 1 < size && col + 1 < size)
                {
                    if (model[i + 1, col + 1].test == 0)
                    {
                        turn(botTurn, model[i + 1, col + 1], i + 1, col + 1);
                        indexes = new int[] { i + 1, col + 1 };
                        return indexes;
                    }
                }
            }
        }

        int a = 0;

        do
        {
            int indx = Random.Range(0, 3);
            int indy = Random.Range(0, 3);
            GameModel indModel = model[indx, indy];
            a = indModel.test;
            indexes = new int[] { indx, indy };
        } while (a != 0);

        turn(botTurn, model[indexes[0], indexes[1]], indexes[0], indexes[1]);
        return indexes;
    }
}
