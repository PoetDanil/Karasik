using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Mime;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using YG;

[RequireComponent(typeof(Button))]
public class CellButton : MonoBehaviour{
    [SerializeField] private float price = 1.0f;
    [SerializeField] private TypeOfProduct type;
    [SerializeField] private ShopManager shopManager;
    
    private TMP_Text priceText;

    bool isOn = false;

    void Start() {
        priceText = GetComponentInChildren<TMP_Text>();
        
        if (!Check()) {
            priceText.text = "Купить";
            priceText.color=Color.white;
        }
        else 
            DisableButton();
    }
    public void Click() {
        if (Check()) {
            if (ShopManager.TypeOfFishes == type) {
                ShopManager.TypeOfFishes = TypeOfProduct.standart;
                ShopManager.SwitchFish?.Invoke();
                DisableButton();
                return;
            }
            ShopManager.TypeOfFishes = type;
            ShopManager.SwitchFish?.Invoke();
            SwitchButton();
            return;
        }

        if (PlayerInput.coins >= price) {
            PlayerInput.coins -= price;
            
            foreach (var i in shopManager.fishes) 
                if (i.type == type) i.isBought=true;

            ShopManager.TypeOfFishes = type;
            ShopManager.SwitchFish?.Invoke();
            
            SwitchButton();
        }
    }

    public void SwitchButton() {
        if(!Check()) return;
        
        isOn = !isOn;
        
        if(isOn == false) {
            priceText.text = "Выключено";
            priceText.color = Color.red;
        }
        else {
            priceText.text = "Включено";
            priceText.color = Color.green;
        }
    }

    public void DisableButton() {
        if(!Check()) return;
        isOn = false;
        priceText.text = "Выключено";
        priceText.color = Color.red;
    }
    
    private bool Check() {
        foreach (var i in shopManager.fishes) {
            if (i.type == type & i.isBought) return true;
        }
        return false;
    }
}
