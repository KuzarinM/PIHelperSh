using PIHelperSh.AI.Core.Models;
using PIHelperSh.AI.Core.Models.ImageGenerator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PIHelperSh.AI.Core.Interfaces
{
    public interface ICloudAIImageGenerator : IDisposable
    {
        public Task<AIImageGeneratorResponce> RequestGeneration(AIPromtForImage promt);

        public Task<AIImageGeneratorResponce> GetImage(AIImageGeneratorResponce image);

        public Task<AIImageGeneratorResponce> GetImage(AIPromtForImage promt);
    }
}
