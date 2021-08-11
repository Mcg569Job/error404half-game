using UnityEngine;

[System.Serializable]
public class Pool 
{
    [Header("Pool Settings")]
    public GameObject _ObjectPrefab;
    public int _ObjectCount;
   [HideInInspector] public GameObject[] _Objects;
    private int _ObjectId = 0;

    public void CreatePool(Transform parent)
    {
        _Objects = new GameObject[_ObjectCount];

        for (int i = 0; i < _ObjectCount; i++)
        {
            _Objects[i] = GameObject.Instantiate(_ObjectPrefab, Vector3.zero, _ObjectPrefab.transform.rotation,parent);
            _Objects[i].SetActive(false);
        }
    }


    public GameObject GetObject()
    {
        GameObject o = _Objects[_ObjectId];
        o.SetActive(true);

        _ObjectId++;
        if (_ObjectId > _ObjectCount - 1) _ObjectId = 0;

        return o;
    }
}
