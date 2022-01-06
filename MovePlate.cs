using UnityEngine;

public class MovePlate : MonoBehaviour
{
    private GameObject engine;
    private GameManager GameManager;

    private ChessPiece refrence;
    public ChessPiece Refrence { set => refrence = value; }

    public bool isAttack = false;

    private int x, y;
    public int XCoord { get => x; set => x = value; }
    public int YCoord { get => y; set => y = value; }

    private void Awake()
    {
        engine = GameObject.FindGameObjectWithTag("GameController");
        GameManager = engine.GetComponent<GameManager>();
    }

    private void Start()
    {
        if (isAttack)
            GetComponent<SpriteRenderer>().color = new Color(0.5176471f, 0f, 0.2024749f);
    }

    public void OnMouseDown()
    {
        if (isAttack)
        {
            if (GameManager.GetPosition(x, y).tag == "WhiteKing")
            {
                GameManager.Winner = "Black";
                GameManager.IsGameOver = true;
            }
            else if (GameManager.GetPosition(x, y).tag == "BlackKing")
            {
                GameManager.Winner = "White";
                GameManager.IsGameOver = true;
            }

            Destroy(GameManager.GetPosition(x, y).gameObject);
        }

        GameManager.EmptyPosition(refrence.XPos, refrence.YPos);
        refrence.XPos = x;
        refrence.YPos = y;
        refrence.SetCoords();

        GameManager.SetPosition(refrence);
        GameManager.ClearMoves();
        GameManager.SwapBoard();
        
        if (!GameManager.IsGameOver)
        {
            GameManager.NextTurn();
            Debug.Log(GameManager.Player);
        }
    }

    public void SetCoords(int x, int y)
    {
        this.x = x;
        this.y = y;
        this.transform.position = new Vector3(x - 3.5f, y - 3.5f, -9);
    }
}
