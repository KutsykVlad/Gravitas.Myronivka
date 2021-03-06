namespace Gravitas.Infrastructure.Platform.Manager.Scale
{
    public class ScaleValidationResult
    {
        public bool IsValid => IsPerimetrValid
                               && IsNoOverload
                               && IsGrossValueValid
                               && IsTareValueValid
                               && IsProcessWithoutTrailer
                               && IsRejectedScaleValueValid;

        public string ValidationMessage { get; set; } = "Повідомлення. ";

        public bool IsPerimetrValid { get; set; } = true;
        public bool IsNoOverload { get; set; }  = true;
        public bool IsGrossValueValid { get; set; }  = true;
        public bool IsTareValueValid { get; set; } = true;
        public bool IsProcessWithoutTrailer { get; set; } = true;
        public bool IsRejectedScaleValueValid { get; set; } = true;
    }
}