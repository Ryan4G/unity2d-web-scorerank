using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

[Serializable]
public class HighScore
{
    public int? id;
    public string name;
    public int score;

    public string md5;
}

[Serializable]
public class HighScoreList
{
    public HighScore[] scores;
}

public class RankScores : MonoBehaviour
{
    InputField input_username;
    InputField input_score;

    Button btn_upload;
    Button btn_download;

    ScrollRect m_scrollrect;

    bool is_connecting = false;

    Text txt_result;

    public Text m_preItemText;

    // Start is called before the first frame update
    void Start()
    {
        foreach(Transform t in this.GetComponentsInChildren<Transform>())
        {
            if (t.name.CompareTo("input_username") == 0)
            {
                input_username = t.GetComponent<InputField>();
            }
            else if (t.name.CompareTo("input_score") == 0)
            {
                input_score = t.GetComponent<InputField>();
            }
            else if (t.name.CompareTo("btn_upload") == 0)
            {
                btn_upload = t.GetComponent<Button>();
            }
            else if (t.name.CompareTo("btn_download") == 0)
            {
                btn_download = t.GetComponent<Button>();
            }
            else if (t.name.CompareTo("Scroll View") == 0)
            {
                m_scrollrect = t.GetComponent<ScrollRect>();
            }
            else if (t.name.CompareTo("txt_result") == 0)
            {
                txt_result = t.GetComponent<Text>();
            }
        }

        btn_upload?.onClick.AddListener(() =>
        {
            updateResult("Uploading...");

            if (is_connecting)
            {
                return;
            }

            is_connecting = true;

            StartCoroutine(IUploadScore());
        });

        btn_download?.onClick.AddListener(() =>
        {
            updateResult("Downloading...");

            if (is_connecting)
            {
                return;
            }

            is_connecting = true;

            StartCoroutine(IDownloadScore());
        });
    }

    private IEnumerator IDownloadScore()
    {
        UnityWebRequest www = UnityWebRequest.Get($"http://localhost:53944/ScoreRank/GetScores?rows=10&pages=0");

        yield return www.SendWebRequest();

        if (www.isHttpError || www.isNetworkError)
        {
            is_connecting = false;

            yield break;
        }
        else
        {
            if (www.isDone)
            {
                is_connecting = false;

                try
                {
                    var data = JsonUtility.FromJson<HighScoreList>(www.downloadHandler.text);

                    var parent = m_scrollrect.content;

                    foreach(var hi in data?.scores)
                    {
                        Transform tmp = Instantiate(m_preItemText, parent).transform;
                        tmp.localPosition = Vector3.zero;
                        tmp.localRotation = Quaternion.identity;
                        tmp.localScale = Vector3.one;

                        tmp.GetComponent<Text>().text = $"ID:{hi.id} Name:{hi.name} Score:{hi.score}";
                    }

                    this.updateResult("Download Success");
                }
                catch(Exception ex)
                {
                    Debug.Log($"Pharse JSON Error: {ex.Message}");
                }
            }
        }

    }

    private IEnumerator IUploadScore()
    {
        HighScore highScore = new HighScore();

        var name = input_username.text;
        var score = 0;

        if (!int.TryParse(input_score.text, out score))
        {
            is_connecting = false;

            Debug.Log("Score must be integer");
            yield break;
        }

        if (string.IsNullOrEmpty(name))
        {
            is_connecting = false;

            Debug.Log("Name must be string");
            yield break;
        }

        //highScore.id = 0;
        highScore.name = name;
        highScore.score = score;
        highScore.md5 = CalculateMD5(highScore);

        Debug.Log(highScore.md5);

        var json = JsonUtility.ToJson(highScore);
        UnityWebRequest www = new UnityWebRequest($"http://localhost:53944/ScoreRank/UploadScore", "POST");
        www.SetRequestHeader("Content-Type", "application/json");

        www.uploadHandler = new UploadHandlerRaw(Encoding.UTF8.GetBytes(json));
        www.downloadHandler = new DownloadHandlerBuffer();

        yield return www.SendWebRequest();

        if (www.isHttpError || www.isNetworkError)
        {
            is_connecting = false;

            yield break;
        }
        else
        {
            if (www.isDone)
            {
                is_connecting = false;

                Debug.Log(www.downloadHandler.text);

                this.updateResult("Upload Success");
            }
        }
    }

    private void updateResult(string text)
    {
        txt_result.text = text;
    }

    private string CalculateMD5(HighScore highScore)
    {
        var md5Str = $"{highScore.name}#{highScore.score}#MD5";

        System.Security.Cryptography.MD5 md5 = System.Security.Cryptography.MD5.Create();

        var bytes = md5.ComputeHash(Encoding.UTF8.GetBytes(md5Str));

        StringBuilder sb = new StringBuilder();

        foreach (var b in bytes)
        {
            sb.Append(b.ToString("X2"));
        }

        return sb.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
