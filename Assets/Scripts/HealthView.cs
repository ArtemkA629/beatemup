using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class HealthView : MonoBehaviour
{
    [SerializeField] private Image _progressBar;

    [Inject] private Player _player;

    public void OnHealthChanged()
    {
        _progressBar.fillAmount = _player.Health.Amount / _player.MaxHealthAmount;
    }
}
