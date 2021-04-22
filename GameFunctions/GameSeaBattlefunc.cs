using System;

namespace GameFunctions
{
    public class GameSeaBattlefunc
    {
        protected const int four = 1; // количество 4 палубных кораблей
        protected const int three = 2; // количество 3 палубных кораблей
        protected const int two = 3; // количество 2 палубных кораблей
        protected const int one = 4; // количество 1 палубных кораблей
        public static int Indent = 2;
        public int[,] Field1 = new int[10, 10]; //0 - пустая клетка, 1 - корабль, 2 - попадание по кораблю, 3 - промах
        public static readonly string[] str1 = {"а", "б", "в", "г", "д", "е", "ж", "з", "и", "к"};
        public static readonly string[] str2 = {"1", "2", "3", "4", "5", "6", "7", "8", "9", "10"};
        protected static int[,] RemoteField = new int[10, 10];
        protected static int[,] LocalField = new int[10, 10];
        public int Number = 0;
        public int Step = new int();
        protected int[] Letter = new int[101];
        protected int[] Index = new int[101];
        public int Points = 0;


        public void Output(int[,] Field)
        {
            if (Indent > 20)
            {
                Indent = 2;
                Console.Clear();
            }

            for (int i = 0; i < 10; i++)
            {
                // Отрисовываем заголовок игрового поля (а, б, в, г ....)
                Console.SetCursorPosition(2 * i + 3, 0);
                Console.Write(str1[i]);
            }

            for (int i = 0; i < 10; i++)
            {
                // Отрисовываем столбиком поля вида 1| 2|...
                Console.SetCursorPosition(0, i + 1);
                Console.Write(str2[i]);
                Console.SetCursorPosition(2, i + 1);
                Console.Write("| ");
                for (int j = 0; j < 10; j++)
                {
                    Console.SetCursorPosition(2 * j + 3, i + 1); //Заполняем игровое поле
                    Part(LocalField[i, j]);
                }
            }

            for (int i = 0; i < 10; i++) // Отрисовываем заголовок игрового поля противника (а, б, в, г ....)
            {
                Console.SetCursorPosition(2 * i + 3, 13);
                Console.Write(str1[i]);
            }

            for (int i = 0; i < 10; i++) // Отрисовываем столбиком поля вида 1| 2|... в поле противника
            {
                Console.SetCursorPosition(0, i + 14);
                Console.Write(str2[i]);
                Console.SetCursorPosition(2, i + 14);
                Console.Write("| ");
                for (int j = 0; j < 10; j++) //Заполняем игровое поле (значения из массива 10Х10)
                {
                    Console.SetCursorPosition(2 * j + 3, i + 14);
                    Part(Field[i, j]);
                }
            }
        }

        public void Part(int a) //Модели кораблей, пустых полей и попадания 
        {
            switch (a)
            {
                case 0:
                    Console.Write('+'); // Пустое поле
                    break;
                case 1:
                    Console.Write('■'); // Корабль
                    break;
                case 2:
                    Console.Write('X'); //Попадание по кораблю
                    break;
                case 3:
                    Console.Write('O'); // Промах
                    break;
            }
        }

