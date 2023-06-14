using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIFacilityCard : MonoBehaviour
{
    [SerializeField] private GridObjectFacility facility;
    [Header("Setted during playtime")]
    [SerializeField] private Image facilityIcon;
    [SerializeField] private TMP_Text facilityGearcoinPrice;
    [SerializeField] private TMP_Text facilityInformationPrice;
    [SerializeField] private TMP_Text facilityDescription;
    [SerializeField] private string extraDescription;
    [SerializeField] private GameObject redLockedImage;
    [SerializeField] private GameObject researchButton;

    private void Start()
    {
        facilityIcon.sprite = facility.gridObjectIcon;
        facilityGearcoinPrice.text = facility.gridObjectGearcoinPrice.ToString() + " Gearcoins";
        facilityDescription.text = facility.description + extraDescription;
        facilityInformationPrice.text = facility.gridObjectInformationPrice.ToString() + " Information";
    }
    public void Unlock()
    {
        redLockedImage.SetActive(false);
        researchButton.SetActive(false);
        facilityInformationPrice.gameObject.SetActive(false);
        
    }
    public void OnButtonTryResearch()
    {
        if (PlayerSystem.Instance.information < facility.gridObjectInformationPrice)
        { UIUserInterface.Instance.PopResult("Not enough information!", Color.red); return; }
        PlayerSystem.Instance.ResearchFacility(facility.prefabIndex);
        PlayerSystem.Instance.AlterateInformation(-facility.gridObjectGearcoinPrice);
        UIUserInterface.Instance.PopResult("Facility sucessfully researched!", Color.green);
        Unlock();
    }
    public void OnButtonTryCreate()
    {
        if (PlayerSystem.Instance.gearcoins < facility.gridObjectGearcoinPrice)
        { UIUserInterface.Instance.PopResult("Not enough gearcoins!", Color.red); return; }
        PlayerSystem.Instance.AlterateFacilitiesStored(facility.prefabIndex, +1);
        PlayerSystem.Instance.AlterateGearcoins(-facility.gridObjectGearcoinPrice);
        UIUserInterface.Instance.PopResult("Facility sucessfully created!", Color.green);
    }
}
