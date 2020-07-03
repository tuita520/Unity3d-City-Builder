﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingManager
{
    private GridStructure _grid;
    private IPlacementManager _placementManager;
    private StructureRepository _structureRepository;
    private StructureModificationFactory _structureModificationFactory;
    private StructureModificationHelper _structureModificationHelper;


    public BuildingManager(int cellSize, int width, int length, IPlacementManager placementManager, StructureRepository structureRepository)
    {
        _grid = new GridStructure(cellSize, width, length);
        this._placementManager = placementManager;
        this._structureRepository = structureRepository;
        this._structureModificationFactory = new StructureModificationFactory(structureRepository, _grid, placementManager);

    }

    public void PrepareBuildingManager(Type classType)
    {
        _structureModificationHelper = _structureModificationFactory.GetHelper(classType);
    }

    public void PrepareStructureForModification(Vector3 inputPosition, string structureName, StructureType structureType)
    {
        _structureModificationHelper.PrepareStructureForModification(inputPosition, structureName, structureType);
    }

    public void ConfirmModification()
    {
        _structureModificationHelper.ConfirmModifications();
    }

    public void CancelModification()
    {
        _structureModificationHelper.CancelModifications();
    }

    public void PrepareStructureForRemovalAt(Vector3 inputPosition)
    {
        _structureModificationHelper.PrepareStructureForModification(inputPosition,"", StructureType.None);
    }

    public GameObject CheckForStructureInGrid(Vector3 inputPosition)
    {
        Vector3 gridPosition = _grid.CalculateGridPosition(inputPosition);
        if(_grid.IsCellTaken(gridPosition))
        {
            return _grid.GetStructureFromTheGrid(gridPosition);
        }
        return null;
    }

    public GameObject CheckForStructureInDictionary(Vector3 inputPosition)
    {
        Vector3 gridPosition = _grid.CalculateGridPosition(inputPosition);
        GameObject structureToReturn = null;
        structureToReturn = _structureModificationHelper.AccessStructureInDictionary(gridPosition);
        if(structureToReturn != null)
        {
            return structureToReturn;
        }
        structureToReturn = _structureModificationHelper.AccessStructureInDictionary(gridPosition);
        return structureToReturn;
    }
}
