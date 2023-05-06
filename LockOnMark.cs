using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockOnMark : MonoBehaviour
{

    [SerializeField]private Camera _targetCamera;

    [SerializeField]private RectTransform _parentUI;

    //一番上の親に付いているコンポーネントを取得する。Canvas
    //子のコンポーネントはのGetComponentChildren<RectTransform>();
    private  RectTransform UI;
    private  RectTransform _childrenUI;
    private float _timer;//C#で少数を使う場合は「f」を入れる
    //[SerializeField]
    private Transform _target;
    //[SerializeField]
    private Transform _targetUI;
    [SerializeField]private Vector3 _worldOffset = new Vector3(0f, 12.0f, 0f);

    private Vector3 _targetScreenPos;
    private Vector3 uiLocalPos;
    private GameObject _lockOnTarget;
    private string _lockOnTargetTag; 
    private float scale = 3.0f;
    //private float _timer = 0f;

/*
    //初期化メッソド（Prefabから生成する時などに使う）
    public void Initialize(Transform target, Camera targetCamera = null) {
        _target = target;
        _targetCamera = targetCamera != null ? targetCamera : Camera.main;

        Debug.Log("_target" + _target.transform.localPosition);

        OnUpdatePosition();
    }
*/

    private void Awake(){
  
        if(_targetCamera == null){
         _targetCamera = Camera.main;
        }

        //_parentUI = GetComponentInParent<RectTransform>();
        //一番上の親に付いているコンポーネントを取得する。Canvas
        //子のコンポーネントはのGetComponentChildren<RectTransform>();
        //_parentUI = _targetUI.parent.GetComponent<RectTransform>();
        //_parentUI = this.GetComponent<RectTransform>();
        //このcontextでKeyword "this"は使えない。理由は不明。

         UI = GetComponent<RectTransform>();
        //_childrenUI = GetComponentInChildren<RectTransform>();

        //Debug.Log("LockOnMark _parentUI   " + _parentUI);
        //Debug.Log("LockOnMark         UI  " + UI);
        //Debug.Log("LockOnMark _childrenUI " + _childrenUI);
    }

    void Update()
    {
       
         _timer += Time.deltaTime;
         if (_timer >= 10.0f){ this.Delete(); }

        
            scale = scale - 0.5f;
            if (scale >= 0.5f)
            {
                this.transform.localScale = new Vector3(scale, scale, scale);
                //_timer = 0f;
            }

            if (_lockOnTargetTag != null)
            {
                OnUpdatePosition();
            }
    }

    private void OnUpdatePosition(){
 
        var cameraTransform = _targetCamera.transform;
        var cameraDir = cameraTransform.forward;

        /*
         * オブジェクトと見かけ上同じ位置にUIを表示するためには
         * オブジェクトのワールド座標→スクリーン座標→TransFormRectのローカル座標
         * の順に座標変換する必要があります。
         * このとき、カメラ背後に位置するオブジェクトもスクリーン座標に投影されるため
         * 背後のものを非表示にしたい場合は前後判定を行う必要があります。
         */
  
        if (GameObject.FindGameObjectWithTag(_lockOnTargetTag) == null) {
            //Debug.Log("this Destroy " + this.gameObject);
            Destroy(this.gameObject);
        }

        _target = _lockOnTarget.transform;

        var targetWorldPos = _target.position + _worldOffset;
        var targetDir = targetWorldPos - cameraTransform.position;
        

        //内積を使ってカメラ前方かどうか判定
        var isFront = Vector3.Dot(cameraDir,targetDir) > 0;

        this.gameObject.SetActive(isFront);
        //_targetUI.gameObject.SetActive(isFront);
        if(!isFront)return;


        //positionを使って設定する。
        //この場合、positionに(0,0)を設定しているのでアンカーの位置に表示される
        //GetComponent<RectTransform>().position = Vector3.zero;

        //positionを使って設定する。
        //この場合、anchoredPositionに(0,0)を設定しているので画面左下に表示される
        //GetComponent<RectTransform>()anchoredPosition = Vector3.zero;

        GetComponent<RectTransform>().position = RectTransformUtility.WorldToScreenPoint(Camera.main, _target.position + _worldOffset);


        //DoMoveメッソドの場合
        //this.transform.DoMove(_target.position,1.0f);
        //this.transform.DOAnchorPos(_target.position,1.0f);

        /*
        _targetScreenPos = _targetCamera.WorldToScreenPoint(targetWorldPos);


        var rectTransform = GetComponent<RectTransform>();

        var corners = new Vector3[4];
        rectTransform.GetWorldCorners(corners);

        var prect = _parentUI.GetComponent<RectTransform>();
        var pcorners = new Vector3[4];
        prect.GetWorldCorners(pcorners);

        //for (var i = 0; i < corners.Length; i++){Debug.Log($"World Corners[{i}] : {corners[i]}");}

        Debug.Log("pcorners[0].x= " +pcorners[0].x + "  pcorners[0].y = " +pcorners[0].y + "  pcorners[0].z = " +pcorners[0].z);
        Debug.Log("corners[0].x = " + corners[0].x + "   corners[0].y = " + corners[0].y + "   corners[0].z = " + corners[0].z);
        Debug.Log("_targetScreenPos.x " + _targetScreenPos.x + ":  _targetScreenPos.y " + _targetScreenPos.y + ":  _targetScreenPos.z " + _targetScreenPos.z);


        uiLocalPos = _targetScreenPos - corners[0];
        uiLocalPos.z = 0f;
        this.transform.position = uiLocalPos;
        */


            //public Vector3 WorldToScreenPoint(Vector3 position);

            //Debug.Log("_parentUI.transform " +_parentUI.transform);
            //Debug.Log("_targetScreenPos     " +_targetScreenPos);
            //Debug.Log("_targetCamera        " +_targetCamera);
            //Vector2 uiLocalPos;
            /*
                    Vector3 thisPosition = _targetScreenPos;
                    Vector3 worldPosition = Camera.main.ScreenToWorldPoint(thisPosition);
                    worldPosition.z = 0f;

                    Vector3 localPosition = this.transform.InverseTransformPoint(targetWorldPos);
             */
        /*
            RectTransformUtility.ScreenPointToLocalPointInRectangle(
                _parentUI,
                _targetScreenPos,
                null,//オーバーレイモードの場合null
                out uiLocalPos
            );
        */

        /*public static bool ScreenPointToLocalPointInRectangle(
         * RectTransform rect, 変換先のRectTransFormローカル座標の親を指定します。
         * Vector2 screenPoint,変換元のスクリーン座標を指定
         * Camera cam,
         * out Vector2 localPoint
         * );
         * 2023/05/04メモ　Prehab化したオブジェクトだとOverridesのaplly Allができず_parentUIがnullになる。
         * LockOnMarkオブジェクトを配列で5つ宣言してやってみる。
         */

        //Debug.Log("LocOnMark.cs localPosition  " + localPosition);

        //this.transform.localPosition =  uiLocalPos;
         //_targetUI.localPosition =  uiLocalPos;
            //this.localPosition =  uiLocalPos;
        //Debug.Log("_targetScreenPos" + _targetScreenPos);
        //Debug.Log("_targetUI.localPosition" + this.transform.localPosition);

    }

    public void SetTarget(string tagName)
    {
        _lockOnTargetTag = tagName;
    }

    public void SetTargetObject(GameObject target)
    {
        _lockOnTarget = target;
        Debug.Log("LockOnMark_SetTarget = " + _lockOnTarget);
    }

    public void Delete() { Destroy(this.gameObject); }

}
