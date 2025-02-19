using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TurnText : MonoBehaviour
{

    // Start is called before the first frame update
    public TMP_Text text;
    void Start()
    {
    }
    private void Awake()
    {

        //text.text = "Apple";


    }
    // Update is called once per frame
    void Update()
    {
        text.text = "Current Turn:" + TurnStorage.currentTurn.ToString();
    }
}
