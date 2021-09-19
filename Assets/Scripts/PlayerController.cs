using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    float vMove;

    private Animator animator;    
    private CharacterController cc; 
    
    private Vector3 newPoz;
    private Vector3 moveDir = Vector3.zero;
    private Vector3 gravity = Vector3.zero;
    private bool isRunning = false;
    public static bool gameOver = false;

    [SerializeField]
    private float laneDistance;
    [SerializeField]
    //private int targetLane;
    private float targetLane;
    [SerializeField]
    //private int currentLane = 1;
    private float currentLane = 1;
    [SerializeField]
    private float LaneSpeed = 25;

    [SerializeField]
    private AnimationClip jump_Full;  
    public float animJumpLenght;

    private bool jumping = false; 
    private bool sliding = false; 
    public float jumpHeight;
    public float jumpForce;

    public ParticleSystem dustEffect;
    public GameObject pickUpCoin;
    public GameObject deathParticles;

    public float ratio;
    public float startPosition;
    private BoxCollider boxCollider;
    private Vector3 boxColliderSize;

    private UIManager uiManager;
    public AudioClip coinSound;
    public AudioSource[] Sounds;


    void Start()
    {
        animator = GetComponent<Animator>();        
        cc = GetComponent<CharacterController>();
        animJumpLenght = jump_Full.length;
        boxCollider = GetComponent<BoxCollider>();
        boxColliderSize = boxCollider.size;
        uiManager = FindObjectOfType<UIManager>();        
    }
        
    void PlaySound()
    {
        Sounds[1].Play();
    }
    void Score()
    {
        if (gameOver)
            return;
        ScoreScript.distanceValue += Time.deltaTime * WorldController.currSpeed;
    }   

    void Update()
    {
        if (gameOver == false)
        {            
            isRunning = true;

            Invoke("Score", 2);            

            moveDir = new Vector3(0, 0, 0);
            moveDir += gravity;

            vMove = Input.GetAxisRaw("Vertical");

            if (cc.isGrounded)
            {              
                gravity = Vector3.down;
                dustEffect.Play();
            }
            else
            {              
                gravity += Physics.gravity * 3 * Time.deltaTime;
            }

            if (!isRunning)
                return;
                           
            StartCoroutine(GoMove());
            
            if (jumping)
            {                    
                ratio = (transform.position.y - startPosition) / animJumpLenght;
                if (ratio >= 1f)
                {
                    jumping = false;
                    animator.SetBool("Jumping", false);
                }
                else
                {
                    gravity.y = Mathf.Lerp(transform.position.y, jumpHeight, jumpForce);                   
                }
            }

            if (sliding)
            {
                 StartCoroutine(GoSlide());
            }

            cc.Move(moveDir * Time.deltaTime);

            newPoz = transform.position;
            newPoz.x = Mathf.Lerp(newPoz.x, (currentLane - 1) * laneDistance, LaneSpeed * Time.deltaTime);
            transform.position = newPoz;             
            
        }
    }
       
    IEnumerator GoSlide()
    {
        yield return new WaitForSeconds(0.5f);
        boxCollider.size = boxColliderSize;
        sliding = false;
        animator.SetBool("Sliding", false);
    }
    IEnumerator GoMove()
    {
        if (Input.GetKeyDown(KeyCode.A) | Input.GetKeyDown(KeyCode.LeftArrow) | SwipeManager.swipeLeft)
        {
            ChangeLane(-1);
            if (targetLane > -1 && transform.position.y < 0.14)
                animator.SetTrigger("left");
        }
        else if (Input.GetKeyDown(KeyCode.D) | Input.GetKeyDown(KeyCode.RightArrow) | SwipeManager.swipeRight)
        {
            ChangeLane(1);
            if (targetLane < 3 && transform.position.y < 0.14)
                animator.SetTrigger("right");
        }
        else if (vMove > 0 | SwipeManager.swipeUp)
        {
            if (cc.isGrounded)
                Jump();
        }
        else if (vMove < 0 | SwipeManager.swipeDown)
        {           
                Slide();
        }
        yield return null;
    }

    void ChangeLane(int direction)
    {
        if (!isRunning)
            return;

        targetLane = currentLane + direction;

        if (targetLane < 0 || targetLane > 2)
            // Ignore, we are on the borders.
            return;

        currentLane = targetLane;

        newPoz = new Vector3((currentLane - 1) * laneDistance, 0, 0);
    }

    void Jump()
    {
        if (jumping)
            return;
        
        startPosition = transform.position.y;               
        animator.SetBool("Jumping", true);
        jumping = true;
        dustEffect.Play();
              
    }

    void Slide()
    {
        if (sliding)
            return;
        
        gravity = Vector3.down * 30;
        Vector3 newSize = boxCollider.size;
        newSize.y = newSize.y / 2;
        boxCollider.size = newSize;
        animator.SetBool("Sliding", true);
        sliding = true;
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("danger"))
        {
            Instantiate(deathParticles, transform.position, Quaternion.Euler(90,90,0));
            Sounds[2].Play();
            Sounds[1].volume = 0.5f;
            GameOver();
            HighscoreTable.instance.AddHighscoreEntry(ScoreScript.distanceValue, ScoreScript.coinsValue ,Menu.theName);
        }

        if (other.CompareTag("Coin"))
        {            
            GameObject newCoinParticles = Instantiate(pickUpCoin, other.transform.position, Quaternion.Euler(90,90,0));
            Destroy(other.gameObject);
            Destroy(newCoinParticles, 2);
            ScoreScript.coinsValue += 1;
            Sounds[0].PlayOneShot(coinSound);
        }

        if (other.CompareTag("Tire"))
        {
            Sounds[3].Play();            
        }
    }

    private void GameOver()
    {
        gameOver = true;
        uiManager.deathUI.SetActive(true);
    }

    

}
