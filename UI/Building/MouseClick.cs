using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using STORY_ENUM;

public class MouseClick : MonoBehaviour {

    public Camera []_mainCam;

    public GameObject _BuildingListView;

    public GameObject _RootTarget;

    private GameObject _Building = null;

    /// <summary>
    /// 마우스가 다운된 오브젝트
    /// </summary>
    private GameObject _target;
    /// <summary>
    /// 마우스 좌표
    /// </summary>
    private Vector3 _MousePos;

    private Vector3 _scale;

    private bool _mouseState;

    public float sensitivityX = 1F;

    public float minimumY = -180F;
    public float maximumY = 180F;

    float rotationY = 0F;

    delegate void MouseMove();

    private Dictionary<int, Delegate> _eventFun;
    int _nDeleageIndex = 0;

    //4.x 버전에서는 'void Start()'로 바꿔 주세요.
    void Start()
    {
        _eventFun = new Dictionary<int, Delegate>();

        _eventFun.Add(0, new MouseMove( MapRotation ));
        _eventFun.Add(1, new MouseMove( BuildingMove ));

        //_mainCam = Camera.main;
        _BuildingListView = GameObject.Find("Panel(BuildingList)");

        //Debug.Log(_BuildingListView.name);
    }

    // Update is called once per frame 
    void Update()
    {
        if (FactoryManager.Instance.GetPopUP() )
        {
            return;
        }
        
        //마우스가 내려갔는지?
        if (true == Input.GetMouseButtonDown(0))
        {
            //내려갔다.

            //타겟을 받아온다.
            _target = GetClickedObject();

            if (_target)
            {
                _scale = _target.transform.localScale;
                _target.transform.localScale = new Vector3(_scale.x * 1.1f, _scale.y * 1.1f, _scale.z * 1.1f);
                CreateBuilding();
            }

            _MousePos = Input.mousePosition;
            _mouseState = true;
        }
        else if (true == Input.GetMouseButtonUp(0))
        {
            //마우스가 올라 갔다.
            //마우스 정보를 바꾼다.

            _Building = null;
            GameObject ObjTemp = GetClickedObject();

            if (_target)
            {
                //Vector3 scale = _target.transform.localScale;
                _target.transform.localScale = _scale;
       
            }

            if (ObjTemp)
            {
                
                if (_target == ObjTemp)
                {
                    BuildingEvent(_target.name);
                }

            }

            _mouseState = false;
        }


        if (_mouseState)
        {
            MouseMove fun = (MouseMove)_eventFun[_nDeleageIndex];
            fun();
        }
        
    }

    void BuildingMove()
    {
        return;
        if (_Building != null)
        {

            Ray ray = _mainCam[0].ScreenPointToRay(Input.mousePosition);

            //Vector3 MousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            _Building.transform.localPosition = ray.origin;
        }
    }

    void CreateBuilding()
    {
        return;
        Ray ray = _mainCam[0].ScreenPointToRay(Input.mousePosition);


        GameObject obj = Resources.Load("Prefabs/Map/Object/buillding01_P", typeof(GameObject)) as GameObject;

        _Building = Instantiate(obj, ray.origin, obj.transform.rotation) as GameObject;
        _Building.transform.localScale = obj.transform.localScale;

        //Vector3 MousePos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, Input.mousePosition.z));
        //타겟의 위치 변경
        //_Building.transform.localPosition = ray.origin;

    }

    void MapRotation()
    {
        //float rotationX = transform.localEulerAngles.y + Input.GetAxis("Mouse X") * sensitivityX;

        rotationY += ((_MousePos.x - Input.mousePosition.x) * sensitivityX);

        rotationY = Mathf.Clamp(rotationY, minimumY, maximumY);
        _RootTarget.transform.localEulerAngles = new Vector3(0, -rotationY, 0);

        _MousePos = Input.mousePosition;
    }

    private bool BuildingEvent( string strName )
    {
        if (strName == "buillding02_P")
        {
			FactoryManager.Instance.CreatePopUp(POPUP_TYPE.DungeonTypePopUp);
			//FactoryManager.Instance.CreatePopUp(POPUP_TYPE.ResultPopUp);
        }

        return true;
    }


    string GetLayerTag()
    {
        string strResult = "BUILDING";

        _nDeleageIndex = 0;

         if (_BuildingListView.GetComponent<BuildingListView>().bShow)
         {
             _nDeleageIndex = 1;
             strResult = "2DNGUI";
         }

         return strResult;
    }

    /// <summary>
    /// 마우스가 내려간 오브젝트를 가지고 옵니다.
    /// </summary>
    /// <returns>선택된 오브젝트</returns>
    private GameObject GetClickedObject()
    {
        //충돌이 감지된 영역
        RaycastHit hit;
        //찾은 오브젝트
        GameObject target = null;

        //마우스 포이트 근처 좌표를 만든다.
        Ray ray = _mainCam[_nDeleageIndex].ScreenPointToRay(Input.mousePosition);

        string LayerTag = GetLayerTag();

        int layerMask = ((1 << LayerMask.NameToLayer(LayerTag)));

        //Debug.Log(layerMask.ToString());
        //Debug.Log(LayerMask.NameToLayer(LayerTag).ToString());

        //마우스 근처에 오브젝트가 있는지 확인
        //if (true == (Physics.Raycast(ray.origin, ray.direction * 10, out hit)))

        if (true == ( Physics.Raycast(ray.origin, ray.direction, out hit, Mathf.Infinity, layerMask ) ) )
        {
            //있다!

            //있으면 오브젝트를 저장한다.
            target = hit.collider.gameObject;

            //Debug.Log(target.name);
            return target;
        }

        return null;
    }
}
