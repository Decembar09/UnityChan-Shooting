
//Preferences>Asset Pipeline>Auto Refresh(Disabled,Enabled)　　　
//Disabledでスクリプトを編集するたびにコンパイルされなくなる。

//再生するときは必ずコンパイルする。
//#if UNUTY_EDITOR



using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Effect1Base : MonoBehaviour
{
    private float _timer = 0f;

    // Update is called once per frame
    void Update()
    {
        _timer += Time.deltaTime;
        if (_timer >= 1f) { Destroy(this.gameObject); }
    }
}