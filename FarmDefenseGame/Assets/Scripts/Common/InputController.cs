using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Events;
using UnityEditor;
using System;
using System.Linq;
using System.Collections.Generic;

namespace WolfVillage.Common
{
    [CustomEditor(typeof(InputController))]
    public class InspectorEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            var tmp = target as InputController;
            var actionAsset = tmp.ActionAsset;
            var inputActionList = tmp.InputCallbackList;
            if (actionAsset == null || inputActionList == null) return;

            var currentActionNames = actionAsset.Select(action => action.name).OrderBy(name => name).ToArray();
            var beforeActionNames = inputActionList.Select(inputAction => inputAction.ActionName).OrderBy(name => name).ToArray();
            if (!Enumerable.SequenceEqual(currentActionNames, beforeActionNames))
            {
                // Action が変化したので更新
                UpdateActionList();
            }
        }

        public void UpdateActionList()
        {
            var testController = target as InputController;
            var actionAsset = testController.ActionAsset;
            var inputActionList = testController.InputCallbackList;

            var inputSystemList = actionAsset?.ToLookup(value => value.actionMap.name);
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
                    if (notFind) testController.InputCallbackList.Add(new InputCallback(actionMapName, inputSystemAction.name));
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
                    if (notFind) testController.InputCallbackList.Remove(inputSystemAction);
                }
            }

            if (testController.InputCallbackList.Count != actionAsset.Count())
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
    public class InputController : MonoBehaviour
    {
        [SerializeField] public InputActionAsset ActionAsset;
        [SerializeField] public List<InputCallback> InputCallbackList;

        private string currentActionMapName;

        private void Awake()
        {
            SetCallbackForInputActionAsset(ActionMapName.SearchMap);
            currentActionMapName = ActionMapName.SearchMap;
            ActionAsset.Enable();
        }

        private void SetCallbackForInputActionAsset(string actionMapName)
        {
            foreach (var inputCallback in InputCallbackList)
            {
                if (actionMapName != inputCallback?.ActionMapName) continue;
                var action = ActionAsset?.FindActionMap(inputCallback?.ActionMapName)?.FindAction(inputCallback?.ActionName);
                if (action == null) continue;
                action.performed += (context) => inputCallback?.Callback?.Invoke(context);
                action.Enable();
            }
        }

        private void UnSetCallbackForInputActionAsset(string actionMapName)
        {
            foreach(var inputCallback in InputCallbackList)
            {
                if (actionMapName != inputCallback?.ActionMapName) continue;
                var action = ActionAsset?.FindActionMap(inputCallback?.ActionMapName)?.FindAction(inputCallback?.ActionName);
                if (action == null) continue;
                action.performed -= (context) => inputCallback?.Callback?.Invoke(context);
                action.Disable();
            }
        }

        public void SwitchActionMaps(string actionMapName)
        {
            UnSetCallbackForInputActionAsset(currentActionMapName);
            SetCallbackForInputActionAsset(actionMapName);
            currentActionMapName = actionMapName;
        }
    }
}
