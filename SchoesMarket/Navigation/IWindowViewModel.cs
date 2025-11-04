using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoesMarket.Navigation
{
    public interface IWindowViewModel
    {
        event Action RequestClose;
        event Action RequestNavigateToMain;
    }
}
