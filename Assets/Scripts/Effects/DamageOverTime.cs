using UnityEngine;

public class DamageOverTime : MonoBehaviour
{
    // Start is called before the first frame update
    int damage;
    int duration;
    OwnTime ownTime;
    Health healthToHurt;
    public void Initialize(int damage,int duration,int currentTurn,Health target)
    {

        ownTime=gameObject.AddComponent(typeof(OwnTime)) as OwnTime;
        this.damage = damage;
        this.duration = duration;
        ownTime.currentTurn = currentTurn;
        healthToHurt = target;
    }
    // Update is called once per frame
    void Update()
    {
        if (ownTime.currentTurn < GameManager.instance.currentTurn)
        {
            if (healthToHurt != null)
            {
                healthToHurt.changeHealth(-damage);
                duration=0;
                ownTime.UpdateTurn();
                if (duration <= 0)
                {
                    Debug.Log("Destroying self timeout!?");
                    Object.Destroy(gameObject);
                    Object.Destroy(this);
                }
            }
            else
            {
                Debug.Log("Destroying self?");
                Object.Destroy(gameObject);
                Object.Destroy(this);
            }
        }
    }
}
