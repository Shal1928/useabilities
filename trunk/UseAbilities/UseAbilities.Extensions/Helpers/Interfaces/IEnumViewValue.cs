using System;

namespace UseAbilities.Extensions.Helpers.Interfaces
{
    public interface IEnumViewValue
    {
        Enum Value
        {
            get;
        }

        string View
        {
            get;
        }
    }
}
