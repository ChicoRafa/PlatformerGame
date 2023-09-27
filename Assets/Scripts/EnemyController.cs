using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public float speed = 2f;
    public float distance = 3f;
    private float positionLeft, positionRight;
    public bool isMovingRight = true;
    public SpriteRenderer spriteR;
    // Start is called before the first frame update
    void Start()
    {
        spriteR = gameObject.GetComponent<SpriteRenderer>();
        positionLeft = gameObject.transform.position.x - distance;
        positionRight = gameObject.transform.position.x + distance;
    }

    // Update is called once per frame
    void Update()
    {
        //manejo de la dirección y velocidad de movimiento
        if (isMovingRight)
        {
            gameObject.transform.Translate(Vector2.right * speed * Time.deltaTime);
        }
        else
        {
            gameObject.transform.Translate(Vector2.left * speed * Time.deltaTime);
        }
        //manejo del cambio de dirección
        if (transform.position.x >= positionRight)
        {
            isMovingRight = false;
            spriteR.flipX = false;
        }
        if (transform.position.x <= positionLeft)
        {
            isMovingRight = true;
            spriteR.flipX = true;
        }
    }
    //Método que controla las colisiones con los pinchos
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Spikes")
        {
            Physics2D.IgnoreCollision(gameObject.GetComponent<Collider2D>(), collision.gameObject.GetComponent<Collider2D>(), true);
        }
    }
}
