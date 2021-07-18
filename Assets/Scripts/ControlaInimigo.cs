using Assets.Scripts;
using Scripts.Utils;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlaInimigo : MonoBehaviour, IMatavel
{
    public float distanciaMinima = 2.5f;
    public float distanciaMaxima = 30f;
    public Status status;
    public AudioClip somDeMorte;
    public float intervaloPasseio;

    private GameObject jogador;
    private Rigidbody rb;
    private Vector3 direcao;
    private Quaternion rotacao;
    private Animator animator;
    private Vector3 posicaoPasseioAtual;
    private float contadorTempoPasseio;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
        jogador = GameObject.FindWithTag("Player");

        DefineZumbi();
    }

    private void FixedUpdate()
    {
        float distancia = Vector3.Distance(rb.position, jogador.transform.position);
        if (distancia > distanciaMaxima)
        {
            var boss = GameObject.FindWithTag("Boss");
            var distanciaParaOBoss = boss != null ? Vector3.Distance(rb.position, boss.transform.position) : 0;
            if (distanciaParaOBoss > distanciaMinima && distanciaParaOBoss < distanciaMaxima)
            {
                MoveZumbi(boss.transform.position);
            }
            else
            {
                contadorTempoPasseio -= Time.deltaTime;
                if (contadorTempoPasseio <= 0)
                {
                    posicaoPasseioAtual = MovimentoPersonagem.GeraPosicaoDentroRaio(transform.position, status.velocidade);
                    contadorTempoPasseio = intervaloPasseio;
                }
                if (Vector3.Distance(transform.position, posicaoPasseioAtual) > 0.05)
                {
                    MoveZumbi(posicaoPasseioAtual);
                }
            }
        }
        else if (distancia > distanciaMinima)
        {
            MoveZumbi(jogador.transform.position);
            animator.SetBool("atacando", false);
        }
        else
        {
            direcao = jogador.transform.position - rb.position;
            animator.SetBool("atacando", true);
        }
        GetComponent<Animator>().SetFloat("movendo", direcao.magnitude);
        rb.MoveRotation(MovimentoPersonagem.NovaRotacao(direcao));
    }

    void MoveZumbi(Vector3 destino)
    {
        direcao = destino - rb.position;
        rb.MovePosition(MovimentoPersonagem.NovaPosicao(rb.position, direcao, status.velocidade));
    }    

    void AtacaJogador()
    {
        jogador.GetComponent<ControlaJogador>().DanoSofrido(status.danoAtaque);
    }

    void DefineZumbi()
    {
        int roupaZumbi = Random.Range(1, 28);

        transform.GetChild(roupaZumbi).gameObject.SetActive(true);
    }

    public void DanoSofrido(int dano)
    {
        status.DanoSofrido(dano);
        
        if (status.Morto)
        {
            Morrer();
        }
    }

    public void Morrer()
    {
        ControlaAudio.instancia.PlayOneShot(somDeMorte);
        Destroy(gameObject);
    }
}
