using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(RectTransform), typeof(GridLayoutGroup))]
public class GridCellFit : MonoBehaviour
{
    [SerializeField] private int _columnsCount;
    [SerializeField] private int _rowsCount;
    [SerializeField] private bool _squareCells;

    private void Start()
    {
        Fit();
    }

    [ExecuteInEditMode]
    [ContextMenu("Fit")]
    private void Fit()
    {
        var grid = GetComponent<GridLayoutGroup>();
        var rect = GetComponent<RectTransform>();

        var width = rect.rect.width / _columnsCount;
        var height = rect.rect.height / _rowsCount;

        if (_squareCells)
        {
            var avg = (width + height) / 2;
            grid.cellSize = new Vector2(avg, avg);
        }
        else
        {
            grid.cellSize = new Vector2(width, height);
        }
    }
}
