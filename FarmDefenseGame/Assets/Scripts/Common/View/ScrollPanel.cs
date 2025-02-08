using System;
using UnityEngine;

namespace WolfVillage.Common
{
    public abstract class ScrollPanel<ViewModel> : MonoBehaviour where ViewModel : ScrollPanelVM
    {
        protected ViewModel viewModel;
        protected Action<ViewModel> SelectAction;
        protected Action<ViewModel> FocusAction;

        public void Setup(ViewModel viewModel, Action<ViewModel> onFocus, Action<ViewModel> onSelect)
        {
            this.viewModel = viewModel;
            FocusAction = onFocus;
            SelectAction = onSelect;
            UpdateView();
        }
        public ViewModel Data => viewModel;
        public abstract void UpdateView();
        public abstract void OnSelect();
        public abstract void OnFocus();
        public abstract void OnUnFocus();
        public abstract void Dispose();
    }
}