using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    public Transform[] boardSpaces; //Assign board space transforms in Inspector
    private Dictionary<int, List<GameObject>> spaceOccupants = new Dictionary<int, List<GameObject>>();

    private int currentSpaceIndex = 0;
    public float moveSpeed = 1f; //Player character move speed.

    public float jumpScaleMultiplier = 1.2f; //Scale factgor for the jump effect
    public float pauseDuration = 0.2f; // Pause duration on each space


    public Vector3 offset = new Vector3(0.2f, 0, 0); // Offset for the character to sit on the space (so it's not in the center

    // Start is called before the first frame update
    void Start()
    {
        // Initialize space occupants dictionary
        for (int i = 0; i < boardSpaces.Length; i++)
        {
            spaceOccupants[i] = new List<GameObject>();
        }
    }

    // Update is called once per frame
    void Update()
    {

    }



    public void MoveCharacter(int spaces)
    {
        // Calculate the target space index
        int targetSpaceIndex = (currentSpaceIndex + spaces) % boardSpaces.Length;

        // Start the coroutine to move the character
        StartCoroutine(SmoothMove(targetSpaceIndex));

        FindObjectOfType<DiceRoll>().SetCharacterMoving(true); // Disable the dice roll button while the character is moving
    }


    IEnumerator SmoothMove(int targetSpaceIndex)
    {
        Vector3 originalScale = transform.localScale;  // Store the original scale


        while (currentSpaceIndex != targetSpaceIndex)
        {
            int nextSpaceIndex = (currentSpaceIndex + 1) % boardSpaces.Length;
            Vector3 startPosition = transform.position;
            Vector3 targetPosition = boardSpaces[nextSpaceIndex].position;

            float journeyLength = Vector3.Distance(startPosition, targetPosition);
            float journeyCovered = 0;

            while (transform.position != targetPosition)
            {
                // Move towards the target position
                transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);

                // Calculate the proportion of the journey covered
                journeyCovered += moveSpeed * Time.deltaTime;
                float fractionOfJourney = journeyCovered / journeyLength;

                // Use a sine wave for smoother scaling
                float scale = 1 + (Mathf.Sin(fractionOfJourney * Mathf.PI) * (jumpScaleMultiplier - 1));
                transform.localScale = originalScale * scale;

                yield return null;
            }

            // Pause briefly on each space
            yield return new WaitForSeconds(pauseDuration);

            // Update current space index
            currentSpaceIndex = nextSpaceIndex;
        }

        // Reset scale at the end
        transform.localScale = originalScale;

        //  Update space occupants
        spaceOccupants[currentSpaceIndex].Remove(gameObject);
        spaceOccupants[targetSpaceIndex].Add(gameObject);

        // Adjust position for multiple characters
        AdjustPositionForMultipleCharacters(targetSpaceIndex);

        // Update current space index
        currentSpaceIndex = targetSpaceIndex;


        FindObjectOfType<DiceRoll>().SetCharacterMoving(false); // Re-enable the dice roll button
    }

    void AdjustPositionForMultipleCharacters(int spaceIndex)
    {
        // Get the list of occupants for the space
        List<GameObject> occupants = spaceOccupants[spaceIndex];

        // Calculate the offset for each character
        float totalOffset = (occupants.Count - 1) * offset.x;
        float startX = -(totalOffset / 2);

        // Loop through each occupant and adjust its position
        for (int i = 0; i < occupants.Count; i++)
        {
            Vector3 positionOffset = offset * i;
            occupants[i].transform.position = boardSpaces[spaceIndex].position + positionOffset;
        }
    }
}

