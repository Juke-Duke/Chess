using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    // Game State
    private string player = "White";
    public string Player { get => player; }

    private bool gameOver = false;
    public bool IsGameOver { get => gameOver; set => gameOver = value; }

    private string winner;
    public string Winner 
    { 
        get => winner; 
        set 
        { 
            winner = value;
            Debug.Log($"{winner} won!");
        }
    }


    [SerializeField] private Tile tile;
    [SerializeField] private ChessPiece WhitePawn, WhiteKnight, WhiteBishop, WhiteRook, WhiteQueen, WhiteKing, 
                                        BlackPawn, BlackKnight, BlackBishop, BlackRook, BlackQueen, BlackKing;

    private ChessPiece[,] positions = new ChessPiece[8,8]; // 8X8 matrix to show positions of all pieces in play

    private void Start()
    {
        // Creates 8X8 tile board
        for (int x = 0; x < 8; ++x)
            for (int y = 0; y < 8; ++y)
            {
                // Tile Placement and Color
                Tile square = Instantiate(tile, new Vector3(-3.5f + x, -3.5f + y, 10), Quaternion.identity);
                bool color = (x % 2 != y % 2);
                square.WhatColor(color);
                
            }

        // Array of white pieces
        ChessPiece[] whitePieces = new ChessPiece[16] { CreatePiece(WhiteRook, 0, 0), CreatePiece(WhiteKnight, 1, 0), 
        CreatePiece(WhiteBishop, 2, 0), CreatePiece(WhiteQueen, 3, 0), CreatePiece(WhiteKing, 4, 0), 
        CreatePiece(WhiteBishop, 5, 0), CreatePiece(WhiteKnight, 6, 0), CreatePiece(WhiteRook, 7, 0), 
        CreatePiece(WhitePawn, 0, 1), CreatePiece(WhitePawn, 1, 1), CreatePiece(WhitePawn, 2, 1),
        CreatePiece(WhitePawn, 3, 1), CreatePiece(WhitePawn, 4, 1), CreatePiece(WhitePawn, 5, 1), 
        CreatePiece(WhitePawn, 6, 1), CreatePiece(WhitePawn, 7, 1) };

        // Array of black pieces
        ChessPiece[] blackPieces = new ChessPiece[16] { CreatePiece(BlackRook, 0, 7), CreatePiece(BlackKnight, 1, 7), 
        CreatePiece(BlackBishop, 2, 7), CreatePiece(BlackQueen, 3, 7), CreatePiece(BlackKing, 4, 7), 
        CreatePiece(BlackBishop, 5, 7), CreatePiece(BlackKnight, 6, 7), CreatePiece(BlackRook, 7, 7),
        CreatePiece(BlackPawn, 0, 6), CreatePiece(BlackPawn, 1, 6), CreatePiece(BlackPawn, 2, 6), 
        CreatePiece(BlackPawn, 3, 6), CreatePiece(BlackPawn, 4, 6), CreatePiece(BlackPawn, 5, 6), 
        CreatePiece(BlackPawn, 6, 6), CreatePiece(BlackPawn, 7, 6) };

        // Records the coordinates of each position into the positions matrix at the start of the game
        for (int i = 0; i < 16; ++i)
        {
            whitePieces[i].Team = "White";
            SetPosition(whitePieces[i]);
            blackPieces[i].Team = "Black";
            SetPosition(blackPieces[i]);
        }
    }

    private void Update()
    {
        if (gameOver == true && Input.GetKeyDown(KeyCode.Space))
        {
            gameOver = false;
            SceneManager.LoadScene("Chess");
        }
    }

    // Lets GameManager activate each piece that is created at the start of the game
    private ChessPiece CreatePiece(ChessPiece piece, int x, int y)
    {
        ChessPiece creation = Instantiate(piece, new Vector3(0, 0, -1), Quaternion.identity);
        creation.XPos = x;
        creation.YPos = y;
        creation.SetCoords();

        return creation;
    }

    public void NextTurn()
    {
        if (player == "White")
            player = "Black";
        else
            player = "White";
    }

    // Outputs current ChessPiece at given x and y coordinates
    public ChessPiece GetPosition(int x, int y)
    {
        return positions[x, y];
    }

    // Records the piece in the positions matrix
    public void SetPosition(ChessPiece piece)
    {
        positions[piece.XPos, piece.YPos] = piece;
    }

    // Sets a index to null in the positions matrix after a piece leaves it
    public void EmptyPosition(int x, int y)
    {
        positions[x, y] = null;
    }

    public bool ValidPosition(int x, int y)
    {
        if (x < 0 || y < 0 || x >= positions.GetLength(0) || y >= positions.GetLength(1))
            return false;
        
        return true;
    }

    public void ClearMoves()
    {
        GameObject[] moves = GameObject.FindGameObjectsWithTag("MovePlate");

        foreach (GameObject move in moves)
            Destroy(move);
    }

    public void SwapBoard()
    {
        for (int x = 0; x < 8; ++x)
            for (int y = 0; y < 4; ++y)
            {
                if (GetPosition(x, y) != null && GetPosition(7 - x, 7 - y) == null)
                {
                    GetPosition(x, y).XPos = 7 - x;
                    GetPosition(x, y).YPos = 7 - y;
                    GetPosition(x, y).SetCoords();
                    SetPosition(GetPosition(x, y));
                    EmptyPosition(x, y);
                }
                else if (GetPosition(x, y) == null && GetPosition(7 - x, 7 - y) != null)
                {
                    GetPosition(7 - x, 7 - y).XPos = x;
                    GetPosition(7 - x, 7 - y).YPos = y;
                    GetPosition(7 - x, 7 - y).SetCoords();
                    SetPosition(GetPosition(7 - x, 7 - y));
                    EmptyPosition(7 - x, 7 - y);
                }
                else if (GetPosition(x, y) != null && GetPosition(7 - x, 7 - y) != null)
                {
                    GetPosition(x, y).XPos = 7 - x;
                    GetPosition(x, y).YPos = 7 - y;
                    GetPosition(x, y).SetCoords();
                    GetPosition(7 - x, 7 - y).XPos = x;
                    GetPosition(7 - x, 7 - y).YPos = y;
                    GetPosition(7 - x, 7 - y).SetCoords();
                    ChessPiece temp = GetPosition(7 - x, 7 - y);
                    SetPosition(GetPosition(x, y));
                    SetPosition(temp);
                }
            }
    }
}
