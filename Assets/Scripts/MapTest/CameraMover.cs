using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UniRx.Triggers;

public class CameraMover : Base
{

    // Use this for initialization
    void Start()
    {
        float speed = 0.5f;
        this.UpdateAsObservable().Where(_ => Input.GetKey(KeyCode.UpArrow))
            .Subscribe(_ => Move(0, speed));
        this.UpdateAsObservable().Where(_ => Input.GetKey(KeyCode.RightArrow))
            .Subscribe(_ => Move(speed, 0));
        this.UpdateAsObservable().Where(_ => Input.GetKey(KeyCode.DownArrow))
            .Subscribe(_ => Move(0, -speed));
        this.UpdateAsObservable().Where(_ => Input.GetKey(KeyCode.LeftArrow))
            .Subscribe(_ => Move(-speed, 0));
    }
}