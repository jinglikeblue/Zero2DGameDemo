using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using DG.Tweening;

public class FocusUI : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, ISelectHandler, IDeselectHandler
{

	
	void Start () {
		
	}

    private void OnEnable()
    {
        //var et = GetComponent<EventTrigger>();
        //AddEvent(et, EventTriggerType.Select, OnFocusIn);
        //AddEvent(et, EventTriggerType.PointerEnter, OnFocusIn);
        //AddEvent(et, EventTriggerType.Deselect, OnFocusOut);
        //AddEvent(et, EventTriggerType.PointerExit, OnFocusOut);

    }

    private void OnDisable()
    {
        //var et = GetComponent<EventTrigger>();
        //et.triggers.Clear();
    }

    //void AddEvent(EventTrigger et, EventTriggerType id, UnityAction<BaseEventData> callback)
    //{
    //    //EventTrigger.Entry e = new EventTrigger.Entry();
    //    //e.eventID = id;
    //    //e.callback.AddListener(callback);
    //    //et.triggers.Add(e);
    //}


    void Update () {
		
	}

    void OnFocusIn()
    {
        transform.DOScale(Vector3.one * 1.2f, 0.2f);
        //Debug.LogFormat("OnFocusIn");
    }

    void OnFocusOut()
    {
        transform.DOScale(Vector3.one * 1f, 0.2f);
        //Debug.LogFormat("OnFocusOut");
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        OnFocusOut();
    }

    public void OnDeselect(BaseEventData eventData)
    {
        OnFocusOut();
    }

    public void OnSelect(BaseEventData eventData)
    {
        OnFocusIn(); 
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        OnFocusIn();
    }
}
