using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayTimeControl : MonoBehaviour
{
    [SerializeField]private TextMeshProUGUI _timeText;
    [SerializeField]private PouseBase _gamePouseBase;

    public static bool beingMeasured; // 計測中であることを表す変数

    private float time; // 経過時間を格納する変数
    
    // Start is called before the first frame update
    void Start()
    {
        // 初期値は「計測中」状態
        beingMeasured = true;
        time = 0.0f;
        //_timeText = GetComponent<Text>();
        _timeText.text = "TIME:" + "00:00:00";
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(2))// 0左クリック; 1右クリック; 2真ん中 
        {
            _gamePouseBase.GamePouse(beingMeasured);
            
            // TimerScriptのbeingMeasuredの値を反転させる
            beingMeasured = !beingMeasured;
            // ボタンの文言を切り替える
            _timeText.text = beingMeasured ? "STOP" : "START";
        }

        if (!beingMeasured)
        {
            return;///// 「計測中ではない」場合、以降の処理は実行しない /////
        }

        /*Time.unscaledTimeについて
            Time.unscaledDeltaTimeはTime.deltaTime同様、前回のフレームからの経過時間（秒）を表していますが、
            Time.deltaTimeとは異なりTime.timeScaleの影響を受けません。
            そのため、Time.unscaledDeltaTimeを使ったタイマーはTime.timeScaleの値によらず一定の速さで進み続けています。
        */
        time += Time.deltaTime;
        //ToString(“0.00”)はfloat型をstring型に変換するメソッドです。
        //引数は変換後のフォーマットを意味しており、上記の書き方は「（四捨五入をし、）小数第２位まで表示する」ことを表しています。
        _timeText.text = "TIME:" + time.ToString("000.00");
    }

    public void SetTime(int time)
    {
        //Time.deltaTime//約0.02秒
        _timeText.text = "TIME:" + time;
    }

/*
    public void OnClick() //ObjectがButtonの時
    {
        // beingMeasuredの値を反転させる
        beingMeasured = !beingMeasured;
        // ボタンの文言を切り替える
        _timeText.text = beingMeasured ? "STOP" : "START";
    }
 */
}
