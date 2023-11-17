namespace Emotiv.Models;
/// <summary>
///  Possible actions:
///  neutral ,blink ,winkL ,winkR ,horiEye ,surprise ,frown ,smile ,clench ,laugh ,smirkLeft , smirkRight
/// </summary>
public class FacialExpression
{
    public FaceActions EyeAction { get; set; }
    public FaceActions UpperFaceAction { get; set; }
    public double UpperFacePower { get; set; }
    public FaceActions LowerFaceAction { get; set; }
    public double LowerFacePower { get; set; }
}


public enum FaceActions
{
    frown,
    smile,
    clench,
    // The values below are not used in the current version of the app
    neutral,
    blink,
    winkL,
    winkR,
    horiEye,
    surprise,
    laugh,
    smirkLeft,
    smirkRight,
    lookL,
    lookR
}

