using UnityEngine;
using System.Collections;

public class AnimateButton: MonoBehaviour
{


    public Animator animator;
    public AudioSource audioData;

    // Use this for initialization
    void Start()
    {
        audioData = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        animator.SetBool("isPressed", GameManager.instance.active);

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<PlayerMovement>())
        {

            GameManager.instance.active = !GameManager.instance.active;
            audioData.Play();
        }
    }   

}

