using UnityEngine;

public class Tile : MonoBehaviour
{
    private GameObject engine;
    private GameManager GameManager;

    // Color and Highlighter of tile
    [SerializeField] private Color white, black;
    [SerializeField] private SpriteRenderer sprite;

    private void Awake()
    {
        engine = GameObject.FindGameObjectWithTag("GameController");
        GameManager = engine.GetComponent<GameManager>();
    }

    public void WhatColor(bool color)
    {
        sprite.color = color ? white : black;
    }
}
