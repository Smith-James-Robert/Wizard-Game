using UnityEngine;
//This code is for the assignment.
public class TriggerBasedPortal : MonoBehaviour
{
    public int linkedX, linkedY;
    public int positionX, positionY;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<PlayerMovement>())
        {
            Vector2 position = collision.transform.position;
            Tile t = collision.GetComponent<PlayerMovement>().character;
            GameManager.instance.grid.MoveTileToLocation(positionX, positionY, linkedX, linkedY, t.id);
            t.location.y = linkedY;
            t.location.x = linkedX;
            position = GameManager.instance.grid.GetCentralWorldPosition(linkedX, linkedY);
            collision.transform.position = position;
        }

    }
}
