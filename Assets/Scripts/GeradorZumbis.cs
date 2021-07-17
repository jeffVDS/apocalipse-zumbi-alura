using Scripts.Utils;
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
    public float distanciaMinima = 2.5f;
    public float distanciaMaxima = 15;
    public LayerMask layerMaskZumbi;

    private float contadorTempo = 0;
    private float intervaloZumbi;
    private float contadorTempoTotal = 0;
    private string position;
    private GameObject jogador;
    private float raioGeracao = 3;
    private float distanciaMinimaJogador = 20;


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

            if (quantidadeMortes >= mortesMinimas && Vector3.Distance(transform.position, jogador.transform.position) > distanciaMinimaJogador)
            {
                StartCoroutine(GerarNovoZumbi());
            }
        }

        if (quantidadeMortes % 10 == 0)
        {
            intervaloZumbi = intervaloInicialZumbi * (1 - (quantidadeMortes / 10000));
        }

    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, raioGeracao);
    }

    IEnumerator GerarNovoZumbi()
    {
        Collider[] colisores;
        Vector3 posicaoGeracao;
        do
        {
            posicaoGeracao = MovimentoPersonagem.GeraPosicaoDentroRaio(transform.position, raioGeracao);
            colisores = Physics.OverlapSphere(posicaoGeracao, 1, layerMaskZumbi);
            yield return null;
        } while (colisores.Length > 0);

        var instancia = Instantiate(zumbi, posicaoGeracao,
            transform.rotation).GetComponent<ControlaInimigo>();
        instancia.transform.localScale = instancia.transform.localScale * escala;
        instancia.status.danoAtaque = ataque;
        instancia.status.velocidade = velocidade;
        instancia.status.vidaMax = vidaMax;
        instancia.status.IniciaVida();
        instancia.distanciaMaxima = distanciaMaxima;
        instancia.distanciaMinima = distanciaMinima;

    }
}
