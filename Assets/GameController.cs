using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{

    public List<Player> players;
    private int currentPlayerIndex;

    // Start is called before the first frame update
    void Start()
    {
        currentPlayerIndex = 0;
        UpdateHUDS();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void NextTurn()
    {
        currentPlayerIndex = (currentPlayerIndex + 1) % players.Count;
        UpdateHUDS();
    }

    private void UpdateHUDS()
    {
        for (int i = 0; i < players.Count; i++)
        {
            bool isCurrentPlayer = (i == currentPlayerIndex);
            players[i].playerHUD.SetTurnIndicator(isCurrentPlayer);
        }
    }
}
