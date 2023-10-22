using Cysharp.Threading.Tasks;

namespace Chickenen.Heart
{
    public interface IMurderable
    {
        UniTask Die();
    }
}