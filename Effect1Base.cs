
//Preferences>Asset Pipeline>Auto Refresh(Disabled,Enabled)�@�@�@
//Disabled�ŃX�N���v�g��ҏW���邽�тɃR���p�C������Ȃ��Ȃ�B

//�Đ�����Ƃ��͕K���R���p�C������B
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