using UnityEngine;

public class User : MonoBehaviour
{
    [SerializeField] private Camera _camera;
    [SerializeField] private Flag _flag;
    [SerializeField] private SpawnerFlags _spawnerFlags;

    private BaseBots _selectedBaseBots;
    private Flag _currentFlag;
    private bool _isBaseBotsSelected;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            HandleMouseClick();
        }
    }

    private void HandleMouseClick()
    {
        Ray ray = _camera.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            if (_isBaseBotsSelected)
            {
                PlaceFlag(hit);
            }
            else
            {
                SelectBaseBot(hit);
            }
        }
    }

    private void PlaceFlag(RaycastHit hit)
    {
        Vector3 flagPosition = new Vector3(hit.point.x, hit.point.y + 1, hit.point.z);

        if (hit.transform.TryGetComponent(out Ground ground))
        {
            if (_currentFlag == null)
            {
                _currentFlag = _spawnerFlags.Create(flagPosition);

                _currentFlag.transform.SetParent(_selectedBaseBots.transform);

                _selectedBaseBots.SetFlag(_currentFlag);
            }
            else
            {
                _currentFlag.transform.position = flagPosition;
            }

            _selectedBaseBots = null;

            _isBaseBotsSelected = false;
        }
    }

    private void SelectBaseBot(RaycastHit hit)
    {
        if (hit.transform.TryGetComponent(out BaseBots baseBots))
        {
            _selectedBaseBots = baseBots;

            _currentFlag = baseBots.GetComponentInChildren<Flag>();

            _isBaseBotsSelected = true;
        }
    }
}
