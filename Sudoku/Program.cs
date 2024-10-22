using System;

class Program
{
    static void Main(string[] args)
    {
        SudokuGame game = new SudokuGame();
        Console.WriteLine("Welcome to the Sudoku Game!");
        Console.WriteLine("Use row, column and number to place a number (Example: 1 1 5).");
        Console.WriteLine("Enter 'q' to quit.");

        while (!game.IsSolved())
        {
            game.DisplayBoard();

            // Input format: row, col, num
            Console.Write("Enter row, column, and number (1-9) separated by spaces: ");
            string input = Console.ReadLine();

            if (input.ToLower() == "q")
            {
                Console.WriteLine("Thanks for playing!");
                break;
            }

            try
            {
                string[] parts = input.Split(' ');
                int row = int.Parse(parts[0]) - 1; // -1 to convert to 0-based index
                int col = int.Parse(parts[1]) - 1;
                int num = int.Parse(parts[2]);

                if (game.PlaceNumber(row, col, num))
                {
                    Console.WriteLine("Number placed successfully!");
                }
                else
                {
                    Console.WriteLine("Invalid move! Try again.");
                }
            }
            catch
            {
                Console.WriteLine("Invalid input! Please enter row, column, and number (e.g., 1 1 5).");
            }
        }

        if (game.IsSolved())
        {
            Console.WriteLine("Congratulations! You've solved the Sudoku!");
            game.DisplayBoard();
        }
    }
}

public class SudokuGame
{
    private int[,] board;
    private const int Size = 9; // 9x9 Sudoku grid
    
    public SudokuGame()
    {
        board = new int[Size, Size];
        GeneratePuzzle();
    }

    // Method to generate a basic Sudoku puzzle (for simplicity, predefined)
    private void GeneratePuzzle()
    {
        // Simple hardcoded puzzle for demonstration (0 represents empty spaces)
        board = new int[,] {
            {5, 3, 0, 0, 7, 0, 0, 0, 0},
            {6, 0, 0, 1, 9, 5, 0, 0, 0},
            {0, 9, 8, 0, 0, 0, 0, 6, 0},
            {8, 0, 0, 0, 6, 0, 0, 0, 3},
            {4, 0, 0, 8, 0, 3, 0, 0, 1},
            {7, 0, 0, 0, 2, 0, 0, 0, 6},
            {0, 6, 0, 0, 0, 0, 2, 8, 0},
            {0, 0, 0, 4, 1, 9, 0, 0, 5},
            {0, 0, 0, 0, 8, 0, 0, 7, 9}
        };
    }

    // Method to display the Sudoku board
    public void DisplayBoard()
    {
        for (int i = 0; i < Size; i++)
        {
            for (int j = 0; j < Size; j++)
            {
                Console.Write(board[i, j] == 0 ? ". " : board[i, j] + " ");
            }
            Console.WriteLine();
        }
    }

    // Method to check if a move is valid
    public bool IsValidMove(int row, int col, int num)
    {
        // Check row
        for (int i = 0; i < Size; i++)
            if (board[row, i] == num)
                return false;

        // Check column
        for (int i = 0; i < Size; i++)
            if (board[i, col] == num)
                return false;

        // Check 3x3 sub-grid
        int startRow = row / 3 * 3;
        int startCol = col / 3 * 3;
        for (int i = 0; i < 3; i++)
            for (int j = 0; j < 3; j++)
                if (board[startRow + i, startCol + j] == num)
                    return false;

        return true;
    }

    // Method to place a number on the board
    public bool PlaceNumber(int row, int col, int num)
    {
        if (IsValidMove(row, col, num))
        {
            board[row, col] = num;
            return true;
        }
        return false;
    }

    // Check if the board is completely filled
    public bool IsSolved()
    {
        for (int i = 0; i < Size; i++)
            for (int j = 0; j < Size; j++)
                if (board[i, j] == 0)
                    return false;
        return true;
    }
}
