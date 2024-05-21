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
    private bool objetivoDetectado;

    public GameObject PanelGameOver;

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
        float distancia = Vector3.Distance(personaje.position, this.transform.position);
        if (distancia < 8)
        {
        agente.SetDestination(personaje.position);
        rotarCerdo();
        }
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

    //Si toca un objeto 2d con el tag "Player" se activa el panel de Game Over y desactiva el jugador
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            PanelGameOver.SetActive(true);
            personaje.gameObject.SetActive(false);
        }
    }

}
