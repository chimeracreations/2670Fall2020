﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;
using UnityEngine.UI;


public class SpriteAtlasScript : MonoBehaviour
{
    public SpriteAtlas atlas;
    public string spriteName;
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Image>().sprite = atlas.GetSprite(spriteName);
    }

    public void ChangeSprite(string name)
    {
        GetComponent<Image>().sprite = atlas.GetSprite(name);
    }

}
