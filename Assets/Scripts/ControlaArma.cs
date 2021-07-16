using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlaArma : MonoBehaviour
{
    public GameObject bala;
    public GameObject canoDaArma;
    public AudioClip somDoTiro;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (gameObject.GetComponent<ControlaJogador>().status.vida > 0)
        {
            if (Input.GetButtonDown("Fire1"))
            {
                Instantiate(bala, canoDaArma.transform.position, canoDaArma.transform.rotation);
                ControlaAudio.instancia.PlayOneShot(somDoTiro);
            }
        }
    }
}
