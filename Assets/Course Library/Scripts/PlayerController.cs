using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PlayerController : MonoBehaviour
{
    public Text text;
    private Rigidbody playerRb;
    private Animator playerAnim;
    public ParticleSystem deathParticle;
    public ParticleSystem dirtParticle;
    private AudioSource playerAudio;
    public AudioClip jumpSound;
    public AudioClip DeathSound;
    public float jumpForce;
    public float gravityModifier;
    public bool isJumped = true;
    public bool gameOver;
    public AudioSource backgroundSound;
    public int doubleJump = 2;
    public float score = 0;
    public float pointForSecond = 5;


 
    
    
    // Start is called before the first frame update
    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
        Physics.gravity *= gravityModifier;
        playerAnim = GetComponent<Animator>();
        playerAudio = GetComponent<AudioSource>();
        backgroundSound = GameObject.Find("Main Camera").GetComponent<AudioSource>();
        
        
        
        
        
    }

    // Update is called once per frame
    void Update()
    { 
            if (gameOver == false)
            {
                Jumping();
            }
            text.text = Mathf.Round(score).ToString();

            RunFast();

            score += Time.deltaTime;

       



    }
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Ground"))
        {
            isJumped = true;
            dirtParticle.Play();
            doubleJump = 2;
            
        }
        else if (collision.gameObject.CompareTag("Obstacle"))
        {
            Debug.Log("Game-Over");
            gameOver = true;
            playerAnim.SetBool("Death_b", true);
            playerAnim.SetInteger("DeathType_int", 1);
            deathParticle.Play();
            dirtParticle.Stop();
            playerAudio.PlayOneShot(DeathSound, 1.0f);
            backgroundSound.Stop();
            
        }
        
    }
    private void Jumping()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isJumped && doubleJump == 2)
        {
            playerRb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            
            playerAnim.SetTrigger("Jump_trig");
            dirtParticle.Stop();
            playerAudio.PlayOneShot(jumpSound,1.0f);
            doubleJump--;

           
        }
        else if (Input.GetKeyDown(KeyCode.Space) && isJumped && doubleJump == 1)
        {
            playerRb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            playerAnim.SetTrigger("Jump_trig");
            dirtParticle.Stop();
            playerAudio.PlayOneShot(jumpSound, 1.0f);
            doubleJump--;
        }
        else if (doubleJump == 0)
        {
            isJumped = false;
        }

    }
    private void RunFast()

    {  if(Input.GetKey(KeyCode.F))
        {
            playerAnim.SetFloat("Speed_f", 3);
            score += 3 * Time.deltaTime;
        }
    else if (Input.GetKeyUp(KeyCode.F)) {
            playerAnim.SetFloat("Speed_f", 1);
            score += Time.deltaTime;
        
        }
    
       
       
    }
    
}
