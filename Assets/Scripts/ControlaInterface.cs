using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ControlaInterface : MonoBehaviour
{
    private Slider sliderVida;
    private GameObject score;
    private Text txtScore;
    private GameObject painelGameOver;
    private ControlaJogador jogador;
    private float duracaoPartida;

    // Start is called before the first frame update
    void Start()
    {
        duracaoPartida = 0;

        sliderVida = (Slider)transform.Find("SliderVida").gameObject.GetComponent<Slider>();
        sliderVida.value = sliderVida.maxValue;

        score = transform.Find("Score").gameObject;
        score.SetActive(true);
        txtScore = score.transform.Find("TxtScore").gameObject.GetComponent<Text>();


        painelGameOver = this.transform.Find("PainelGameOver").gameObject;
        painelGameOver.gameObject.SetActive(false);

        jogador = GameObject.FindWithTag("Player").GetComponent<ControlaJogador>();
        Time.timeScale = 1;
    }

    // Update is called once per frame
    void Update()
    {
        duracaoPartida += Time.deltaTime;

        var time = TimeSpan.FromSeconds(duracaoPartida);
        var minutes = Convert.ToInt32(Math.Truncate(time.TotalMinutes)).ToString();
        var seconds = time.Seconds;
        txtScore.text = $"Tempo\t\t{minutes}m{seconds}s\t\t\tMatou\t\t" + jogador.killCount.ToString("D7");
    }

    public void AtualizaSliderVida(int vida)
    {
        sliderVida.value = vida;
    }

    public void NovaPartida()
    {
        SceneManager.LoadScene("Game");
    }

    public void EncerraPartida()
    {
        score.SetActive(false);

        var txtDuracaoPartida = painelGameOver.transform.Find("TextosGameOver").Find("TxtDuracaoPartida").gameObject.GetComponent<Text>();
        var time = TimeSpan.FromSeconds(duracaoPartida);
        var duracaoMinutes = Convert.ToInt32(Math.Truncate(time.TotalMinutes)).ToString();
        var duracaoSeconds = time.Seconds;
        txtDuracaoPartida.text = $"Sobreviveu {duracaoMinutes}m{duracaoSeconds}s e Matou " + jogador.killCount.ToString("D7");

        if (duracaoPartida > PlayerPrefs.GetFloat("melhorDuracao"))
            PlayerPrefs.SetFloat("melhorDuracao", duracaoPartida);

        

        var melhorTime = TimeSpan.FromSeconds(PlayerPrefs.GetFloat("melhorDuracao"));
        var melhorMinutes = Convert.ToInt32(Math.Truncate(melhorTime.TotalMinutes)).ToString();
        var melhorSeconds = melhorTime.Seconds;
        if (jogador.killCount > PlayerPrefs.GetInt("melhorContagemMortes"))
            PlayerPrefs.SetInt("melhorContagemMortes", jogador.killCount);

        var txtMelhorScore = painelGameOver.transform.Find("TextosGameOver").Find("TxtMelhorScore").gameObject.GetComponent<Text>();
        txtMelhorScore.text = $"Melhor: Tempo:{melhorMinutes}m{melhorSeconds}s - Matou: " + PlayerPrefs.GetInt("melhorContagemMortes").ToString("D7");

        painelGameOver.gameObject.SetActive(true);
        Time.timeScale = 0;
    }
}
