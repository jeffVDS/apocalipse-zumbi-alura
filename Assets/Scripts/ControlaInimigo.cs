using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlaInimigo : MonoBehaviour
{
    public float velocidade = 5;
    public float distanciaMinima = 2.5f;
    public GameObject jogador;

    private Rigidbody rb;
    private Vector3 direcao;
    private Quaternion rotacao;
    private Animator animator;
    

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
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

        rb.MoveRotation(rotacao);

    }

    void AtacaJogador()
    {
        Time.timeScale = 0;
        jogador.GetComponent<ControlaJogador>().textoGameOver.SetActive(true);
        jogador.GetComponent<ControlaJogador>().vivo = false;
    }
}
