//Started까지 완성해서 db에 올리기까지 진행 [아래 코드는 IDReport인데 다른 ID도 구조는 동일]
  
// SAVE DB
void InsertIDReportDataToDatabase(IDReport reportData)
{
    try
    {
        // SQL 삽입 쿼리
        string query = "INSERT INTO IDReport (Datetime, ReportID, ReportName, ModelID, OPID, MaterialID) " +
                       "VALUES (@Datetime, @ReportID, @ReportName, @ModelID, @OPID, @MaterialID)";
       using (SqlCommand command = new SqlCommand(query, dbConnection))
        {
            command.Parameters.AddWithValue("@Datetime", reportData.Datetime);
            command.Parameters.AddWithValue("@ReportID", reportData.reportID);
            command.Parameters.AddWithValue("@ReportName", reportData.reportName);
            command.Parameters.AddWithValue("@ModelID", reportData.ModelID);
            command.Parameters.AddWithValue("@OPID", reportData.OPID);
            command.Parameters.AddWithValue("@MaterialID", reportData.MaterialID);
           command.ExecuteNonQuery();
            updateErrtext("DB 삽입 성공: {reportData.reportID}\r\n");
        }
    }
    catch (Exception ex)
    {
        updateErrtext("DB 삽입 실패: {ex.Message}\r\n");
    }
}



