using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ControlaInterface : MonoBehaviour
{
    private Slider sliderVida;

    // Start is called before the first frame update
    void Start()
    {
        sliderVida = (Slider)transform.Find("SliderVida").gameObject.GetComponent<Slider>();
        sliderVida.value = sliderVida.maxValue;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AtualizaSliderVida(int vida)
    {
        sliderVida.value = vida;
    }
}
