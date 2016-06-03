using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class keyBarFunction : MonoBehaviour, IPointerDownHandler, IPointerUpHandler {

    public GameObject _effect;
    private KeyGeneratorFunction _generator;
    private HitSound _audio;

	// Use this for initialization
	void Start () {
        GameObject effect = Instantiate(_effect, this.transform.position, this.transform.rotation) as GameObject;
        effect.transform.SetParent(this.transform.parent.transform);
        _effect = effect;
        _effect.SetActive(false);
        _generator = this.transform.parent.GetComponent<KeyGeneratorFunction>();
        _audio = GameObject.Find("AudioCenter").GetComponent<HitSound>();
	}

    public void OnPointerDown(PointerEventData data)
    {
        _generator.TouchKeys();
        this.PlaySound();
        _effect.SetActive(true);
    }

    public void OnPointerUp(PointerEventData data)
    {
        _effect.SetActive(false);
    }

    private void PlaySound()
    {
        _audio.PlaySound();
    }

    //Auto
    public void AutoDown(float time)
    {
        _generator.TouchKeys();
        this.PlaySound();
        _effect.SetActive(true);
        Invoke("AutoUp", time);
    }

    public void AutoUp()
    {
        _effect.SetActive(false);
    }
}
