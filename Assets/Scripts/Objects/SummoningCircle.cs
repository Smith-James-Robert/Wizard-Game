using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Tree;
using UnityEngine;

public class SummoningCircle : MonoBehaviour
{
    public bool active;
    public int startingX, startingY;
    Tile behaviour;
    OwnTime ownTime;
    public GameObject fireGuy, birdy, golem, armour, working;
    private FireGuy fireGuyBehaviour;
    private MeleeEnemy birdyBehaviour,golemBehaviour,armourBehaviour;
    // Start is called before the first frame update
    void Start()
    {
        behaviour = new Tile((startingX, startingY), this.gameObject);
        this.transform.position = GameManager.instance.grid.GetCentralWorldPosition(startingX, startingY);
        GameManager.instance.grid.AddValue(startingX, startingY, behaviour);
        Vector3 position = this.transform.position;
        position.z = 0;
        this.transform.position = position;
    }

    // Update is called once per frame
    void Update()
    {
        if (active)
        { 
        this.transform.Rotate(0, 0, 0.5f);
        }
    }
    public bool summonAttempt(int summonGuy)
    {
        if (GameManager.instance.grid.GetWalkable(behaviour.location.x, behaviour.location.y))
        {
            switch (summonGuy)
            {
                case 0:
                    working = Instantiate(fireGuy);
                    fireGuyBehaviour = working.GetComponent<FireGuy>();
                    fireGuyBehaviour.startingX = startingX;
                    fireGuyBehaviour.startingY= startingY;
                    active = false;
                    return true;
                case 1:
                    working = Instantiate(armour);
                    armourBehaviour = working.GetComponent<MeleeEnemy>();
                    armourBehaviour.startingX = startingX;
                    armourBehaviour.startingY= startingY;
                    active = false;
                    return true;
                case 2:
                    working = Instantiate(birdy);
                    armourBehaviour= working.GetComponent<MeleeEnemy>();
                    armourBehaviour.startingX = startingX;
                    armourBehaviour.startingY= startingY;
                    active = false;
                    return true;
                case 3:
                    working = Instantiate(golem);
                    golemBehaviour = working.GetComponent<MeleeEnemy>();
                    golemBehaviour.startingX = startingX;
                    golemBehaviour.startingY = startingY;
                    active = false;
                    return true;
            }
        }
        return false;
    }
}
