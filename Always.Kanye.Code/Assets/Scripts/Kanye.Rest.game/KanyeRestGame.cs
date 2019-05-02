using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(KanyeRestReader))]
public class KanyeRestGame : MonoBehaviour
{
    public KanyeRestUI _UI;

    private KanyeRestReader _DataReader;


    private void Start()
    {
        KanyeRestReader.OnQuoteSuccess += OnObtainQuoteSuccess;
        KanyeRestReader.OnQuoteFailure += OnObtainQuoteFailure;

        _DataReader = gameObject.GetComponent<KanyeRestReader>();
        _DataReader.BeginObtainQuote();
    }

    private void OnDestroy()
    {
        KanyeRestReader.OnQuoteSuccess -= OnObtainQuoteSuccess;
        KanyeRestReader.OnQuoteFailure -= OnObtainQuoteFailure;
    }
    
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && _DataReader._IsReady)
        {
            _UI.FadeQuote();
            _DataReader.BeginObtainQuote();
        }
    }

    private void OnObtainQuoteSuccess(KanyeRestData data)
    {
        _UI.SetQuote(data.quote);
    }

    private void OnObtainQuoteFailure(string error)
    {
        Debug.Log("Error: " + error);
    }
}
