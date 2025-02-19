using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Spellbase : MonoBehaviour
{
    // Start is called before the first frame update

    public abstract int GetDirection();
    public abstract void SetSpell(Spell spell);
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
