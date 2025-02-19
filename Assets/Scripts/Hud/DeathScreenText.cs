using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DeathScreenText : MonoBehaviour
{
    // Start is called before the first frame update
    public TMP_Text text;

    void Start()
    {
        text.text = "But you survived " + TurnStorage.currentTurn.ToString() + " turn(s)!";

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
