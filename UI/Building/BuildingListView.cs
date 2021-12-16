using UnityEngine;
using System.Collections;

public class BuildingListView : MonoBehaviour
{
    bool _bShow = false;

    virtual public bool bShow
    {
        get
        {
            return _bShow;
        }
        set
        {
            _bShow = value;
        }
    }

    public void ClickEvect()
    {
        if (FactoryManager.Instance.GetPopUP())
        {
            return;
        }

        if (!_bShow)
        {
            ShowBuildingView();
        }
        else
        {
            HideBuildingView();
        }
    }

    public void ShowBuildingView()
    {
        //UIScrollView View = gameObject.GetComponentInChildren<UIScrollView>();

        //View.enabled = false;

        //BoxCollider der;

        _bShow = true;
        Hashtable hash = new Hashtable();
        hash.Add("position", new Vector3(335.0f, -22.0f, 0));
        hash.Add("Speed", 70.0f);
        hash.Add("isLocal", true);


        iTween.MoveTo( gameObject, hash );
    }

    void OnTriggerEnter(Collider other)
    {
        Debug.Log("OnTriggerEnter" + other.gameObject.name);
        //Destroy(other.gameObject);
    }

    public void HideBuildingView()
    {
        _bShow = false;

        Hashtable hash = new Hashtable();
        hash.Add("position", new Vector3(470.0f, -22.0f, 0));
        hash.Add("Speed", 70.0f);
        hash.Add("isLocal", true);


        iTween.MoveTo(gameObject, hash);
    }
}