        protected void Stroke(int[,] Field, int i, int j) //Есть ли попападание?
        {
            int Long = 1;
            int x = j;
            int y = i;
            for (int k = 1; k < 4; k++)
            {
                try
                {
                    if (Field[i - k, j] == 2)
                    {
                        Long++;
                        y--;
                    }

                    if (Field[i - k, j] == 1)
                    {
                        return;
                    }

                    if (Field[i - k, j] == 0 || Field[i - k, j] == 3)
                    {
                        break;
                    }
                }
                catch (IndexOutOfRangeException)
                {
                    break;
                }
            }

            for (int k = 1; k < 4; k++)
            {
                try
                {
                    if (Field[i + k, j] == 2)
                    {
                        Long++;
                    }

                    if (Field[i + k, j] == 1)
                    {
                        return;
                    }

                    if (Field[i + k, j] == 0 || Field[i + k, j] == 3)
                    {
                        break;
                    }
                }
                catch (IndexOutOfRangeException)
                {
                    break;
                }
            }

            if (Long > 1)
            {
                for (int k = y - 1; k < y + Long + 1 && k < 10; k++)
                {
                    if (k < 0)
                    {
                        k++;
                    }

                    for (int l = x - 1; l < x + 2 && l < 10; l++)
                    {
                        if (l < 0)
                        {
                            l++;
                        }

                        if (Field[k, l] != 2)
                        {
                            Field[k, l] = 3;
                            Field1[k, l] = 3;
                        }
                    }
                }

                return;
            }

            for (int k = 1; k < 4; k++)
            {
                try
                {
                    if (Field[i, j - k] == 2)
                    {
                        Long++;
                        x--;
                    }

                    if (Field[i, j - k] == 1)
                    {
                        return;
                    }

                    if (Field[i, j - k] == 0 || Field[i, j - k] == 3)
                    {
                        break;
                    }
                }
                catch (IndexOutOfRangeException)
                {
                    break;
                }
            }

            for (int k = 1; k < 4; k++)
            {
                try
                {
                    if (Field[i, j + k] == 2)
                    {
                        Long++;
                    }

                    if (Field[i, j + k] == 1)
                    {
                        return;
                    }

                    if (Field[i, j + k] == 0 || Field[i, j + k] == 3)
                    {
                        break;
                    }
                }
                catch (IndexOutOfRangeException)
                {
                    break;
                }
            }

            if (Long > 1)
            {
                for (int l = y - 1; l < y + 2 && l < 10; l++)
                {
                    for (int k = x - 1; k < x + Long + 1 && k < 10; k++)
                    {
                        if (k < 0)
                        {
                            k++;
                        }

                        if (l < 0)
                        {
                            l++;
                        }

                        if (Field[l, k] != 2)
                        {
                            Field[l, k] = 3;
                            Field1[l, k] = 3;
                        }
                    }
                }

                return;
            }

            if (Long == 1)
            {
                for (int k = y - 1; k < y + 2 && k < 10; k++)
                {
                    if (k < 0)
                    {
                        k = 0;
                    }

                    for (int l = x - 1; l < x + 2 && l < 10; l++)
                    {
                        if (l < 0)
                        {
                            l = 0;
                        }

                        if (Field[k, l] != 2)
                        {
                            Field[k, l] = 3;
                            Field1[k, l] = 3;
                        }
                    }
                }
            }
        }
    }

    public class User : GameSeaBattlefunc
    {
        public User()
        {
            // "Случайная расстановка кораблей?"
            Console.Clear();
            Number = 0;
            Four();
            while (Number < three)
            {
                Three();
            }

            Number = 0;
            while (Number < two)
            {
                Two();
            }

            Number = 0;
            while (Number < one)
            {
                One();
            }
        }

        public void Strike() // GAME PLAY Игры
        {
            if (Win())
            {
                return;
            }

            Console.SetCursorPosition(30, Indent++);
            Console.WriteLine("Выстрел №: " + ++Step);
            Boolean letter = true;
            while (letter)
            {
                Console.SetCursorPosition(30, Indent++);
                Console.Write("Ваш выстрел: ");
                switch (Console.Read()) //Парсим ввод пользователя наприимер А1 (пока парсим "а")
                {
                    case 'а':
                        Letter[Step] = 0;
                        letter = false;
                        break;
                    case 'б':
                        Letter[Step] = 1;
                        letter = false;
                        break;
                    case 'в':
                        Letter[Step] = 2;
                        letter = false;
                        break;
                    case 'г':
                        Letter[Step] = 3;
                        letter = false;
                        break;
                    case 'д':
                        Letter[Step] = 4;
                        letter = false;
                        break;
                    case 'е':
                        Letter[Step] = 5;
                        letter = false;
                        break;
                    case 'ж':
                        Letter[Step] = 6;
                        letter = false;
                        break;
                    case 'з':
                        Letter[Step] = 7;
                        letter = false;
                        break;
                    case 'и':
                        Letter[Step] = 8;
                        letter = false;
                        break;
                    case 'к':
                        Letter[Step] = 9;
                        letter = false;
                        break;
                }
            }

            Index[Step] = Convert.ToInt32(Console.ReadLine()) - 1; // Парсим индекс в вводе А1 (то есть "1")
            if (Hit(Index[Step], Letter[Step])) // Стреляем
            {
                Points++;
                Strike();
            }
        } // Игра

