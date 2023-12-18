internal class Day13
{
    static void Day13()
    {
        /*
        Categorise subjects by age as follows

        Child < 13
        Teenage 13-19 inclusive
        Young Adult: 20-30 inclusive
        Adult 31-60 inclusive
        Older person 61 and older
        For each category, calculate and report (on screen and in file) the following:

        Total time spent online 
        Average time a subject in this category spends online
        Identify the subject that spends the most time online
        Provide Design (Flow Chart and Pseudocode)
        Code solution
        */
        string localPath = @"..\..\..\..\..\inputFiles\"; // we get the parent solution directory 
        string csvFileName = "TimeOnlineByAge";
        string csvFilePath = Path.Combine(localPath, csvFileName + ".csv");

        List<DataFromFile> dataList = TimeOnlineSurv.ParseCSV(csvFilePath);
        string outputString = TimeOnlineSurv.Calculate();
        Print(outputString);

        //Write(outputText); ?
    }
}