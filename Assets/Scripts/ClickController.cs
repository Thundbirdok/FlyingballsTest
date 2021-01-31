using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

class ClickController
{

    private Camera _camera;

    private MonoBehaviour mono;

    private int clickCounter = 0;
    private float clickTime = 0;
    private const float clickDelay = 0.5f;

    private bool isCoroutineAlowed = true;

    private int resultOfClicks = 0;
    private RaycastHit resultHit;

    public ClickController(MonoBehaviour ballController, Camera camera)
    {

        mono = ballController;

        _camera = camera;

    }

    public int Check(out RaycastHit hit)
    {

        if (resultOfClicks != 0)
        {

            hit = resultHit;

            int tmp = resultOfClicks;
            resultOfClicks = 0;

            return tmp;

        }

        if (CheckRaycast(out hit))
        {

            ++clickCounter;

            if (clickCounter == 1 && isCoroutineAlowed)
            {

                clickTime = Time.time;
                mono.StartCoroutine(CheckClick());
                resultHit = hit;

            }

        }

        return 0;

    }

    private bool CheckRaycast(out RaycastHit hit)
    {

        if (Input.GetMouseButtonDown(0)
            && Physics.Raycast(_camera.ScreenPointToRay(Input.mousePosition), out hit)
            && hit.transform.CompareTag("Ball"))
        {

            return true;

        }

        hit = new RaycastHit();

        return false;

    }

    private IEnumerator CheckClick()
    {

        isCoroutineAlowed = false;

        while (Time.time < clickTime + clickDelay)
        {

            if (clickCounter == 2)
            {

                //Двойной клик
                resultOfClicks = 2;

                break;

            }

            yield return new WaitForEndOfFrame();

        }

        if (resultOfClicks == 0)
        {

            //Одиночный клик
            resultOfClicks = 1;

        }

        clickCounter = 0;
        clickTime = 0;
        isCoroutineAlowed = true;

    }

}

