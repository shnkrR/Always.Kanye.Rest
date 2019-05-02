using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KanyeRestUI : MonoBehaviour
{
    public Text _LabelQuote;


    public void SetQuote(string quote)
    {
        StopAllCoroutines();
        StartCoroutine(DelayedSetQuote(quote));
    }

    public void FadeQuote()
    {
        StopAllCoroutines();
        StartCoroutine(DelayedFadeQuote());
    }

    private IEnumerator DelayedSetQuote(string quote)
    {
        Color color = _LabelQuote.color;
        while (color.a > 0.0f)
        {
            color.a -= (Time.deltaTime * 5.0f);
            _LabelQuote.color = color;
            yield return null;
        }

        _LabelQuote.text = "\"" + quote + "\"";

        while (_LabelQuote.color.a < 1.0f)
        {
            color.a += (Time.deltaTime * 5.0f);
            _LabelQuote.color = color;
            yield return null;
        }
    }

    private IEnumerator DelayedFadeQuote()
    {
        Color color = _LabelQuote.color;
        while (color.a > 0.0f)
        {
            color.a -= (Time.deltaTime * 5.0f);
            _LabelQuote.color = color;
            yield return null;
        }

        _LabelQuote.text = "\"" + "\"";
    }
}
