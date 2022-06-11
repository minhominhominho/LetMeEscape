using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    private float timer;
    private bool isTurnedOnce;
    private float updatingTime = 0.5f;

    private float changingAngle = 45;
    private bool isStartFromLeftSide;


    private void Start()
    {
        isStartFromLeftSide = Random.Range(-1, 1) == 0 ? true : false;
        changingAngle *= isStartFromLeftSide ? 1 : -1;

        Vector3 rot = transform.rotation.eulerAngles;
        transform.rotation = transform.rotation = Quaternion.Euler(new Vector3(rot.x, rot.y, rot.z - changingAngle / 2));
    }

    private void Update()
    {
        timer += Time.deltaTime;
        if (timer >= updatingTime && !isTurnedOnce)
        {
            isTurnedOnce = true;
            Vector3 rot = transform.rotation.eulerAngles;
            transform.rotation = Quaternion.Euler(new Vector3(rot.x, rot.y, rot.z + changingAngle));
        }
        else if (timer >= updatingTime * 2)
        {
            timer = 0;
            isTurnedOnce = false;
            Vector3 rot = transform.rotation.eulerAngles;
            transform.rotation = Quaternion.Euler(new Vector3(rot.x, rot.y, rot.z - changingAngle));
        }
    }
}
