$variants = "FOR", "PARTITIONER", "FOR_THLOCAL", "PARTITIONER_THLOCAL"

foreach ($variant in $variants) {
    # for some reason the --force flag doesn't work with /p:DefineConstants (doesn't get reset) so manually deleting the bin and obj folder is required
    Remove-Item -Path ".\TravellingSalesmanProblem\bin", ".\TravellingSalesmanProblem\obj" -Recurse
    & dotnet publish -c Release /p:DefineConstants=$variant --nologo --force -o "C:\TSP_PerfTests\$variant"
}