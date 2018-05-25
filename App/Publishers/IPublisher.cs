using Emgu.CV;
using Emgu.CV.Structure;

namespace App {
    public interface IPublisher {
        void ComputeFrame(Image<Bgr, byte> currentFrame);
    }
}