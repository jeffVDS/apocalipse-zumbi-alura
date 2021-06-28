using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlaJogador : MonoBehaviour
{
    public float velocidade = 10;
    public LayerMask mascaraChao;


    private Rigidbody rigidBody;
    private Vector3 direcao;
    private Vector3 movimentoNormalizado;

    
    // Start is called before the first frame update
    void Start(){
        rigidBody = GetComponent<Rigidbody>();
    }
    void Update()
    {
        float eixoX = Input.GetAxis("Horizontal");
        float eixoZ = Input.GetAxis("Vertical");

        direcao = new Vector3(eixoX, 0, eixoZ);
        
        GetComponent<Animator>().SetBool("movendo", (direcao != Vector3.zero));

    }

    void FixedUpdate()
    {
        movimentoNormalizado = (direcao) * Time.deltaTime * velocidade;

        var novaPosicao = rigidBody.position + movimentoNormalizado;

        rigidBody.MovePosition(novaPosicao);

        Ray raio = Camera.main.ScreenPointToRay(Input.mousePosition);

        RaycastHit raioChaoHit;

        if (Physics.Raycast(raio, out raioChaoHit, 100, mascaraChao.value))
        {
            Vector3 posicaoMiraJogador = raioChaoHit.point - transform.position;

            posicaoMiraJogador.y = transform.position.y;

            Quaternion novaRotacao = Quaternion.LookRotation(posicaoMiraJogador);

            this.rigidBody.MoveRotation(novaRotacao);
        }    
    }
}
