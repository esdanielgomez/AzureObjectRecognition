using Microsoft.Azure.CognitiveServices.Vision.CustomVision.Prediction;
using Microsoft.Azure.CognitiveServices.Vision.CustomVision.Prediction.Models;
using Microsoft.Azure.CognitiveServices.Vision.CustomVision.Training;
using Microsoft.Azure.CognitiveServices.Vision.CustomVision.Training.Models;
using ApiKeyServiceClientCredentials = Microsoft.Azure.CognitiveServices.Vision.CustomVision.Prediction.ApiKeyServiceClientCredentials;

namespace CognitiveServices;

public class CustomVision
{
    private string Key = "068025707e8f493eaad848df6edc6bd3";
    private string Endpoint = "https://eastus.api.cognitive.microsoft.com/";
    private string ProjectID = "b551affd-2906-469f-b9b9-e8e7e645078b";
    private string publishedModelName = "DotVVM_";
    private double minProbability = 0.75;

    private CustomVisionTrainingClient trainingApi;
    private CustomVisionPredictionClient predictionApi;
    private Project project;

    public CustomVision()
    {
        trainingApi = AuthenticateTraining(Endpoint, Key);
        predictionApi = AuthenticatePrediction(Endpoint, Key);
        project = trainingApi.GetProject(new Guid(ProjectID));
    }

    private CustomVisionTrainingClient AuthenticateTraining(string endpoint, string trainingKey)
    {
        return new CustomVisionTrainingClient(new Microsoft.Azure.CognitiveServices.Vision.CustomVision.Training.ApiKeyServiceClientCredentials(trainingKey))
        {
            Endpoint = endpoint
        };
    }

    private CustomVisionPredictionClient AuthenticatePrediction(string endpoint, string predictionKey)
    {
        return new CustomVisionPredictionClient(new ApiKeyServiceClientCredentials(predictionKey))
        {
            Endpoint = endpoint
        };
    }

    public List<PredictionModel> DetectObjects(string imageFile)
    {
        using (var stream = File.Open(imageFile, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
        {
            var result = predictionApi.DetectImage(project.Id, publishedModelName, stream);
            return result.Predictions.Where(x => x.Probability > minProbability).ToList();
        }
    }
}