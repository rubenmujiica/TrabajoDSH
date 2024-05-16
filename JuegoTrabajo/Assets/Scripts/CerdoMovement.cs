using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CerdoMovement : MonoBehaviour
{
    public Transform personaje;
    private NavMeshAgent agente;

    private SpriteRenderer sprite;
    // private Transform objetivo;

    private void Awake()
    {
        agente = GetComponent<NavMeshAgent>();
        sprite = GetComponent<SpriteRenderer>();
    }


    private void Start()
    {
        agente.updateRotation = false;
        agente.updateUpAxis = false;
    }


    private void Update()
    {
        agente.SetDestination(personaje.position);
        rotarCerdo();
    }


    void rotarCerdo()
    {
        if (this.transform.position.x > personaje.position.x)
        {
            sprite.flipX = true;
        }
        else
        {
            sprite.flipX = false;
        }
    }
}
