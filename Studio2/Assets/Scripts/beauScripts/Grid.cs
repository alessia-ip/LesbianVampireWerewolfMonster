﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid<TGridObject>
{
    public event EventHandler<OnGridObjectChangedEventArgs> OnGridObjectChanged;
    public class OnGridObjectChangedEventArgs : EventArgs {
        public int x;
        public int y;
    }
    
    private int width;
    private int height;
    private float cellSize;
    private Vector3 originPosition;
    private TGridObject[,] gridArray;
    
    public Grid(int width, int height, float cellSize, Vector3 originPosition, Func<Grid<TGridObject>, int, int, TGridObject> createGridobject)
    {
        this.width = width;
        this.height = height;
        this.cellSize = cellSize;
        this.originPosition = originPosition;

        gridArray = new TGridObject[width, height];

        for (int x = 0; x < gridArray.GetLength(0); x++)
        {
            for (int y = 0; y < gridArray.GetLength(1); y++)
            {
                gridArray[x, y] = createGridobject(this, x, y);
                //Debug.Log(x + " " + y);
                //Debug.DrawLine(GetWorldPosition(x,y),GetWorldPosition(x, y +1), Color.white, 100f);
                //Debug.DrawLine(GetWorldPosition(x,y),GetWorldPosition(x +1, y), Color.white, 100f);
            }
                //Debug.DrawLine(GetWorldPosition(0, height), GetWorldPosition(width,height), Color.white, 100f);
                //Debug.DrawLine(GetWorldPosition(width,0),GetWorldPosition(width,height), Color.white, 100f);
        }
        //Debug.Log(width + " " + height);
    }

    public int GetWidth()
    {
        return width;
    }

    public int GetHeight()
    {
        return height;
    }

    public float GetCellSize()
    {
        return cellSize;
    }

  public Vector3 GetWorldPosition(int x, int y)
    {
       // return new Vector3((x - y),(x + y)) * cellSize/2 + originPosition;
       return new Vector3(((x - y) * cellSize/2),((x + y) * cellSize/4)) + originPosition;
    }

    public void GetXY(Vector3 worldPosition, out int x, out int y)
    {
        /*
        x = Mathf.CeilToInt(((worldPosition - originPosition).x/(cellSize/2) + (worldPosition - originPosition).y / (cellSize/2))/2);
        y = Mathf.FloorToInt(((worldPosition - originPosition).y / (cellSize/2) - (worldPosition - originPosition).x / (cellSize/2))/2);
    */
        x = Mathf.CeilToInt(((worldPosition - originPosition).x/(cellSize/2) + (worldPosition - originPosition).y / (cellSize/4))/2);
        y = Mathf.FloorToInt(((worldPosition - originPosition).y / (cellSize/4) - (worldPosition - originPosition).x / (cellSize/2))/2);
    }

    public void SetGridObject(int x, int y, TGridObject value)
    {
        if (x >= 0 && y >= 0 && x < width && y < height)
        {
            gridArray[x, y] = value;
            if (OnGridObjectChanged != null) OnGridObjectChanged(this, new OnGridObjectChangedEventArgs { x = x, y = y });
        }
    }

    public void SetGridObject(Vector3 worldPosition, TGridObject value)
    {
        int x, y;
        GetXY(worldPosition, out x,out y);
        SetGridObject(x, y, value);
    }

    public TGridObject GetGridObject(int x, int y)
    {
        if (x >= 0 && y >= 0 && x < width && y < height)
        {
            return gridArray[x, y];
        }
        else
        {
            return default(TGridObject);
        }
    }
    
    public void TriggerGridObjectChanged(int x, int y) {
        if (OnGridObjectChanged != null) OnGridObjectChanged(this, new OnGridObjectChangedEventArgs { x = x, y = y });
    }

    public TGridObject GetGridObject(Vector3 worldPosition)
    {
        int x, y;
        GetXY(worldPosition, out x, out y);
        return GetGridObject(x, y);
    }
}
