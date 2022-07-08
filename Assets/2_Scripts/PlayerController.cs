using System;
using TMPro;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private int speed = 5;
    
    [SerializeField] private float minRoadX = -3.55f;
    [SerializeField] private float maxRoadX = 3.55f;

    float screenDis;
    float roadDisX;
    float disDifX;

    float minDis;
    float maxDis;
    float? firstMousePosX;

    private bool canMove = false;

    Vector3 firstPos;

    public int rank;

    public TextMeshProUGUI rankText;

    private void OnEnable()
    {
        EventManager.start += StartGame;
        EventManager.finish += FinishGame;
    }

    private void OnDisable()
    {
        EventManager.start -= StartGame;
        EventManager.finish -= FinishGame;
    }


    void Start()
    {
        screenDis = (Screen.width * 7.5f / 10f) - (Screen.width * 2.5f / 10f);

        roadDisX = Mathf.Abs(maxRoadX - minRoadX);

        disDifX = screenDis / roadDisX;
    }

    void StartGame()
    {
        firstPos = transform.position;
        canMove = true;
        GetComponent<Animator>().SetBool("Run", true);
    }

    void FinishGame()
    {
        canMove = false;
        GetComponent<Animator>().SetBool("Run", false);
        transform.Translate(Vector3.zero);
        Preferences.isFinish = true;
    }
    
    void Update()
    {
        MouseControlX();

        rankText.text = "Pos: " + rank.ToString() + "/11";
    }
    void FixedUpdate()
    {
        if (!Preferences.isFinish)
            transform.Translate(Vector3.forward * Time.deltaTime * speed);
    }

    void MouseControlX()
    {
        if (canMove)
        {
            if (Input.GetMouseButtonDown(0))
            {
                firstMousePosX = Input.mousePosition.x;
            }
            else if (Input.GetMouseButton(0) && !Preferences.isFinish)
            {
                if (firstMousePosX > Input.mousePosition.x)    // Sol
                {
                    minDis = transform.position.x - (((float)firstMousePosX - Input.mousePosition.x) / disDifX);
                    if (minDis >= minRoadX)
                    {
                        transform.position = new Vector3(minDis, transform.position.y, transform.position.z);
                        firstMousePosX = Input.mousePosition.x;
                    }
                    else
                    {
                        transform.position = new Vector3(minRoadX, transform.position.y, transform.position.z);
                    }
                }
                else if (firstMousePosX < Input.mousePosition.x)   // Sa�
                {
                    maxDis = transform.position.x + ((Input.mousePosition.x - (float)firstMousePosX) / disDifX);
                    if (maxDis <= maxRoadX)
                    {
                        transform.position = new Vector3(maxDis, transform.position.y, transform.position.z);
                        firstMousePosX = Input.mousePosition.x;
                    }
                    else
                    {
                        transform.position = new Vector3(maxRoadX, transform.position.y, transform.position.z);
                    }
                }
                if (Input.GetMouseButtonDown(0))
                {
                }
            }
            else if (Input.GetMouseButtonUp(0))
            {
                firstMousePosX = null;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Obstacle")
        {
            transform.position = firstPos;
        }

        if ( other.tag == "Last Plane" )
        {
            EventManager.FinishEvent();
        }
    }
}
