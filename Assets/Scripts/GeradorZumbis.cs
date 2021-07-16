using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeradorZumbis : MonoBehaviour
{
    public GameObject zumbi;
    public float intervaloInicialZumbi = 3;
    public float mortesMinimas;
    public int vidaMax;
    public int ataque = 10;
    public float velocidade = 5;
    public float escala = 1;

    private float contadorTempo = 0;
    private float intervaloZumbi;
    private float contadorTempoTotal = 0;
    private string position;
    private GameObject jogador;


    // Start is called before the first frame update
    void Start()
    {
        jogador = GameObject.FindWithTag("Player");
        intervaloZumbi = intervaloInicialZumbi;
    }

    // Update is called once per frame
    void Update()
    {
        contadorTempo += Time.deltaTime;
        int quantidadeMortes = jogador.GetComponent<ControlaJogador>().killCount;

        if (contadorTempo > intervaloZumbi)
        {
            contadorTempo = 0;

            if (quantidadeMortes >= mortesMinimas)
            {
                var instancia = Instantiate(zumbi, transform.position, transform.rotation).GetComponent<ControlaInimigo>();
                instancia.transform.localScale = instancia.transform.localScale * escala;
                instancia.status.danoAtaque = ataque;
                instancia.status.velocidade = velocidade;
                instancia.status.vidaMax = vidaMax;
                instancia.status.IniciaVida();
            }


        }

        if(quantidadeMortes % 10 == 0) {
            intervaloZumbi = intervaloInicialZumbi * (1 - (quantidadeMortes / 10000));
        }
        

    }
}
