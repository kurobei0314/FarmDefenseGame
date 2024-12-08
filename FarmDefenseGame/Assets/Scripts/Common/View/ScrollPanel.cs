using System;
using UnityEngine;

namespace WolfVillage.Common
{
    public abstract class ScrollPanel<ViewModel> : MonoBehaviour where ViewModel : ScrollPanelVM
    {
        protected ViewModel viewModel;
        protected Action<ViewModel> ClickAction;

        public void Setup(ViewModel viewModel, Action<ViewModel> onClick)
        {
            this.viewModel = viewModel;
            ClickAction = onClick;
            UpdateView();
        }

        public abstract void UpdateView();
        public abstract void OnClick();
    }
}