﻿using System;
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

    }

}
