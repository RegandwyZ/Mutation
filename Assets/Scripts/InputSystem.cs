using UnityEngine;

public class InputSystem : MonoBehaviour
{
    private Character _selectCharacter;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
            HandleLeftClick();

        if (Input.GetMouseButtonDown(1))
            HandleRightClick();
    }

    private void HandleLeftClick()
    {
        var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out var hit))
        {
            Character character = hit.collider.GetComponent<Character>();
            if (character != null)
            {
                if (_selectCharacter == null)
                {
                    _selectCharacter = character;
                    _selectCharacter.SelectUnit();
                }
                else
                {
                    _selectCharacter.DeSelectUnit();
                    if (_selectCharacter != character)
                    {
                        _selectCharacter = character;
                        _selectCharacter.SelectUnit();
                    }
                    else
                    {
                        _selectCharacter = null;
                    }
                }
            }
            else
            {
                if (_selectCharacter != null)
                {
                    _selectCharacter.DeSelectUnit();
                    _selectCharacter = null;
                }
            }
        }
    }

    private void HandleRightClick()
    {
        if (_selectCharacter == null)
            return;

        var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out var hit))
        {
            if (_selectCharacter.IsSelected)
            {
                _selectCharacter.Move(hit.point);
                if (Input.GetMouseButtonDown(0))
                {
                    _selectCharacter.DeSelectUnit();
                    _selectCharacter = null;
                }
            }
        }
    }
}