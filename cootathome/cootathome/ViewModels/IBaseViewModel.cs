using System;
using System.Collections.Generic;
using System.Text;

namespace cootathome.ViewModels
{
    public interface IBaseViewModel
    {
        void CleanUp(IBaseViewModel obj);
    }
}
