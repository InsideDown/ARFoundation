using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CarouselController : MonoBehaviour
{
    //TODO: This list will come from JSON eventually; just adding to try it out
    public int CarouselItemCount = 0;
    public GameObject CarouselItemHolder;
    public CarouselItem CarouselItemPrefab;
    public CarouselSwipe CarouselSwipe;

    private void Awake()
    {

        if (CarouselItemHolder == null)
            throw new System.Exception("A CarouselItemHolder must be populated in CarouselController");

        if (CarouselItemPrefab == null)
            throw new System.Exception("A CarouselItemPrefab must be populated in CarouselController");

        if (CarouselSwipe == null)
            throw new System.Exception("A CarouselSwipe must be populated in CarouselController");

    }

    private void Start()
    {
        InitCarousel();
        CarouselSwipe.InitCarousel(CarouselItemCount);
    }

    private void InitCarousel()
    {
        UnInit();
        for(int i=0;i< CarouselItemCount;i++)
        {
            CarouselItem curCarouselItem = Instantiate(CarouselItemPrefab, CarouselItemHolder.transform, false) as CarouselItem;
            curCarouselItem.Init(i+1);
        }
        //EventManager.Instance.ARObjectPlaced();
        //Instantiate(ObjectToPlacePrefab, _PlacementPose.position, _PlacementPose.rotation, PlacedObjectHolder.transform);
        //DisablePlacementIndicator();
        //_ARObjectPlaced = true;
    }

    private void UnInit()
    {
        foreach (Transform child in CarouselItemHolder.transform)
        {
            Destroy(child.gameObject);
        }
    }
}
