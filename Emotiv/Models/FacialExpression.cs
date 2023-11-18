namespace Emotiv.Models;
/// <summary>
///  Possible actions:
///  neutral ,blink ,winkL ,winkR ,horiEye ,surprise ,frown ,smile ,clench ,laugh ,smirkLeft , smirkRight
/// </summary>
public class FacialExpression
{
    public string EyeAction { get; set; }
    public string UpperFaceAction { get; set; }
    public double UpperFacePower { get; set; }
    public string LowerFaceAction { get; set; }
    public double LowerFacePower { get; set; }
}
public static class ExpressionConstants
{
    public static Dictionary<string, bool> PositivenessCorrespondence =
        new Dictionary<string, bool>
        {
             {"neutral", true},
             {"blink", false},
             {"winkL", true},
             {"winkR", true},
             {"horiEye", false},
             {"surprise", true},
             {"frown", false},
             {"smile", true},
             {"clench", false},
             {"laugh", true},
             {"smirkLeft ", false},
             {"smirkRighk", false}
        };
}