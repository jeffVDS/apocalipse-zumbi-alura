using Assets.Scripts;
using Scripts.Utils;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlaInimigo : MonoBehaviour, IMatavel
{
    public float distanciaMinima = 2.5f;
    public Status status;
    public AudioClip somDeMorte;

    private GameObject jogador;
    private Rigidbody rb;
    private Vector3 direcao;
    private Quaternion rotacao;
    private Animator animator;
    
    

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
        direcao = jogador.transform.position - transform.position;

        float distancia = Vector3.Distance(rb.position, jogador.transform.position);

        if (distancia > distanciaMinima)
        {
            rb.MovePosition(MovimentoPersonagem.NovaPosicao(rb.position, direcao, status.velocidade));

            animator.SetBool("atacando", false);
        }
        else
        {
            animator.SetBool("atacando", true);
        }

        rb.MoveRotation(MovimentoPersonagem.NovaRotacao(direcao));
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
