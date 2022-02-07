using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingObject : MonoBehaviour
{
    public string characterName;

    static public MovingObject instance;

    public string currentMapName; // transfermap 스크립트의 transfermapname 변수값 저장
    //public string currentSceneName; //추가한 변수, scenename을 지정한다.

    private BoxCollider2D boxCollider;
    public LayerMask layerMask;

    //public AudioClip walkSound_1; // 플레이어 걷기 사운드파일 이름
    //public AudioClip walkSound_2;

    //private AudioSource audioSource; // 사운드 플레이어

    public float speed; // 플레이어 기본 스피드
    
    private Vector3 vector; // 플레이어 이동

    public float runSpeed; // 플레이어 달릴 때 스피드
    private float applyRunSpeed; // 달리기 적용
    private bool applyRunFlag = false;

    public int walkCount; // 1칸씩 걷기 위한 기능 
    private int currentWalkCount; // n 픽셀만큼 이동시킬 것인가 (ex. 2.4 * 20 = 48 >> currentwalkcount는 20이다.)

    private bool canMove = true;

    private Animator animator;

    public SpeakManager manager;

    public void Move()
    {
        StartCoroutine(MoveCoroutine());
    }

    // not move
    public void notMove()
    {
        canMove = false;
        StopCoroutine(MoveCoroutine());
    }

    // Start is called before the first frame update
    void Start()
    {
        if (instance == null)
        {
            DontDestroyOnLoad(this.gameObject);
            boxCollider = GetComponent<BoxCollider2D>();
            animator = GetComponent<Animator>();
            instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }
    IEnumerator MoveCoroutine()
    {
        while(Input.GetAxisRaw("Vertical") != 0 || Input.GetAxisRaw("Horizontal") !=0)
        {
            if (Input.GetKey(KeyCode.LeftShift))
            {
                applyRunSpeed = runSpeed;
                applyRunFlag = true;
            }
            else
            {
                applyRunSpeed = 0;
                applyRunFlag = false;
            }

            vector.Set(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"), transform.position.z);

            if(vector.x != 0)
            {
                vector.y = 0;
            }

            animator.SetFloat("DirX", vector.x);
            animator.SetFloat("DirY", vector.y);

            RaycastHit2D hit;
            Vector2 start = transform.position; // 캐릭터의 현재 위치 값
            Vector2 end = start + new Vector2(vector.x * speed * walkCount, vector.y * speed * walkCount); // 캐릭터가 이동하고자 하는 위치 값

            boxCollider.enabled = false;
            hit = Physics2D.Linecast(start, end, layerMask); // start->end로 레이저를 쐈을 때 걸리는 게 없다면 null, 걸리는 게 있다면 방해물로 인식
            boxCollider.enabled = true;

            if (hit.transform != null)
            {
                break;
            }

            animator.SetBool("Walking", true);

            while (currentWalkCount < walkCount)
            {
                /*if (currentMapName == "start")
                {
                    audioSource.clip = walkSound_1;
                    audioSource.Play();
                }
                else
                {
                    audioSource.clip = walkSound_2;
                    audioSource.Play();
                }
                */
                if (vector.x != 0)
                {
                    transform.Translate(vector.x * (speed + applyRunSpeed), 0, 0);
                }
                else if (vector.y != 0)
                {
                    transform.Translate(0, vector.y * (speed + applyRunSpeed), 0);
                }
                if (applyRunFlag)
                {
                    currentWalkCount++;
                }
                currentWalkCount++;
                yield return new WaitForSeconds(0.01f);
            }
            currentWalkCount = 0;
        }
        animator.SetBool("Walking", false);
        canMove = true;
    }

    // Update is called once per frame
    void Update()
    {
        if(canMove)
        {
            if (Input.GetAxisRaw("Horizontal") != 0 || Input.GetAxisRaw("Vertical") != 0)
            {
                canMove = false;
                StartCoroutine(MoveCoroutine());
            }
        }

     /*Scan Object
        if (Input.GetButtonDown("Jump") && scanObject != null)
            Speakmanager.Action(scanObject);
     */
    }
}