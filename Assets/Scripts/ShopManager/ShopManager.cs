using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using YG;

public enum TypeOfProduct{
    standart,
    fish,
    fishisj,
    USSPERMIT,
    apple,
    mta
}

[Serializable]
public class Product{
    public GameObject obj;
    public TypeOfProduct type;
    public bool isBought;
}

public class ShopManager : MonoBehaviour
{
    [SerializeField] private GameObject standart_fish, shopMenu, pauseMenu;
    [SerializeField] private Text CountOfPoints_Text;
    [SerializeField] private PlayerInput points;
    [SerializeField] private CellButton[] buttons;
    
    public static TypeOfProduct TypeOfFishes;
    
    public static Action SwitchFish;
    
    public Product[] fishes;

    private bool _isOpenShopMenu = false;

    void Start() {
        standart_fish.SetActive(true);
        TypeOfFishes=TypeOfProduct.standart;
    }

    void Update() {
        CountOfPoints_Text.text = PlayerInput.coins.ToString();
    }
    
    public void SwitchShopMenu() {
        shopMenu.SetActive(!_isOpenShopMenu);
        pauseMenu.SetActive(!shopMenu.activeSelf);
        _isOpenShopMenu = !_isOpenShopMenu;
    }
    
    void DisableAllButtons() {
        foreach (var i in buttons) 
            i.DisableButton();
    }

    void EnableProduct() {
        foreach (var i in fishes) 
            i.obj.SetActive(false);
        
        foreach (var i in fishes) {
            if (i.type == TypeOfFishes) 
                i.obj.SetActive(true);
        }
    }

    void OnEnable() {
        SwitchFish+=EnableProduct;
        SwitchFish+=DisableAllButtons;
    }

    void OnDisable() {
        SwitchFish-=EnableProduct;
        SwitchFish-=DisableAllButtons;
    }
}
