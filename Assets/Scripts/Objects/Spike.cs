using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spike : MonoBehaviour
{
    public int damage;
    public int startingX, startingY;
    public int difficulty;
    public DamagingTile behaviour;
    // Start is called before the first frame update
    void Start()
    {
        behaviour = new DamagingTile((startingX, startingY),this.gameObject,true,true,damage,difficulty);
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
