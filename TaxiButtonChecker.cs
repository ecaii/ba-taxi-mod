using System;
using System.Linq;
using Entities;
using Extensions;
using Helpers;
using Il2CppInterop.Runtime.InteropTypes.Arrays;
using UI.Smartphone.Apps.Contacts;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace BATaxiMod;

public class TaxiButtonChecker : MonoBehaviour
{
    private Contacts _contacts;
    private GameObject CallTaxiButton;

    private void Start()
    {
        Address taxiAddress = new Address();
        taxiAddress.streetName = StreetName.EleventhStreet;
        taxiAddress.streetNumber = 5;
        BuildingRegistration taxiRegistration = BuildingHelper.GetBuildingRegistration(taxiAddress);
        Contact taxi = Contact.AddContact("NY Taxis", ContactCategoryName.General, "Taxi Service", taxiRegistration);

        GameObject Buttons = GameObject.Find(
            "Canvases/FullMenu/Canvas/AppsContainer/Contacts/Layout 30-70/Right/Conversation/Header/Buttons/LeftSide");

        GameObject callButton = GameObject.Find(
            "Canvases/FullMenu/Canvas/AppsContainer/Contacts/Layout 30-70/Right/Conversation/Header/Buttons/LeftSide/CallButton");


        if (callButton)
        {
            GameObject callClone = GameObject.Instantiate(callButton, Buttons.transform);
            callClone.name = "CallTaxiButton";
            Button callTaxiButton = callClone.GetComponent<Button>();

            if (callTaxiButton)
            {
                callTaxiButton.onClick.AddListener((UnityAction)(() => OnTaxiCalled()));
            }
        }

        GameObject ContactsObject = GameObject.Find("Canvases/FullMenu/Canvas/AppsContainer/Contacts");
        _contacts = ContactsObject.GetComponent<Contacts>();

        CallTaxiButton =
            GameObject.Find(
                "Canvases/FullMenu/Canvas/AppsContainer/Contacts/Layout 30-70/Right/Conversation/Header/Buttons/LeftSide/CallTaxiButton");

        Plugin.Log.LogInfo("TaxiButtonChecker::Start");
    }

    static void OnTaxiCalled()
    {
        GameObject trafficHolder = GameObject.Find("TrafficHolder");
        Il2CppReferenceArray<Transform> cars = trafficHolder.transform.GetChildren();
        Transform taxiClone = cars.ToList().FirstOrDefault(v => v.name.Contains("Taxi(Clone)"));
        GameObject player = GameObject.Find("GameManager/Player");

        taxiClone.transform.position = player.transform.position;
    }

    private void Update()
    {
        try
        {
            if (_contacts && CallTaxiButton)
            {
                if (_contacts._selectedContact.name.Contains("NY Taxis"))
                {
                    CallTaxiButton.active = true;
                }
                else if (CallTaxiButton.active)
                {
                    CallTaxiButton.active = false;
                }
            }
        }
        catch (Exception e)
        {
        }
    }
}