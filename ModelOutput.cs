using System;
using System.Collections.Generic;
using Microsoft.ML.Data;

namespace Dog_Site
{
    public class ModelOutput
    {
        [ColumnName("PredictedLabel")]
        public String Prediction { get; set; }
        public float[] Score { get; set; }
    }
}
