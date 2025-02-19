using UnityEngine;

public class Element
{
    const int damageMult = 1;
    public int damage;
    public int mobility;
    public int defence;
    public enum Elements
    {
        Fire,
        Water,
        Earth,
        Air
    }
    public Elements element;
    public Element(int element)
    {
        this.element = (Elements)element;
        damage = 0; mobility=0; defence = 0;
    }
    public Element(Element element)
    {
        this.element = element.element;
        this.damage = element.damage;
        mobility = element.mobility;
        defence = element.defence;
    }
    public int GetPrimaryDamage()
    {
        if (element == Elements.Fire)
        {
            damage = 6;
        }
        else if (element == Elements.Water)
        {
            damage = 2;
        }
        else if (element == Elements.Earth)
        {
            damage = 4;
        }
        else if (element == Elements.Air)
        {
            damage = 2;
        }
        Debug.Log("Element" + element);
        Debug.Log("Element Damage " +  damage); 
        return damage*damageMult;
    }
    public int GetPrimaryMobility()
    {
        if (element == Elements.Fire)
        {
            mobility = 4;
        }
        else if (element == Elements.Water)
        {
            mobility = 6;
        }
        else if (element ==Elements.Earth)
        {
            mobility = 2;
        }
        else if (element == Elements.Air)
        {
            mobility = 6;
        }
        return mobility;
    }
    public int GetPrimaryHealth()
    {
        if (element == Elements.Fire)
        {
            defence = 2;
        }
        else if (element == Elements.Water)
        {
            defence = 4;
        }
        else if (element== Elements.Earth)
        {
            defence = 6;
        }
        else if (element== Elements.Air)
        {
            defence = 2;
        }
        return defence;
    }
    public int GetElement()
    {
        return (int)element;
    }
}
