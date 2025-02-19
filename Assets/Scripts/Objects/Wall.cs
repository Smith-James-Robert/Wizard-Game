using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Wall : MonoBehaviour
{
    Tile behaviour;
    public int startingX, startingY;
    // Start is called before the first frame update
    void Start()
    {
        behaviour = new Tile((startingX, startingY), this.gameObject, false, false,false,int.MaxValue);
        this.transform.position = GameManager.instance.grid.GetCentralWorldPosition(startingX, startingY);
        GameManager.instance.grid.AddValue(startingX, startingY, behaviour);
        Vector3 position = this.transform.position;
        position.z = 0;
        this.transform.position = position;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
