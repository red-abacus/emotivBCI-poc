using Emotiv.Models;

namespace Emotiv.Services.ExpressionsInterpreter;

public interface IExpressionsInterpreterService
{
    void InterpretLog(FacialExpression log);
    void StartAnalizing();
    bool StopAnalysisAndSendResult();
}
