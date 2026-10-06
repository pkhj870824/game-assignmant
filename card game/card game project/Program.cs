using System;
using System.Threading;

while(true)
{
    Console.WriteLine("=== 카드 짝 맞추기 게임 ===");
    Console.WriteLine("난이도를 선택해 주세요");
    Console.WriteLine("1. EASY");
    Console.WriteLine("2. NORMAL");
    Console.WriteLine("3. HARD");
    int array1 = 0;
    int array2 = 0;
    int arrayNum = 0;
    int level = int.Parse(Console.ReadLine());

    if (level == 1)
    {
        array1 = 2;
        array2 = 4;
        arrayNum = 8;
    }
    else if (level == 2)
    {
        array1 = 4;
        array2 = 4;
        arrayNum = 16;
    }
    else if (level == 3)
    {
        array1 = 4;
        array2 = 6;
        arrayNum = 24;
    }
    int [,] card = new int[array1,array2];

    Random random = new Random();

    int[] number = new int[arrayNum]; 

    for (int i =0; i< number.Length; i++)
    {
        number [i] = i/2 + 1;
    }

    for (int i =0; i< number.Length; i++)
    {
        int randomNum = random.Next(0, number.Length);
        int temp = number[i];
        number[i] = number[randomNum];
        number[randomNum] = temp;
    }

    int index = 0;

    for (int i = 0; i<array1; i++)
    {
        for (int j = 0; j<array2; j++)
        {
            card[i, j] = number[index];
            index++;
        }
    }

    int firstrow = 0;
    int firstcol = 0;
    bool[,] open = new bool[array1,array2];
    int count = 0;
    int tryCount = 0;
    int matchCount = 0;

    while (matchCount<(arrayNum/2))
    {
        Console.Clear();
        gametable(card, open);
        Console.WriteLine("행과열을 띄워서 입력해주세요");
        string[] select = Console.ReadLine().Split(" ");
        int row = int.Parse(select[0])-1;
        int col = int.Parse(select[1])-1;
        tryCount++;


 
        if (row >= array1 || col >= array2 || row < 0 || col < 0)
        {
            Console.WriteLine("잘못된 입력입니다 다시 입력해주세요.");
            Thread.Sleep(1500);
            continue;
        }

        if (open[row, col])
        {
            Console.WriteLine("중복된 입력입니다 다시 입력해주세요.");
            Thread.Sleep(1500);
            continue;
        }
        else
        {
            open[row, col] = true;
            if (count == 0)
            {
                firstrow = row;
                firstcol = col;
                count++;
            }
            else if (count == 1)
            {
                Console.Clear();
                gametable(card, open);
                if (card[firstrow, firstcol] == card[row, col])
                {
                    Console.WriteLine("맞았습니다");
                    Thread.Sleep(1500);
                    matchCount++;
                }
                else
                {
                    Console.WriteLine("틀렸습니다");
                    Thread.Sleep(1500);
                    open[firstrow, firstcol] = false;
                    open[row, col] = false;
                }
                count = 0;
            }
        }
    }
    Console.Clear();
    gametable(card, open);
    Console.WriteLine($"총 시도 횟수: {tryCount}");

    string retry;
    while (true)
    {
        Console.WriteLine("재시도 하시겠습니까? [y/n]");
        retry = Console.ReadLine();
        if (retry == "y" || retry == "n")
        {
            break;
        }
        Console.WriteLine("잘못된 입력입니다 y 혹은 n을 입력해 주세요");
    }

    if(retry == "y")
    {
        continue;
    }
    else if(retry == "n")
    {
        break;
    }

    void gametable(int[,] card, bool[,] open)
    {
        for (int i = 0; i < array1; i++)
        {
            for (int j = 0; j < array2; j++)
            {
                if (open[i, j])
                {
                    Console.Write($"{card[i, j]} ");
                }
                else
                {
                    Console.Write("* ");
                }
            }
            Console.WriteLine();
        }
    }
}



