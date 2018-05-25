using Emgu.CV;
using Emgu.CV.Structure;
using SharpMediator;
using System.Linq;

namespace App.MotionDetector {
    public class MotionDetectorPublisher : IPublisher {
        private Image<Bgr, byte> _lastFrame;
        public static int PixelThreshold = 60;
        public static int MotionThreshold = 1000;
        
        public void ComputeFrame(Image<Bgr, byte> currentFrame) {
            if (_lastFrame == null) {
                _lastFrame = currentFrame.Copy();
                return;
            }
            var difference = _lastFrame.AbsDiff(currentFrame);
            difference = difference.ThresholdBinary(new Bgr(PixelThreshold, PixelThreshold, PixelThreshold), new Bgr(255, 255, 255));
            _lastFrame.Dispose();
            _lastFrame = currentFrame.Copy();

            var dif = 0;
            dif = difference.Bytes.Sum(b => b);
            //for (var i = 0; i < difference.Size.Width; i++) {
            //    for (var j = 0; j < difference.Size.Height; j++) {
            //        dif += difference.Data[j, i, 0];
            //    }
            //}
            if (dif > MotionThreshold) {
                Mediator.Default.Publish(new MotionDetected());
            }
            difference.Dispose();
        }
    }
    public class MotionDetected {

    }
}
