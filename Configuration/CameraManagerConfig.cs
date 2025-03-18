using CameraProjectionRenderingToolkit;

namespace GHTweaks.Configuration
{
    public class CameraManagerConfig
    {
        public float CPRTFieldOfView { get; set; }
        
        public float CPRTIntensity { get; set; }

        public float CPRTScreenshotRenderSizeFactor { get; set; }

        public float CPRTRenderSizeFactor { get; set; }

        public float CPRTOrthographicSize { get; set; }

        public float CPRTPerspectiveOffset { get; set; }

        public float CPRTAdaptiveTolerance { get; set; }

        public float CPRTAdaptivePower { get; set; }

        public float CPRTFilterSharpen { get; set; }

        public CPRT.CPRTFilterMode? CPRTFilterMode { get; set; }

        public CPRT.CPRTType? CPRTProjectionType { get; set; }
    }
}
