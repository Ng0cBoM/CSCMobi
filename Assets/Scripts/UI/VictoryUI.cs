using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VictoryUI : MonoBehaviour, IUI
{
    [SerializeField] private Button _playButton;

    private void Awake()
    {
        _playButton.onClick.AddListener(
            () =>
            {
                GameManager.I.LoadLevel();
                Hide();
            });
    }

    private void OnDestroy()
    {
        _playButton.onClick.RemoveAllListeners();
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }

    public void Setup(UIManager ui)
    {
    }

    public void Show()
    {
        gameObject.SetActive(true);
    }
}