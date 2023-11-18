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

    public bool StopAnalysisAndSendResult()
    {
        _isAnalizing = false;
        var result = string.Empty;
        (string expression, double value) maxAvgExpression = (string.Empty, 0);

        (string expression, double value) smileAvgExpression = (string.Empty, 0);
        (string expression, double value) frownAvgExpression = (string.Empty, 0);

        foreach (var key in _values.Keys)
        {
            var avg = _values[key].Select(power => power).Average();
            if (avg > maxAvgExpression.value)
            {
                maxAvgExpression = (key, avg);
            }
            if (key == "smile")
            {
                smileAvgExpression = ("smile", avg);
            }
            if (key == "frown")
            {
                frownAvgExpression = ("frown", avg);
            }
            result += $"{key} {avg}\n";
        }
        var isHot = false;

        Console.WriteLine("\"########################## " + JsonSerializer.Serialize(result));

        if (smileAvgExpression.value > frownAvgExpression.value)
        {
            maxAvgExpression = smileAvgExpression;
            Console.WriteLine($"########################## Smile {smileAvgExpression.expression} {smileAvgExpression.value}");
            isHot = true;
        }
        else if (frownAvgExpression.value > smileAvgExpression.value)
        {
            maxAvgExpression = frownAvgExpression;
            Console.WriteLine($"########################## Frown {frownAvgExpression.expression} {frownAvgExpression.value} ");
            isHot = false;
        }
        else if (maxAvgExpression.expression != string.Empty)
        {
            isHot = ExpressionConstants.PositivenessCorrespondence[maxAvgExpression.expression];
            Console.WriteLine($"########################## {maxAvgExpression.expression} {maxAvgExpression.value} ");
        }
        else
        {
            Console.WriteLine($"########################## Attention! No reaction recorded ");
        }
        Console.WriteLine($"########################## {isHot}");
        return isHot;
    }
}
