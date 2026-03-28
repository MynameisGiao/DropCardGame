using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum TypeCollection
{
    All,
    Legendary,
    Epic,
    Common
} 

public class ButtonTypeCollection : MonoBehaviour
{
    [SerializeField] private GameObject _scaler;
    [SerializeField] private GameObject btn_lock;
    [SerializeField] private TypeCollection _type;
    [SerializeField] private Button _button;
    private CollectionView _collectionView;

    private void OnEnable()
    {
        if (_button != null)
        {
            _button.onClick.AddListener(OnButtonClicked);
        }
    }

    private void OnDisable()
    {
        if (_button != null)
        {
            _button.onClick.RemoveListener(OnButtonClicked);
        }
    }

    private void OnButtonClicked()
    {
        if (_collectionView != null)
        {
            _collectionView.OnClickTypeCollection(_type);
        }
    }

    public void SetCollectionView(CollectionView collectionView)
    {
        _collectionView = collectionView;
    }

    public void UpdateButtonSelection(TypeCollection selectedType)
    {
        bool isChosen = (_type == selectedType);
        Setup(isChosen);
    }

    public void Setup(bool isChosen)
    {
        btn_lock.SetActive(!isChosen);
        if (isChosen)
        {
            _scaler.transform.localScale = Vector3.one * 1.1f;
        }
        else
        {
            _scaler.transform.localScale = Vector3.one * 0.9f;
        }
    }
}
