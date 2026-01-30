using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

class Program
{
    enum Direction { Up, Down, Left, Right }

    static int width = 30;
    static int height = 15;

    static List<(int x, int y)> snake = new()
    {
        (5,6),
        (6,6),
        (7,6)
    };

    static Direction direction = Direction.Right;

    static (int x, int y) food;
    static int score = 0;
    static Random rand = new Random();

    static void Main()
    {
        Console.CursorVisible = false;
        food = GenerateFood();

        while (true)
        {
            HandleInput();
            MoveSnake();

            if (HitWall() || HitSelf())
                break;

            if (snake[0] == food)
            {
                score++;
                snake.Add(snake[^1]);
                food = GenerateFood();
            }

            Draw();
            Thread.Sleep(120);
        }

        Console.Clear();
        Console.WriteLine("GAME OVER");
        Console.WriteLine($"Score: {score}");
        Console.ReadKey();
    }

    static void Draw()
    {
        Console.Clear();

        for (int x = 0; x < width; x++)
        {
            Console.SetCursorPosition(x, 0);
            Console.Write("X");
            Console.SetCursorPosition(x, height - 1);
            Console.Write("X");
        }

        for (int y = 0; y < height; y++)
        {
            Console.SetCursorPosition(0, y);
            Console.Write("X");
            Console.SetCursorPosition(width - 1, y);
            Console.Write("X");
        }

        Console.SetCursorPosition(food.x, food.y);
        Console.Write("O");

        foreach (var chunk in snake)
        {
            Console.SetCursorPosition(chunk.x, chunk.y);
            Console.Write("O");
        }

        Console.SetCursorPosition(0, height);
        Console.Write($"Score: {score}");
    }
    static void MoveSnake()
    {
        var head = snake[0];

        var newHead = direction switch
        {
            Direction.Up => (head.x, head.y - 1),
            Direction.Down => (head.x, head.y + 1),
            Direction.Left => (head.x - 1, head.y),
            Direction.Right => (head.x + 1, head.y),
            _ => head
        };

        snake.Insert(0, newHead);
        snake.RemoveAt(snake.Count - 1);
    }

    static void HandleInput()
    {
        if (!Console.KeyAvailable)
            return;

        var input = Console.ReadKey(true).Key;

        switch (input)
        {
            case ConsoleKey.UpArrow when direction != Direction.Down:
                direction = Direction.Up;
                break;
            case ConsoleKey.DownArrow when direction != Direction.Up:
                direction = Direction.Down;
                break;
            case ConsoleKey.LeftArrow when direction != Direction.Right:
                direction = Direction.Left;
                break;
            case ConsoleKey.RightArrow when direction != Direction.Left:
                direction = Direction.Right;
                break;
        }
    }

    static bool HitWall()
    {
        var head = snake[0];
        return head.x <= 0 || head.x >= width - 1 ||
               head.y <= 0 || head.y >= height - 1;
    }

    static bool HitSelf()
    {
        var head = snake[0];
        return snake.Skip(1).Contains(head);
    }

    static (int x, int y) GenerateFood()
    {
        (int x, int y) pos;
        do
        {
            pos = (rand.Next(1, width - 2), rand.Next(1, height - 2));
        }
        while (snake.Contains(pos));

        return pos;
    }
}
