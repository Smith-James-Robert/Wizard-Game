using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;

public class Mud : MonoBehaviour
{
    // Start is called before the first frame update
    public int difficulty;
    public int startingX, startingY;
    public Tile behaviour;
    void Start()
    {
        behaviour = new Tile((startingX, startingY), this.gameObject, true, true, true, difficulty);
        this.transform.position = GameManager.instance.grid.GetCentralWorldPosition(startingX, startingY);
        GameManager.instance.grid.AddValue(startingX, startingY, behaviour);
        Vector3 position = this.transform.position;
        position.z = 1;
        this.transform.position = position;
    }

    // Update is called once per frame
    void Update()
    {

    }
}
