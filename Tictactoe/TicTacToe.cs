using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


using static System.Console;

namespace Tictactoe
{
    class TicTacToe
    {
        private int[][] Board;
        private bool GameOver = false;
        int currentPlayer = 1;

        public TicTacToe()
        {
            Initialize();
            Start();
        }

        public void ResetGame()
        {
            currentPlayer = 1;

            for (int i = 0; i < Board.Length; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    Board[i][j] = 0;
                }
            }
        }

        private void Start()
        {
            DisplayBoard();
            do
            {
                int[] input = GetInput();
                MakeMove(input);
                DisplayBoard();

                if (CheckWinner())
                {
                    WriteLine("Player " + currentPlayer + " wins the game! Press any key to play again.");
                    ReadKey();
                    ResetGame();
                    DisplayBoard();
                    continue;
                }
                SwitchPlayer();
            }
            while (!GameOver);

            WriteLine("Game over!!");
        }

        private void MakeMove(int[] moves)
        {
            if (moves[0] == -1)
            {
                GameOver = true;
                return;
            }
            int row = moves[0] - 1;
            int col = moves[1] - 1;

            if(GetBoardSpot(row,col) != 0)
            {
                WriteLine("This position is already taken, choose another.  Press a key to continue");
                ReadKey();
                return;
            }

            Board[moves[0] - 1][moves[1] - 1] = currentPlayer;
        }

        private bool CheckWinner()
        {
            if (Board[0][0] == Board[0][1] && Board[0][1] == Board[0][2] && Board[0][0] != 0)
            {
                return true;
            }
            else if(Board[1][0] == Board[1][1] && Board[1][1] == Board[1][2] && Board[1][0] != 0)
            {
                return true;
            }
            else if(Board[2][0] == Board[2][1] && Board[2][1] == Board[2][2] && Board[2][0] != 0)
            {
                return true;
            }
            else if(Board[0][0] == Board[1][1] && Board[1][1] == Board[2][2] && Board[0][0] != 0)
            {
                return true;
            }
            else if(Board[0][2] == Board[1][1] && Board[1][1] == Board[2][0] && Board[0][2] != 0)
            {
                return true;
            }
            return false;
        }

        private int GetBoardSpot(int row,int col)
        {
            return Board[row][col];
        }

        private void Initialize()
        {
            Board = new int[3][];

            for(int i = 0; i < Board.Length; i++)
            {
                Board[i] = new int[3];

                for(int j = 0; j < 3; j++)
                {
                    Board[i][j] = 0;
                }
            }
        }

        private void DisplayBoard()
        {
            Console.Clear();
            StringBuilder sb = new StringBuilder(" ------------\n");
            for (int i = 0; i < Board.Length; i++)
            {
                sb.Append("| ");
                for (int j = 0; j < Board[i].Length; j++)
                {
                    sb.Append(DecodeSpace( Board[i][j]) + " | ");
                }
                sb.Append("\n");
            }
            sb.Append(" ------------\n");
            WriteLine(sb.ToString());
        }

        private string DecodeSpace(int space)
        {
            switch (space)
            {
                case 0:
                    return " ";
                case 1:
                    return "X";
                case 2:
                    return "O";
                default:
                    return "!";  // shouldn't reach here
            }
        }

        private int[] GetInput()
        {
            while (true)
            {
                int[] r = new int[2];

                Console.WriteLine("Player " + currentPlayer + " select the ROW, COL you wish to play. Comma separated.  example 3,1   'Q' to quit");
                string input = Console.ReadLine();
                string checkQuit = input.ToUpper();
                if (checkQuit == "Q")
                {
                    r[0] = -1;
                }
                else
                {
                    string[] split = input.Split(',');
                    int row, col;

                    if (split.Length != 2 ||
                        !int.TryParse(split[0], out row) ||
                        !int.TryParse(split[1], out col) ||
                        row < 0 || row > 3 || col < 0 || col > 3
                        )
                        continue;

                    r[0] = row;
                    r[1] = col;
                }
                return r;
            }
        }

        private void SwitchPlayer()
        {
            if(currentPlayer == 1)
            {
                currentPlayer = 2;
            }
            else
            {
                currentPlayer = 1;
            }
        }
    }
}
