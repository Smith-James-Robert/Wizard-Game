using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine.UIElements;

public class PlayerMovement : MonoBehaviour
{
   
    public int startingX,startingY;
    private ActionPointManager actionPointManager;
    public CharacterBase character;
    [SerializeField]
    private Spellcasting spellcasting;
    public AudioSource audioData;
    private bool casting;
    // Use this for initialization
    void Awake()
    {
        actionPointManager = GameManager.instance.gameObject.GetComponent<ActionPointManager>();
        character = new CharacterBase((startingX, startingY),this.gameObject, false, true);
        this.transform.position = GameManager.instance.grid.GetCentralWorldPosition(startingX, startingY);
        Vector3 position = this.transform.position;
        position.z = -1;
        this.transform.position = position;
        casting = false;
        //audioData=GetComponent<AudioSource>();
        audioData.volume = 0.25f;
    }
    private void Start()
    {
        GameManager.instance.grid.AddValue(startingX, startingY,character);
        GameManager.instance.GetPlayer();
        spellcasting  = gameObject.GetComponent<Spellcasting>();

    }
    public void TakeAction(int apCost)
    {
        actionPointManager.PlayerAP = Mathf.Max(actionPointManager.PlayerAP - apCost, 0);
        CheckTurn();
    }
    private void CheckTurn()
    {
        if (actionPointManager.PlayerAP==0)
        {
            GameManager.instance.currentTurn++;
            TurnStorage.currentTurn++;
            actionPointManager.PlayerAP = actionPointManager.MaxAP;
        }
    }
    private bool CanMove(int x, int y)
    {
        return GameManager.instance.grid.GetWalkable(character.location.x + x, character.location.y + y);
    }
    private int MoveTime(int x, int y)
    {
        return GameManager.instance.grid.GetDifficulty(character.location.x + x, character.location.y + y)+1;

    }
    // Update is called once per frame
    void Update()
    {
        casting = GameManager.instance.playerCasting;
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            if (casting)
            {
                spellcasting.CastSpell(-1,0);
                GameManager.instance.playerCasting = false;
            }
            else
            {
                if (CanMove(-1, 0))
                {
                    Vector3 position = this.transform.position;
                    GameManager.instance.grid.MoveTile(character.location.x, character.location.y, -1, 0, character.id);
                    position.x-= GameManager.SIZE;
                    character.location.x--;
                    this.transform.position = position;
                    TakeAction(MoveTime(0, 0));
                    character.Collide();
                    audioData.Play(0);

                }
            }


        }
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            if (casting)
            {
                spellcasting.CastSpell(1, 0);
                GameManager.instance.playerCasting = false;
            }
            else
            {
                if (CanMove(1, 0))
                {
                    Vector3 position = this.transform.position;
                    GameManager.instance.grid.MoveTile(character.location.x, character.location.y, 1, 0, character.id);
                    position.x+= GameManager.SIZE;
                    character.location.x++;
                    this.transform.position = position;
                    TakeAction(MoveTime(0, 0));
                    character.Collide();
                    audioData.Play(0);

                }
                else
                {
                    //Play sound
                }
            }


        }
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            if (casting)
            {
                spellcasting.CastSpell(0, 1);
                GameManager.instance.playerCasting = false;
            }
            else
            {
                if (CanMove(0, 1))
                {
                    Vector3 position = this.transform.position;
                    GameManager.instance.grid.MoveTile(character.location.x, character.location.y, 0, 1, character.id);
                    position.y+=GameManager.SIZE;
                    character.location.y++;
                    this.transform.position = position;
                    TakeAction(MoveTime(0, 0));
                    character.Collide();
                    audioData.Play(0);
                }
                else
                {
                    //Play sound
                }
            }

        }
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            if (casting)
            {
                spellcasting.CastSpell(0, -1);
                GameManager.instance.playerCasting = false;
            }
            else
            {
                if (CanMove(0, -1))
                {
                    Vector3 position = this.transform.position;
                    GameManager.instance.grid.MoveTile(character.location.x, character.location.y, 0, -1, character.id);
                    position.y-= GameManager.SIZE;
                    character.location.y--;
                    this.transform.position = position;
                    TakeAction(MoveTime(0, 0));
                    character.Collide();
                    audioData.Play(0);

                }
                else
                {
                    //Play sound
                }
            }
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            TakeAction(99);
            character.Collide();


        }
        //findnerd.com/list/view/Unity3D-Moving-an-Object-with-Keyboard-Input/65/#sthash.wMbajIS4.dpuf


    }
}
