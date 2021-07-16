using Assets.Scripts;
using Scripts.Utils;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ControlaJogador : MonoBehaviour, IMatavel
{
    public LayerMask mascaraChao;
    public GameObject textoGameOver;
    public Text textoKillCount;
    public int killCount;
    public AudioClip somDeDano;
    public Status status;

    private Rigidbody rb;
    private Vector3 direcao;
    private Vector3 movimentoNormalizado;
    private ControlaInterface controlaInterface;
    private Vector3 lastPosition;

    void Start(){
        rb = GetComponent<Rigidbody>();

        controlaInterface = GameObject.FindWithTag("UI").GetComponent<ControlaInterface>();

        killCount = 0;
        textoKillCount.gameObject.SetActive(true);
        
        Time.timeScale = 1;

        status.IniciaVida();
    }

    void Update()
    {
        if (!status.Morto)
        {
            textoKillCount.text = "Matou " + killCount;

            float eixoX = Input.GetAxis("Horizontal");
            float eixoZ = Input.GetAxis("Vertical");

            direcao = new Vector3(eixoX, 0, eixoZ);

            GetComponent<Animator>().SetFloat("movendo", direcao.magnitude);
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
        if (!status.Morto)
        {
            rb.MovePosition(MovimentoPersonagem.NovaPosicao(rb.position, direcao, status.velocidade));

            rb.MoveRotation(PosicaoMira());
        }
    }

    private Quaternion PosicaoMira()
    {
        Ray raio = Camera.main.ScreenPointToRay(Input.mousePosition);

        RaycastHit raioChaoHit;

        if (Physics.Raycast(raio, out raioChaoHit, 100, mascaraChao.value))
        {
            Vector3 posicaoMiraJogador = raioChaoHit.point - transform.position;

            posicaoMiraJogador.y = transform.position.y;

           return MovimentoPersonagem.NovaRotacao(posicaoMiraJogador);
        }

        return rb.rotation;
    }

    public void DanoSofrido(int dano)
    {
        status.DanoSofrido(dano);
        
        controlaInterface.AtualizaSliderVida(status.vida);

        ControlaAudio.instancia.PlayOneShot(somDeDano);

        if (status.Morto)
        {
            Morrer();
        }
    }

    public void Morrer()
    {
        textoGameOver.SetActive(true);
        Time.timeScale = 0;
    }
}
