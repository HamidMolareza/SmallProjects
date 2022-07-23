using System.Text.Json;
using System.Text.RegularExpressions;

namespace CounterOfMatrixZeros {
    public static class Program {
        private static Configs _configs = null!;

        public static void Main() {
            _configs = ReadConfig();

            var files = GetFiles().ToList();
            Console.WriteLine($"{files.Count} files found.");

            var matrices = InitMatricesData();
            var numbersRegex = new Regex(_configs.NumbersRegex);

            foreach (var file in files) {
                var data = File.ReadAllText(file);

                var numbers = numbersRegex.Matches(data)
                    .Select(match => match.Value)
                    .ToList();

                var startIndexOfMatrices = GetStartIndexOfMatrices(
                    _configs.NumOfMatrixColumns, _configs.NumOfMatricesInRow);
                var matrixColumnIndex = 0;

                for (var i = 0; i < _configs.NumOfMatricesInFile; i++) {
                    var matrix = GetMatrix(startIndexOfMatrices[matrixColumnIndex], numbers);
                    matrices[i].Add(matrix);

                    matrixColumnIndex++;
                    if (matrixColumnIndex < _configs.NumOfMatricesInRow)
                        continue;
                    matrixColumnIndex = 0;
                    for (var j = 0; j < startIndexOfMatrices.Count; j++)
                        startIndexOfMatrices[j] += _configs.NumOfNumbersInRows * _configs.NumOfMatrixRows;
                }
            }

            var result = CountZeroes(matrices);

            Print(result);
        }

        private static void Print(IReadOnlyList<List<int>> matrices) {
            for (var i = 0; i < matrices.Count; i += _configs.NumOfMatricesInRow) {
                for (var row = 0; row < _configs.NumOfMatrixRows; row++) {
                    for (var column = 0; column < _configs.NumOfMatrixColumns; column++) {
                        var matrix = string.Join('\t', matrices[i + column].Skip(row * 3).Take(3));
                        Console.Write(matrix);

                        if (column != 2)
                            Console.Write("\t|\t");
                    }

                    Console.WriteLine();
                }

                Console.WriteLine(new string('-', _configs.NumOfMatrixColumns * _configs.NumOfMatricesInRow * 9));
            }
        }

        private static Configs ReadConfig() => JsonSerializer.Deserialize<Configs>(
                                                   File.ReadAllText(Configs.ConfigFilePath)) ??
                                               throw new ArgumentNullException(nameof(_configs),
                                                   "Can not load configs");

        private static IEnumerable<string> GetFiles() => Directory.GetFiles(_configs.FilesDir,
            _configs.FilesFormat, SearchOption.AllDirectories);

        private static List<List<List<string>>> InitMatricesData() {
            var matrices = new List<List<List<string>>>(_configs.NumOfMatricesInFile);
            for (var i = 0; i < _configs.NumOfMatricesInFile; i++) {
                matrices.Add(new List<List<string>>());
            }

            return matrices;
        }

        private static List<int> GetStartIndexOfMatrices(int numOfMatrixColumns, int numOfMatricesInRows) {
            var result = new List<int>(numOfMatricesInRows);
            var matrixIndex = 0;
            for (var i = 0; i < numOfMatricesInRows; i++) {
                result.Add(matrixIndex);
                matrixIndex += numOfMatrixColumns;
            }

            return result;
        }

        private static List<string> GetMatrix(int startIndexOfMatrix, IReadOnlyCollection<string> numbers) {
            var matrix = new List<string>(_configs.NumOfNumbersInMatrix);

            for (var j = 0; j < _configs.NumOfMatrixRows; j++) {
                var skipCount = (j * _configs.NumOfMatricesInRow * _configs.NumOfMatrixColumns) + startIndexOfMatrix;
                matrix.AddRange(numbers.Skip(skipCount)
                    .Take(_configs.NumOfMatrixColumns)
                    .ToList());
            }

            return matrix;
        }

        private static List<List<int>> CountZeroes(List<List<List<string>>> matrices) {
            var result = new List<List<int>>(_configs.NumOfMatricesInFile);

            foreach (var matrix in matrices) {
                var numOfZeroes = new List<int>(new int[_configs.NumOfNumbersInMatrix]);
                for (var i = 0; i < _configs.NumOfNumbersInMatrix; i++) {
                    numOfZeroes[i] += matrix.Count(m => m[i] == "0");
                }

                result.Add(numOfZeroes);
            }

            return result;
        }
    }
}