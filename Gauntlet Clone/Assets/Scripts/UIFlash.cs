using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

/* FILE HEADER
*  Edited by: Chase Morgan
*  Last Updated: 05/14/2024
*  Script Description: Quick and dirty flash for UI elements
*/


public class UIFlash : MonoBehaviour
{
    [SerializeField] private float flashIncrement = 1;
    [SerializeField] protected float flashFadeTime = 0.5f;

    private float time = 0f;
    private bool alphaDecrement = true;

    private void Start()
    {
        StartCoroutine(Flash());
    }

    private IEnumerator Flash()
    {
        yield return new WaitUntil(() => gameObject.activeInHierarchy);
        TMP_Text text = GetComponent<TMP_Text>();
        while (time < flashFadeTime)
        {
            if (alphaDecrement)
                text.color = new Color(text.color.r, text.color.g, text.color.b, Mathf.Lerp(1, 0, time / flashFadeTime));
            else
                text.color = new Color(text.color.r, text.color.g, text.color.b, Mathf.Lerp(0, 1, time / flashFadeTime));

            time += Time.deltaTime;
            yield return null;
        }

        time = 0;
        if (alphaDecrement)
            text.color = new Color(text.color.r, text.color.g, text.color.b, 0);
        else
            text.color = new Color(text.color.r, text.color.g, text.color.b, 1);

        alphaDecrement = !alphaDecrement;

        yield return new WaitForSeconds(flashIncrement - flashFadeTime);
        StartCoroutine(Flash());
    }
}
