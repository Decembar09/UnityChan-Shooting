using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// �e�ۂ̏���
/// </summary>
public class ShotMove : MonoBehaviour
{
    private float _shotLifeTimer = 2.0f;//C#�ŏ������g���ꍇ�́uf�v������

    // Update is called once per frame
    void Update()
    {
        _shotLifeTimer -= Time.deltaTime;//��̈�b�ԂɂP�t���[������

        if (_shotLifeTimer <= 0) {
            Destroy(this.gameObject);//addComponent�Œǉ����Ă���I�u�W�F�N�g���폜
        }
    }

    //�I�u�W�F�N�g�R���|�[�l���g��collider��[is Trigger]���`�F�b�N�ɂ��Ă����ƌĂяo������^��
    private void OnTriggerEnter(Collider other){

        if (other.gameObject.layer == 9)//layer 9 Enemy
        {
            Destroy(this.gameObject);
        }
    }//������̔�r�ł�==���g��
}
