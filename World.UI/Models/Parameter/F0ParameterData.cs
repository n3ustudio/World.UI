using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Musiqual.Parameter;
using Scrosser.Models;

namespace World.UI.Models.Parameter
{
    public sealed class F0ParameterData : ParameterData
    {

        private F0ParameterData(
            int horizontalTotal,
            double verticalTotal,
            string name = "F0",
            List<double> parameterList = null,
            double tolerance = 0.00001)
            :
            base(horizontalTotal, verticalTotal, true, name, parameterList, tolerance)
        {

        }

        public static F0ParameterData CreateF0ParameterData(
            List<double> parameterList,
            double tolerance = 0.00001) =>
            new F0ParameterData(parameterList.Count, 5000, "F0", parameterList, tolerance);

        /// <summary>
        /// Export the F0 data.
        /// </summary>
        /// <returns>The exported data.</returns>
        public List<double> Export()
        {
            List<Musiqual.Models.Parameter> pList = new List<Musiqual.Models.Parameter>();
            foreach (Musiqual.Models.Parameter parameter in ParameterList)
            {
                pList.Add(parameter);
            }
            pList.Sort((pA, pB) =>
            {
                if (pA.Position.Position > pB.Position.Position) return 1;
                if (pA.Position.Position < pB.Position.Position) return -1;
                return 0;
            });
            List<double> resultList = new List<double>();
            int index = 0;
            double prevData = 0;
            foreach (Musiqual.Models.Parameter parameter in pList)
            {
                while (true)
                {
                    if (index >= parameter.Position.Position) break;
                    resultList.Add(prevData);
                    index++;
                }
                resultList.Add(parameter.Value.Position);
                prevData = parameter.Value.Position;
                index++;
            }

            while (true)
            {
                if (resultList.Count == HorizontalTotal) break;
                resultList.Add(prevData);
            }

            return resultList;
        }

    }

    public sealed class F0DeltaParameterData : ParameterData
    {

        private F0DeltaParameterData(
            List<double> f0List,
            int horizontalTotal,
            double verticalTotal,
            string name = "PIT",
            List<double> parameterList = null,
            double tolerance = 0.00001)
            :
            base(horizontalTotal, verticalTotal, false, name, parameterList, tolerance)
        {
            f0OriginList = f0List;
        }

        public static F0DeltaParameterData CreateF0DeltaParameterData(
            List<double> parameterList,
            double tolerance = 0.00001)
        {
            List<double> pList = new List<double>();
            for (int i = 0; i < parameterList.Count; i++) pList.Add(0);

            return new F0DeltaParameterData(pList, pList.Count, 5000, "PIT", pList, tolerance);
        }

        private List<double> f0OriginList { get; }

        /// <summary>
        /// Export the F0 data.
        /// </summary>
        /// <returns>The exported data.</returns>
        public List<double> Export()
        {
            List<Musiqual.Models.Parameter> pList = new List<Musiqual.Models.Parameter>();
            foreach (Musiqual.Models.Parameter parameter in ParameterList)
            {
                pList.Add(parameter);
            }
            pList.Sort((pA, pB) =>
            {
                if (pA.Position.Position > pB.Position.Position) return 1;
                if (pA.Position.Position < pB.Position.Position) return -1;
                return 0;
            });
            List<double> resultList = new List<double>();
            int index = 0;
            double prevData = 0;
            foreach (Musiqual.Models.Parameter parameter in pList)
            {
                while (true)
                {
                    if (index >= parameter.Position.Position) break;
                    resultList.Add(f0OriginList[index] + prevData);
                    index++;
                }
                resultList.Add(f0OriginList[index] + parameter.Value.Position);
                prevData = parameter.Value.Position;
                index++;
            }

            while (true)
            {
                if (resultList.Count == HorizontalTotal) break;
                resultList.Add(prevData);
            }

            return resultList;
        }

    }

}
