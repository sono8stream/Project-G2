using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UniRx.Triggers;

public class SelectSample : Base
{
    // Use this for initialization
    void Start()
    {
        Observable.Return(new Vector2(0, 1.5f))
            .Subscribe(v => transform.position = v);

        this.UpdateAsObservable()
            .Select(_ => 2)
            .Subscribe(l => Move(0.01f * l, 0));
    }
}