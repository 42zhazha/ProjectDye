using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DyePot : DyeObject
{
    class Cuisine
    {
        public DyeObject dye;
        public float Ripening = 0;
    }
    //是否放入主材料
    public bool hasChief = false;
    public bool isCharred = false;
    public bool isCooking = true;
    float Endure = 0;

    List<Cuisine> cuisines = new List<Cuisine>();

    private void Update()
    {
        if (isCooking && cuisines.Count > 0)
            Cook();
    }

    public bool AddCuisine(DyeObject dye)
    {
        if (isCharred)
            return false;
        if (dye.id == 1 && hasChief)
            return false;
        if (cuisines.Count >= 3)
            return false;
        if (dye.CanChop && dye.ChopFinish == false)
            return false;

        if (dye.id == 1)
            hasChief = true;

        cuisines.Add(new Cuisine() { dye = dye, Ripening = 0 });
        Endure = 0;
        Destroy(dye.gameObject);
        RenderRecipe();

        return true;
    }

    void Cook()
    {

        for (int i = 0; i < cuisines.Count; i++)
        {
            if (cuisines[i].Ripening >= 1)
                continue;
            else
            {
                cuisines[i].Ripening += Time.deltaTime;
                return;
            }
        }
        Endure += Time.deltaTime;
        if (Endure >= 3)
        {
            //焦了
            isCharred = true;
        }
    }

    void RenderRecipe()
    {
        int recipe = 0;
        for (int i = 0; i < cuisines.Count; i++)
        {
            recipe += cuisines[i].dye.id;
        }
        switch (recipe)
        {

        }
    }

}
