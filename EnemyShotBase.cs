using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShotBase : MonoBehaviour
{

    private float _shotLifeTimer = 20.0f;//C#�ŏ������g���ꍇ�́uf�v������
    private Quaternion move_q = Quaternion.Euler(0f, 10.0f, 0f);
    // Update is called once per frame
    void Update()
    {
        _shotLifeTimer -= Time.deltaTime;//��̈�b�ԂɂP�t���[������

        if (_shotLifeTimer == 0)
        {
            Destroy(gameObject);//addComponent�ł��̃X�N���v�g��ǉ����Ă���I�u�W�F�N�g���폜
        }

        //���̃I�u�W�F�N�g����]���������ꍇ
        Quaternion q = this.transform.rotation;
        this.transform.rotation = q * move_q;

    }


    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.layer != 9)//layer 9 Enemy
        {
            if (other.tag == "MainCamera")
            {
                //�_���[�W���󂯂�
                //PlayerBase�N���X��PlayerDamage()���\�b�h�̌Ăяo��
                //PlayerBase.cs���Q��
                PlayerBase.GetInstance().PlayerDamage();
                //Debug.Log("PlayerDamage");
            }
            //Debug.Log("EnemyShotDelete");

         Destroy(gameObject);
         }
    }
        
}
