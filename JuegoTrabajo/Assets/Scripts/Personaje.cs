using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Personaje : MonoBehaviour
{
    [SerializeField] private float velocidad;

    private Rigidbody2D rig;
    private Animator anim;
    private SpriteRenderer spritePersonaje;

    private void Awake()
    {
        rig = GetComponent<Rigidbody2D>();
        anim = GetComponentInChildren<Animator>();
        spritePersonaje = GetComponentInChildren<SpriteRenderer>();
    }

    private void FixedUpdate()
    {
        Movimiento();
    }

    private void Movimiento()
    {

        float hor = Input.GetAxis("Horizontal");
        float ver = Input.GetAxis("Vertical");

        rig.velocity = new Vector2 (hor, ver) * velocidad;
        anim.SetFloat ("Camina", Mathf.Abs(rig.velocity.magnitude));

        if(hor > 0)
        {
            spritePersonaje.flipX = false;
        }
        else if (hor < 0)
        {
            spritePersonaje.flipX=true;
        }
    
    }
}
