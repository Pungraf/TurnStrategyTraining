using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIAssets : MonoBehaviour
{
    public static UIAssets Instance { get; private set; }

    [SerializeField] List<Sprite> spritesList = new List<Sprite>();

    private void Awake()
    {
        if (Instance != null)
        {
            Debug.LogError("There is more then one  UIAssets! " + transform + " - " + Instance);
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    public Sprite GetSprite(string name)
    {
        foreach (Sprite sprite in spritesList)
        {
            if (sprite.name == name)
            {
                return sprite;
            }
        }
        return null;
    }

}
