using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class ClickToPlaySound : MonoBehaviour,IPointerClickHandler {

    private HitSound _audio;

    void Start()
    {
        _audio = GameObject.Find("AudioCenter").GetComponent<HitSound>();
    }

	// Update is called once per frame
    public void OnPointerClick(PointerEventData data)
    {
        _audio.PlaySound();
    }
}
