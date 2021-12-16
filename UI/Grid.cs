using UnityEngine;
using System.Collections;

public class Grid : MonoBehaviour {

    public GameObject _Item;

    public Texture[] _Textures;
    /** 처음 객체가 로딩될 때, 초기화 함수 호출 */
    void Start()
    {
        //StartCoroutine(InitItem());
    }

    /** Grid 초기화 */
    IEnumerator InitItem()
    {
        //GameObject Item = Resources.Load("Prefabs/UI/Building/BuildingItem", typeof(GameObject)) as GameObject;

        // 이미지의 수 만큼 반복합니다.
        for (int i = 0; i < 8; i++)
        {
            //일단 생성합니다. 무조건...

            GameObject obj = Instantiate(_Item, Vector3.zero, Quaternion.identity) as GameObject;

            //생성된 GameObject의 부모가 누구인지 명확히 알려줍니다. (내가 니 애비다!!)
            obj.transform.parent = transform;

            //NGUI는 자동이 너무많이 짜증나니 수동으로 Scale을 조정해줍니다.
            obj.transform.localScale = new Vector3(1f, 1f, 1f);
            obj.transform.localPosition = new Vector3( 0, 0, 0 );

            //UITexture texture = GetChildObj(obj, "Icon").GetComponent<UITexture>();
            //texture.mainTexture = _Textures[i];

        }

        //Prefab을 생성한 이후에 Position이 모두 같아서 겹쳐지므로 Reposition시키도록 합니다.
        GetComponent<UIGrid>().Reposition();

        yield return null;
    }

    /** 객체의 이름을 통하여 자식 요소를 찾아서 리턴하는 함수 */
    GameObject GetChildObj(GameObject source, string strName)
    {
        Transform[] AllData = source.GetComponentsInChildren<Transform>();
        GameObject target = null;

        foreach (Transform Obj in AllData)
        {
            if (Obj.name == strName)
            {
                target = Obj.gameObject;
                break;
            }
        }

        return target;
    }
}
