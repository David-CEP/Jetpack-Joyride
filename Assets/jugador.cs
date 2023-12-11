using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class jugador : MonoBehaviour
{
    void Update()
    {
        Move();
    }
    private void Move()
    {
        Vector2 tempPos = gameObject.GetComponent<Rigidbody2D>().velocity;
        tempPos.y = Input.GetAxis("Jump") * 6f;
        gameObject.GetComponent<Rigidbody2D>().velocity = tempPos;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Finish")
        {
            SceneManager.LoadScene(0);
        }
    }
}