        public bool Hit(int i, int j)
        {
            if (RemoteField[i, j] == 0)
            {
                RemoteField[i, j] = 3;
                Field1[i, j] = 3;
                Output(Field1);
                Console.SetCursorPosition(30, 0);
                Console.Write("Промах!   ");
                return false;
            }

            if (RemoteField[i, j] == 1)
            {
                RemoteField[i, j] = 2;
                Field1[i, j] = 2;
                Stroke(RemoteField, i, j);
                Output(Field1);
                Console.SetCursorPosition(30, 0);
                Console.Write("Попадание!");
                return true;
            }

            Console.SetCursorPosition(30, 0);
            Console.Write("Нельзя стрелять в эту клетку");
            Console.SetCursorPosition(30, 4);
            Console.WriteLine();
            Step--;
            return true;
        }

        public bool Win()
        {
            if (Points == 20)
            {
                Console.SetCursorPosition(10, 0);
                Console.Write("Вы победили!");
                return true;
            }

            return false;
        }

        private void Four()
        {
            var random = new Random();
            int x = random.Next(10);
            int y = random.Next(10);
            if (x > 5)
            {
                y = random.Next(5);
                for (int i = y; i < y + 4; i++)
                {
                    LocalField[i, x] = 1;
                }

                return;
            }

            if (y > 5)
            {
                x = random.Next(5);
                for (int j = x; j < x + 4; j++)
                {
                    LocalField[y, j] = 1;
                }

                return;
            }

            int k = random.Next(1);
            if (k == 0)
            {
                for (int i = y; i < y + 4; i++)
                {
                    LocalField[i, x] = 1;
                }
            }
            else
            {
                for (int j = x; j < x + 4; j++)
                {
                    LocalField[y, j] = 1;
                }
            }
        } //Кораблики

        private void Three()
        {
            var random = new Random();
            var x = random.Next(10);
            var y = random.Next(10);
            if (y > 6)
            {
                x = random.Next(7);
                for (int i = y - 1; i < y + 2; i++)
                {
                    if (i < 0)
                    {
                        i++;
                    }

                    if (i > 9)
                    {
                        break;
                    }

                    for (int j = x - 1; j < x + 4; j++)
                    {
                        if (j < 0)
                        {
                            j++;
                        }

                        if (j > 9)
                        {
                            break;
                        }

                        if (LocalField[i, j] != 0)
                        {
                            return;
                        }
                    }
                }

                for (int j = x; j < x + 3; j++)
                {
                    LocalField[y, j] = 1;
                }

                Number++;
                return;
            }

            if (x > 6)
            {
                y = random.Next(7);
                for (int i = y - 1; i < y + 4; i++)
                {
                    if (i < 0)
                    {
                        i++;
                    }

                    if (i > 9)
                    {
                        break;
                    }

                    for (int j = x - 1; j < x + 2; j++)
                    {
                        if (j < 0)
                        {
                            j++;
                        }

                        if (j > 9)
                        {
                            break;
                        }

                        {
                            if (LocalField[i, j] != 0)
                            {
                                return;
                            }
                        }
                    }
                }

                for (int i = y; i < y + 3; i++)
                {
                    LocalField[i, x] = 1;
                }

                Number++;
                return;
            }

            int k = random.Next(1);
            if (k == 0)
            {
                for (int i = y - 1; i < y + 4; i++)
                {
                    if (i < 0)
                    {
                        i++;
                    }

                    if (i > 9)
                    {
                        break;
                    }

                    for (int j = x - 1; j < x + 2; j++)
                    {
                        if (j < 0)
                        {
                            j++;
                        }

                        if (j > 9)
                        {
                            break;
                        }

                        if (LocalField[i, j] != 0)
                        {
                            return;
                        }
                    }
                }

                for (int i = y; i < y + 3; i++)
                {
                    LocalField[i, x] = 1;
                }

                Number++;
            }
            else
            {
                for (int i = y - 1; i < y + 2; i++)
                {
                    if (i < 0)
                    {
                        i++;
                    }

                    if (i > 9)
                    {
                        break;
                    }

                    for (int j = x - 1; j < x + 4; j++)
                    {
                        if (j < 0)
                        {
                            j = 0;
                        }

                        if (j > 9)
                        {
                            break;
                        }

                        if (LocalField[i, j] != 0)
                        {
                            return;
                        }
                    }
                }

                for (int j = x; j < x + 3; j++)
                {
                    LocalField[y, j] = 1;
                }

                Number++;
            }
        }

