using UnityEngine;

public class SaveWrapper : MonoBehaviour
{
    [SerializeField] private SaveSystem _saveSystem;
    private PlayerInput _input;

    public static SaveWrapper instance;

    private void Awake()
    {
        _input = new PlayerInput();
        instance = this;
    }

    private void OnEnable()
    {
        _input.Enable();
    }

    private void OnDisable()
    {
        _input.Disable();
    }

    private void Update()
    {
        if (_input.Player.Save.WasPerformedThisFrame())
        {
            SaveGame();
        }

        if (_input.Player.Load.WasPerformedThisFrame())
        {
            LoadGame();
        }

    }

    public void LoadGame()
    {
        Debug.Log("Loading");
        _saveSystem.LoadData();
    }

    public void SaveGame()
    {
        Debug.Log("Saving");
        _saveSystem.SaveData();
    }
}
