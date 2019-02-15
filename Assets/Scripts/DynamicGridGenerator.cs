using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class DynamicGridGenerator : MonoBehaviour
{
    public Dictionary<string, GameObject> celdas;
    public Text numOfRows;
    public Text numOfColumns;
    public RectTransform panelRow;
    public GameObject whiteCell;
    public GameObject blackCell;
    public Sprite spriteAnt;
    public Toggle mostrarCabezal;


    public Transform grid;

    public int rowSize;
    public int columnSize;

    public class celdä
    {
        public GameObject gridCell;
        public int estado;
    }



    void Initialize()
    {
        try
        {
           

            rowSize = int.Parse(numOfRows.text);
            columnSize = int.Parse(numOfColumns.text);
            celdas = new Dictionary<string, GameObject>();
        }catch(System.Exception ex) { }

        if (numOfRows.text == "" || numOfRows.text == null)
        {
            rowSize = 20;
            columnSize = 20;
            celdas = new Dictionary<string, GameObject>();
        }
    }
		
    public void ClearGrid()
    {
        for (int count = 0; count < grid.childCount; count++)
        {
            Destroy(grid.GetChild(count).gameObject);
        }
    }
		
    public void GenerateGrid()
    {
		
        ClearGrid();
				
        Initialize();
		
        GameObject cellInputField = null;
        RectTransform rowParent;
        for (int rowIndex = 0; rowIndex < rowSize; rowIndex++)
        {
            rowParent = (RectTransform)Instantiate(panelRow);
            rowParent.transform.SetParent(grid);
            rowParent.transform.localScale = Vector3.one;
            
            for (int colIndex = 0; colIndex < columnSize; colIndex++)
            {

                cellInputField = (GameObject)Instantiate(whiteCell);
                
                    
                
                
                cellInputField.transform.SetParent(rowParent);
                cellInputField.GetComponent<RectTransform>().localScale = Vector3.one;
                celdas.Add(colIndex+ ","+ rowIndex, cellInputField);
                cellInputField.GetComponent<Celda>().x = colIndex;
                cellInputField.GetComponent<Celda>().y = rowIndex;
                cellInputField.GetComponent<Celda>().estado = 0;
                if (mostrarCabezal.isOn)
                {
                    cellInputField.GetComponent<Image>().sprite = Resources.Load<Sprite>("celda_"+ cellInputField.GetComponent<Celda>().estado);
                }
                else
                {
                    cellInputField.GetComponent<Image>().color = Color.white;
                }
                

            }
        }
        
       
    }
}
