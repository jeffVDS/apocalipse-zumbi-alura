using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlaInimigo : MonoBehaviour
{
    public float velocidade = 5;
    public float distanciaMinima = 2.5f;
    public GameObject jogador;

    private Rigidbody rigidBody;
    private Vector3 direcao;
    private Quaternion rotacao;
    

    // Start is called before the first frame update
    void Start()
    {
        rigidBody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        direcao = jogador.transform.position - transform.position;
        rotacao = Quaternion.LookRotation(direcao);

    }

    private void FixedUpdate()
    {
        float distancia = Vector3.Distance(rigidBody.position, jogador.transform.position);

        if (distancia > distanciaMinima)
        {
            rigidBody.MovePosition(rigidBody.position + (direcao.normalized * velocidade * Time.deltaTime));

            rigidBody.MoveRotation(rotacao);
        }

    }
}
