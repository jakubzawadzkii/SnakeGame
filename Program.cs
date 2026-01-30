using System;
using System.Collections.Generic;
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

    static void Main()
    {
        Console.CursorVisible = false;

        while (true)
        {
            HandleInput();
            MoveSnake();
            Draw();
            Thread.Sleep(120);
        }
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

        foreach (var chunk in snake)
        {
            Console.SetCursorPosition(chunk.x, chunk.y);
            Console.Write("O");
        }
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
            case ConsoleKey.UpArrow:
                direction = Direction.Up;
                break;
            case ConsoleKey.DownArrow:
                direction = Direction.Down;
                break;
            case ConsoleKey.LeftArrow:
                direction = Direction.Left;
                break;
            case ConsoleKey.RightArrow:
                direction = Direction.Right;
                break;
        }
    }
}
