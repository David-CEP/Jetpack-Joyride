using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class jugador : MonoBehaviour
{
    public TMP_Text jsonScore;
    public TMP_Text highScore;
    public TMP_Text coins;
    public bala balaObj;
    public bool movingBullet = false;
    public float monedas = 0;

    private void Start()
    {
        string tempHighScore = File.ReadAllText(Application.dataPath + "/scoreSaver.json");
        scoreObj highScoreCheck = JsonUtility.FromJson<scoreObj>(tempHighScore);
        highScore.text = highScoreCheck.score.ToString();
        coins.text = highScoreCheck.coins.ToString();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return) && !movingBullet)
        {
            Vector3 setPos = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, gameObject.transform.position.z);
            balaObj.transform.position = setPos;
            balaObj.GetComponent<Rigidbody2D>().velocity = Vector2.right * 8f;
            movingBullet = true;
        }
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
            string tempHighScore = File.ReadAllText(Application.dataPath + "/scoreSaver.json");
            scoreObj highScoreCheck = JsonUtility.FromJson<scoreObj>(tempHighScore);
            scoreObj tempScore = new scoreObj();
            tempScore.score = jsonScore.text;
            tempScore.coins = coins.text;
            if(int.Parse(highScoreCheck.score) < int.Parse(tempScore.score))
            {
                string score = JsonUtility.ToJson(tempScore, true);
                File.WriteAllText(Application.dataPath + "/scoreSaver.json", score);
            }
            SceneManager.LoadScene(0);
        }
        if(collision.gameObject.tag == "Coin")
        {
            Debug.Log(int.Parse(coins.text));
            collision.gameObject.SetActive(false);
        }
    }
}