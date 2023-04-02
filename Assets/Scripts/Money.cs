using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Money : MonoBehaviour
{

    [SerializeField] private TMP_Text _coinsValue;

    private int _money;

    private void Awake()
    {
        _money = PlayerPrefs.GetInt("money");
        _coinsValue.text = _money.ToString();
    }

    private void SaveMoney()
    {
        PlayerPrefs.SetInt("money", _money);
    }

    public void IncreaseMoney(int score)
    {
        _money += score;
        _coinsValue.text = _money.ToString();
        SaveMoney();
    }

    public void SpendMoney(int price)
    {
        _money -= price;
        _coinsValue.text = _money.ToString();
        SaveMoney();
    }

    public bool CheckMoney (int price)
    {
        if (price < _money || price == _money)
            return true;
        else
            return false;

    }
}
