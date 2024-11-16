using System;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class SpaceShipComponent : MonoBehaviour
{
    public Rigidbody2D SpaceShip;
    private Vector2 Angle = new Vector2(0, 0.75f);
    public float Speed = 2f;
    private float speedIncreaseRate = 0.1f; 
    private float maxSpeed = 10f;
    public TextMeshProUGUI scoretext;
    public float playerScore = 0;
    public LogicMnagerScript logic;
    public TextMeshProUGUI speedtext;
    bool isalive = true;

    public bool spaceisalive = true  ; 

    void Start()
    {
        Speed = 2f;
        playerScore = 0;
        logic = GameObject.FindGameObjectWithTag("Logic").GetComponent<LogicMnagerScript>();
        transform.Rotate(new Vector3(0, 0, 45) );
        speedtext.text = "0";
        speedtext.text = "0";
        isalive = true;
    }

    void Update()
    {
        // Toggle direction when space is pressed
        if (isalive)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                Angle = Angle == new Vector2(0, 0.75f) ? Vector2.down : new Vector2(0, 0.75f);
                transform.Rotate(Angle == new Vector2(0, 0.75f) ? new Vector3(0, 0, 90) : new Vector3(0, 0, -90));
            }

            if (Speed < maxSpeed)
            {
                Speed += speedIncreaseRate * Time.deltaTime * 3;
            }
            playerScore += 0.001f;
            scoretext.text = Math.Round(playerScore).ToString();
            speedtext.text = Math.Round(Speed).ToString();
            // Move the spaceship
            SpaceShip.position = SpaceShip.position + (Angle * 100 * Speed) * Time.deltaTime;
        }
    }
    // void OnCollisionEnter2D(Collision2D other)
    // {
    //     if (other.gameObject.layer== 6 )
    //     {
    //                 logic.gameOver();
    //                 spaceisalive =false;
    //     }
 
    // }

    void OnTriggerEnter2D(Collider2D other)
    {

        logic.gameOver();
        isalive = false;
        SpaceShip.gravityScale=400;
        
    }
}
