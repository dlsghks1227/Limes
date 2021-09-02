using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniMapManager
{
    public void SetColor(GameObject parent)
    {
        if (parent == null)
        {
            return;
        }
        SpriteRenderer componant = parent.transform.Find("MiniMap_Icon").GetComponent<SpriteRenderer>();

        if (parent.layer == 10)
        {
            componant.color = Color.red;
        }

    }
}
