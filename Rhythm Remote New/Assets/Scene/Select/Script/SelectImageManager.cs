using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SelectImageManager : MonoBehaviour {

    public GameObject _mapImage;
    public float _imageDistance = 110.0f;
    private int _previousTarget = 0;
    private int _currentTarget = 0;
    private List<string> _songsTextData = new List<string>();
    private Sprite[] _sprites;
    private Vector3 _targetPosition;
    private string _sceneName;
    private GameObject _autoButton;
    public GameObject _blackMask;
    public Vector3 _scalingParameter = new Vector3(0.3f, 0.3f, 0.3f);
    public Text _title;


    void Awake()
    {
        this.LoadSprites();
        this.LoadData();
    }

	// Use this for initialization
	void Start () {
        _targetPosition = this.transform.position;
        _blackMask.GetComponent<Image>().raycastTarget = false;
        _autoButton = GameObject.Find("Auto") as GameObject;

        this.SetSongTitle();
	}

    void FixedUpdate()
    {
        this.transform.position = Vector3.Lerp(this.transform.position, _targetPosition, 0.025f);
    }

    private void LoadSprites()
    {
        Sprite[] sprites = Resources.LoadAll<Sprite>("SongsImage");
        _sprites = sprites;
    }

    private void LoadData()
    {
        string[] splitFile = new string[] { "\r\n", "\r", "\n" };
        TextAsset txt = Resources.Load<TextAsset>("SongsTextData");
        string[] text = txt.text.Split(splitFile, System.StringSplitOptions.RemoveEmptyEntries);
        for (int i = 0; i < text.Length; i++)
        {
            _songsTextData.Add(text[i]);
            GameObject obj = Instantiate(_mapImage, this.transform.position, this.transform.rotation) as GameObject;
            obj.transform.SetParent(this.transform);
            obj.transform.Translate(i * _imageDistance, 0.0f, 0.0f);
            Image mapImage = obj.GetComponent<Image>();
            mapImage.preserveAspect = true;
            mapImage.raycastTarget = false;
            mapImage.sprite = _sprites[i];
            if (i == 0)
                obj.transform.localScale = _scalingParameter;
        }
    }

    public void OnRightArrowClick()
    {
        if (_currentTarget != _sprites.Length - 1)
        {
            _previousTarget = _currentTarget;
            _currentTarget++;
            _targetPosition = new Vector3(_targetPosition.x - _imageDistance, _targetPosition.y, _targetPosition.z);
            this.ScaleTargetMap();
            this.SetSongTitle();
        }
    }

    public void OnLeftArrowClick()
    {
        if (_currentTarget != 0)
        {
            _previousTarget = _currentTarget;
            _currentTarget--;
            _targetPosition = new Vector3(_targetPosition.x + _imageDistance, _targetPosition.y, _targetPosition.z);
            this.ScaleTargetMap();
            this.SetSongTitle();
        }
    }

    public void OnStartClick()
    {
        _blackMask.GetComponent<Image>().raycastTarget = true;
        _blackMask.GetComponent<Animator>().SetBool("_switch", true);
        PlayerPrefs.SetString("SongsTextData", _songsTextData[_currentTarget]);
        PlayerPrefs.SetInt("AutoPlay", _autoButton.GetComponent<ClickToSwitchColor>().GetStatas());
        _sceneName = "Game";
        Invoke("LoadScene", 1.0f);
    }

    public void OnIndexClick()
    {
        _blackMask.GetComponent<Image>().raycastTarget = true;
        _blackMask.GetComponent<Animator>().SetBool("_switch", true);
        _sceneName = "Index";
        Invoke("LoadScene", 1.0f);
    }

    private void SetSongTitle()
    {
        string text = _songsTextData[_currentTarget];
        string[] split = text.Split(new string[]{" "}, System.StringSplitOptions.RemoveEmptyEntries);
        string newText = "";
        for (int i = 2; i < split.Length; i++)
            newText += (split[i] + " ");
        _title.text = newText;
    }

    private void LoadScene()
    {
        SceneManager.LoadSceneAsync(_sceneName, LoadSceneMode.Single);
    }

    private void ScaleTargetMap()
    {
        Transform mapPrevious = this.transform.GetChild(_previousTarget);
        Transform mapCurrent = this.transform.GetChild(_currentTarget);
        mapPrevious.localScale = mapCurrent.localScale;
        mapCurrent.localScale = _scalingParameter;
    }
}
