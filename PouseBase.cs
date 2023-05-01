using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PouseBase : MonoBehaviour
{
    [SerializeField]
    private GameObject _gamePouse;
    //[SerializeField]private GameObject _gamePouseText;

    // Start is called before the first frame update
    void Start()
    {
        _gamePouse.SetActive(false);
        //_gamePouseText.SetActive(false);
    }

    // Update is called once per frame
    //void Update(){}

    public void GamePouse(bool pouse)
    {
        /*
        Time.timeScaleは「Unity内の時間」の経過する速さを表す変数です。
        初期値は１で、これは「現実世界の時間」の速さと等しいことを意味しています。
        例えばこの値を0.5にすると、「現実世界の時間」の半分の速さでゆっくりと時間が流れ、
        値を２にすると、「現実世界の時間」の倍の速さですばやく時間が流れる、といった具合になります。
        つまり、「現実世界の時間」の速さとの比を設定値として持つわけです。

        設定値はEdit > Project Settings > Timeで確認できます。
         */
        if (pouse) { Time.timeScale = 0; }
        else { Time.timeScale = 1; }

        _gamePouse.SetActive(pouse);
        //_gamePouseText.SetActive(pouse);
    }

}
