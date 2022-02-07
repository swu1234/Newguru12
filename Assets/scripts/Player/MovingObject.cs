using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingObject : MonoBehaviour
{
    public string characterName;

    static public MovingObject instance;

    public string currentMapName; // transfermap ��ũ��Ʈ�� transfermapname ������ ����
    //public string currentSceneName; //�߰��� ����, scenename�� �����Ѵ�.

    private BoxCollider2D boxCollider;
    public LayerMask layerMask;

    //public AudioClip walkSound_1; // �÷��̾� �ȱ� �������� �̸�
    //public AudioClip walkSound_2;

    //private AudioSource audioSource; // ���� �÷��̾�

    public float speed; // �÷��̾� �⺻ ���ǵ�

    private Vector3 vector; // �÷��̾� �̵�

    public float runSpeed; // �÷��̾� �޸� �� ���ǵ�
    private float applyRunSpeed; // �޸��� ����
    private bool applyRunFlag = false;

    public int walkCount; // 1ĭ�� �ȱ� ���� ��� 
    private int currentWalkCount; // n �ȼ���ŭ �̵���ų ���ΰ� (ex. 2.4 * 20 = 48 >> currentwalkcount�� 20�̴�.)

    private bool canMove = true;

    private Animator animator;

    public SpeakManager manager;
    Rigidbody2D rigid;
    GameObject scanObject;

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
            rigid = GetComponent<Rigidbody2D>();
            instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }


    }
    IEnumerator MoveCoroutine()
    {
        while (Input.GetAxisRaw("Vertical") != 0 || Input.GetAxisRaw("Horizontal") != 0)
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

            if (vector.x != 0)
            {
                vector.y = 0;
            }

            animator.SetFloat("DirX", vector.x);
            animator.SetFloat("DirY", vector.y);

            RaycastHit2D hit;
            Vector2 start = transform.position; // ĳ������ ���� ��ġ ��
            Vector2 end = start + new Vector2(vector.x * speed * walkCount, vector.y * speed * walkCount); // ĳ���Ͱ� �̵��ϰ��� �ϴ� ��ġ ��

            boxCollider.enabled = false;
            hit = Physics2D.Linecast(start, end, layerMask); // start->end�� �������� ���� �� �ɸ��� �� ���ٸ� null, �ɸ��� �� �ִٸ� ���ع��� �ν�
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
        if (canMove)
        {
            if (Input.GetAxisRaw("Horizontal") != 0 || Input.GetAxisRaw("Vertical") != 0)
            {
                canMove = false;
                StartCoroutine(MoveCoroutine());
            }
        }

        //Direction
        if (Input.GetKey("up") || Input.GetKey("w"))
            vector = Vector3.up;
        else if (Input.GetKey("down") || Input.GetKey("s"))
            vector = Vector3.down;
        else if (Input.GetKey("left") || Input.GetKey("a"))
            vector = Vector3.left;
        else if (Input.GetKey("right") || Input.GetKey("d"))
            vector = Vector3.right;


        //Scan Object
        if (Input.GetButtonDown("Jump") && scanObject != null)
            manager.Action(scanObject);

    }

    void FixedUpdate()
    {
        //Ray
        Debug.DrawRay(rigid.position, vector * 0.7f, new Color(0,1,0));
        RaycastHit2D rayHit = Physics2D.Raycast(rigid.position, vector, 0.7f, LayerMask.GetMask("character"));

        if (rayHit.collider != null)
        {
            scanObject = rayHit.collider.gameObject;
        }
        else
            scanObject = null;
    }
}