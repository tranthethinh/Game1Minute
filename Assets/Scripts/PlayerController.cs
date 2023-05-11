using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Vector3 movement;
    private Rigidbody playerRb;
    private float speed = 5f;
    public float jumpForce = 1.0f;
    private float yBound=20f;
    private bool spaceKeyPressed = false;
    public AudioClip explode;
    public AudioClip water;
    public AudioClip normal;
    private GameManager gameManger;
    private bool ableJumps = false;//play can jump when touch Passable Ground
    private float maxSpeed = 22;

    // Start is called before the first frame update
    void Start()
    {
        playerRb = gameObject.GetComponent<Rigidbody>();
        gameManger = GameObject.Find("Game Manager").GetComponent<GameManager>();

    }

    // Update is called once per frame
    private void Update()
    {
        if (gameManger.isGameActive)
        {
            float horizontalInput = Input.GetAxis("Horizontal");
            float verticalInput = Input.GetAxis("Vertical");
            movement = new Vector3(horizontalInput, 0f, verticalInput);
            if (Input.GetKeyDown(KeyCode.Space)&&ableJumps)
            {
                spaceKeyPressed = true;
                ableJumps = false;
            }
        }
        else
        {
            playerRb.velocity = Vector3.zero;
        }

        if (playerRb.velocity.magnitude > maxSpeed)
        {
            playerRb.velocity = Vector3.ClampMagnitude(playerRb.velocity, maxSpeed);
        }
    }
    void FixedUpdate()
    {
        MovePlayer(movement);
        if (transform.position.y > yBound || transform.position.y < -0.5f)
        {
            ResetPositon();
        }
    }
    void MovePlayer(Vector3 movement)
    {


        playerRb.AddForce(movement * speed, ForceMode.Acceleration);
        //transform.Translate(movement * speed * Time.deltaTime);

        if (spaceKeyPressed)
        {
            playerRb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            spaceKeyPressed = false;
        }
    }
    void ResetPositon()
    {
        transform.position = new Vector3 (0,1,0);
        playerRb.velocity = Vector3.zero;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Explode"))
        {

            Vector3 awayFromEnemy = (transform.position - collision.gameObject.transform.position).normalized;
            
            playerRb.AddForce(awayFromEnemy *  48, ForceMode.Impulse);

        }

        if (collision.gameObject.CompareTag("Goal"))
        {
            gameManger.GameWin();
            

        }
        if (collision.gameObject.CompareTag("Explode"))
        {
            AudioSource.PlayClipAtPoint(explode, transform.position);
        }
        if (collision.gameObject.CompareTag("Passable Ground"))
        {
            AudioSource.PlayClipAtPoint(water, transform.position);
        }
        if (collision.gameObject.CompareTag("Normal Ground"))
        {
            AudioSource.PlayClipAtPoint(normal, transform.position);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Passable Ground"))
        {
            AudioSource.PlayClipAtPoint(water, transform.position);
            ableJumps = true;
        }
        
    }
}
