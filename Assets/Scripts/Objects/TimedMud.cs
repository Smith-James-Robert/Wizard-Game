using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;

public class TimedMud : MonoBehaviour
{
    // Start is called before the first frame update
    public int difficulty;
    public int startingX, startingY;
    public int duration;
    public Tile behaviour;
    public OwnTime time;
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
        if (time.currentTurn<GameManager.instance.currentTurn)
        {
            duration--;
            if (duration <=0)
            {
                GameManager.instance.grid.RemoveValue(behaviour.location.x, behaviour.location.y, behaviour);
                Destroy(gameObject);
                Destroy(this);
            }
            time.UpdateTurn();
        }
    }
}
