using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class obstaculos : MonoBehaviour
{
    public TMP_Text score;
    public int scoreInt = 0;
    public int hitCounterCoin = 0;
    public int hitCounterPower = 0;
    public coin coinObj;
    public turboObj turboObj;
    public controlesObj controlesObj;
    public jugador Player;
    public int powerReset;

    private void Start()
    {
        gameObject.transform.localScale = new Vector3(gameObject.transform.localScale.x, Random.Range(3f, 7f), gameObject.transform.localScale.z);
    }

    private void Update()
    {
        gameObject.GetComponent<Rigidbody2D>().velocity = Vector2.left * 4f;

        if (hitCounterCoin == 3)
        {
            coinObj.GetComponent<Rigidbody2D>().velocity = Vector2.left * 5f;
            hitCounterCoin = 0;
        }

        if(hitCounterPower == 7)
        {
            if (Random.Range(-1,1) >= 0)
            {
                turboObj.GetComponent<Rigidbody2D>().velocity = Vector2.left * 5f;
            }
            else
            {
                controlesObj.GetComponent<Rigidbody2D>().velocity = Vector2.left * 5f;
            }
            hitCounterPower = 0;
        }

        if(powerReset == 5)
        {
            Time.timeScale = 1f;
            Player.power = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Respawn")
        {
            gameObject.SetActive(false);
            Quaternion resetRot = gameObject.transform.localRotation;
            resetRot.z = 0f;
            gameObject.transform.localRotation = resetRot;
            gameObject.transform.position = new Vector3(10f, Random.Range(-3.86f, 4.5f), 0f);
            gameObject.transform.localScale = new Vector3(gameObject.transform.localScale.x, Random.Range(3f, 7f), gameObject.transform.localScale.z);
            Quaternion tempRot = gameObject.transform.localRotation;
            tempRot.z = Random.Range(-50f, -50f);
            gameObject.transform.localRotation = tempRot;
            gameObject.SetActive(true);
        }

        if(collision.gameObject.tag == "Score")
        {
            if (Time.timeScale == 2f || Player.GetComponent<Rigidbody2D>().gravityScale < 0)
            {
                powerReset++;
                hitCounterCoin++;
                scoreInt++;
                score.text = scoreInt.ToString();
            }
            else
            {
                hitCounterCoin++;
                hitCounterPower++;
                scoreInt++;
                score.text = scoreInt.ToString();
            }
        }
    }
}
