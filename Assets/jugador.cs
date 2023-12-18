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
    public float velocity = 6f;
    public bool power = false;

    private void Start()
    {
        string tempHighScore = File.ReadAllText(Application.dataPath + "/scoreSaver.json");
        scoreObj highScoreCheck = JsonUtility.FromJson<scoreObj>(tempHighScore);
        highScore.text = highScoreCheck.score.ToString();
        coins.text = highScoreCheck.coins.ToString();
        monedas = int.Parse(coins.text);
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

        if (!power)
        {
            Move();
        }else
        {
            MoveNegative();
        }
    }
    
    private void Move()
    {
        Vector2 tempPos = gameObject.GetComponent<Rigidbody2D>().velocity;
        tempPos.y = Input.GetAxis("Jump") * velocity;
        gameObject.GetComponent<Rigidbody2D>().velocity = tempPos;
    }

    private void MoveNegative()
    {
        Vector2 tempPos = gameObject.GetComponent<Rigidbody2D>().velocity;
        tempPos.y = Input.GetAxis("JumpNegative") * velocity;
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
            int puntuacionTemp = int.Parse(tempScore.score);
            int monedasTemp = int.Parse(tempScore.score);
            if(int.Parse(highScoreCheck.score) < puntuacionTemp)
            {
                string score = JsonUtility.ToJson(tempScore, true);
                File.WriteAllText(Application.dataPath + "/scoreSaver.json", score);
            }
            SceneManager.LoadScene(0);
        }
        if(collision.gameObject.tag == "Coin")
        {
            monedas++;
            coins.text = monedas.ToString();
            string tempHighScore = File.ReadAllText(Application.dataPath + "/scoreSaver.json");
            scoreObj tempScore = new scoreObj();
            tempScore.score = jsonScore.text;
            tempScore.coins = coins.text;
            string score = JsonUtility.ToJson(tempScore, true);
            File.WriteAllText(Application.dataPath + "/scoreSaver.json", score);
            collision.gameObject.SetActive(false);
            collision.gameObject.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            collision.gameObject.transform.position = new Vector2(10, Random.Range(-4, 4));
            collision.gameObject.SetActive(true);
        }
        if(collision.gameObject.tag == "Turbo")
        {
            Time.timeScale = 2f;
            collision.gameObject.SetActive(false);
            collision.gameObject.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            collision.gameObject.transform.position = new Vector2(10, Random.Range(-4, 4));
            collision.gameObject.SetActive(true);
        }
        if(collision.gameObject.tag == "Controles")
        {
            power = true;
            gameObject.GetComponent<Rigidbody2D>().gravityScale *= -1;
            collision.gameObject.SetActive(false);
            collision.gameObject.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            collision.gameObject.transform.position = new Vector2(10, Random.Range(-4, 4));
            collision.gameObject.SetActive(true);
        }
    }
}

/*
powerups:
iman

powerup negativo:
inversion controles
*/