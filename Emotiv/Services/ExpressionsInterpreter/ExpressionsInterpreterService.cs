﻿using Emotiv.Models;

namespace Emotiv.Services.ExpressionsInterpreter;

public class ExpressionsInterpreterService : IExpressionsInterpreterService
{
    private Dictionary<string, List<double>> _values;
    private bool _isAnalizing;

    public ExpressionsInterpreterService()
    {
        _values = new Dictionary<string, List<double>>();
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

    public bool StopAnalizing()
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
        return ExpressionConstants.PositivenessCorrespondence[maxAvgExpression.expression];
    }
}
