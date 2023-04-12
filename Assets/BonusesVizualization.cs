using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;
public class BonusesVizualization : MonoBehaviour
{
    [Header("Sprites")]
    [SerializeField] private Sprite destructImage;
    [SerializeField] private Sprite steelShoesImage;
    private Bonuses bonuses;
    private List<CollectibleTypes> toVisualize=new List<CollectibleTypes>();
    List<GameObject> imageList;

    private void Awake()
    {
        bonuses= GameObject.Find("Player").GetComponent<Bonuses>();
        imageList = new List<GameObject>();
        foreach (Transform child in transform)
        {
            if (child.tag == "Bonus" )
            {
                imageList.Add(child.gameObject);
            }
        }
    }

    public void RefreshUIList()
    {
        toVisualize = bonuses.getList();
        Image imageBuffer;
        if(toVisualize.Count == 0 ) { 
            foreach(GameObject image in imageList)
            {
                imageBuffer = image.GetComponent<Image>();
                imageBuffer.enabled = false;
            }
        }
        for(int i=0;i< imageList.Count;i++)
        {
            if(i< toVisualize.Count)
            {
                switch (toVisualize[i])
                {
                    case CollectibleTypes.Destruction:
                        imageBuffer = imageList[i].GetComponent<Image>();
                        imageBuffer.enabled = true;
                        imageBuffer.sprite = destructImage;
                        break;
                    case CollectibleTypes.SteelShoes:
                        imageBuffer = imageList[i].GetComponent<Image>();
                        imageBuffer.enabled = true;
                        imageBuffer.sprite = steelShoesImage;
                        break;
                    case CollectibleTypes.Default:
                        imageBuffer = imageList[i].GetComponent<Image>();
                        imageBuffer.enabled = false;
                        imageBuffer.sprite = null;
                        break;
                }
            }
            else
            {
                imageBuffer = imageList[i].GetComponent<Image>();
                imageBuffer.enabled = false;
                imageBuffer.sprite = null;
            }
          
        }
    }
}
