using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class PlayerHUD : MonoBehaviour
{

    // this is a sprite in the scene and mayb need to be updated to image or variable updated to sprite
    public Image characterIcon;
    public TextMeshProUGUI playerNameText;
    // this is a sprite in the scene and mayb need to be updated to image or variable updated to sprite
    public Image turnIndicator;


    // Start is called before the first frame update
    void Start()
    {
        turnIndicator.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetPlayerName(string name)
    {
        playerNameText.text = name;
    }

    public void SetTurnIndicator(bool isTurn)
    {
        turnIndicator.gameObject.SetActive(isTurn);
    }
}
