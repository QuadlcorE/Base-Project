using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardSelector : MonoBehaviour
{
    private List<GameObject> _selectedCards = new List<GameObject>();
    private List<GameObject> _availableCards = new List<GameObject>();

    private int maxCards = 3;

    private void OnEnable()
    {
        foreach (Transform child in transform)
        {
            if (child.gameObject.active == true) _selectedCards.Add(child.gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SelectItem(GameObject item)
    {
        if (_selectedCards.Count < 4) _selectedCards.Add(item);
    }

    public void DeselectItem(GameObject item)
    {
        if (_selectedCards.Count > 0) _selectedCards.Remove(item);
    }

    
}
