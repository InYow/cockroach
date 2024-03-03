using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Dot : MonoBehaviour
{
    private Image image;
    public DotData spriteData;
    // Start is called before the first frame update
    void Start()
    {
        image = GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void Fill()
    {
        image.sprite = spriteData.fill;
    }
    public void Empty()
    {
        image.sprite = spriteData.empty;
    }
}
