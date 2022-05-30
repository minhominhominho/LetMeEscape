using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallCreater : MonoBehaviour
{
    [SerializeField] private GameObject wall;
    [SerializeField] private GameObject touchIndicator;
    [SerializeField] private int wallCreatingCoolTime;

    private float timer;
    private bool isDrawing;
    private GameObject drawingWall;
    private Vector2 startPos;
    private Vector2 endPos;
    private Vector2 center;


    private void Start()
    {
        timer = wallCreatingCoolTime;
    }

    private void Update()
    {
        // Showing touch point
        if (Input.GetMouseButton(0))
        {
            touchIndicator.SetActive(true);
            touchIndicator.transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            touchIndicator.transform.position = new Vector3(touchIndicator.transform.position.x, touchIndicator.transform.position.y, 0);
        }
        else
        {
            touchIndicator.SetActive(false);
        }

        // Draw Wall
        if (GameManager.Instance.IsGameRunning() && wall != null)
        {
            if (!isDrawing)
            {
                timer += Time.deltaTime;
                if (timer >= wallCreatingCoolTime)
                {
                    if (Input.GetMouseButtonDown(0))
                    {
                        isDrawing = true;

                        drawingWall = Instantiate(wall);
                        drawingWall.GetComponent<BoxCollider2D>().enabled = false;
                        startPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                        endPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

                        drawWall();
                    }
                }
            }
            else
            {
                endPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

                if (Input.GetMouseButtonUp(0))
                {
                    isDrawing = false;
                    drawingWall.GetComponent<BoxCollider2D>().enabled = true;


                    ////////////////////// 생성할 때 겹쳐져 있는 상태인지 확인하는 코드 구현해야됨
                    //drawingWall.GetComponent<BoxCollider2D>().isTrigger = true;

                    //if(drawingWall.GetComponent<BoxCollider2D>().GetContacts())

                    timer = 0;
                }

                drawWall();
            }
        }
    }

    // Calculate and draw wall
    private void drawWall()
    {
        // Set position
        center = (startPos + endPos) / 2;
        drawingWall.transform.position = center;

        // Set scale
        drawingWall.transform.localScale = new Vector3(Vector2.Distance(startPos, endPos), drawingWall.transform.localScale.y, drawingWall.transform.localScale.z);

        // Set rotation
        Vector2 wallDirection = (startPos - endPos);
        float angle = Mathf.Atan2(wallDirection.y, wallDirection.x) * Mathf.Rad2Deg;
        Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        drawingWall.transform.rotation = rotation;
    }
}
