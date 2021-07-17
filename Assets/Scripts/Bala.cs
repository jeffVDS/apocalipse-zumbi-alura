using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bala : MonoBehaviour
{
    private Rigidbody rb;
    private GameObject jogador;
    public float velocidade = 20;
    

    private void Start()
    {
        this.rb = GetComponent<Rigidbody>();
        jogador = GameObject.FindWithTag("Player");
    }


    // Update is called once per frame
    void FixedUpdate()
    {
        this.rb.MovePosition(this.rb.position + transform.forward * velocidade * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider objetoColidido)
    {
        if (objetoColidido.CompareTag("Inimigo") || objetoColidido.CompareTag("Boss"))
        {
            var controleJogador = jogador.GetComponent<ControlaJogador>();

            controleJogador.killCount++;

            objetoColidido.gameObject.GetComponent<ControlaInimigo>().DanoSofrido(controleJogador.status.danoAtaque);
        }

        Destroy(this.gameObject);

    }
}
