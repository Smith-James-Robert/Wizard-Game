using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class HealthManager : MonoBehaviour
{
    [SerializeField]
    private int _maxHealth;
    [SerializeField]
    private int _currentHealth;
    [SerializeField]
    private Image heart;
    [SerializeField]
    private Transform heartContainer;

    [SerializeField]
    private Sprite fullHeart;
    [SerializeField]
    private Sprite threeQuarterHeart;
    [SerializeField]
    private Sprite halfHeart;
    [SerializeField]
    private Sprite quarterHeart;
    [SerializeField]
    private Sprite emptyHeart;  

    private List<Image> hearts = new List<Image>();

    public int PlayerHealth
    {
        get { return _currentHealth; }
        set
        {
            _currentHealth = Mathf.Clamp(value, 0, _maxHealth);
            updateHealth();
            if (_currentHealth==0)
            {
                SceneManager.LoadScene("DeadScene");
            }
        }
    }
    public int MaxHealth
    {
        get { return _maxHealth; }
        set
        {
            _maxHealth = Mathf.Max(0, value);
            setMaxHealth(MaxHealth);
        }
    }
    private void updateHealth()
    {
        for (int i=0; i<hearts.Count; i++)
        {
            int remainderHealth = Mathf.Clamp(PlayerHealth - (i * 4), 0, 4);
            switch (remainderHealth)
            {
                case 0:
                    hearts[i].sprite = emptyHeart;
                    break;
                case 1:
                    hearts[i].sprite = quarterHeart;
                    break;
                case 2:
                    hearts[i].sprite = halfHeart;
                    break;
                case 3:
                    hearts[i].sprite = threeQuarterHeart;
                    break;
                case 4:
                    hearts[i].sprite = fullHeart;
                    break;
            }
        }
    }
    private void setMaxHealth(int maxHealth)
    {

        maxHealth= Mathf.CeilToInt(maxHealth / 4.0f);
        int heartCount = hearts.Count;
        if (maxHealth > heartCount)
        {
            int difference= maxHealth- heartCount;
            for (int i=0;i<difference;i++)
            {
                hearts.Add(Instantiate(heart.gameObject, heartContainer).GetComponent<Image>());
            }
            updateHealth();
        }
        else if (maxHealth < heartCount)
        {
            int difference = heartCount- maxHealth;
            for (int i=0;i<difference;i++)
            {
                int lastHeartIndex = hearts.Count - 1;
                Destroy(hearts[lastHeartIndex].gameObject);
                hearts.RemoveAt(lastHeartIndex);
            }
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        PlayerHealth = PlayerHealth;
        MaxHealth = MaxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
