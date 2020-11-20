using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{

    [SerializeField]
    private float speed = 0.25f;

    [SerializeField]
    private string JSONPathName = "";

    [SerializeField]
    private LineRenderer lineRenderer = null;

    private bool isMoving = false;
    private int pathIndex;
    private BallPath ballPath = new BallPath();

    public float Speed
    {

        get
        {

            return speed;

        }

        set
        {

            if (value > 0)
            {

                speed = value;

            }            

        }

    }

    // Start is called before the first frame update
    void Start()
    {

        ballPath.ReadJSON(JSONPathName);

        GetInStartPosition();

    }

    // Update is called once per frame
    void FixedUpdate()
    {

        if (isMoving)
        {

            if (pathIndex < ballPath.Length)
            {

                transform.position = Vector3.MoveTowards(transform.position, ballPath[pathIndex], Speed);

                DrawPath();

                if (transform.position == ballPath[pathIndex])
                {

                    ++pathIndex;

                }

            }
            else if (pathIndex == ballPath.Length)
            {

                isMoving = false;
                pathIndex = 0;

            }

        }

    }

    public void StartMove()
    {

        isMoving = true;

    }

    public void StopMove()
    {

        isMoving = false;

    }

    public void GetInStartPosition()
    {

        lineRenderer.positionCount = 0;

        isMoving = false;
        pathIndex = 0;
        transform.position = ballPath[pathIndex];

    }

    private void DrawPath()
    {

        if (pathIndex == 0 && transform.position == ballPath[pathIndex])
        {

            lineRenderer.positionCount = 0;

            return;

        }

        lineRenderer.positionCount = pathIndex + 2;

        lineRenderer.SetPosition(0, ballPath[0]);

        for (int i = 1; i < pathIndex; ++i)
        {

            lineRenderer.SetPosition(i, ballPath[i]);

        }

        lineRenderer.SetPosition(pathIndex + 1, transform.position);

    }

}
