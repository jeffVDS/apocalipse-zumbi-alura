using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlaJogador : MonoBehaviour
{
    public float velocidade = 10;

    // Start is called before the first frame update
    void Start()
    {
        
    }
    void Update()
    {
        float eixoX = Input.GetAxis("Horizontal");
        float eixoZ = Input.GetAxis("Vertical");

        var movimentoNormalizado = (new Vector3(eixoX, 0, eixoZ)) * Time.deltaTime * velocidade;

        transform.Translate(movimentoNormalizado);
    }
}
