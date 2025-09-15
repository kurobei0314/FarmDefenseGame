using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Events;
using UnityEditor;
using System;
using System.Linq;
using System.Collections.Generic;
using static UnityEngine.InputSystem.InputAction;

namespace WolfVillage.Common
{
    [CustomEditor(typeof(InputController))]
    public class InspectorEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            var inputController = target as InputController;
            var actionAsset = inputController.ActionAsset;
            var inputActionList = inputController.InputCallbackList;
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
            var inputController = target as InputController;
            var actionAsset = inputController.ActionAsset;
            var inputActionList = inputController.InputCallbackList;

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
                    if (notFind) inputController.InputCallbackList.Add(new InputCallback(actionMapName, inputSystemAction.name));
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
                    if (notFind) inputController.InputCallbackList.Remove(inputSystemAction);
                }
            }

            if (inputController.InputCallbackList.Count != actionAsset.Count())
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

        public void InitializeActionEvent()
            => Action = (context) => Callback?.Invoke(context);
        
        public Action<CallbackContext> Action;
    }

    public interface IInputController
    {
        void Initialize(string actionMapName);
        void SwitchActionMaps(string actionMapName);
        bool IsPressed(string actionMapName, string actionName);
        Vector2 GetReadValueByVector2(string actionName);
    }

    [RequireComponent(typeof(PlayerInput))]
    public class InputController : MonoBehaviour, IInputController
    {
        [SerializeField] public InputActionAsset ActionAsset;
        [SerializeField] public List<InputCallback> InputCallbackList;
        private string currentActionMapName = String.Empty;

        public void Initialize(string actionMapName)
        {
            for (var i = 0; i < InputCallbackList.Count; i++)
            {
                InputCallbackList[i].InitializeActionEvent();
            }
            SwitchActionMaps(actionMapName);
            ActionAsset.Enable();
        }

        public void SwitchActionMaps(string actionMapName)
        {
            if (currentActionMapName != String.Empty)
            {
                UnSetCallbackForInputActionAsset(currentActionMapName);
            }
            SetCallbackForInputActionAsset(actionMapName);
            currentActionMapName = actionMapName;
        }

        private void SetCallbackForInputActionAsset(string actionMapName)
        {
            if (currentActionMapName == actionMapName) return;
            foreach (var inputCallback in InputCallbackList)
            {
                if (actionMapName != inputCallback?.ActionMapName) continue;
                var action = ActionAsset?.FindActionMap(inputCallback?.ActionMapName)?.FindAction(inputCallback?.ActionName);
                if (action == null) continue;
                action.performed += inputCallback?.Action;
                action.Enable();
            }
        }

        private void UnSetCallbackForInputActionAsset(string actionMapName)
        {
            if (currentActionMapName != actionMapName) return;
            foreach(var inputCallback in InputCallbackList)
            {
                if (actionMapName != inputCallback?.ActionMapName) continue;
                var action = ActionAsset?.FindActionMap(inputCallback?.ActionMapName)?.FindAction(inputCallback?.ActionName);
                if (action == null) continue;
                action.performed -= inputCallback?.Action;
                action.Disable();
            }
            currentActionMapName = String.Empty;
        }

        public bool IsPressed(string actionMapName, string actionName)
        {
            if (actionMapName != currentActionMapName) return false;
            return ActionAsset?.FindActionMap(currentActionMapName)?.FindAction(actionName).IsPressed() ?? false;
        }

        public Vector2 GetReadValueByVector2(string actionName)
            => ActionAsset?.FindActionMap(currentActionMapName)?.FindAction(actionName).ReadValue<Vector2>() ?? Vector2.zero;
    }
}