        private void Two()
        {
            var random = new Random();
            var x = random.Next(10);
            var y = random.Next(10);
            if (y > 7)
            {
                x = random.Next(8);
                for (int i = y - 1; i < y + 2; i++)
                {
                    if (i < 0)
                    {
                        i++;
                    }

                    if (i > 9)
                    {
                        break;
                    }

                    for (int j = x - 1; j < x + 3; j++)
                    {
                        if (j < 0)
                        {
                            j++;
                        }

                        if (j > 9)
                        {
                            break;
                        }

                        if (LocalField[i, j] != 0)
                        {
                            return;
                        }
                    }
                }

                for (int j = x; j < x + 2; j++)
                {
                    LocalField[y, j] = 1;
                }

                Number++;
                return;
            }

            if (x > 7)
            {
                y = random.Next(8);
                for (int i = y - 1; i < y + 3; i++)
                {
                    if (i < 0)
                    {
                        i++;
                    }

                    if (i > 9)
                    {
                        break;
                    }

                    for (int j = x - 1; j < x + 2; j++)
                    {
                        if (j < 0)
                        {
                            j++;
                        }

                        if (j > 9)
                        {
                            break;
                        }

                        if (LocalField[i, j] != 0)
                        {
                            return;
                        }
                    }
                }

                for (int i = y; i < y + 2; i++)
                {
                    LocalField[i, x] = 1;
                }

                Number++;
                return;
            }

            int k = random.Next(1);
            if (k == 0)
            {
                for (int i = y - 1; i < y + 3; i++)
                {
                    if (i < 0)
                    {
                        i++;
                    }

                    if (i > 9)
                    {
                        break;
                    }

                    for (int j = x - 1; j < x + 2; j++)
                    {
                        if (j < 0)
                        {
                            j++;
                        }

                        if (j > 9)
                        {
                            break;
                        }

                        if (LocalField[i, j] != 0)
                        {
                            return;
                        }
                    }
                }

                for (int i = y; i < y + 2; i++)
                {
                    LocalField[i, x] = 1;
                }

                Number++;
            }
            else
            {
                for (int i = y - 1; i < y + 2; i++)
                {
                    if (i < 0)
                    {
                        i++;
                    }

                    if (i > 9)
                    {
                        break;
                    }

                    for (int j = x - 1; j < x + 3; j++)
                    {
                        if (j < 0)
                        {
                            j++;
                        }

                        if (j > 9)
                        {
                            break;
                        }

                        if (LocalField[i, j] != 0)
                        {
                            return;
                        }
                    }
                }

                for (int j = x; j < x + 2; j++)
                {
                    LocalField[y, j] = 1;
                }

                Number++;
            }
        }

        private void One()
        {
            var random = new Random();
            var x = random.Next(10);
            var y = random.Next(10);
            for (int i = y - 1; i < y + 2; i++)
            {
                if (i < 0)
                {
                    i++;
                }

                if (i > 9)
                {
                    break;
                }

                for (int j = x - 1; j < x + 2; j++)
                {
                    if (j < 0)
                    {
                        j++;
                    }

                    if (j > 9)
                    {
                        break;
                    }

                    if (LocalField[i, j] != 0)
                    {
                        return;
                    }
                }
            }

            LocalField[y, x] = 1;
            Number++;
        }
    }
}