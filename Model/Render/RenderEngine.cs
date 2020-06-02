using ProjectB.Model.Help;
using System;

namespace ProjectB.Model.Render
{
    class RenderEngine
    {
        public static event Action<Cord> UpdateField;

        public static void TriggerUpdate(Cord cord)
        {
            UpdateField?.Invoke(cord);
        }

    }
}
