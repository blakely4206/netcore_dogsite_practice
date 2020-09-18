using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.ML;

namespace Dog_Site
{
    public class ConsumeModel
    {
        public static ModelOutput Predict(ModelInput input)
        {
            string modelPath;
            MLContext mlContext = new MLContext();

            modelPath = AppDomain.CurrentDomain.BaseDirectory + "MLModel.zip";
            ITransformer mlModel = mlContext.Model.Load(modelPath, out var modelInputSchema);
            var predEngine = mlContext.Model.CreatePredictionEngine<ModelInput, ModelOutput>(mlModel);

            ModelOutput result = predEngine.Predict(input);

            return result;                      
        }
    }
}
