using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Panel : MonoBehaviour
{
    public static Panel Instance;
    public AnimationCurve showCurve;
    public AnimationCurve hideCurve;
    public float animationSpeed;
    public GameObject panel;
    public Image Im;
    public float AlphaValue = 0;

    private void Awake()
    {
        Instance = this;
    }
    public IEnumerator ShowPanel(GameObject gameObject)
    {
        float timer = 0;
        while (timer <= 1)
        {
            gameObject.transform.localScale = Vector3.one * showCurve.Evaluate(timer);
            timer += Time.deltaTime * animationSpeed;
            yield return null;
        }
    }

    IEnumerator HidePanel(GameObject gameObject)
    {
        float timer = 0;
        while (timer <= 1)
        {
            gameObject.transform.localScale = Vector3.one * hideCurve.Evaluate(timer);
            timer += Time.deltaTime * animationSpeed;
            yield return null;
        }
    }

    IEnumerator Hide2Panel(GameObject gameObject)
    {
        float timer = 0;
        while (timer <= 1)
        {
            Im = gameObject.GetComponent<Image>();
            Color co = Im.color;

            co.a = AlphaValue;
            Im.color = co;

            yield return null;
        }
    }

    void Update()
    {
        // if (Input.GetMouseButtonDown(0))
        // {
        //     StartCoroutine(ShowPanel(panel));
        // }

        // else if (Input.GetMouseButtonDown(1))
        // {
        //     StartCoroutine(HidePanel(panel));
        // }
        // else if (Input.GetKeyDown(KeyCode.Space))
        // {
        //     StartCoroutine(Hide2Panel(panel));
        // }
    }
}