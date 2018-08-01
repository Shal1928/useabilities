using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Windows.Input;
using System.Windows.Interop;
using UseAbilities.System.Core;
using UseAbilities.System.Core.Enums;

namespace UseAbilities.System.ApplicationHelper
{
    public class HotKeyHelper : IDisposable
    {
        public const int WM_HOTKEY = 0x0312;

        private static Dictionary<int, HotKeyHelper> _dictHotKeyToCalBackProc;

        private bool _disposed = false;

        public Key Key { get; private set; }

        public KeyModifier KeyModifiers { get; private set; }
        public Action Action { get; private set; }
        public int Id { get; set; }

        // ******************************************************************
        public HotKeyHelper(Key k, KeyModifier keyModifiers, Action action, bool register = true)
        {
            Key = k;
            KeyModifiers = keyModifiers;
            Action = action;
            if (register)
            {
                Register();
            }
        }

        // ******************************************************************
        public bool Register()
        {
            int virtualKeyCode = KeyInterop.VirtualKeyFromKey(Key);
            Id = virtualKeyCode + ((int)KeyModifiers * 0x10000);
            bool result = User32.RegisterHotKey(IntPtr.Zero, Id, (uint)KeyModifiers, (uint)virtualKeyCode);

            if (_dictHotKeyToCalBackProc == null)
            {
                _dictHotKeyToCalBackProc = new Dictionary<int, HotKeyHelper>();
                ComponentDispatcher.ThreadFilterMessage += new ThreadMessageEventHandler(ComponentDispatcherThreadFilterMessage);
            }

            _dictHotKeyToCalBackProc.Add(Id, this);

            Debug.Print(result.ToString() + ", " + Id + ", " + virtualKeyCode);
            return result;
        }

        // ******************************************************************
        public void Unregister()
        {
            HotKeyHelper hotKey;
            if (_dictHotKeyToCalBackProc.TryGetValue(Id, out hotKey))
            {
                User32.UnregisterHotKey(IntPtr.Zero, Id);
            }
        }

        // ******************************************************************
        private static void ComponentDispatcherThreadFilterMessage(ref MSG msg, ref bool handled)
        {
            if (!handled)
            {
                if (msg.message == WM_HOTKEY)
                {
                    HotKeyHelper hotKey;

                    if (_dictHotKeyToCalBackProc.TryGetValue((int)msg.wParam, out hotKey))
                    {
                        if (hotKey.Action != null)
                        {
                            hotKey.Action.Invoke();
                        }
                        handled = true;
                    }
                }
            }
        }

        // ******************************************************************
        // Implement IDisposable.
        // Do not make this method virtual.
        // A derived class should not be able to override this method.
        public void Dispose()
        {
            Dispose(true);
            // This object will be cleaned up by the Dispose method.
            // Therefore, you should call GC.SupressFinalize to
            // take this object off the finalization queue
            // and prevent finalization code for this object
            // from executing a second time.
            GC.SuppressFinalize(this);
        }

        // ******************************************************************
        // Dispose(bool disposing) executes in two distinct scenarios.
        // If disposing equals true, the method has been called directly
        // or indirectly by a user's code. Managed and unmanaged resources
        // can be _disposed.
        // If disposing equals false, the method has been called by the
        // runtime from inside the finalizer and you should not reference
        // other objects. Only unmanaged resources can be _disposed.
        protected virtual void Dispose(bool disposing)
        {
            // Check to see if Dispose has already been called.
            if (!this._disposed)
            {
                // If disposing equals true, dispose all managed
                // and unmanaged resources.
                if (disposing)
                {
                    // Dispose managed resources.
                    Unregister();
                }

                // Note disposing has been done.
                _disposed = true;
            }
        }
    }
}
