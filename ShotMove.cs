using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 弾丸の処理
/// </summary>
public class ShotMove : MonoBehaviour
{
    private float _shotLifeTimer = 2.0f;//C#で少数を使う場合は「f」を入れる

    // Update is called once per frame
    void Update()
    {
        _shotLifeTimer -= Time.deltaTime;//大体一秒間に１フレーム減る

        if (_shotLifeTimer <= 0) {
            Destroy(this.gameObject);//addComponentで追加しているオブジェクトを削除
        }
    }

    //オブジェクトコンポーネントのcolliderの[is Trigger]をチェックにしておくと呼び出される定型文
    private void OnTriggerEnter(Collider other){

        if (other.gameObject.layer == 9)//layer 9 Enemy
        {
            Destroy(this.gameObject);
        }
    }//文字列の比較では==を使う
}
