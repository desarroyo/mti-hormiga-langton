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


    public Transform grid;
		
    private int rowSize;
    private int columnSize;

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
            rowSize = 40;
            columnSize = 40;
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
                //cellInputField.GetComponent<Image>().color = Color.cyan;
                    
                
                
                cellInputField.transform.SetParent(rowParent);
                cellInputField.GetComponent<RectTransform>().localScale = Vector3.one;
                celdas.Add(rowIndex + ","+colIndex, cellInputField);
                cellInputField.GetComponent<Celda>().x = rowIndex;
                cellInputField.GetComponent<Celda>().y = colIndex;

            }
        }
        
       
    }
}
