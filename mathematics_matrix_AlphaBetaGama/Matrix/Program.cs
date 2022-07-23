using Matrix.Models;

namespace Matrix;

public static class Program {
    private static readonly List<int> MatrixOrder = new() {1, 2, 3};
    private const int NumOfOutputMatrices = 23;

    public static void Main() {
        var allPossibilities = GetAllPossibilities();

        allPossibilities.TakeRandomly(allPossibilities.Count / 2)
            .CombineTo(NumOfOutputMatrices)
            .Print();

        Console.Write("Press any key to exit...");
        Console.ReadKey();
    }

    private static List<Matrices> GetAllPossibilities() =>
        (from alphaRow in MatrixOrder
            from alphaColumn in MatrixOrder
            from betaRow in MatrixOrder.Where(order => order != alphaColumn)
            from betaColumn in MatrixOrder
            from gamaRow in MatrixOrder.Where(order => order != betaColumn)
            from gamaColumn in MatrixOrder.Where(order => order != alphaRow)
            select new Matrices(new MatrixCell(alphaRow, alphaColumn), new MatrixCell(betaRow, betaColumn),
                new MatrixCell(gamaRow, gamaColumn)))
        .ToList();

    private static IEnumerable<T> TakeRandomly<T>(this IReadOnlyList<T> items, int count) {
        var randomIndexes = new List<int>(count);
        for (var i = 0; i < count; i++) {
            randomIndexes.Add(GetNewRandomNumber(randomIndexes, 0, items.Count));
        }

        var result = new List<T>(count);
        result.AddRange(randomIndexes.Select(randomIndex => items[randomIndex]));
        return result;
    }

    private static int GetNewRandomNumber(IReadOnlyCollection<int> randomNumbers, int min, int max) {
        var random = new Random();
        int newRandomNumber;
        while (true) {
            newRandomNumber = random.Next(min, max);
            if (randomNumbers.All(randomNumber=> randomNumber != newRandomNumber)) {
                return newRandomNumber;
            }
        }
    }

    private static List<Matrices> CombineTo(this IEnumerable<Matrices> matrices, int count) {
        var matricesList = matrices.ToList();

        var result = matricesList.Take(count).ToList();
        if (matricesList.Count <= count)
            return result;

        var remainMatrices = matricesList.Skip(count).ToList();
        var numOfCombinationMatrixForEachItem = remainMatrices.Count / count;
        var overflowMatrices = remainMatrices.Count % count;

        var totalTaken = 0;
        for (var i = 0; i < count; i++) {
            var takeCount = i < overflowMatrices
                ? numOfCombinationMatrixForEachItem + 1
                : numOfCombinationMatrixForEachItem;
            result[i].Combine(remainMatrices.Skip(totalTaken).Take(takeCount));

            totalTaken += takeCount;
        }
        
        return result;
    }

    private static void Print(this List<Matrices> matrices) {
        foreach (var matrix in matrices) {
            for (var row = 1; row <= MatrixOrder.Count; row++) {
                PrintColumns(matrix.Alpha, row);
                Console.Write("|\t");
                
                PrintColumns(matrix.Beta, row);
                Console.Write("|\t");
                
                PrintColumns(matrix.Gama, row);
                Console.WriteLine();
            }

            Console.WriteLine(new string('-', 83));
        }
    }

    private static void PrintColumns(List<MatrixCell> matrixCells, int row) {
        for (var column = 1; column <= MatrixOrder.Count; column++) {
            var isZero = matrixCells.Exists(cell => cell.Row == row && cell.Column == column);
            Console.Write(isZero ? "0" : "-");
            Console.Write('\t');
        }
    }
}