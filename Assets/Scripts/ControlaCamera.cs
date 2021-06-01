using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlaCamera : MonoBehaviour
{
    public GameObject jogador;

    private Vector3 distancia;

    // Start is called before the first frame update
    void Start()
    {
        distancia = transform.position - jogador.transform.position;
    }

    // Update is called once per frame
    void Update()
    {

        transform.position = jogador.transform.position + distancia;
    }
}
