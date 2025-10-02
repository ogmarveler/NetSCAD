using System;
using System.Collections.Generic;
using System.Linq;
using static NetScad.Core.Measurements.Selector;


namespace NetScad.Core.Utility
{
    // Store (const) enums from NetScad.Core here to be used as static resources or compiled bindings
    public static class EnumHelper
    {
        public static IReadOnlyList<BackgroundType> BackgroundTypeValues => Enum.GetValues(typeof(BackgroundType)).Cast<BackgroundType>().ToList();
        public static IReadOnlyList<UnitSystem> UnitSystemValues => Enum.GetValues(typeof(UnitSystem)).Cast<UnitSystem>().ToList();
    }
}
