// Created by: Jeremy Fischer 11/3/2021
// Edited by (Add name if you do anything to this script): Jeremy Fischer, Madeline Ellingson
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    // Private Variables
    private Rigidbody2D rb;
    private Collider2D coll;
    [SerializeField] private LayerMask Ground;
    private Animator anim;
    private enum State { idle, run, jump, falling, hurt};
    private State state = State.idle;
    [SerializeField] private int healthPoints = 3;

    // Movement
    [SerializeField] float moveForce = 5.0f;
    [SerializeField] float jumpForce = 10.0f;
    [SerializeField] float hurtForce = 10.0f;
    [SerializeField] float hurtForceVertical = 3f;
    [SerializeField] float hurtTime = .5f;
    private float hurtTimer = 0;

    //Sound
    [SerializeField] private AudioSource jumpSoundEffects;
    [SerializeField] private AudioSource collectionSoundEffects;

    // Collectables
    [SerializeField] private int cherries = 0;
    [SerializeField] private int strawberries = 0;
    [SerializeField] private int bananas = 0;
    [SerializeField] private int kiwis = 0;
    [SerializeField] private int oranges = 0;
    [SerializeField] private int pineapples = 0;
    [SerializeField] private int melons = 0;
    [SerializeField] private int apples = 0;

    // Num of Collectables to display
    [SerializeField] private TextMeshProUGUI cherryText;
    [SerializeField] private TextMeshProUGUI strawberryText;
    [SerializeField] private TextMeshProUGUI bananaText;
    [SerializeField] private TextMeshProUGUI kiwiText;
    [SerializeField] private TextMeshProUGUI orangeText;
    [SerializeField] private TextMeshProUGUI pineappleText;
    [SerializeField] private TextMeshProUGUI melonText;
    [SerializeField] private TextMeshProUGUI appleText;

    // UI
    [SerializeField] private DialogueUI dialogueUI;
    public DialogueUI DialogueUI => dialogueUI;
    public IInteractable Interactable { get; set; }

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        coll = GetComponent<Collider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        // Interactions for dialogue
        if (Input.GetKeyDown(KeyCode.E))
        {
            if(Interactable != null)
            {
                Debug.Log("The interactible was not null");
                Interactable.Interact(this);
            }
        }

        if(state != State.hurt)
        {
            Movement();
            VelocityState();
        }
        else
        {
            // The player is hurt, count down the timer
            if (hurtTimer > 0)
            {
                hurtTimer -= Time.deltaTime;
            }
            else
            {
                state = State.idle;
            }  
        }
        anim.SetInteger("State", (int)state);
    }

    // To collect fruits and destroy on impact
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Collectable")
        {
            // Destroy the game object
            Destroy(collision.gameObject);

            // Collecting sound
            collectionSoundEffects.Play();

            // Assign the name of the game object to a variable
            string collectableName = collision.name;

			// Add to the correct fruit and display on the screen
			if (collectableName.Contains("Cherry"))
			{
                cherries += 1;
                cherryText.text = cherries.ToString();
            }
            else if (collectableName.Contains("Strawberry"))
			{
                strawberries += 1;
                strawberryText.text = strawberries.ToString();
            }
            else if (collectableName.Contains("Banana"))
			{
                bananas += 1;
                bananaText.text = bananas.ToString();
            }
            else if (collectableName.Contains("Kiwi"))
			{
                kiwis += 1;
                kiwiText.text = kiwis.ToString();
            }
            else if (collectableName.Contains("Orange"))
			{
                oranges += 1;
                orangeText.text = oranges.ToString();
            }
            else if (collectableName.Contains("Pineapple"))
			{
                pineapples += 1;
                pineappleText.text = pineapples.ToString();
            }
            else if (collectableName.Contains("Melon"))
			{
                melons += 1;
                melonText.text = melons.ToString();
            }
			else
			{
                apples += 1;
                appleText.text = apples.ToString();
            }
        }

    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.tag == "Enemy")
        {
            if(state == State.falling)
            {
                Destroy(other.gameObject);
            }
            else
            {
                Debug.Log("Player now hurt");
                healthPoints -= 1;
                // If the player gets below or at 0 hp then reload current scene.
                if(healthPoints <=0)
                {
                    SceneManager.LoadScene(SceneManager.GetActiveScene().name);
                }
                state = State.hurt;
                hurtTimer = hurtTime;
                if (other.gameObject.transform.position.x < transform.position.x)
                {
                    Debug.Log("Player push to right: ");
                    // enemy is to the right
                    rb.velocity = new Vector2(hurtForce, rb.velocity.y + hurtForceVertical);
                }
                else
                {
                    Debug.Log("Player push to left");
                    // enemy must be left
                    rb.velocity = new Vector2(-hurtForce, rb.velocity.y + hurtForceVertical);
                }
            }
        }
    }

    // Determines the animation played based off the state of the character
    private void VelocityState()
	{
        if (state == State.jump)
        {
            if (rb.velocity.y < .1f)
            {
                state = State.falling;
            }
        }
        else if (state == State.falling)
        {
            if (coll.IsTouchingLayers(Ground))
            {
                state = State.idle;
            }
        }
        else if (Mathf.Abs(rb.velocity.x) > 2f)
        {
            state = State.run;
        }
        else if (rb.velocity.y < 0f)
        {
            state = State.falling;
        }
        else
        {
            state = State.idle;
        }
    }

    private void Movement()
    {
        // Checks the horizontal input, from the general unity input
        float hDirection = Input.GetAxis("Horizontal");

        if (hDirection > 0)
        {
            rb.velocity = new Vector2(moveForce, rb.velocity.y);
            transform.localScale = new Vector2(1, 1);
        }
        else if (hDirection < 0)
        {
            rb.velocity = new Vector2(-moveForce, rb.velocity.y);
            transform.localScale = new Vector2(-1, 1);
        }
        else
        {
            rb.velocity = new Vector2(0, rb.velocity.y);
        }

        // Jump
        if (Input.GetButtonDown("Jump") && coll.IsTouchingLayers(Ground))
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            state = State.jump;
            jumpSoundEffects.Play();
        }
    }
}
