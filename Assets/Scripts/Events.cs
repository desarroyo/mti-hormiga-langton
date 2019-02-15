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
    public Toggle mostrarCabezal;
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
        if (start)
        {
            while (step(false))
            {

            }
        }
        else
        {
            start = true;
        }
        
    }

        public void randomAnt()
    {

        GameObject gameObject = grid.celdas[UnityEngine.Random.Range((grid.columnSize / 2) - (grid.columnSize / 9), (grid.columnSize / 2)) + "," + UnityEngine.Random.Range((grid.rowSize / 2) - (grid.rowSize / 9), (grid.rowSize / 2))];

        inicializaHormiga(true, gameObject);
    }


    private void inicializaHormiga(bool iniciar, GameObject gameObject)
    {
        int ant_direccion = UnityEngine.Random.Range(1, 5);
        Debug.Log(name + " Random!");
        // GameObject gameObject = grid.celdas[UnityEngine.Random.Range(0, grid.columnSize) + "," + UnityEngine.Random.Range(0, grid.rowSize)];

        
        // pointerEventData.pointerEnter.GetComponent<Celda>().estado = 1;
        gameObject.GetComponent<Celda>().hormiga = true;
        gameObject.GetComponent<Celda>().rotacion = ant_direccion;

        gameObject.GetComponent<Image>().color = Color.white;
        if (mostrarCabezal.isOn)
        {
            gameObject.GetComponent<Image>().sprite = Resources.Load<Sprite>("cabezal_" + gameObject.GetComponent<Celda>().estado + ant_direccion);
        }
        else
        {
            
            gameObject.GetComponent<Image>().sprite = Resources.Load<Sprite>("ant_" + gameObject.GetComponent<Celda>().estado  + ant_direccion);
        }
        
        direccionActual = ant_direccion;
        celdaActual = gameObject.GetComponent<Celda>();
        objetoActual = gameObject;
        start = iniciar;
    }

    public void stepButton(bool continuar)
    {
        step(continuar);
    }

    public bool step(bool continuar)
    {

        if(celdaActual == null)
        {
            GameObject gameObject = grid.celdas[UnityEngine.Random.Range((grid.columnSize / 2)-(grid.columnSize / 9), (grid.columnSize/2) ) + "," + UnityEngine.Random.Range((grid.rowSize / 2)-(grid.rowSize / 9), (grid.rowSize/2))];
            inicializaHormiga(false, gameObject);
            return true;
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
                if (mostrarCabezal.isOn)
                {
                    objetoActual.GetComponent<Image>().sprite = Resources.Load<Sprite>("celda_" + objetoActual.GetComponent<Celda>().estado);
                }
                else
                {
                    objetoActual.GetComponent<Image>().color = Color.white;
                }
                
                direccionActual = girarDerecha(direccionActual);
            }
            else
            {
                objetoActual.GetComponent<Celda>().estado = 1;
                if (mostrarCabezal.isOn)
                {
                    objetoActual.GetComponent<Image>().sprite = Resources.Load<Sprite>("celda_" + objetoActual.GetComponent<Celda>().estado);
                }
                else
                {
                    objetoActual.GetComponent<Image>().color = Color.black;
                }
                
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
                objetoActual.GetComponent<Image>().color = Color.white;
                if (mostrarCabezal.isOn)
                {
                    objetoActual.GetComponent<Image>().sprite = Resources.Load<Sprite>("cabezal_" + objetoActual.GetComponent<Celda>().estado+ direccionActual);
                }
                else
                {
                    objetoActual.GetComponent<Image>().sprite = Resources.Load<Sprite>("ant_" + objetoActual.GetComponent<Celda>().estado + direccionActual);
                }
                
                return start;
            }


            c.GetComponent<Image>().color = Color.white;

            if (mostrarCabezal.isOn)
            {
                c.GetComponent<Image>().sprite = Resources.Load<Sprite>("cabezal_" + c.GetComponent<Celda>().estado + direccionActual);
            }
            else
            {
                c.GetComponent<Image>().sprite = Resources.Load<Sprite>("ant_" + c.GetComponent<Celda>().estado + direccionActual);
            }
            
            //c.GetComponent<Celda>().estado = 1;
            c.GetComponent<Celda>().hormiga = true;
            //c.GetComponent<Celda>().rotacion = direccionActual;

            celdaActual = c.GetComponent<Celda>();
            objetoActual = c;


        }
        else
        {
            return false;
        }
        return true;
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
