using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using System.Collections.Generic;

public class TimeKeyJudge : MonoBehaviour, IPointerDownHandler, IPointerUpHandler {

    public GameObject _effect;                               //打到節奏時特效
    public AttackKey _key;
    public TimeKeyGeneraterFunction _timeKeyGenerator;
    public ShowAccEffect _accEffect;

    private HitSound _hitSound;                               //打到節奏時音效
    private ObjectPool _keyPool;
    private bool[] _finishAttack;

    // Use this for initialization
    void Start () {
        _hitSound = GameObject.Find("AudioCenter").GetComponent<HitSound>();
        _keyPool = new ObjectPool(_key.gameObject, 10);
        _finishAttack = new bool[] { false, false, false, false, false, false, false };
    }

    //點擊
    public void OnPointerDown(PointerEventData data)
    {
        if(_timeKeyGenerator.TouchKeys(data.pointerCurrentRaycast.gameObject.ToString()))
            ManufactureKey();
        this.PlaySound();
        _effect.SetActive(true);
    }

    //放開
    public void OnPointerUp(PointerEventData data)
    {
        _effect.SetActive(false);
    }

    //產生key
    private void ManufactureKey()
    {
        float x = transform.parent.position.x;
        float y = GameObject.Find("JudgeLine").transform.position.y;
        AttackKey key = _keyPool.AccessObject(new Vector3(x, y, 0f), this.transform.rotation).GetComponent<AttackKey>();
        key.transform.SetParent(GameObject.Find("Keys").transform);
        key.SetKeyInformation(_keyPool);
    }

    //點擊失敗
    private void KeyFail()
    {
        _accEffect.ShowMiss();
    }

    //播放點擊音效
    private void PlaySound()
    {
        _hitSound.PlaySound();
    }
}
