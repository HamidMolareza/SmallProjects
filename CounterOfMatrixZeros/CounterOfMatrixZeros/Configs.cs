namespace CounterOfMatrixZeros; 

public class Configs {
    public const string ConfigFilePath = "data/config.json";
    public string FilesDir { get; set; }
    public string FilesFormat { get; set; }
    public int NumOfMatricesInFile { get; set; }
    public string NumbersRegex { get; set; }
    public int NumOfMatrixRows { get; set; }
    public int NumOfMatrixColumns{ get; set; }
    public int NumOfMatricesInRow { get; set; }
    public int NumOfNumbersInMatrix => NumOfMatrixRows * NumOfMatrixColumns;
    public int NumOfNumbersInRows => NumOfMatricesInRow * NumOfMatrixColumns;
}