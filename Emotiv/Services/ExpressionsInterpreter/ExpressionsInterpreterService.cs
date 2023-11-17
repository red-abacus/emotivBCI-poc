using Emotiv.Models;

namespace Emotiv.Services.ExpressionsInterpreter;

public class ExpressionsInterpreterService : IExpressionsInterpreterService
{
    private Dictionary<FaceActions, List<double>> _values;
    private bool _isAnalizing;

    public ExpressionsInterpreterService()
    {
        _values = new Dictionary<FaceActions, List<double>>();
        _isAnalizing = false;
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

    public string StopAnalizing()
    {
        _isAnalizing = false;
        var result = string.Empty;
        
        foreach (var key in _values.Keys)
        {
            var avg = _values[key].Select(power => power).Average();
            result += $"{key} {avg}\n";
        }
        return result;
    }
}
