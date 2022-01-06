using UnityEngine;

public class ChessPiece : MonoBehaviour
{
    private GameObject engine;
    private GameManager GameManager;
    [SerializeField] private MovePlate MovePlate;

    public string Team;

    private int x, y;
    public int XPos { get => x; set => x = value; }
    public int YPos { get => y; set => y = value; }

    private void Awake()
    {
        engine = GameObject.Find("GameManager");
        GameManager = engine.GetComponent<GameManager>();
    }

    private void OnMouseDown()
    {  
        if (!GameManager.IsGameOver)
        {
            GameManager.ClearMoves();

            if (GameManager.Player == "White")
            {
                if (this.tag == "WhitePawn")
                    PawnMechanics();
                else if (this.tag == "WhiteKnight")
                    KnightMechanics();
                else if (this.tag == "WhiteBishop")
                    BishopMechanics();
                else if (this.tag == "WhiteRook")
                    RookMechanics();
                else if (this.tag == "WhiteQueen")
                {
                    BishopMechanics();
                    RookMechanics();
                }
                else if (this.tag == "WhiteKing")
                    KingMechanics();
            }
            else
            {
                if (this.tag == "BlackPawn")
                    PawnMechanics();
                else if (this.tag == "BlackKnight")
                    KnightMechanics();
                else if (this.tag == "BlackBishop")
                    BishopMechanics();
                else if (this.tag == "BlackRook")
                    RookMechanics(); 
                else if (this.tag == "BlackQueen")
                {
                    BishopMechanics();
                    RookMechanics();
                }
                else if (this.tag == "BlackKing")
                    KingMechanics(); 
            }
        }
    }

    public void SetCoords()
    {
        if (this.tag == "WhiteBishop" || this.tag == "BlackBishop")
            this.transform.position = new Vector3(x - 3.5f, y - 3.1f, 0);
        else if (this.tag == "WhiteQueen" || this.tag == "BlackQueen")
            this.transform.position = new Vector3(x - 3.5f, y - 3.05f, 0);
        else if (this.tag == "WhiteKing" || this.tag == "BlackKing")
            this.transform.position = new Vector3(x - 3.5f, y - 2.95f, 0);
        else
            this.transform.position = new Vector3(x - 3.5f, y - 3.2f, 0);
    }

    public void ValidMove(int x, int y)
    {
        MovePlate move = Instantiate(MovePlate, new Vector3(x, y, -9), Quaternion.identity);
        move.Refrence = this;
        move.SetCoords(x, y);
    }

    public void ValidAttack(int x, int y)
    {
        MovePlate move = Instantiate(MovePlate, new Vector3(x, y, -9), Quaternion.identity);
        move.isAttack = true;
        move.Refrence = this;
        move.SetCoords(x, y);
    }

        public void ClearMoves()
    {
        GameObject[] moves = GameObject.FindGameObjectsWithTag("MovePlate");

        foreach (GameObject move in moves)
            Destroy(move);
    }

    private void PawnMechanics()
    {
        // Movement
        if (y == 1)
        {
            for (int move = 1; move <= 2; ++move)
            {
                if (GameManager.GetPosition(x, y + move) != null)
                    break;

                ValidMove(x, y + move);
            }
        }
        else if (GameManager.ValidPosition(x, y + 1) && GameManager.GetPosition(x, y + 1) == null)
            ValidMove(x, y + 1);

        // Attack
        if (GameManager.ValidPosition(x - 1, y + 1) && GameManager.GetPosition(x - 1, y + 1) != null && GameManager.GetPosition(x - 1, y + 1).Team != this.Team)
            ValidAttack(x - 1, y + 1);
        if (GameManager.ValidPosition(x + 1, y + 1) && GameManager.GetPosition(x + 1, y + 1) != null && GameManager.GetPosition(x + 1, y + 1).Team != this.Team)
            ValidAttack(x + 1, y + 1);
    }

