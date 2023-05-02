using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// �G�̐����Ǘ�
/// </summary>
public class EnemyManager : MonoBehaviour
{
    [SerializeField]
    private EnemyBase _originalEnemy;
    //private int stage = 0;
    private int Prehab_Num = 0;
    private float _createTimer = 0;

    // Start is called before the first frame update
    //void Start(){}

    // Update is called once per frame
    void Update()
    {
        if (_createTimer < 3.0f)
        {//�ܕb�o�߂��Ȃ��Ɛ�ɐi�܂Ȃ��B
            _createTimer += Time.deltaTime; return;
        }
        _createTimer = 0f;

        if (Prehab_Num <= 9)
        {
            EnemyBase enemyBase = (EnemyBase)Instantiate(_originalEnemy,
                new Vector3(UnityEngine.Random.Range(-40.0f, 40.0f),//x���WRamdom�Ŏ擾
                            UnityEngine.Random.Range(-30.0f, 40.0f),//y���WRamdom�Ŏ擾
                            UnityEngine.Random.Range(10.0f, 90.0f)//z���WRamdom�Ŏ擾
                            ), Quaternion.identity);

            //Camera�Ɍ�������
            enemyBase.gameObject.transform.LookAt(Camera.main.transform.localPosition);

            enemyBase.SetTag("Eyebat" + Prehab_Num);
            //Debug.Log("Prehab_Num  " + enemyBase.tag);
            Prehab_Num++;
        }

    }
}
