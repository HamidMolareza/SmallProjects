<div align="center">
  <h1>Mathematics Problem</h1>
  <br />

  <a href="https://github.com/HamidMolareza/SmallProjects/issues/new?assignees=&labels=bug&template=01_BUG_REPORT.md&title=bug%3A+">Report a Bug</a>
  ·
  <a href="https://github.com/HamidMolareza/SmallProjects/issues/new?assignees=&labels=enhancement&template=02_FEATURE_REQUEST.md&title=feat%3A+">Request a Feature</a>
  .
  <a href="https://github.com/HamidMolareza/SmallProjects/issues/new?assignees=&labels=question&template=04_SUPPORT_QUESTION.md&title=support%3A+">Ask a Question</a>
</div>

<div align="center">
<br />

![GitHub](https://img.shields.io/github/license/HamidMolareza/SmallProjects)
[![code with love by GITHUB_USERNAME](https://img.shields.io/badge/%3C%2F%3E%20with%20%E2%99%A5%20by-Hamid_Molareza-ff1414.svg?style=flat-square)](https://github.com/HamidMolareza)

</div>

## Problem
randomly set half of the terms α<sup>(ι)</sup><sub>i1,i2</sub> B<sup>(ι)</sup><sub>j1,j2</sub> γ<sup>(ι)</sup><sub>k1,k2</sub> with i2 ≠ j1 and j2 ≠ k1 and k2 ≠ i1 to zero.

### Description
In each line (l) we have 3 matrices named `alpha`, `beta` and `gama`. We show `alpha` rows with i<sub>1</sub> index and `alpha` columns with i<sub>2</sub> index. Similarly, we have j<sub>1</sub>j<sub>2</sub> for `beta` and k<sub>1</sub>k<sub>2</sub> for gama.

1) We need to find cells that have the following conditions:

`i2 ≠ j1 and j2 ≠ k1 and k2 ≠ i1`

2) We find half of the available modes in 23 lines (each line contains 3 matrices) **randomly**.

3) We set the found cells value to `0` and display the rest of the cells with `-`.


### Demo
Example of an answer:

<details>
<summary>Screenshots</summary>
In the picture you can see 5 lines of 23 lines.
<br>

![sample result](docs/0.jpg)

</details>

### Built With

C# - Dotnet 6

## How Run

1. Install dotnet 6 SDK
2. Clone this project
3. `cd` to this directory
4. `dotnet build`
5. `dotnet run --project Matrix/Matrix.csproj`

## Support

Reach out to the maintainer at one of the following places:

- [GitHub issues](https://github.com/HamidMolareza/SmallProjects/issues/new?assignees=&labels=question&template=04_SUPPORT_QUESTION.md&title=support%3A+)

## Project assistance

If you want to say **thank you** or/and support active development of this project:

- Add a [GitHub Star](https://github.com/HamidMolareza/SmallProjects) to the project.  🌟

## License

See [LICENSE](../LICENSE) for more information.

