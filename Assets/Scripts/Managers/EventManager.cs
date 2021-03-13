using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager : MonoBehaviour
{
    public delegate void OnRequestCompleted();
    public static event OnRequestCompleted onRequestCompleted;

    public delegate void OnRequestSent();
    public static event OnRequestSent onRequestSent;

    public delegate void OnListInitialized();
    public static event OnListInitialized onListInitialized;

    public delegate void OnFillList(Tournaments tournaments);
    public static event OnFillList onFillList;

    public static void FillList(Tournaments tournaments)
    {
        if (onFillList != null)
        {
            onFillList(tournaments);
        }
    }

    public static void ListInitialized()
    {
        if (onListInitialized != null)
        {
            onListInitialized();
        }
    }
    public static void RequestSent()
    {
        if (onRequestSent != null)
        {
            onRequestSent();
        }
    }

    public static void RequestCompleted()
    {
        if (onRequestCompleted != null)
        {
            onRequestCompleted();
        }
    }
}
