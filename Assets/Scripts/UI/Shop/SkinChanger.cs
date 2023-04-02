using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SkinChanger : MonoBehaviour
{
    public Skin[] Skins;
    private bool[] _stock;

    [SerializeField] private Button _prevButton;
    [SerializeField] private Button _nextButton;
    [SerializeField] private Button _buyButton;
    [SerializeField] private TextMeshProUGUI _price;
    [SerializeField] private Money _money;
    [SerializeField] private Transform _skin;
    [SerializeField] private BirdScreen _birdScreen;
    [SerializeField] private ShopScreen _shopScreen;

    private int _index;

    private void Awake()
    {
        LoadData();

        

        SetChosenButton();
    }

    private void OnEnable()
    {
        _nextButton.onClick.AddListener(OnNextButtonClick);
        _prevButton.onClick.AddListener(OnPrevButtonClick);
        _buyButton.onClick.AddListener(OnBuyButtonClick);
        _birdScreen.CloseButtonClick += SetChosenSkin;
        _shopScreen.BirdButtonClick += SetChosenSkin;
    }

    private void OnDisable()
    {
        _nextButton.onClick.RemoveListener(OnNextButtonClick);
        _prevButton.onClick.RemoveListener(OnPrevButtonClick);
        _buyButton.onClick.RemoveListener(OnBuyButtonClick);
        _birdScreen.CloseButtonClick -= SetChosenSkin;
        _shopScreen.BirdButtonClick -= SetChosenSkin;

    }

    private void OnNextButtonClick()
    {

        if (_index < _skin.childCount -1)
        {
            _index++;

            if (Skins[_index].inStock && Skins[_index].isChosen)
            {
                SetChosenButton();
            }
            else if (!Skins[_index].inStock)
            {
                SetPriceButton();
            }
            else if (Skins[_index].inStock && !Skins[_index].isChosen)
            {
                SetChoseButton();
            }
            _prevButton.interactable = true;
            SetSkin(_index);
        }
        if (_index == _skin.childCount - 1)
        {
            _nextButton.interactable = false;
        }
        Debug.Log(_index);
    }

    private void OnPrevButtonClick()
    {

        if (_index > 0)
        {
            _index--;

            if (Skins[_index].inStock && Skins[_index].isChosen)
            {
                SetChosenButton();
            }
            else if (!Skins[_index].inStock)
            {
                SetPriceButton();
            }
            else if (Skins[_index].inStock && !Skins[_index].isChosen)
            {
                SetChoseButton();
            }
            _nextButton.interactable = true;
            SetSkin(_index);
        }

        if (_index == 0)
        {
            _prevButton.interactable = false;
        }

        Debug.Log(_index);

    }


    private void SetChosenSkin()
    {
        int _chosenSkin = PlayerPrefs.GetInt("chosenSkin");
        _index = _chosenSkin;

        SetSkin(_chosenSkin);
        SetChosenButton();
    }
    private void OnBuyButtonClick ()
    {
        if (_buyButton.interactable && !Skins[_index].inStock)
        {
            int price = int.Parse(_price.text);

            if (_money.CheckMoney(price))
            {
                _money.SpendMoney(price);
                _stock[_index] = true;
                Skins[_index].inStock = true;
                SetChoseButton();
                SavePurchase();
            }
        }
        else if (_buyButton.interactable && !Skins[_index].isChosen)
        {
            PlayerPrefs.SetInt("chosenSkin", _index);

            for (int i = 0; i < Skins.Length; i++)
                Skins[i].isChosen = false;

            Skins[_index].isChosen = true;
            SetChosenButton();
        }
    }

    public void SavePurchase()
    {
        PlayerPrefsX.SetBoolArray("StockArray", _stock);
    }

    private void SetSkin(int skinIndex)
    {
        for (int i = 0; i < _skin.childCount; i++)
        {
            _skin.GetChild(i).gameObject.SetActive(false);
        }
        _skin.GetChild(skinIndex).gameObject.SetActive(true);

    }

    private void LoadData()
    {
        _index = PlayerPrefs.GetInt("chosenSkin");

        _stock = new bool[Skins.Length];

        if (PlayerPrefs.HasKey("StockArray"))
            _stock = PlayerPrefsX.GetBoolArray("StockArray");
        else
            _stock[0] = true;

        Skins[_index].isChosen = true;

        for (int i = 0; i < Skins.Length; i++)
        {
            Skins[i].inStock = _stock[i];
        }
        SetSkin(_index);
    }

    private void SetChosenButton()
    {
        _price.text = "CHOSEN";
        _buyButton.interactable = false;
    }

    private void SetPriceButton()
    {
        _price.text = Skins[_index].cost.ToString();

        int price = int.Parse(_price.text);

        if (_money.CheckMoney(price))
            _buyButton.interactable = true;
        else
            _buyButton.interactable = false;
    }

    private void SetChoseButton()
    {
        _price.text = "CHOSE";
        _buyButton.interactable = true;
    }
}

[System.Serializable]
public class Skin
{
    public int cost;
    public bool inStock;
    public bool isChosen;
}
