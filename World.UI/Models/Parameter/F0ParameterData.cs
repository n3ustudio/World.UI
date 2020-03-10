using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Musiqual.Parameter;

namespace World.UI.Models.Parameter
{
    public sealed class F0ParameterData : ParameterData
    {

        public F0ParameterData(
            bool isNatural = false,
            string name = "PIT",
            List<double> parameterList = null)
            :
            base(isNatural, name, new ObservableCollection<double>(parameterList))
        {

        }

    }
}
