namespace Matrix.Models; 

public class MatrixCell {
    public MatrixCell(int row, int column) {
        Row = row;
        Column = column;
    }
    public int Row { get; set; }
    public int Column { get; set; }

    public override bool Equals(object? obj) =>
        obj is MatrixCell matrixCell
        && matrixCell.Equals(this);

    protected bool Equals(MatrixCell other) {
        return Row == other.Row && Column == other.Column;
    }

    public override int GetHashCode() {
        return HashCode.Combine(Row, Column);
    }

    public static bool operator ==(MatrixCell? left, MatrixCell? right) {
        return Equals(left, right);
    }

    public static bool operator !=(MatrixCell? left, MatrixCell? right) {
        return !Equals(left, right);
    }
}