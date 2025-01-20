using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using YG;

public class ADManager : MonoBehaviour{
    [SerializeField] private int CountCoinsForAD;

    public void CallAD() {
        YandexGame.RewVideoShow(1);
    }
    
    void Reward(int id) {
        if (id == 1) {
            PlayerInput.coins+=CountCoinsForAD;
            AudioListener.volume = 1;
        }
    }

    private void OnEnable() {
        YandexGame.RewardVideoEvent += Reward;
    }

    private void OnDisable() {
        YandexGame.RewardVideoEvent -= Reward;
    }
}
