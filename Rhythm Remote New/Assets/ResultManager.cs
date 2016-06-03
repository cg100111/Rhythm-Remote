using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ResultManager : MonoBehaviour {

    public GameObject _blackMask;
    private string _sceneName;

    public Image _imageSong;
    public Text _songTitle;
    
    public Text _text300;
    public Text _text100;
    public Text _text50;
    public Text _textMiss;
    public Text _textCombos;
    public Text _textScore;
    public Text _textAccuracy;

    void Awake()
    {
        this.PCLoadHitSound();
        this.Load();
    }

    private void PCLoadHitSound()
    {
        #if !UNITY_ANDROID || UNITY_EDITOR
            HitSound hit = GameObject.Find("AudioCenter").GetComponent<HitSound>();
            hit.SetSound();
        #else
        #endif
    } 

    void Start()
    {
        _blackMask.GetComponent<Image>().raycastTarget = false;
    }


    private void Load()
    {
        _imageSong.sprite = Resources.Load<Sprite>("SongsImage/" + PlayerPrefs.GetString("SongsTextData"));
        string text = PlayerPrefs.GetString("SongsTextData");
        string[] split = text.Split(new string[]{" "}, System.StringSplitOptions.RemoveEmptyEntries);
        string newText = "";
        for (int i = 2; i < split.Length; i++)
            newText += (split[i] + " ");
        _songTitle.text = newText;
        _textScore.text = PlayerPrefs.GetInt("Score").ToString();
        _textCombos.text = PlayerPrefs.GetInt("MaxCombos").ToString();

        float three = PlayerPrefs.GetInt("ThreeHundredCount");
        float one = PlayerPrefs.GetInt("OneHundredCount");
        float fifty = PlayerPrefs.GetInt("FiftyCount");
        float miss = PlayerPrefs.GetInt("MissCount");
        float acc = (three * 3 + one * 2 + fifty * 1 ) / ((three + one + fifty + miss) * 3) * 100.0f;

        _text300.text = PlayerPrefs.GetInt("ThreeHundredCount").ToString();
        _text100.text = PlayerPrefs.GetInt("OneHundredCount").ToString();
        _text50.text = PlayerPrefs.GetInt("FiftyCount").ToString();
        _textMiss.text = PlayerPrefs.GetInt("MissCount").ToString();
        _textAccuracy.text = acc.ToString("0.00") + "%";

    }

    public void Retry()
    {
        _blackMask.GetComponent<Image>().raycastTarget = true;
        _blackMask.GetComponent<Animator>().SetBool("_switch", true);
        _sceneName = "Game";
        Invoke("LoadScene", 1.0f);
    }

    public void Exit()
    {
        _blackMask.GetComponent<Image>().raycastTarget = true;
        _blackMask.GetComponent<Animator>().SetBool("_switch", true);
        _sceneName = "Select";
        Invoke("LoadScene", 1.0f);
    }

    private void LoadScene()
    {
        AudioCenter.unloadSound(0);
        SceneManager.LoadSceneAsync(_sceneName, LoadSceneMode.Single);
    }
}
