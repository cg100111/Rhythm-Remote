using UnityEngine;
using System.Collections;

public class AttackKey: MonoBehaviour {

    private ObjectPool _poolTarget;
	
	// Update is called once per frame
	void FixedUpdate() {
        move();
        //如果超過該點擊的位置
        if (this.transform.localPosition.y >= 145.0f)
            Collection();
    }

    //設定資訊
    public void SetKeyInformation( ObjectPool poolObject)
    {
        _poolTarget = poolObject;
    }

    //移動
    private void move()
    {
        this.transform.Translate(0f, 248.0f * 0.008f, 0f);
    }

    //回收自己
    public void Collection()
    {
        _poolTarget.CollectionObject(this.gameObject);
    }
}
