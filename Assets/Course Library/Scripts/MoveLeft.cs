using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveLeft : MonoBehaviour
{
    private float speed = 30.0f;
    private PlayerController stopMove;
    private float leftBound = -15.0f;
    
    // Start is called before the first frame update
    void Start()
    {
        stopMove = GameObject.Find("Player").GetComponent<PlayerController>();
        
    }

    // Update is called once per frame
    void Update()
    {  
       
            Run();

            StartGame();
       
        
        
        
    }
   private void Run ()
    {
        {
            if (Input.GetKey(KeyCode.F))
            {
                speed = 50.0f;
            }
            else if (Input.GetKeyUp(KeyCode.F))
            {
                speed = 30.0f;
            }



        }
    }
    private void StartGame()
    {
        if (stopMove.gameOver == false)
        {
            transform.Translate(Vector3.left * speed * Time.deltaTime);

        }

        if (transform.position.x < leftBound && gameObject.CompareTag("Obstacle"))
        {
            Destroy(gameObject);

        }
    }
}
