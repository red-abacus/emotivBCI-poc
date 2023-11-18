using Emotiv.Models;
using Microsoft.Net.Http.Headers;
using System.Text;
using System.Text.Json;

namespace Emotiv.Services.ExpressionsInterpreter;

public class ExpressionsInterpreterService : IExpressionsInterpreterService
{
    private Dictionary<string, List<double>> _values;
    private bool _isAnalizing;
    private readonly IHttpClientFactory _httpClientFactory;

    public ExpressionsInterpreterService(IHttpClientFactory httpClientFactory)
    {
        _values = new Dictionary<string, List<double>>();
        _isAnalizing = false;
        _httpClientFactory = httpClientFactory;
    }

    public void InterpretLog(FacialExpression facialExpression)
    {
        if (_isAnalizing)
        {
            if (_values.TryGetValue(facialExpression.UpperFaceAction, out var upperFaceActions))
            {
                upperFaceActions.Add(facialExpression.UpperFacePower);
            }
            else
            {
                _values.Add(facialExpression.UpperFaceAction, new List<double> { facialExpression.UpperFacePower });
            }

            if (_values.TryGetValue(facialExpression.LowerFaceAction, out var lowerFaceActions))
            {
                lowerFaceActions.Add(facialExpression.LowerFacePower);
            }
            else
            {
                _values.Add(facialExpression.LowerFaceAction, new List<double> { facialExpression.LowerFacePower });
            }
        }
    }

    public void StartAnalizing()
    {
        _values.Clear();
        _isAnalizing = true;
    }

    public async Task StopAnalysisAndSendResult()
    {
        _isAnalizing = false;
        var result = string.Empty;
        (string expression, double value) maxAvgExpression = (string.Empty, 0);
        foreach (var key in _values.Keys)
        {
            var avg = _values[key].Select(power => power).Average();
            if (avg > maxAvgExpression.value)
            {
                maxAvgExpression = (key, avg);
            }
            result += $"{key} {avg}\n";
        }

        var isHot = ExpressionConstants.PositivenessCorrespondence[maxAvgExpression.expression];
        var httpRequestMessage = new HttpRequestMessage(
           HttpMethod.Post,
           "https://3d8a-2a02-2f0e-300c-a300-29aa-e636-98e9-f7bc.ngrok.io/")
        {
            Content = new StringContent(JsonSerializer.Serialize(new
            {
                rating = isHot ? "HOT" : "NOT_HOT",
                no = "no"
            }), Encoding.UTF8, "application/json")
        };
        var httpClient = _httpClientFactory.CreateClient();
        var response = await httpClient.SendAsync(httpRequestMessage);
    }
}
