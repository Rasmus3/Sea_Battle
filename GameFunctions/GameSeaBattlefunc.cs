using System;

namespace GameFunctions
{
    public class GameSeaBattlefunc
    {
        protected const int four = 1;
        protected const int three = 2;
        protected const int two = 3;
        protected const int one = 4;
        public static int Indent = 2;
        public int[,] Field1 = new int[10, 10]; //0 - пустая клетка, 1 - корабль, 2 - попадание по кораблю, 3 - промах
        public static readonly string[] str1 = {"а", "б", "в", "г", "д", "е", "ж", "з", "и", "к"};
        public static readonly string[] str2 = {"1", "2", "3", "4", "5", "6", "7", "8", "9", "10"};
        protected static int[,] RemoteField = new int[10, 10];
        protected static int[,] LocalField = new int[10, 10];
        
        public void Output(int[,] Field)
        {
            if (Indent > 20)
            {
                Indent = 2;
                Console.Clear();
            }
            for (int i = 0; i < 10; i++)
            { // Отрисовываем заголовок игрового поля (а, б, в, г ....)
                Console.SetCursorPosition(2 * i + 3, 0);
                Console.Write(str1[i]);
            }
            for (int i = 0; i < 10; i++)
            { // Отрисовываем столбиком поля вида 1| 2|...
                Console.SetCursorPosition(0, i + 1);
                Console.Write(str2[i]);
                Console.SetCursorPosition(2, i + 1);
                Console.Write("| ");
                for (int j = 0; j < 10; j++)
                {
                    Console.SetCursorPosition(2 * j + 3, i + 1); //Заполняем игровое поле
                    Part(LocalField[i, j]);
                }
            } for (int i = 0; i < 10; i++) // Отрисовываем заголовок игрового поля противника (а, б, в, г ....)
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
    }
}