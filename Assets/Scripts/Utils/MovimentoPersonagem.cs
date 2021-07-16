using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;


namespace Scripts.Utils
{
    public class MovimentoPersonagem
    {
        public static Vector3 NovaPosicao(Vector3 posicao, Vector3 direcao, float velocidade)
        {
            var movimentoNormalizado = (direcao.normalized) * Time.deltaTime * velocidade;

            return (posicao + movimentoNormalizado);
        }

        public static Quaternion NovaRotacao(Vector3 direcao)
        {
            return Quaternion.LookRotation(direcao.normalized); ;
        }

    }
}