    private void KnightMechanics()
    {
        // TopRightUp
        if (GameManager.ValidPosition(x + 1, y + 2) && GameManager.GetPosition(x + 1, y + 2) == null)
            ValidMove(x + 1, y + 2);
        else if (GameManager.ValidPosition(x + 1, y + 2) && GameManager.GetPosition(x + 1, y + 2) != null && GameManager.GetPosition(x + 1, y + 2).Team != Team)
            ValidAttack(x + 1, y + 2);
        
        // TopRightDown
        if (GameManager.ValidPosition(x + 2, y + 1) && GameManager.GetPosition(x + 2, y + 1) == null)
            ValidMove(x + 2, y + 1);
        else if (GameManager.ValidPosition(x + 2, y + 1) && GameManager.GetPosition(x + 2, y + 1) != null && GameManager.GetPosition(x + 2, y + 1).Team != Team)
            ValidAttack(x + 2, y + 1);

        // TopLeftUp
        if (GameManager.ValidPosition(x - 1, y + 2) && GameManager.GetPosition(x - 1, y + 2) == null)
            ValidMove(x - 1, y + 2);
        else if (GameManager.ValidPosition(x - 1, y + 2) && GameManager.GetPosition(x - 1, y + 2) != null && GameManager.GetPosition(x - 1, y + 2).Team != Team)
            ValidAttack(x - 1, y + 2);   

        // TopLeftDown
        if (GameManager.ValidPosition(x - 2, y + 1) && GameManager.GetPosition(x - 2, y + 1) == null)
            ValidMove(x - 2, y + 1);
        else if (GameManager.ValidPosition(x - 2, y + 1) && GameManager.GetPosition(x - 2, y + 1) != null && GameManager.GetPosition(x - 2, y + 1).Team != Team)
            ValidAttack(x - 2, y + 1);

        // BottomRightDown
        if (GameManager.ValidPosition(x + 1, y - 2) && GameManager.GetPosition(x + 1, y - 2) == null)
            ValidMove(x + 1, y - 2);
        else if (GameManager.ValidPosition(x + 1, y - 2) && GameManager.GetPosition(x + 1, y - 2) != null && GameManager.GetPosition(x + 1, y - 2).Team != Team)
            ValidAttack(x + 1, y - 2);
        
        // BottoRightUp
        if (GameManager.ValidPosition(x + 2, y - 1) && GameManager.GetPosition(x + 2, y - 1) == null)
            ValidMove(x + 2, y - 1);
        else if (GameManager.ValidPosition(x + 2, y - 1) && GameManager.GetPosition(x + 2, y - 1) != null && GameManager.GetPosition(x + 2, y - 1).Team != Team)
            ValidAttack(x + 2, y - 1);

        // BottomLeftDown
        if (GameManager.ValidPosition(x - 1, y - 2) && GameManager.GetPosition(x - 1, y - 2) == null)
            ValidMove(x - 1, y - 2);
        else if (GameManager.ValidPosition(x - 1, y - 2) && GameManager.GetPosition(x - 1, y - 2) != null && GameManager.GetPosition(x - 1, y - 2).Team != Team)
            ValidAttack(x - 1, y - 2);
        
        // BottomLeftUp
        if (GameManager.ValidPosition(x - 2, y - 1) && GameManager.GetPosition(x - 2, y - 1) == null)
            ValidMove(x - 2, y - 1);
        else if (GameManager.ValidPosition(x - 2, y - 1) && GameManager.GetPosition(x - 2, y - 1) != null && GameManager.GetPosition(x - 2, y - 1).Team != Team)
            ValidAttack(x - 2, y - 1);         
    }

    private void BishopMechanics()
    {
        int currX = x, currY = y;

        // TopRight
        while (GameManager.ValidPosition(currX + 1, currY + 1))
        {
            if (GameManager.GetPosition(currX + 1, currY + 1) != null && GameManager.GetPosition(currX + 1, currY + 1).Team != Team)
            {
                ValidAttack(currX + 1, currY + 1);
                break;
            }
            else if (GameManager.GetPosition(currX + 1, currY + 1) != null)
                break;
            else
                ValidMove(currX + 1, currY + 1);
            
            ++currX;
            ++currY;
        }

        currX = x;
        currY = y;

        // BottomRight
        while (GameManager.ValidPosition(currX + 1, currY - 1))
        {
            if (GameManager.GetPosition(currX + 1, currY - 1) != null && GameManager.GetPosition(currX + 1, currY - 1).Team != Team)
            {
                ValidAttack(currX + 1, currY - 1);
                break;
            }
            else if (GameManager.GetPosition(currX + 1, currY - 1) != null)
                break;
            else
                ValidMove(currX + 1, currY - 1);
            
            ++currX;
            --currY;
        }

        currX = x;
        currY = y;

        // TopLeft
        while (GameManager.ValidPosition(currX - 1, currY + 1))
        {
            if (GameManager.GetPosition(currX - 1, currY + 1) != null && GameManager.GetPosition(currX - 1, currY + 1).Team != Team)
            {
                ValidAttack(currX - 1, currY + 1);
                break;
            }
            else if (GameManager.GetPosition(currX - 1, currY + 1) != null)
                break;
            else
                ValidMove(currX - 1, currY + 1);
            
            --currX;
            ++currY;
        }

        currX = x;
        currY = y;

        // BottomLeft
        while (GameManager.ValidPosition(currX - 1, currY - 1))
        {
            if (GameManager.GetPosition(currX - 1, currY - 1) != null && GameManager.GetPosition(currX - 1, currY - 1).Team != Team)
            {
                ValidAttack(currX - 1, currY - 1);
                break;
            }
            else if (GameManager.GetPosition(currX - 1, currY - 1) != null)
                break;
            else
                ValidMove(currX - 1, currY - 1);
            
            --currX;
            --currY;
        }
    }

