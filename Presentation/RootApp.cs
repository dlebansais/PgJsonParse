using System.Collections.Generic;
using System.Windows;

namespace Presentation
{
    public class RootApp : Application
    {
        protected virtual void InitializeApp()
        {
        }

        protected virtual RootControl CreateRootControl()
        {
            return null;
        }

        public IList<RootControl> RootControlList
        {
            get
            {
                List<RootControl> Result = new List<RootControl>();
                foreach (RootControl Item in Windows)
                    Result.Add(Item);

                return Result;
            }
        }
    }
}
