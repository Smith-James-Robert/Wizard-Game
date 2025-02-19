using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour
{
    public MovementTile behaviour;
    public int startingX, startingY, moveX, moveY, linkedX, linkedY;
    public bool teleport, actionAfterMove;
    public ParticleSystem myParticleSystem;
    public AudioSource teleportSound;
    private bool isActive;
    // Start is called before the first frame update
    void Start()
    {

        behaviour = new MovementTile((startingX, startingY), this.gameObject, true, false,actionAfterMove,teleport, linkedX, linkedY, moveX,moveY);
        this.transform.position = GameManager.instance.grid.GetCentralWorldPosition(startingX, startingY);
        GameManager.instance.grid.AddValue(startingX, startingY, behaviour);
        Vector3 position = this.transform.position;
        position.z = 0;
        this.transform.position = position;
        myParticleSystem.Stop();
        isActive = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.instance.active && !isActive)
        {
            myParticleSystem.Play();
            isActive = true;
        }
        else if (!GameManager.instance.active && isActive)
        {
            myParticleSystem.Stop();
            teleportSound.Play();
            isActive = false;
        }
    }
}
