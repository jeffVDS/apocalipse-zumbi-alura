using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ControlaJogador : MonoBehaviour
{
    public float velocidade = 10;
    public LayerMask mascaraChao;
    public GameObject textoGameOver;
    public Text textoKillCount;
    public int killCount;
    public int vida = 100;


    private Rigidbody rigidBody;
    private Vector3 direcao;
    private Vector3 movimentoNormalizado;
    private ControlaInterface controlaInterface;

    void Start(){
        rigidBody = GetComponent<Rigidbody>();

        controlaInterface = GameObject.FindWithTag("UI").GetComponent<ControlaInterface>();

        killCount = 0;
        textoKillCount.gameObject.SetActive(true);
        
        Time.timeScale = 1;
    }

    void Update()
    {
        

        if (vida > 0)
        {
            textoKillCount.text = "Matou " + killCount;

            float eixoX = Input.GetAxis("Horizontal");
            float eixoZ = Input.GetAxis("Vertical");

            direcao = new Vector3(eixoX, 0, eixoZ);

            GetComponent<Animator>().SetBool("movendo", (direcao != Vector3.zero));
        }
        else
        {
            if (Input.GetButtonDown("Fire1"))
            {
                SceneManager.LoadScene("Game");
            }
        }

    }

    void FixedUpdate()
    {
        if (vida > 0)
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

    public void DanificaJogador(int dano)
    {
        
        vida -= dano;
        
        controlaInterface.AtualizaSliderVida(vida);

        if (vida <= 0)
        {
            textoGameOver.SetActive(true);
            Time.timeScale = 0;
        }
    }
}
