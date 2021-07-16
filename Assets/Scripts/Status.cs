using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Status
{
    public int vidaMax;
    [HideInInspector]
    public int vida;
    
    public float velocidade;
    public int danoAtaque;

    public bool Morto
    {
        get
        {
            return (vida <= 0);
        }
    }

    public void IniciaVida()
    {
        vida = vidaMax;
    }

    public void DanoSofrido(int dano)
    {
        vida -= dano;
    }
}
