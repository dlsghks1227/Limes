using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.InputSystem;

public class BarricadeGenerator : MonoBehaviour
{
    [SerializeField]
    private GameObject barricade = null;

    [SerializeField]
    private Grid grid = null;

    public void Generate(int density, Vector2[] position)
    {
        int type = 2;

        switch (type)
        {
            case (1):
                Utype(density, position);
                break;
            case (2):
                Default(density, position);
                break;
        }
    }

    private void Utype(int density, Vector2[] position)
    {
        Debug.Log("U");

        int lineCount = 0;
        int densCount = 0;
        bool isBari = true;

        float Ystandard = position[0].y;

        for (int i = 0; i < position.Length; i++)
        {
            if (isBari)
            {
                if (position[i].y <= Ystandard)
                {
                    BuildingEntity entity = Instantiate(barricade).GetComponent<BuildingEntity>();
                    entity.gameObject.layer = LayerMask.NameToLayer("Enemy");
                    grid.Locate(entity, position[i], EBuilding.BARRICADE, 0, false);
                }
            }
            else
            {
                grid.SetBuildingTilesByPosition(new Vector2(position[i].x, position[i].y), EBuilding.NONE);
            }

            lineCount = i + 1;
            if (lineCount >= position.Length)
            {
                lineCount = 0;
            }

            if (position[i].x != position[lineCount].x)
            {
                densCount++;

                if (densCount < density + 1)
                {
                    isBari = false;
                }
                else if (densCount >= density + 1)
                {
                    isBari = true;
                    densCount = 0;
                }
            }
            else if (position[i].y > Ystandard)
            {
                grid.SetBuildingTilesByPosition(new Vector2(position[i].x, position[i].y), EBuilding.NONE);
            }
        }
    }

    private void Default(int density, Vector2[] position)
    {
        int lineCount = 0;
        int densCount = 0;
        bool isBari = true;

        for (int i = 0; i < position.Length; i++)
        {
            if (isBari)
            {
                BuildingEntity entity = Instantiate(barricade).GetComponent<BuildingEntity>();
                entity.gameObject.layer = LayerMask.NameToLayer("Enemy");
                grid.Locate(entity, position[i], EBuilding.BARRICADE, 0, false);
            }
            else
            {
                grid.SetBuildingTilesByPosition(new Vector2(position[i].x, position[i].y), EBuilding.NONE);
            }

            lineCount = i + 1;
            if(lineCount >= position.Length)
            {
                lineCount = 0;
            }

            if (position[i].x != position[lineCount].x)
            {
                densCount++;

                if (densCount < density + 1)
                {
                    isBari = false;
                }
                else if (densCount >= density + 1) 
                {
                    isBari = true;
                    densCount = 0;
                }
            }
        }
    }

    public void SetBarricadeVec(int territory, Vector2 center, ETerritory type)
    {
        //int size = 1;
        //for (int i = 1; i <= territory; i++)
        //{
        //    size += i * 4;
        //}

        int count = 0;
        int ter = (territory + 1);

        int size = 4 * ter;

        Vector2[] barricadeVec = new Vector2[size];
        Vector2 pos = new Vector2(0, 0);

        for (float j = center.y + ter; j >= center.y - ter; j--)
        {
            for (float k = center.x - ter; k <= center.x + ter; k++)
            {
                float distance = Math.Abs(center.x - k) + Math.Abs(center.y - j);

                if (distance == ter)
                {
                    pos = new Vector2(k, j);
                    if (grid.GetBuildingTileState(grid.GetWorldToGrid(pos).x, grid.GetWorldToGrid(pos).y) == EBuilding.NONE)
                    {
                        barricadeVec[count] = pos;
                        grid.SetBuildingTilesByPosition(pos, EBuilding.BARRICADE);
                    }
                    count++;
                }

            }
        }
    }

}