namespace Matrix.Models;

public class Matrices {
    public Matrices(MatrixCell alphaCell, MatrixCell betaCell, MatrixCell gamaCell) {
        Alpha.Add(alphaCell);
        Beta.Add(betaCell);
        Gama.Add(gamaCell);
    }

    public List<MatrixCell> Alpha { get; set; } = new();
    public List<MatrixCell> Beta { get; set; } = new();
    public List<MatrixCell> Gama { get; set; } = new();

    public void Combine(IEnumerable<Matrices> matrices) {
        foreach (var matrix in matrices) {
            Alpha.AddRange(matrix.Alpha);
            Beta.AddRange(matrix.Beta);
            Gama.AddRange(matrix.Gama);
        }
    }
}