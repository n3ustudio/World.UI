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

        private F0ParameterData(
            bool isNatural = true,
            string name = "F0",
            List<double> parameterList = null,
            double tolerance = 0.00001)
            :
            base(isNatural, name, parameterList, tolerance)
        {

        }

        public F0ParameterData CreateF0ParameterData(
            List<double> parameterList = null,
            double tolerance = 0.00001) =>
            new F0ParameterData(true, "F0", parameterList, tolerance);

    }

    public sealed class F0DeltaParameterData : ParameterData
    {

        private F0DeltaParameterData(
            List<double> f0List,
            bool isNatural = false,
            string name = "PIT",
            List<double> parameterList = null,
            double tolerance = 0.00001)
            :
            base(isNatural, name, parameterList, tolerance)
        {
            f0OriginList = f0List;
        }

        public F0DeltaParameterData CreateF0DeltaParameterData(
            List<double> parameterList = null,
            double tolerance = 0.00001)
        {
            List<double> pList = new List<double>();
            for (int i = 0; i < parameterList.Count; i++) pList.Add(0);

            return new F0DeltaParameterData(f0OriginList, false, "PIT", pList, tolerance);
        }

        private List<double> f0OriginList { get; }

    }

}
