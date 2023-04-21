using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Guard : Enemy
{
    [SerializeField] private Light guardLight;

    private void Update()
    {
        CalcluateRouteByLight(guardLight);
    }
}