    private void RookMechanics()
    {
        int currX = x, currY = y;

        // Right
        while (GameManager.ValidPosition(currX + 1, y))
        {
            if (GameManager.GetPosition(currX + 1, y) != null && GameManager.GetPosition(currX + 1, y).Team != Team)
            {
                ValidAttack(currX + 1, y);
                break;
            }
            else if (GameManager.GetPosition(currX + 1, y) != null)
                break;
            else
                ValidMove(currX + 1, y);
            
            ++currX;
        }

        currX = x;

        // Left
        while (GameManager.ValidPosition(currX - 1, y))
        {
            if (GameManager.GetPosition(currX - 1, y) != null && GameManager.GetPosition(currX - 1, y).Team != Team)
            {
                ValidAttack(currX - 1, y);
                break;
            }
            else if (GameManager.GetPosition(currX - 1, y) != null)
                break;
            else
                ValidMove(currX - 1, y);
            
            --currX;
        }

        currX = x;

        // Up
        while (GameManager.ValidPosition(x, currY + 1))
        {
            if (GameManager.GetPosition(x, currY + 1) != null && GameManager.GetPosition(x, currY + 1).Team != Team)
            {
                ValidAttack(x, currY + 1);
                break;
            }
            else if (GameManager.GetPosition(x, currY + 1) != null)
                break;
            else
                ValidMove(x, currY + 1);
            
            ++currY;
        }

        currY = y;

        // Down
        while (GameManager.ValidPosition(x, currY - 1))
        {
            if (GameManager.GetPosition(x, currY - 1) != null && GameManager.GetPosition(x, currY - 1).Team != Team)
            {
                ValidAttack(x, currY - 1);
                break;
            }
            else if (GameManager.GetPosition(x, currY - 1) != null)
                break;
            else
                ValidMove(x, currY - 1);
            
            --currY;
        }
    }

    private void KingMechanics()
    {
        // Right
        if (GameManager.ValidPosition(x + 1, y))
        {
            if (GameManager.GetPosition(x + 1, y) != null && GameManager.GetPosition(x + 1, y).Team != Team)
                ValidAttack(x + 1, y);
            else if (GameManager.GetPosition(x + 1, y) == null)
                ValidMove(x + 1, y);
        }

        // Left
        if (GameManager.ValidPosition(x - 1, y))
        {
            if (GameManager.GetPosition(x - 1, y) != null && GameManager.GetPosition(x - 1, y).Team != Team)
                ValidAttack(x - 1, y);
            else if (GameManager.GetPosition(x - 1, y) == null)
                ValidMove(x - 1, y);
        }

        // Up
        if (GameManager.ValidPosition(x, y + 1))
        {
            if (GameManager.GetPosition(x, y + 1) != null && GameManager.GetPosition(x, y + 1).Team != Team)
                ValidAttack(x, y + 1);
            else if (GameManager.GetPosition(x, y + 1) == null)
                ValidMove(x, y + 1);
        }

        // Down
        if (GameManager.ValidPosition(x, y - 1))
        {
            if (GameManager.GetPosition(x, y - 1) != null && GameManager.GetPosition(x, y - 1).Team != Team)
                ValidAttack(x, y - 1);
            else if (GameManager.GetPosition(x, y - 1) == null)
                ValidMove(x, y - 1);
        }

        // TopRight
        if (GameManager.ValidPosition(x + 1, y + 1))
        {
            if (GameManager.GetPosition(x + 1, y + 1) != null && GameManager.GetPosition(x + 1, y + 1).Team != Team)
                ValidAttack(x + 1, y + 1);
            else if (GameManager.GetPosition(x + 1, y + 1) == null)
                ValidMove(x + 1, y + 1);
        }

        // BottomRight
        if (GameManager.ValidPosition(x + 1, y - 1))
        {
            if (GameManager.GetPosition(x + 1, y - 1) != null && GameManager.GetPosition(x + 1, y - 1).Team != Team)
                ValidAttack(x + 1, y - 1);
            else if (GameManager.GetPosition(x + 1, y - 1) == null)
                ValidMove(x + 1, y - 1);
        }

        // TopLeft
        if (GameManager.ValidPosition(x - 1, y + 1))
        {
            if (GameManager.GetPosition(x - 1, y + 1) != null && GameManager.GetPosition(x - 1, y + 1).Team != Team)
                ValidAttack(x - 1, y + 1);
            else if (GameManager.GetPosition(x - 1, y + 1) == null)
                ValidMove(x - 1, y + 1);
        }

        // BottomLeft
        if (GameManager.ValidPosition(x - 1, y - 1))
        {
            if (GameManager.GetPosition(x - 1, y - 1) != null && GameManager.GetPosition(x - 1, y - 1).Team != Team)
                ValidAttack(x - 1, y - 1);
            else if (GameManager.GetPosition(x - 1, y - 1) == null)
                ValidMove(x - 1, y - 1);
        }
    }
}
