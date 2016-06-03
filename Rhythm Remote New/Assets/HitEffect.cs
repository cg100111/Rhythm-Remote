using UnityEngine;
using System.Collections;

public class HitEffect : MonoBehaviour {

    private ObjectPool _poolTarget;
    private Animator _animator;
    public float _time = 1.1f;

    public void Play()
    {
        _animator = this.GetComponent<Animator>();
        _animator.Play("hitEffect", -1, 0);
        Invoke("Collection", _time);
    }

    public void SetPoolTarget(ObjectPool poolTarget)
    {
        _poolTarget = poolTarget;
    }

    private void Collection()
    {
        _poolTarget.CollectionObject(this.gameObject);
    }
}
