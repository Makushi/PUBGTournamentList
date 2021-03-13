using UnityEngine;
using TMPro;

public class ListItem : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI Id;
    [SerializeField] private TextMeshProUGUI Date;

    //Setter for the tournament id field
    public void SetId(string id)
    {
        Id.text = id;
    }

    //Setter for the tournament date field
    public void SetDate(string date)
    {
        Date.text = date;
    }
}
