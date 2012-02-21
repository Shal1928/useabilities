using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Common.Code.UseAbilities.MVVM.Base
{
    public class WindowedViewModel : ViewModelBase
    {
        public override void Close()
        {
            base.Close();
        }

        //TODO: Make OnClosed event
    }
}
