using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockOnMark : MonoBehaviour
{

    [SerializeField]private Camera _targetCamera;

    private static RectTransform _parentUI;
    //一番上の親に付いているコンポーネントを取得する。Campas
    //子のコンポーネントはのGetComponentChildren<RectTransform>();

    //[SerializeField]
    private Transform _target;
    //[SerializeField]
    private Transform _targetUI;
    [SerializeField]private Vector3 _worldOffset=new Vector3(0f, 0f,0f);

    private Vector2 _targetScreenPos;
    private Vector2 uiLocalPos;
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
        
    public void SetTarget(string tagName) {
        _lockOnTargetTag = tagName;
    }

    private void Awake(){
  
        if(_targetCamera == null){
         _targetCamera = Camera.main;
        }

        _parentUI = GetComponentInParent<RectTransform>();
        //一番上の親に付いているコンポーネントを取得する。Campas
        //子のコンポーネントはのGetComponentChildren<RectTransform>();

        //_parentUI = _targetUI.parent.GetComponent<RectTransform>();
        //_parentUI = this.GetComponent<RectTransform>();
        //このcontextでKetword "this"は使えない。理由は不明。
    }

    void Update()
    {
      /*if (_timer < 0.1f)
        {
            _timer += Time.deltaTime; return;
        }*/

        
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
         *オブジェクトのワールド座標→スクリーン座標→TransFormRectのローカル座標
         *の順に座標変換する必要があります。
         * このとき、カメラ背後に位置するオブジェクトもスクリーン座標に投影されるため
         * 背後のものを非表示にしたい場合は前後判定を行う必要があります。
         */
        
        
        //try{
        Debug.Log("_lockOnTargetTag  " + _lockOnTargetTag);

        _lockOnTarget = GameObject.FindGameObjectWithTag(_lockOnTargetTag);

        if (_lockOnTarget == null) { Destroy(this.gameObject); }

        _target = _lockOnTarget.transform;

        var targetWorldPos = _target.position + _worldOffset;
        var targetDir = targetWorldPos - cameraTransform.position;
        

        //内積を使ってカメラ前方かどうか判定
        var isFront = Vector3.Dot(cameraDir,targetDir) > 0;

        this.gameObject.SetActive(isFront);
        //_targetUI.gameObject.SetActive(isFront);
        if(!isFront)return;
        

        _targetScreenPos = _targetCamera.WorldToScreenPoint(targetWorldPos);
        //public Vector3 WorldToScreenPoint(Vector3 position);

         //Debug.Log(_parentUI, transform);
         //Debug.Log(_targetCamera);

        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            _parentUI,
            _targetScreenPos,
            _targetCamera,//オーバーレイモードの場合null
            out uiLocalPos);

        /*public static bool ScreenPointToLocalPointInRectangle(
         * RectTransform rect, 変換先のRectTransFormローカル座標の親を指定します。
         * Vector2 screenPoint,変換元のスクリーン座標を指定
         * Camera cam,
         * out Vector2 localPoint
         * );
         */

        this.transform.localPosition =  uiLocalPos;
         //_targetUI.localPosition =  uiLocalPos;
            //this.localPosition =  uiLocalPos;
        //Debug.Log("_targetScreenPos" + _targetScreenPos);
        //Debug.Log("_targetUI.localPosition" + this.transform.localPosition);
        //}catch(IOException e){Debug.Log("Exception" + e);}
        //}catch(NullReferenceException e){Debug.Log("Exception" + e);}
    }
    public void Delete() { Destroy(this.gameObject); }

}
