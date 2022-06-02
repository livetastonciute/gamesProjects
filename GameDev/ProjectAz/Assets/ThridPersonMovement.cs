using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ThridPersonMovement : MonoBehaviour
{
    public CharacterController controller;
    public Transform cam;
    [SerializeField]
    public GameObject Player;
    private GameObject[] hearts;

    public float speed = 6f;
    public float turnSmoothTime = 0f;
    float turnSmoothVelocity;
    public float gravity = -9.81f;
    public float jumpHeight =  3f;

    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;

    private bool CheckpointExists = false;
    Vector3 velocity;
    bool isGrounded;

	private UI t;
    private CollectibleTrigger ct;

    public static bool playerControlsEnabled = true;

    [SerializeField]
    public GameObject GameOverPanel;

    Vector3 groundVelocity;
    Vector3 moveDir2;
    PlayerSwitch skinNumber;
    Vector3 normal;

    public AudioSource[] sounds;
    public AudioSource gameOverSound;
    public GameObject Character;

    public bool gameHasEnded = false;

    private void Awake()
    {
        sounds = GetComponents<AudioSource>();
        gameOverSound = sounds[3];

        t = GetComponent<UI>();
        ct = GetComponent<CollectibleTrigger>();
        playerControlsEnabled = true;
    }
    public void setGameEnd()
    {
        gameHasEnded = false;
    }
    // Update is called once per frame
    void Update()
    {
        if (playerControlsEnabled)
        {
            if (controller.transform.position.y <= -10 || t.hp <= 0)
            {
                if (ct.CheckpointExists() && t.lives > 0)
                {
                    ResetCheckpoint();
                    t.RemoveLife();
                    t.AddHp(1000);
                }
                else
                {
                    gameHasEnded = true;
                    GameOverPanel.SetActive(true);
                }
            }


            isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

            if (isGrounded && velocity.y < 0)
            {
                velocity.y = -2f;
            }

            velocity.x = 0f;
            velocity.z = 0f;

            float horizontal = Input.GetAxisRaw("Horizontal");
            float vertical = Input.GetAxisRaw("Vertical");
            Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;

            if (direction.magnitude >= 0.1f)
            {
                float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
                float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
                transform.rotation = Quaternion.Euler(0f, angle, 0f);

                Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
                moveDir2 = moveDir;
                controller.Move(moveDir.normalized * speed * Time.deltaTime);
            }

            if (Input.GetButtonDown("Jump") && isGrounded)
            {
                velocity.y += Mathf.Sqrt(jumpHeight * -2f * gravity);
            }

            if (isGrounded || Input.GetButtonDown("Jump"))
            {
                groundVelocity = new(moveDir2.x * 3, 5);
            }

            velocity.y += gravity * Time.deltaTime;

            controller.Move(velocity * Time.deltaTime);
        }
    }

    void OnControllerColliderHit(ControllerColliderHit hit)
    {
        skinNumber = Player.GetComponent<PlayerSwitch>();
        if (skinNumber.whichAvatarIsOn == 2 && hit.gameObject.tag == "WallJump")
        {
            normal = hit.normal;
            if (!isGrounded)
            {
                //Character.GetComponent<Animator>().Play("WallJump");
                print("wall hit");
                //if(groundVelocity.x > 0)
                velocity = Vector3.Reflect(groundVelocity, normal);
                //velocity.y = -1f;
            }
        }
    }



    public void ResetScene()
    {
        if(gameHasEnded == false)
        {
            gameOverSound.Play();//GAME FINISH SOUND
            gameHasEnded = true;
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }

    public void ResetCheckpoint()
    {
        Vector3 Chekcpoint = ct.GetCheckpoint();
        //Vector3 tp = new Vector3(-3.21f, 1.37f, 89.61f);
        controller.enabled = false;
        Player.transform.position = Chekcpoint;
        controller.enabled = true;
    }

}
