using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ����� Input�� ����ϴ� �Լ�
/// </summary>
public class InputManager
{
    // �ൿ - Ű�ڵ尡 ����� ��ųʸ�
    private static Dictionary<InputAction, KeyCode> keyDict = new Dictionary<InputAction, KeyCode>();

    // �ε�� Ŭ����
    class InputKey
    {
        public InputAction inputAction;
        public KeyCode keycode;
    }

    public void OnStart()
    {
        // �ε� & ��ųʸ��� �߰�
        List<InputKey> inputs = GameManager.Instance.SpreadData.GetDatas<InputKey>(SheetType.Key);

        foreach(var pair in inputs)
        {
            keyDict.Add(pair.inputAction, pair.keycode);
        }
    }

    // Ű�� ������ ��
    public static bool GetKey(InputAction inputAction)
    {
        if (!keyDict.ContainsKey(inputAction))
            return false;

        return Input.GetKey(keyDict[inputAction]);
    }

    // Ű�� ������ ��
    public static bool GetKeyDown(InputAction inputAction)
    {
        if (!keyDict.ContainsKey(inputAction))
            return false;

        return Input.GetKeyDown(keyDict[inputAction]);
    }

    // Ű�� ������ ���� ��
    public static bool GetkeyUp(InputAction inputAction)
    {
        if (!keyDict.ContainsKey(inputAction))
            return false;

        return Input.GetKeyUp(keyDict[inputAction]);
    }

    // �ൿ�� ����Ǵ� key�� �ٲ��ִ� �Լ�
    public static void ChangeKey(InputAction inputAction, KeyCode key)
    {
        keyDict[inputAction] = key;
    }
}