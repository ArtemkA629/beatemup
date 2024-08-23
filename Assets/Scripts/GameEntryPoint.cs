using UnityEngine;

public class GameEntryPoint : MonoBehaviour
{
    [SerializeField] private Player _player;

    private void OnEnable()
    {
        _player.Init();
    }
}
