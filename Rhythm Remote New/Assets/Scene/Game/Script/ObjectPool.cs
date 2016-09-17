using UnityEngine;
using System.Collections.Generic;

public class ObjectPool{

    private List<GameObject> _objects;
    private GameObject _parent;
    private List<int> _emptyRecord = new List<int>();           
    private List<bool> _notUsedRecord;                                //記錄沒使用的Object

    //---------------------------------
    private Vector3 _poolPosition = new Vector3(-999.0f, -999.0f, -999.0f);
    private Quaternion _poolRotation = new Quaternion();
    //---------------------------------


    public ObjectPool(GameObject obj, int count)
    {
        _parent = obj;
        _objects = new List<GameObject>();
        _notUsedRecord = new List<bool>();
        this.Create(count);
    }

    //建立GameObject pool
    private void Create(int count)
    {
        for (int i = 0; i < count; i++)
        {
            _objects.Add((GameObject)(GameObject.Instantiate(_parent, _poolPosition, _poolRotation)));
            _notUsedRecord.Add(true);
            _objects[i].SetActive(false);
        }
    }

    //回收GameObject
    public void CollectionObject(GameObject obj)
    {
        obj.SetActive(false);
        obj.transform.position = _poolPosition;
        _notUsedRecord[_emptyRecord[0]] = true;
        _emptyRecord.RemoveAt(0);
    }

    //使用GameObject 傳入開始座標,旋轉角度
    public GameObject AccessObject(Vector3 position, Quaternion rotation)
    {
        for (int i = 0; i < _notUsedRecord.Count; i++)
        {
            if (_notUsedRecord[i])
            {
                _notUsedRecord[i] = false;
                _emptyRecord.Add(i);
                _objects[i].transform.position = position;
                _objects[i].transform.rotation = rotation;
                _objects[i].SetActive(true);
                return _objects[i];
            }
        }
        return null;
    }
}