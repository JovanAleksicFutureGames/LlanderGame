using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleInterpolation : MonoBehaviour
{
    private void Update()
    {
        if(transform.position.y < 72f)
            transform.Translate(Vector3.up * Time.deltaTime);

        else if(transform.position.y > 85f)
            transform.Translate(Vector3.down * Time.deltaTime);
    }
}
