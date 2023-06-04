using UnityEngine;

public abstract class PipePuzzleValidator : MonoBehaviour
{
    public abstract bool CheckSolvency(Pipe[,] pipes);
}
