using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public const int HEIGHT = 20;
    public const int WIDTH = 38;
    public const float SIZE = 0.5f;

    [SerializeField]
    private GameObject roomSpawner;
    public static GameManager instance = null;
    public int currentTurn = 0;
    [SerializeField]
    private int _maxAP;
    [SerializeField]
    public GameObject playerObject;
    public CharacterBase player;
    private int _ap;
    public Grid grid;
    public bool active = false;// Temporary for assignment
    public bool playerCasting = false;
    public int actionPoints
    {
        get { return _ap;}
        set { _ap = Mathf.Clamp(value,0, _maxAP); }
    }
    public int maxAP
    {
        get { return _maxAP; }
        set {
            _maxAP = Mathf.Max(value,0);
            _ap = Mathf.Clamp(_ap,0, _maxAP);
        }
    }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            Vector3 location;

            location = Camera.main.ScreenToWorldPoint(Vector3.zero);
            location.z += 20;
            grid = new Grid(WIDTH, HEIGHT, SIZE, location);
            Instantiate(roomSpawner);
            TurnStorage.currentTurn = 0;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }
    public void GetPlayer()
    {
        player = playerObject.GetComponent<PlayerMovement>().character;
    }
}
