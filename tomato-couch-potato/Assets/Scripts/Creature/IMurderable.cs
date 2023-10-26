using Cysharp.Threading.Tasks;

namespace trrne.Heart
{
    public interface IMurderable
    {
        UniTask Die();
    }
}