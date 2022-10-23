using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rd2d;
    

    public float speed;

     public float jump = 3;

     // Animation
     public Animator animator;
     private bool facingRight = true;

    // score and lives text
    public TextMeshProUGUI scoretext;
    public TextMeshProUGUI livesText;

    private int lives;

    private int scoreValue;

    // Win lose text
     public GameObject winTextObject;
     public GameObject loseTextObject;
     //Ground check stuff
     private bool isOnGround;
    public Transform groundcheck;
    public float checkRadius;
    public LayerMask allGround;


    // Start is called before the first frame update
    void Start()
    {
         rd2d = GetComponent<Rigidbody2D>();
        scoreValue = 0;
        
        rd2d = GetComponent<Rigidbody2D>();
        lives = 3;
        
         SetCountText();
        winTextObject.SetActive(false);
         
         SetCountText();
        loseTextObject.SetActive(false);
    }

    // Update is called once per frame

     void SetCountText()
    {
        scoretext.text = "Score: " + scoreValue.ToString();
        if (scoreValue >= 8 )
        {
            winTextObject.SetActive(true);
            
       
            SoundManagerScript.PlaySound("WinMusic");
        }
            
            scoretext.text = "Score: " + scoreValue.ToString();
            if (scoreValue == 4) 
        {
            transform.position = new Vector2(0f, 103f);
            lives = 3;
        }
         livesText.text = "lives " + lives.ToString();
         if (lives == 0)
        {
            loseTextObject.SetActive(true);
            Destroy(gameObject);
        }
    }
    
    void FixedUpdate()
    {
       // Movement 
       float hozMovement = Input.GetAxis("Horizontal");
       float verMovement = Input.GetAxis("Vertical");
       rd2d.AddForce(new Vector2(hozMovement * speed,  verMovement * speed)); 

        // Ground check stuff
        isOnGround = Physics2D.OverlapCircle(groundcheck.position, checkRadius, allGround);

        // Animation
        animator.SetFloat("Hoz", Mathf.Abs(Input.GetAxis("Horizontal")));
        animator.SetFloat("Vert", Input.GetAxis("Vertical"));
        
        if (facingRight == false && hozMovement > 0)
        {
            Flip();
        }

        else if (facingRight == true && hozMovement < 0)
        {
            Flip();
        }   
    }
     private void OnCollisionEnter2D(Collision2D collision)
    {
       if (collision.collider.tag == "Coin")
        {
            scoreValue += 1;
            SetCountText();
            Destroy(collision.collider.gameObject);
        }

        

         if (collision.collider.tag == "Enemy") 
        {
            lives -= 1;
            livesText.text = "lives " + lives.ToString();
            Destroy(collision.collider.gameObject);
            
            SetCountText();
        }
     }
     private void OnCollisionStay2D(Collision2D collision)
    {  
         if (collision.collider.tag == "Ground" && isOnGround)
            {
                if (Input.GetKey(KeyCode.W))
                {
                    rd2d.AddForce(new Vector2(0f, jump), ForceMode2D.Impulse);
                }
            }
    }

     // Close Application
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
    }
    
    void Flip()
    {
        facingRight = !facingRight;
        Vector2 Scaler = transform.localScale;
        Scaler.x = Scaler.x * -1;
        transform.localScale = Scaler;
    }
}
