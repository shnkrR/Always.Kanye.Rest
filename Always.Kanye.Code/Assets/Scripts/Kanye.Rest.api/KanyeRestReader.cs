using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

public class KanyeRestReader : MonoBehaviour
{
    public static System.Action<KanyeRestData> OnQuoteSuccess;
    public static System.Action<string> OnQuoteFailure;

    public const string _URL = "https://api.kanye.rest";

    public KanyeRestData _KanyeRestData { get; private set; }

    public bool _IsReady { get; private set; }

    private UnityWebRequest mWebObject;

    
    private void Awake()
    {
        _IsReady = true;
    }

    private void OnDestroy()
    {
        StopAllCoroutines();
        mWebObject.Dispose();
        mWebObject = null;
    }

    public void BeginObtainQuote()
    {
        if (!_IsReady)
            return;

        _IsReady = false;

        StartCoroutine(DelayedObtainQuote());
    }

    private IEnumerator DelayedObtainQuote()
    {
        mWebObject = UnityWebRequest.Get(_URL);
        yield return mWebObject.SendWebRequest();

        if (mWebObject.isNetworkError || mWebObject.isHttpError)
        {
            Debug.Log(mWebObject.error);
            OnQuoteFailure(mWebObject.error);
        }
        else
        {
            try
            {
                _KanyeRestData = JsonUtility.FromJson<KanyeRestData>(mWebObject.downloadHandler.text);
                OnQuoteSuccess(_KanyeRestData);
            }
            catch (Exception e)
            {
                Debug.Log(e.ToString());
                OnQuoteFailure(e.ToString());
            }
        }

        //mWebObject.Dispose();

        _IsReady = true;

        yield return null;
    }
}
