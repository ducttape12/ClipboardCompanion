using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows;
using System.Windows.Input;
using ClipboardCompanion.Services;

namespace ClipboardCompanion.ViewModels
{
    public class GuidCreatorCompanionViewModel : CompanionViewModelBase
    {
        public override event PropertyChangedEventHandler PropertyChanged;

        public GuidCreatorCompanionViewModel(IHotKeyService hotKeyService) : base(hotKeyService)
        {
        }

        public override Action HotKeyPressedAction
        {
            get
            {
                return () =>
                {
                    Clipboard.SetText(Guid.NewGuid().ToString());
                };
            }
        }

        public override void Initialize()
        {
            base.Initialize();

            IsEnabled = true;
            ShiftModifier = true;
            ControlModifier = true;
            Key = Key.G;
        }
    }
}
