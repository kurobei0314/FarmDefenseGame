using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Events;
using UnityEditor;
using System;
using System.Linq;
using System.Collections.Generic;


[CustomEditor(typeof(TestController))]
public class InspectorEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        var tmp = target as TestController;
        var playerInput = tmp.PlayerInput;
        var inputActionList = tmp.List;
        if (playerInput == null || inputActionList == null) return;

        var currentActionNames = playerInput.actions.Select(action => action.name).OrderBy(name => name).ToArray();
        var beforeActionNames = inputActionList.Select(inputAction => inputAction.ActionName).OrderBy(name => name).ToArray();
        if (!Enumerable.SequenceEqual(currentActionNames, beforeActionNames))
        {
            // Action が変化したので更新
            UpdateActionList();
        }
    }

    public void UpdateActionList()
    {
        var testController = target as TestController;
        var playerInput = testController.PlayerInput;
        var inputActionList = testController.List;

        var inputSystemList = playerInput?.actions.ToLookup(value => value.actionMap.name);
        var inputActions = inputActionList.ToLookup(action => action.ActionMapName);

        // 新たにInputSystemから追加されたものを取得し、追加する
        foreach (var list in inputSystemList)
        {
            var actionMapName = list.Key;
            foreach (var inputSystemAction in list)
            {
                var notFind = true;
                foreach(var inputAction in inputActions[actionMapName])
                {
                    if (inputSystemAction.name == inputAction.ActionName)
                    {
                        notFind = false;
                        break;
                    }
                }
                if (notFind) testController.List.Add(new InputCallback(actionMapName, inputSystemAction.name));
            }
        }

        // 逆にSerializeFieldにしかないものを見つけて削除する
        foreach(var list in inputActions)
        {
            var actionMapName = list.Key;

            foreach(var inputSystemAction in list)
            {
                var notFind = true;
                foreach(var action in inputSystemList[actionMapName])
                {
                    if (inputSystemAction.ActionName == action.name)
                    {
                        notFind = false;
                        break;
                    }
                }
                if (notFind) testController.List.Remove(inputSystemAction);
            }
        }

        if (testController.List.Count != playerInput.actions.Count())
        {
            Debug.LogError("system Inputに設定されているaction数とcallbackの数が合いません");
        }
    }
}

[Serializable]
public class InputCallback
{
    [SerializeField] public string ActionMapName;
    [SerializeField] public string ActionName;
    [SerializeField] public UnityEvent<InputAction.CallbackContext> Callback;

    public InputCallback(string actionMapName, string actionName)
    {
        ActionMapName = actionMapName;
        ActionName = actionName;
    }
}

[RequireComponent(typeof(PlayerInput))]
public class TestController : MonoBehaviour
{
    [SerializeField] public PlayerInput PlayerInput;
    [SerializeField] public List<InputCallback> List;

    private void Awake()
    {
        
    }

    public void SwitchActionMaps()
    {

    }

    private void OnMove(InputValue value)
    {
        Debug.LogError("OnMove!!!");
    }

    private void OnStickInput(InputValue value)
    {
        Debug.LogError("StickInput!!!!");
    }
}
