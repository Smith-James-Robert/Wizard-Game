using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OwnTime : MonoBehaviour
{
    public int currentTurn;
    // Start is called before the first frame update
    void Start()
    {
        currentTurn = GameManager.instance.currentTurn;
    }

    public void UpdateTurn()
    {
        if (currentTurn < GameManager.instance.currentTurn)
        {
            currentTurn++;
        }
    }
}
