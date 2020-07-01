using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;

public class CarouselSwipe : MonoBehaviour, IDragHandler, IEndDragHandler, IBeginDragHandler, IInitializePotentialDragHandler
{
    public float SwipeThreshold = 0.2f; //percentage of the screen do we need to swipe before we trigger a swipe

    private Vector3 _PanelLocation; //hold our current panel position
    private bool _RouteToParent = false; //whether or not we should route our calls up the tree
    private int _CurItemInt = 0;
    private int _TotalItemInt = 0;

    private void Start()
    {
        _PanelLocation = this.gameObject.transform.localPosition;
    }

    /// <summary>
    /// Do action for all parents
    /// </summary>
    private void DoForParents<T>(Action<T> action) where T : IEventSystemHandler
    {
        Transform parent = transform.parent;
        while (parent != null)
        {
            foreach (var component in parent.GetComponents<Component>())
            {
                if (component is T)
                    action((T)(IEventSystemHandler)component);
            }
            parent = parent.parent;
        }
    }

    public void InitCarousel(int totalItems)
    {
        _TotalItemInt = totalItems;
    }

    /// <summary>
    /// Always route initialize potential drag event to parents
    /// </summary>
    public void OnInitializePotentialDrag(PointerEventData eventData)
    {
        DoForParents<IInitializePotentialDragHandler>((parent) => { parent.OnInitializePotentialDrag(eventData); });
    }


    public void OnBeginDrag(PointerEventData eventData)
    {
        //check to see if we're trying to drag vertically
        if(Mathf.Abs(eventData.delta.x) < Mathf.Abs(eventData.delta.y))
        {
            _RouteToParent = true;
        }

        if(_RouteToParent)
            DoForParents<IBeginDragHandler>((parent) => { parent.OnBeginDrag(eventData); });
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (_RouteToParent)
        {
            DoForParents<IDragHandler>((parent) => { parent.OnDrag(eventData); });
        }
        else
        {
            float difference = eventData.pressPosition.x - eventData.position.x;
            this.gameObject.transform.localPosition = _PanelLocation - new Vector3(difference, 0, 0);
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (_RouteToParent)
        {
            DoForParents<IEndDragHandler>((parent) => { parent.OnEndDrag(eventData); });
        }
        else
        {
            float percentage = (eventData.pressPosition.x - eventData.position.x) / Screen.width;
            if (Mathf.Abs(percentage) >= SwipeThreshold)
            {
                Vector3 newLocation = _PanelLocation;
                bool allowSwipe = false;
                if (percentage > 0)
                {
                    if(_CurItemInt < _TotalItemInt - 1)
                    {
                        _CurItemInt++;
                        allowSwipe = true;
                        newLocation += new Vector3(-Screen.width, 0, 0);
                    }
                }
                else if (percentage < 0)
                {
                    if(_CurItemInt > 0)
                    {
                        _CurItemInt--;
                        allowSwipe = true;
                        newLocation += new Vector3(Screen.width, 0, 0);
                    }
                }
                //if we can swipe we need to set a new location
                if (allowSwipe)
                    _PanelLocation = newLocation;
                
            }
            AnimToPos(_PanelLocation);
        }
        _RouteToParent = false;
    }

    private void AnimToPos(Vector3 newLocalPos)
    {
        this.gameObject.transform.DOLocalMove(newLocalPos, 0.2f).SetEase(Ease.OutQuad);
    }
}
