using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DiceRoll : MonoBehaviour
{

    public CharacterMovement characterMovement;
    public TextMeshProUGUI diceRollText;
    public float displayDuration = 5f;

    private bool isCharacterMoving = false; // Tracks if the character is moving

    // Start is called before the first frame update
    void Start()
    {
        diceRollText.text = "";
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnMouseDown()
    {
        if (!isCharacterMoving)
        {
            RollDice();
        }
    }



    public void RollDice()
    {
        int roll = Random.Range(1, 13);
        Debug.Log("Rolled: " + roll);
        characterMovement.MoveCharacter(roll);

        //Diplays the roll on the screen
        diceRollText.text = roll.ToString();
        StartCoroutine(HideRollResultAfterDelay());

        isCharacterMoving = true;
    }

    IEnumerator HideRollResultAfterDelay()
    {
        yield return new WaitForSeconds(displayDuration);
        diceRollText.text = "";
    }

    public void SetCharacterMoving(bool isMoving)
    {
        isCharacterMoving = isMoving;
    }
}
