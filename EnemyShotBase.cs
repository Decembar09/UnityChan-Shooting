using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShotBase : MonoBehaviour
{

    private float _shotLifeTimer = 20.0f;//C#で少数を使う場合は「f」を入れる
    private Quaternion move_q = Quaternion.Euler(0f, 10.0f, 0f);
    // Update is called once per frame
    void Update()
    {
        _shotLifeTimer -= Time.deltaTime;//大体一秒間に１フレーム減る

        if (_shotLifeTimer == 0)
        {
            Destroy(gameObject);//addComponentでこのスクリプトを追加しているオブジェクトを削除
        }

        //このオブジェクトを回転させたい場合
        Quaternion q = this.transform.rotation;
        this.transform.rotation = q * move_q;

    }


    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.layer != 9)//layer 9 Enemy
        {
            if (other.tag == "MainCamera")
            {
                //ダメージを受ける
                //PlayerBaseクラスのPlayerDamage()メソッドの呼び出し
                //PlayerBase.csを参照
                PlayerBase.GetInstance().PlayerDamage();
                //Debug.Log("PlayerDamage");
            }
            //Debug.Log("EnemyShotDelete");

         Destroy(gameObject);
         }
    }
        
}
