using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using YG;

public class SaveController : MonoBehaviour{
    [SerializeField] private ShopManager shopManager;

    void Save() {
        YandexGame.savesData.points = PlayerInput.coins;

        YandexGame.savesData.products.Clear();
        foreach (var i in shopManager.fishes) {
            ProductSave productSave = new ProductSave();
            productSave.type = i.type;
            productSave.isBought = i.isBought;
            
            YandexGame.savesData.products.Add(productSave);
        }
        
        YandexGame.SaveProgress();
    }

    void Load() {
        PlayerInput.coins = YandexGame.savesData.points;

        foreach (var i in YandexGame.savesData.products) {
            foreach (var g in shopManager.fishes) {
                if (i.type == g.type) 
                    g.isBought = i.isBought;
            }
        }
    }
    
    void Start() {
        Load();
        StartCoroutine(AutoSave());
    }

    IEnumerator AutoSave() {
        yield return new WaitForSeconds(3f);
        Save();
        StartCoroutine(AutoSave());
    }

    private void OnEnable() {
        YandexGame.GetDataEvent += Load;
    }

    private void OnDisable() {
        YandexGame.GetDataEvent -= Load;
    }
}
