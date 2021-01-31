using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BallController : MonoBehaviour
{

    [SerializeField]
    private Camera _camera = null;

    [SerializeField]
    private Transform target = null;
    [SerializeField]
    private Slider slider = null;

    [SerializeField]
    private float cameraRotationRadius = 5f;

    [SerializeField]
    private float cameraRotationSpeed = 0.2f;

    [SerializeField]
    private Transform[] ballsPool = null;
    private int poolIndex;

    private ClickController clickController;
    private CameraRotationController cameraRotationController;

    void Start()
    {

        clickController = new ClickController(this, _camera);
        cameraRotationController = new CameraRotationController(_camera.transform, cameraRotationRadius, cameraRotationSpeed);

    }

    void Update()
    {

        Controll();

    }

    private void Controll()
    {

        int clicks = clickController.Check(out RaycastHit hit);

        if (clicks == 1)
        {

            if (target.name == hit.transform.name)
            {

                hit.transform.GetComponent<Ball>().StartMove();

                return;

            }

            target.GetComponent<Ball>().StopMove();

            target = hit.transform;
            target.GetComponent<Ball>().StartMove();
            slider.value = target.GetComponent<Ball>().Speed;

            FindBallIndex();

            return;

        }

        if (clicks == 2)
        {

            hit.transform.GetComponent<Ball>().GetInStartPosition();

        }

    }

    private void FindBallIndex()
    {

        for (int i = 0; i < ballsPool.Length; ++i)
        {

            if (ballsPool[i].name == target.name)
            {

                poolIndex = i;

            }

        }

    }

    void LateUpdate()
    {

        cameraRotationController.RotateAround(target.position, Input.GetAxis("Horizontal"));

    }

    public void SpeedChanged()
    {

        if (slider.value == 0)
        {

            target.GetComponent<Ball>().StopMove();

            return;

        }

        target.GetComponent<Ball>().Speed = slider.value;

    }

    public void SwitchToLeftBall()
    {

        if (poolIndex == 0)
        {

            poolIndex = ballsPool.Length - 1;

        }
        else
        {

            --poolIndex;

        }

        target = ballsPool[poolIndex].transform;

    }

    public void SwitchToRightBall()
    {

        if (poolIndex == ballsPool.Length - 1)
        {

            poolIndex = 0;

        }
        else
        {

            ++poolIndex;

        }

        target = ballsPool[poolIndex].transform;

    }

}
