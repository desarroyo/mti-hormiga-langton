using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;

public class Events : MonoBehaviour, IPointerClickHandler
{

    // Next update in second
    private float nextUpdate = 1f;
    private float velocidad = 1f;

    public bool start = false;
    private Celda celdaActual;
    GameObject objetoActual;
    public DynamicGridGenerator grid;
    private int direccionActual = DOWN;
    private int direccionAnterior = DOWN;

    const int UP = 1;
    const int DOWN = 2;
    const int LEFT = 3;
    const int RIGHT = 4;

    //Detect if a click occurs
    public void OnPointerClick(PointerEventData pointerEventData)
    {

        //Output to console the clicked GameObject's name and the following message. You can replace this with your own actions for when clicking the GameObject.
        if (/*!start && */pointerEventData.pointerEnter != null && pointerEventData.pointerEnter.GetComponent<Celda>() != null)
        {

            inicializaHormiga(true, pointerEventData.pointerEnter);

        }
        
    }

    public void continuar()
    {
        start = true;
    }

        public void randomAnt()
    {

        GameObject gameObject =  grid.celdas[UnityEngine.Random.Range(0, grid.columnSize)+","+ UnityEngine.Random.Range(0, grid.rowSize)];

        inicializaHormiga(true, gameObject);
    }


    private void inicializaHormiga(bool iniciar, GameObject gameObject)
    {
        int ant_direccion = UnityEngine.Random.Range(1, 5);
        Debug.Log(name + " Random!");
        // GameObject gameObject = grid.celdas[UnityEngine.Random.Range(0, grid.columnSize) + "," + UnityEngine.Random.Range(0, grid.rowSize)];

        gameObject.GetComponent<Image>().color = Color.white;
        // pointerEventData.pointerEnter.GetComponent<Celda>().estado = 1;
        gameObject.GetComponent<Celda>().hormiga = true;
        gameObject.GetComponent<Celda>().rotacion = ant_direccion;
        gameObject.GetComponent<Image>().sprite = Resources.Load<Sprite>("ant_" + ant_direccion);
        direccionActual = ant_direccion;
        celdaActual = gameObject.GetComponent<Celda>();
        objetoActual = gameObject;
        start = iniciar;
    }

    public void step(bool continuar)
    {

        if(celdaActual == null)
        {

        }

        start = continuar;
        if (grid.celdas.ContainsKey(celdaActual.x + 1 + "," + celdaActual.y))
        {

            
            //objetoActual.GetComponent<Celda>().estado = 2;
            objetoActual.GetComponent<Celda>().hormiga = false;
            objetoActual.GetComponent<Celda>().rotacion = -1;
            objetoActual.GetComponent<Image>().sprite = Resources.Load<Sprite>("white");
            //objetoActual.GetComponent<Image>().color = Color.black;


            direccionAnterior = direccionActual;
            if (objetoActual.GetComponent<Celda>().estado == 1)
            {
                objetoActual.GetComponent<Celda>().estado = 0;
                objetoActual.GetComponent<Image>().color = Color.white;
                direccionActual = girarDerecha(direccionActual);
            }
            else
            {
                objetoActual.GetComponent<Celda>().estado = 1;
                objetoActual.GetComponent<Image>().color = Color.black;
                direccionActual = girarIzquierda(direccionActual);
            }


            GameObject c = null;

            try { 
            if (direccionActual == LEFT)
            {
                c = grid.celdas[(celdaActual.x - 1) + "," + celdaActual.y];
            }
            else if (direccionActual == RIGHT)
            {
                c = grid.celdas[(celdaActual.x + 1) + "," + celdaActual.y];
            }
            else if (direccionActual == UP)
            {
                c = grid.celdas[celdaActual.x + "," + (celdaActual.y - 1)];
            }
            else if (direccionActual == DOWN)
            {
                c = grid.celdas[celdaActual.x + "," + (celdaActual.y + 1)];
            }
            }catch(Exception ex)
            {
                start = false;
                return;
            }



            c.GetComponent<Image>().sprite = Resources.Load<Sprite>("ant_" + direccionActual);
            //c.GetComponent<Celda>().estado = 1;
            c.GetComponent<Celda>().hormiga = true;
            //c.GetComponent<Celda>().rotacion = direccionActual;

            celdaActual = c.GetComponent<Celda>();
            objetoActual = c;


        }
    }

    void Update()
    {


        // If the next update is reached
        if (start && Time.time >= nextUpdate)
        {
            Debug.Log(Time.time + ">=" + nextUpdate);
            // Change the next update (current second+1)
            nextUpdate = Time.time + 0;
            // Call your fonction
            step(true);
        }
        
    }

    private int girarIzquierda(int direccionActual)
    {
        if (direccionActual == UP)
        {
            return LEFT;
        }
        else if (direccionActual == LEFT)
        {
            return DOWN;
        }
        else if (direccionActual == DOWN)
        {
            return RIGHT;
        }
        else if (direccionActual == RIGHT)
        {
            return UP;
        }

        return direccionActual;
    }

    private int girarDerecha(int direccionActual)
    {
        if (direccionActual == UP)
        {
            return RIGHT;
        }
        else if (direccionActual == LEFT)
        {
            return UP;
        }
        else if (direccionActual == DOWN)
        {
            return LEFT;
        }
        else if (direccionActual == RIGHT)
        {
            return DOWN;
        }

        return direccionActual;
    }
}
