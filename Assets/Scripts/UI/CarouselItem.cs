using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CarouselItem : MonoBehaviour
{
    public TextMeshProUGUI ItemIntTxt;
    [HideInInspector]
    public int ItemInt;

    private void Awake()
    {
        if (ItemIntTxt == null)
            throw new System.Exception("An ItemIntTxt must be defined in CarouselItem");
    }

    public void Init(int itemInt)
    {
        ItemIntTxt.text = itemInt.ToString();
    }
}
