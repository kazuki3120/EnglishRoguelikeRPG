using UnityEngine;

public class CameraController : MonoBehaviour
{
    //カメラの移動量
    [SerializeField, Range(1f, 200.0f)] //インスペクターから感度設定
    private float _positionStep = 200.0f;

    //カメラのtransform  
    private Transform _camTransform;
    //マウスの始点 
    private Vector3 _startMousePos;
    private Vector3 _presentCamPos;


    void Start()
    {
        _camTransform = this.gameObject.transform;
    }


    void Update()
    {
        //カメラの縦横移動 マウス
        CameraSlideMouseControl(); 

        //カメラ移動範囲制御
        Vector3 currentPos = transform.position;

        currentPos.x = Mathf.Clamp(currentPos.x, 0, 0); //x固定
        currentPos.y = Mathf.Clamp(currentPos.y, -30, 350); //yの範囲 -30 ~ 350

        transform.position = currentPos;

    }


    private void CameraSlideMouseControl()
    {
        //右クリックを押したとき
        if (Input.GetMouseButtonDown(1))
        {
            _startMousePos = Input.mousePosition;
            _presentCamPos = _camTransform.position;
        }

        //右クリックを離したとき
        if (Input.GetMouseButton(1))
        {
            //(移動開始座標 - マウスの現在座標) / 解像度 で正規化
            float x = (_startMousePos.x - Input.mousePosition.x) / Screen.width;
            float y = (_startMousePos.y - Input.mousePosition.y) / Screen.height;

            x = x * _positionStep;
            y = y * _positionStep;

            Vector3 velocity = new Vector3(x, y);
            velocity = velocity + _presentCamPos;
            _camTransform.position = velocity;
        }
    }
}