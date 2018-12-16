using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UniRx.Triggers;

public class Base : MonoBehaviour
{
    protected void Move(float dx,float dy)
    {
        transform.position += new Vector3(dx, dy, 0);
    }
}