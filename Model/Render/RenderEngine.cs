using ProjectB.Model.Help;
using System;

namespace ProjectB.Model.Render
{
    class RenderEngine
    {
        private static readonly RenderEngine engine = null;
        public static RenderEngine Engine
        {
            get
            {
                if (engine is null)
                {
                    return new RenderEngine();
                }
                else
                {
                    return engine;
                }
            }
        }

        public static event Action<Cord> UpdateField;

        public static void TriggerUpdate(Cord cord)
        {
            UpdateField?.Invoke(cord);
        }

    }
}
