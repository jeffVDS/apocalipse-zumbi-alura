using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bala : MonoBehaviour
{
    private Rigidbody rigidbody;

    public float velocidade = 20;

    private void Start()
    {
        this.rigidbody = GetComponent<Rigidbody>();
    }


    // Update is called once per frame
    void FixedUpdate()
    {
        this.rigidbody.MovePosition(this.rigidbody.position + transform.forward * velocidade * Time.deltaTime);
    }
}
