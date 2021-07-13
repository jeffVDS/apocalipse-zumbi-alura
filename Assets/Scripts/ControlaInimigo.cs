using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlaInimigo : MonoBehaviour
{
    public float velocidade = 5;
    public float distanciaMinima = 2.5f;
    public int ataque = 10;
    
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

        int roupaZumbi = Random.Range(1,28);

        this.transform.GetChild(roupaZumbi).gameObject.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        direcao = jogador.transform.position - transform.position;
        rotacao = Quaternion.LookRotation(direcao);

    }

    private void FixedUpdate()
    {
        float distancia = Vector3.Distance(rb.position, jogador.transform.position);

        if (distancia > distanciaMinima)
        {
            rb.MovePosition(rb.position + (direcao.normalized * velocidade * Time.deltaTime));

            animator.SetBool("Atacando", false);
        }
        else
        {
            animator.SetBool("Atacando", true);
        }

        rb.MoveRotation(rotacao.normalized);

    }

    void AtacaJogador()
    {
        jogador.GetComponent<ControlaJogador>().DanificaJogador(ataque);
    }
}
