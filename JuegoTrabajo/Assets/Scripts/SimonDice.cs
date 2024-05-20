using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SimonDice : MonoBehaviour
{
    public Image ColorArriba;
    public GameObject ColorGris;
    public List<Color> Colores; // Lista de 4 colores

    public int Nivel;
    public int ColoresPorMostrar;
    public int ColoresPorTocar;
    public Text TextoArriba;
    public List<int> Secuencia;

    public Text LevelText;
    public GameObject GameOver;

    // Start is called before the first frame update
    void Start(){
        Colores = new List<Color> {
        new Color(0.0f, 0.267f, 1.0f, 1.0f),
        new Color(1.0f, 0.0f, 0.043f, 1.0f),
        new Color(0.0f, 1.0f, 0.176f, 1.0f),
        new Color(1.0f, 0.596f, 0.02f, 1.0f)
    };
        Secuencia = new List<int>();
        StartCoroutine(Comenzar());
    }

    public void Generator(){
        Nivel++;
        LevelText.text = "Nivel: " + Nivel;
        Secuencia.Add(Random.Range(0,4));
        MostrarColores();
    }

    public void MostrarColores(){
        if(Secuencia.Count <= ColoresPorMostrar)
        {
        ColorArriba.color = Color.white;
        ColoresPorMostrar = 0;
        ColoresPorTocar = Secuencia.Count;
        TextoArriba.text = ColoresPorTocar.ToString();
        ColorGris.SetActive(false);
        }
        else
        {
        ColorArriba.color = Colores[Secuencia[ColoresPorMostrar]];
        StartCoroutine(ProximoColor());
        }
    }

    public void ComprobarColor(int ID){
        if(ID == Secuencia[ColoresPorMostrar])
        {
            ColoresPorMostrar++;
            ColoresPorTocar--;
            TextoArriba.text = ColoresPorTocar.ToString();
            if (ColoresPorMostrar == Secuencia.Count)
            {
                ColorGris.SetActive(true);
                TextoArriba.text = "";
                ColoresPorTocar = 0;
                ColoresPorMostrar = 0;
                StartCoroutine(Comenzar());
            }
        }
        else
        {
            GameOver.SetActive(true);
            ColorGris.SetActive(true);
            TextoArriba.text = "";
            ColoresPorTocar = 0;
            ColoresPorMostrar = 0;
        }
    }

    IEnumerator Comenzar(){
        yield return new WaitForSeconds(0.5f);
        Generator();
    }

    IEnumerator ProximoColor(){
        yield return new WaitForSeconds(0.3f);
        ColorArriba.color = Color.white;
        yield return new WaitForSeconds(0.7f);
        ColoresPorMostrar++;
        MostrarColores();
    }

}