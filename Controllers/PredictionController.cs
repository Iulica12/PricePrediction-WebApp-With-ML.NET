﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.ML;
using static Plotly.NET.StyleParam.DrawingStyle;
using static PricePrediction.MLModel;

namespace Price_Prediction.Controllers
{
    public class PredictionController : Controller
    {
        public IActionResult Index()
        {
     
            return View();

        }
        public IActionResult Price(ModelInput input)
        {
            // Load the model
            MLContext mlContext = new MLContext();
            // Create predection engine related to the loaded train model
            ITransformer mlModel =
           mlContext.Model.Load(@"..\PricePredictionModel\MLModel.zip", out var
           modelInputSchema);
            var predEngine = mlContext.Model.CreatePredictionEngine<ModelInput,
           ModelOutput>(mlModel);
            // Try model on sample data to predict fair price
            ModelOutput result = predEngine.Predict(input);
            ViewBag.Price = result.Score;
            return View(input);
        }

    }
}
