﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime;
using UnityEngine;

public class GridStructure
{
    
    private int _cellSize, _width, _length;
    private Cell[,] _grid;

    public GridStructure(int cellSize, int width, int length)
    {
        this._cellSize = cellSize;
        this._width = width;
        this._length = length;
        _grid = new Cell[_width, _length];
        for(int row = 0; row < _grid.GetLength(0); row++)
        {
            for(int col = 0; col < _grid.GetLength(1); col++)
            {
                _grid[row, col] = new Cell();
            }
        }
    }
    public Vector3 CalculateGridPosition(Vector3 inputPosition)
    {
        int x = Mathf.FloorToInt((float)inputPosition.x / _cellSize);
        int z = Mathf.FloorToInt((float)inputPosition.z / _cellSize);
        return new Vector3(x * _cellSize, 0, z * _cellSize);
    }

    private Vector2Int CalculateGridIndex(Vector3 gridPosition)
    {
        return new Vector2Int((int)(gridPosition.x / _cellSize), (int)(gridPosition.z / _cellSize)); 
    }

    public bool IsCellTaken(Vector3 gridPosition)
    {
        var cellIndex = CalculateGridIndex(gridPosition);
        if(CheckIndexValidity(cellIndex))
            return _grid[cellIndex.y, cellIndex.x].IsTaken; 

        throw new IndexOutOfRangeException ("No index " + cellIndex + " in Grid");
    }

    public void PlaceStructureOnTheGrid(GameObject structure, Vector3 gridPosition, StructureBaseSO structureData)
    {
        var cellIndex = CalculateGridIndex(gridPosition);
        if(CheckIndexValidity(cellIndex))
            _grid[cellIndex.y, cellIndex.x].SetConstruction(structure, structureData);
    }

    public GameObject GetStructureFromTheGrid(Vector3 gridPosition)
    {
        var cellIndex = CalculateGridIndex(gridPosition);
        return _grid[cellIndex.y, cellIndex.x].GetStructure(); 
    }

    public StructureBaseSO GetStructureDataFromTheGrid(Vector3 gridPosition)
    {
        var cellIndex = CalculateGridIndex(gridPosition);
        return _grid[cellIndex.y, cellIndex.x].GetStructureData();
    }

    public void RemoveStrucutreFromTheGrid(Vector3 gridPosition)
    {
        var cellIndex = CalculateGridIndex(gridPosition);
        _grid[cellIndex.y, cellIndex.x].RemoveStructure(); 
    }

    private bool CheckIndexValidity(Vector2Int cellIndex)
    {
          if(cellIndex.x >= 0 && cellIndex.x < _grid.GetLength(1) && cellIndex.y >= 0 && cellIndex.y < _grid.GetLength(0))
            return true;
        return false;
    }

    public Vector3Int? GetPositionOfNeighborIfExists(Vector3 gridPoisition, Direction direction)
    {
        Vector3Int? neighborPoisition = Vector3Int.FloorToInt(gridPoisition);

        switch (direction)
        {
            case Direction.Up:
                neighborPoisition += new Vector3Int(0, 0, _cellSize);
                Debug.Log(neighborPoisition + " this is up");
                break;
            case Direction.Right:
                neighborPoisition += new Vector3Int(_cellSize, 0, 0);
                Debug.Log(neighborPoisition + " this is right");
                break;
            case Direction.Down:
                neighborPoisition += new Vector3Int(0, 0, -_cellSize);
                Debug.Log(neighborPoisition + " this is down");
                break;
            case Direction.Left:
                neighborPoisition += new Vector3Int(-_cellSize, 0, 0);
                Debug.Log(neighborPoisition + " this is left");
                break;
        }

        var index = CalculateGridIndex(neighborPoisition.Value);
        if(CheckIndexValidity(index) == false)
        {
            return null;
        }

        return neighborPoisition;
    }
}

public enum Direction
{
    Up = 1,
    Right = 2,
    Down = 4,
    Left = 8
}

