using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ListManager : MonoBehaviour
{
    [SerializeField]
    private Transform ListHolder = null;

    [SerializeField]
    private GameObject item = null;

    [SerializeField]
    private RectTransform content = null;
    
    //Initial list items amount
    private int itemCount = 250;
    
    private bool listInitialized = false;

    //Y offset to avoid empty space at the bottom of the list
    private int yOffset = 700;

    //List item height
    private int itemHeight = 100;

    private void OnEnable()
    {
        EventManager.onFillList += RefreshList;
    }

    private void OnDisable()
    {
        EventManager.onFillList -= RefreshList;
    }
    private void Start()
    {
        InitializeList();
    }

    private void InitializeList()
    {
        //Set the list size for the max amount of items
        content.sizeDelta = new Vector2(0, itemCount * itemHeight);

        //Create an initial list of item to use them as needed (pseudo object pool)
        for (int i = 0; i < itemCount; i++)
        {
            //Set the spawning position
            float spawnY = 25 + (i * itemHeight);
            Vector3 pos = new Vector3(0.0f, -spawnY, ListHolder.position.z);
            
            //Instatiating the list item
            GameObject ListItemObject = Instantiate(item, pos, ListHolder.rotation);

            //Setting Parent
            ListItemObject.transform.SetParent(ListHolder, false);

            //Disabling the list item for future use
            ListItemObject.SetActive(false);
        }

        /*
        * Notify the list is ready to be used
        * RequestManager.cs uses this event to know we are ok for firing an API request
        */
        EventManager.ListInitialized();
    }

    private void RefreshList(Tournaments tournaments)
    {
        /*If the list was already filled with items 
         * we reset it in case the new amount 
         * received is different than the previous request
         */
        if (listInitialized)
            ResetList();

        FillTournamentList(tournaments);
    }

    private void ResetList()
    {
        for (int i = 0; i < itemCount; i++)
        {
            ListHolder.GetChild(i).gameObject.SetActive(false);
        }
    }

    private void FillTournamentList(Tournaments tournaments)
    {
        Transform listItemObject = null;
        
        content.sizeDelta = new Vector2(0, (tournaments.data.Count * itemHeight) - yOffset);

        for (int i = 0; i < tournaments.data.Count; i++)
        {
            //Get a list item game object
            listItemObject = ListHolder.GetChild(i);

            //Enable the list item
            listItemObject.gameObject.SetActive(true);

            //Getting the list item component
            ListItem listItem = listItemObject.GetComponent<ListItem>();
           
            //Setting the id field
            listItem.SetId(tournaments.data[i].id);

            //Setting the date field
            listItem.SetDate(tournaments.data[i].attributes.createdAt);
        }

        listInitialized = true;
    }
}
