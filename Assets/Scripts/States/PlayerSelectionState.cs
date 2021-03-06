﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSelectionState : PlayerState
{
    private Vector3? _previousPosition;

    public PlayerSelectionState(GameManager gameManager, BuildingManager buildingManager, IResourceManager resourceManager) 
        :base(gameManager, buildingManager, resourceManager)
    {

    }

    public override void EnterState(string model)
    {
        base.EnterState(model);
        if (this._gameManager.uIController.GetStructureInfoVisability())
        {
            StructureBaseSO data = this._buildingManager.GetStructureDataFromPosition(_previousPosition.Value);
            if(data)
            {
                UpdateStructureInfoPanel(data);
            }
            else
            {
                this._gameManager.uIController.HideStructureInfo();
                _previousPosition = null;
            }

        }
    }

    public override void OnInputPointerDown(Vector3 position)
    {
        StructureBaseSO data = _buildingManager.GetStructureDataFromPosition(position);
        if (data)
        {
            UpdateStructureInfoPanel(data);
            _previousPosition = position;
        }
        else
        {
            this._gameManager.uIController.HideStructureInfo();
            data = null;
            _previousPosition = null;
        }

        return;
    }

    private void UpdateStructureInfoPanel(StructureBaseSO structureData)
    {
        Type structureDataType = structureData.GetType();
        if (structureDataType == typeof(SingleFacilitySO))
        {
            this._gameManager.uIController.DisplayFacilityStructureInfo((SingleFacilitySO)structureData);
        }
        else if (structureDataType == typeof(ZoneStructureSO))
        {
            this._gameManager.uIController.DisplayZoneStructureInfo((ZoneStructureSO)structureData);
        }
        else if(structureDataType == typeof(ManufacturerBaseSO))
        {
            this._gameManager.uIController.DisplayManufactureStructureInfo((ManufacturerBaseSO)structureData);
        }
        else
        {
            this._gameManager.uIController.DisplayBasicStructureInfo(structureData);
        }
    }

    public override void OnCancel()
    {
        return;
    }
}
