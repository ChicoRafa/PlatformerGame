using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float horizontalInput = 0f;
    public float speed = 5f;
    public PlayerMovement movement;
    public bool isAlive = true;
    public GameManager manager;
    public Animator anim;
    public AudioSource audioS;
    public AudioClip coinSound;
    public AudioClip hurtSound;
    public AudioClip jumpSound;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //mmovimiento horizontal del jugador
        horizontalInput = Input.GetAxisRaw("Horizontal");

        //llamada al script de salto
        if (Input.GetButtonDown("Jump") && isAlive)
        {
            if (movement.m_Grounded)
            {
                anim.SetTrigger("Jump");
                audioS.PlayOneShot(jumpSound, 1f);
            }
            movement.Jump();
        }
        //Set Animations
        anim.SetBool("Grounded", movement.m_Grounded);
        anim.SetBool("isAlive", isAlive);
        if (horizontalInput == 0)
        {
            anim.speed = 1f;
            anim.SetBool("Move", false);
        }
        else
        {
            if (isAlive && movement.m_Grounded)
            {
                anim.speed = 1 * Mathf.Abs(horizontalInput);
            }
            anim.SetBool("Move", true);
        }

    }
    //Método que hace que el movimiento no depende de tu máquina
    private void FixedUpdate()
    {
        //el deltatime viene a ser para que la velocidad sea constante
        if (isAlive) { movement.Move(horizontalInput * speed * Time.deltaTime); }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Cherry")
        {
            Debug.Log("Cherry Picked!");
            Destroy(collision.gameObject);
            manager.totalCoins++;
            audioS.PlayOneShot(coinSound, 1f);
        }

        if (collision.gameObject.tag == "PoisonedCherry")
        {
            Destroy(collision.gameObject);
            isAlive = false;
            anim.SetTrigger("Die");
            audioS.PlayOneShot(hurtSound, 1f);
        }

        if (collision.gameObject.tag == "CheckPoint")
        {
            manager.spawnPoint = collision.gameObject.transform;
        }

        if (collision.gameObject.tag == "LevelEnd")
        {
            manager.FinishLevel();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Spikes" || collision.gameObject.tag == "Enemy")
        {
            Die();
            //gameObject.GetComponent<Rigidbody2D>().gravityScale = 0f;
            gameObject.transform.Translate(Vector3.up * 10f * Time.deltaTime);
        }
        
        if (collision.gameObject.tag == "CliffSpikes")
        {
            Die();
            //gameObject.GetComponent<Rigidbody2D>().gravityScale = 0f;
            gameObject.transform.Translate(Vector3.up * 50f * Time.deltaTime);
        }

        if (collision.gameObject.tag == "WeakPoint")
        {
            //GetComponent<BoxCollider2D>().enabled = false;
           // StartCoroutine(EnableCollision(1.0F));
            collision.transform.parent.GetComponent<BoxCollider2D>().enabled = false;
            Destroy(collision.transform.parent.gameObject);
        }
    }

    public void Die()
    {
        isAlive = false;
        anim.SetTrigger("Die");
        audioS.PlayOneShot(hurtSound, 1f);
        GetComponent<BoxCollider2D>().enabled = false;
        GetComponent<CircleCollider2D>().enabled = false;
        StartCoroutine(EnableCollision(2.0F));
    }

    IEnumerator EnableCollision(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        GetComponent<BoxCollider2D>().enabled = true;
        GetComponent<CircleCollider2D>().enabled = true;
    }
}
