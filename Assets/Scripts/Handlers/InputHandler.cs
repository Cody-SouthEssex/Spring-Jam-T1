using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Control
{
    Up, Down, Left, Right,
    Dodge,
    Escape,
    Interact,
    Primary,
    Jump,
}

public enum KeyPressType
{
    Up, Down, Held
}

public class InputHandler : MonoBehaviour
{
    [System.Serializable]
    public class ControlKeycode
    {
        public Control control;
        public KeyCode keyCode;

        public ControlKeycode(Control control, KeyCode keyCode)
        {
            this.control = control;
            this.keyCode = keyCode;
        }
    }

    public List<ControlKeycode> currentKeybinds = new List<ControlKeycode>
    {
        new ControlKeycode(Control.Up, KeyCode.W),
        new ControlKeycode(Control.Left, KeyCode.A),
        new ControlKeycode(Control.Right, KeyCode.D),
        new ControlKeycode(Control.Down, KeyCode.S),
        new ControlKeycode(Control.Dodge, KeyCode.Space),
        new ControlKeycode(Control.Escape, KeyCode.Escape),
        new ControlKeycode(Control.Interact, KeyCode.E),
        new ControlKeycode(Control.Primary, KeyCode.Mouse0),
        new ControlKeycode(Control.Jump, KeyCode.Q)
    };
    private List<ControlKeycode> defaultKeybinds = new List<ControlKeycode>
    {
        new ControlKeycode(Control.Up, KeyCode.W),
        new ControlKeycode(Control.Left, KeyCode.A),
        new ControlKeycode(Control.Right, KeyCode.D),
        new ControlKeycode(Control.Down, KeyCode.S),
        new ControlKeycode(Control.Dodge, KeyCode.Space),
        new ControlKeycode(Control.Escape, KeyCode.Escape),
        new ControlKeycode(Control.Interact, KeyCode.E),
        new ControlKeycode(Control.Primary, KeyCode.Mouse0),
        new ControlKeycode(Control.Jump, KeyCode.Q)
    };

    public Dictionary<Control, KeyCode> keybindDictionary = new Dictionary<Control, KeyCode>();


    // Start is called before the first frame update
    void Start()
    {
        LoadDefaults();
    }

    private void LoadKeybinds()
    {
        foreach (var keybind in currentKeybinds)
        {
            keybindDictionary.Add(keybind.control, keybind.keyCode);
        }
    }

    private void LoadDefaults()
    {
        currentKeybinds.Clear();
        currentKeybinds.AddRange(defaultKeybinds);

        LoadKeybinds();
    }

    public bool GetInput(Control control, KeyPressType keyPressType)
    {
        switch (keyPressType)
        {
            case KeyPressType.Up:
                if (Input.GetKeyUp(keybindDictionary[control]))
                {
                    return true;
                }
                break;
            case KeyPressType.Down:
                if (Input.GetKeyDown(keybindDictionary[control]))
                {
                    return true;
                }
                break;
            case KeyPressType.Held:
                if (Input.GetKey(keybindDictionary[control]))
                {
                    return true;
                }
                break;
        }

        return false;
    }
}
