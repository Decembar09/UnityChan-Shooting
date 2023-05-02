using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 敵の生成管理
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
        {//五秒経過しないと先に進まない。
            _createTimer += Time.deltaTime; return;
        }
        _createTimer = 0f;

        if (Prehab_Num <= 9)
        {
            EnemyBase enemyBase = (EnemyBase)Instantiate(_originalEnemy,
                new Vector3(UnityEngine.Random.Range(-40.0f, 40.0f),//x座標Ramdomで取得
                            UnityEngine.Random.Range(-30.0f, 40.0f),//y座標Ramdomで取得
                            UnityEngine.Random.Range(10.0f, 90.0f)//z座標Ramdomで取得
                            ), Quaternion.identity);

            //Cameraに向かせる
            enemyBase.gameObject.transform.LookAt(Camera.main.transform.localPosition);

            enemyBase.SetTag("Eyebat" + Prehab_Num);
            //Debug.Log("Prehab_Num  " + enemyBase.tag);
            Prehab_Num++;
        }

    }
}
