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
            int total,
            string name = "F0",
            List<double> parameterList = null,
            double tolerance = 0.00001,
            double viewTotal = 500,
            double viewMin = 0,
            double viewMax = 500)
            :
            base(total, true, name, parameterList, tolerance, viewTotal, viewMin, viewMax)
        {

        }

        public static F0ParameterData CreateF0ParameterData(
            List<double> parameterList = null,
            double tolerance = 0.00001) =>
            new F0ParameterData(parameterList.Count, "F0", parameterList, tolerance);

    }

    public sealed class F0DeltaParameterData : ParameterData
    {

        private F0DeltaParameterData(
            List<double> f0List,
            int total,
            string name = "PIT",
            List<double> parameterList = null,
            double tolerance = 0.00001,
            double viewTotal = 500,
            double viewMin = -250,
            double viewMax = 250)
            :
            base(total, false, name, parameterList, tolerance, viewTotal, viewMin, viewMax)
        {
            f0OriginList = f0List;
        }

        public static F0DeltaParameterData CreateF0DeltaParameterData(
            List<double> parameterList = null,
            double tolerance = 0.00001)
        {
            List<double> pList = new List<double>();
            for (int i = 0; i < parameterList.Count; i++) pList.Add(0);

            return new F0DeltaParameterData(pList, pList.Count, "PIT", pList, tolerance);
        }

        private List<double> f0OriginList { get; }

    }

}
