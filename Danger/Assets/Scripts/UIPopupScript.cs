﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIPopupScript : MonoBehaviour
{
    public static event System.EventHandler<Territory> TerritoryConfirmed;
    public Territory Territory;
    public Text Status;
    public Text Name;
    public Text Continent;
    public Text Owner;
    public Renderer Renderer;
    public TerritoryDisplayCriteria Criteria;

    public static UIPopupScript Script;

    public bool IsDisplayed { get
        {
            return Renderer.enabled;
        } set
        {
            Renderer.enabled = value;
        }
    }
    public Button ConfirmBtn;

    /// <summary>
    /// Confirm button being clicked raises this event
    /// </summary>
    public void ClickConfirm()
    {
        TerritoryConfirmed?.Invoke(this, Territory);
    }

    public void Display(Territory territory)
    {
        Status.text = "Confirm, or click another territory..";
        Territory = territory;
        Name.text = Territory.Name;
        Continent.text = Territory.Continent.Name;
        Owner.text = Territory.Owner == GameManager.NotOwned ? "N/A" : "Owner: " + Territory.Owner.Name;
    }

    public static bool DisplayIfSatisfy(Territory t)
    {
        if(Script.Criteria.DoesSatisfy(t))
        {
            Script.Display(t);
            return true;
        }
        return false;
    }

    public void WantingTerritory(TerritoryDisplayCriteria criteria)
    {
        Criteria = criteria;
        string criteriaText = "Click on a territory that:";
        if (criteria.MustBeInContinent.HasValue)
        {
            criteriaText += "\r\nIs in " + criteria.MustBeInContinent.Value.Name;
        }
        if (criteria.MustBeOwnedBy.HasValue)
        {
            criteriaText += "\r\nIs owned by " + criteria.MustBeOwnedBy.Value.Name;
        }
        Status.text = criteriaText;
        Name.text = "...";
        Continent.text = "...";
        Owner.text = "...";
    }

    // Start is called before the first frame update
    void Start()
    {
        UIPopupScript.Script = this;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}