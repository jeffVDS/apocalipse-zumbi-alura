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
        if (objetoColidido.CompareTag("Inimigo"))
        {
            Destroy(objetoColidido.gameObject);
            jogador.GetComponent<ControlaJogador>().killCount++;
        }

        Destroy(this.gameObject);
    }
}
