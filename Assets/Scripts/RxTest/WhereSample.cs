using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UniRx.Triggers;

public class WhereSample : Base
{

    // Use this for initialization
    void Start()
    {
        Observable.Return(new Vector2(0, 0.5f))
            .Subscribe(v => gameObject.transform.position = v);

        this.UpdateAsObservable().Where(_ => Input.GetMouseButton(0))
            .Subscribe(_ => Move(0.01f, 0));
    }
}
