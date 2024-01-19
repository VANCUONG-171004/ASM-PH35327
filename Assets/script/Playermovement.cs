using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement; 

public class Playermovement : MonoBehaviour
{
    public float speed;
    private Rigidbody2D rb;

    private Animator animator;

    float horizontal;
    bool isfaisingRight = false;


    float jumpower = 7f;

    public int tao = 0;
    public TextMeshProUGUI quatao;


    public GameObject gameover;

    private enum MovementState { idle, running, jumping, falling }
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

    }

    // Update is called once per frame
    void Update()
    {
        if ( rb.velocity.x<-9f || rb.velocity.x >40f)
        {
            return;
        }
        
        horizontal = Input.GetAxisRaw("Horizontal");
        
        rb.velocity = new Vector2(horizontal * speed, rb.velocity.y);

        
        

        quaydau();

        if (Input.GetKeyDown(KeyCode.Space))
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpower);

        }

        updateAniamtionState();

        

    }

    void quaydau()
    {
        if (horizontal < 0f && !isfaisingRight || horizontal > 0 && isfaisingRight)
        {
            isfaisingRight = !isfaisingRight;
            Vector3 ls = transform.localScale;
            ls.x *= -1f;
            transform.localScale = ls;


        }
    }

    private void updateAniamtionState()
    {
        MovementState state;

        if (horizontal > 0f)
        {
            state = MovementState.running;
        }
        else if (horizontal < 0f)
        {
            state = MovementState.running;
        }
        else
        {
            state = MovementState.idle;
        }

        if (rb.velocity.y > .1f)
        {
            state = MovementState.jumping;

        }
        else if (rb.velocity.y < -.1f)
        {
            state = MovementState.falling;
        }

        animator.SetInteger("state", (int)state);


    }

    void GameOver(){
        gameover.SetActive(true);
    }


    public void ResetGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        rb.bodyType = RigidbodyType2D.Dynamic;
    }
    private void OnTriggerEnter2D(Collider2D other)
    {

        if (other.gameObject.tag == "trap")
        {
            animator.SetTrigger("hit");
            rb.bodyType = RigidbodyType2D.Static;
            GameOver();
        }

        if(other.gameObject.tag == "tao")
        {
            tao++;
            quatao.SetText(tao.ToString());
            Destroy(other.gameObject);
        }

        if (other.gameObject.tag == "saw")
        {
            animator.SetTrigger("hit");
            rb.bodyType = RigidbodyType2D.Static;
            GameOver();
        }

       
    }

}
