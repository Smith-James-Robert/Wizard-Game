using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ActionPointManager : MonoBehaviour
{
    [SerializeField]
    private int _maxAP;
    [SerializeField]
    private int _currentAP;
    [SerializeField]
    private Image AP;
    [SerializeField]
    private Transform APContainer;

    [SerializeField]
    private Sprite fullAP;
    [SerializeField]
    private Sprite emptyAP;


    private List<Image> actionPoints = new List<Image>();

    public int PlayerAP
    {
        get { return _currentAP; }
        set
        {
            _currentAP = Mathf.Clamp(value, 0, _maxAP);
            updateAP();

        }
    }
    public int MaxAP
    {
        get { return _maxAP; }
        set
        {
            _maxAP = Mathf.Max(0, value);
            setMaxAP(MaxAP);
        }
    }
    private void updateAP()
    {
        for (int i = 0; i < actionPoints.Count; i++)
        {
            if (i<_currentAP)
            {
                actionPoints[i].sprite = fullAP;
            }
            else
            {
                actionPoints[i].sprite=emptyAP;
            }
        }
    }
    private void setMaxAP(int maxAP)
    {

        int actionPointCount = actionPoints.Count;
        if (maxAP > actionPointCount)
        {
            int difference = maxAP - actionPointCount;
            for (int i = 0; i < difference; i++)
            {
                actionPoints.Add(Instantiate(AP.gameObject, APContainer).GetComponent<Image>());
            }
            updateAP();
        }
        else if (maxAP < actionPointCount)
        {
            int difference = actionPointCount - maxAP ;
            for (int i = 0; i < difference; i++)
            {
                int lastIndex = actionPoints.Count - 1;
                Destroy(actionPoints[lastIndex].gameObject);
                actionPoints.RemoveAt(lastIndex);
            }
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        PlayerAP = PlayerAP;
        MaxAP = MaxAP;
    }

    // Update is called once per frame
    void Update()
    {

    }
}
