using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;

namespace learningapp.Pages;

public class IndexModel(ILogger<IndexModel> logger, IConfiguration configuration) : PageModel
{
     public List<Course> Courses=[];
    private readonly ILogger<IndexModel> _logger = logger;
    private readonly IConfiguration _configuration = configuration;

  public void OnGet()
    {
       
        string connectionString = _configuration.GetConnectionString("AZURE_SQL_CONNECTIONSTRING")!;
        var sqlConnection = new SqlConnection(connectionString);
        sqlConnection.Open();

        var sqlcommand = new SqlCommand(
        "SELECT CourseID,CourseName,Rating FROM Course;",sqlConnection);
    using SqlDataReader sqlDatareader = sqlcommand.ExecuteReader();
    while (sqlDatareader.Read())
    {
      Courses.Add(new Course()
      {
        CourseID = int.Parse(sqlDatareader["CourseID"].ToString() ?? ""),
        CourseName = sqlDatareader["CourseName"].ToString(),
        Rating = decimal.Parse(sqlDatareader["Rating"].ToString() ?? "")
      });
    }
  }
}
