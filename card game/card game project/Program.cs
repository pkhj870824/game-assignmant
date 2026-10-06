using System;
using System.Threading;

int [,] card = new int[4,4];

Random random = new Random();

int[] number = new int[16]; 

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

for (int i = 0; i<4; i++)
{
    for (int j = 0; j<4; j++)
    {
        card[i, j] = number[index];
        index++;
    }
}

int firstrow = 1;
int firstcol = 1;
bool[,] open = new bool[4,4];
int count = 0;
int tryCount = 0;
int matchCount = 0;

while (matchCount<8)
{
    Console.Clear();
    gametable(card, open);
    Console.WriteLine("행과열을 띄워서 입력해주세요");
    string[] select = Console.ReadLine().Split(" ");
    int row = int.Parse(select[0])-1;
    int col = int.Parse(select[1])-1;
    tryCount++;


 
    if (row >= 4 || col >= 4 || row < 0 || col < 0)
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

void gametable(int[,] card, bool[,] open) 
{
    for (int i = 0; i < 4; i++)
    {
        for (int j = 0; j < 4; j++)
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