using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ShowFPS : MonoBehaviour
{
    float deltaTime = 0.0f;
    private Text _fps;

    void Start()
    {
        _fps = this.GetComponent<Text>();
    }

    void Update()
    {
        deltaTime += (Time.deltaTime - deltaTime) * 0.1f;
    }

    void FixedUpdate()
    {
        float fps = 1.0f / deltaTime;
        _fps.text = string.Format("({0:0} fps)", fps);
    }
}