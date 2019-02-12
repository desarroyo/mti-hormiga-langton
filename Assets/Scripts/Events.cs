using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Events : MonoBehaviour, IPointerClickHandler
{

    private float time = 0.0f;
    public float interpolationPeriod = 0.9f;
    public bool start = false;
    private Celda celdaActual;
    public DynamicGridGenerator grid;

    const int UP = 1;
    const int DOWN = 2;
    const int LEFT = 3;
    const int RIGHT = 4;

    //Detect if a click occurs
    public void OnPointerClick(PointerEventData pointerEventData)
    {
        int ant_position = Random.Range(1, 5);

        //Output to console the clicked GameObject's name and the following message. You can replace this with your own actions for when clicking the GameObject.
        if(/*!start && */pointerEventData.pointerEnter != null && pointerEventData.pointerEnter.GetComponent<Celda>() != null)
        {
            Debug.Log(name + " Game Object Clicked!");
            pointerEventData.pointerEnter.GetComponent<Image>().color = Color.white;
            pointerEventData.pointerEnter.GetComponent<Celda>().estado = 1;
            pointerEventData.pointerEnter.GetComponent<Celda>().hormiga = true;
            pointerEventData.pointerEnter.GetComponent<Celda>().rotacion = ant_position;
            pointerEventData.pointerEnter.GetComponent<Image>().sprite = Resources.Load<Sprite>("ant_" + ant_position);

            celdaActual = pointerEventData.pointerEnter.GetComponent<Celda>();

            start = true;
        }
        
    }
    

    void Update()
    {
        time += Time.deltaTime;

        if (start && time >= interpolationPeriod)
        {
            time = 0.0f;
            Debug.Log(time + " Time!");

            if (grid.celdas.ContainsKey(celdaActual.x+1+","+celdaActual.y))
            {
                GameObject c = grid.celdas[celdaActual.x + 1 + "," + celdaActual.y];

                c.GetComponent<Image>().sprite = Resources.Load<Sprite>("ant_" + 2);
                celdaActual = c.GetComponent<Celda>();
            }



        }
    }


    }
