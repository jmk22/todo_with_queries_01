using System.Collections.Generic;
using System.Data.SqlClient;

namespace ToDoList
{
  public class Task
  {
    private int id;
    private string description;

    public Task(string Description)
    {
      description = Description;
    }

    public string GetDescription()
    {
      return description;
    }

    public void SetDescription(string newDescription)
    {
      description = newDescription;
    }

    public static List<string> All()
    {
      List<string> AllDescriptions = new List<string>{};
      SqlConnection conn = DB.Connection();
      SqlDataReader rdr = null;
      conn.Open();
      try
      {
          SqlCommand cmd = new SqlCommand("SELECT * FROM tasks", conn);
          rdr = cmd.ExecuteReader();

          while (rdr.Read())
          {
              AllDescriptions.Add(rdr.GetString(1));
          }
      }
      finally
      {
          if (rdr != null)
          {
              rdr.Close();
          }

          if (conn != null)
          {
              conn.Close();
          }
      }
      return AllDescriptions;
    }

    public static void Save()
    {
      SqlConnection conn = DB.Connection();
      SqlDataReader rdr;
      conn.Open();

      SqlCommand cmd = new SqlCommand("INSERT INTO tasks (description) VALUES (@TaskDescription)", conn);

      SqlParameter testParameter = new SqlParameter();
      testParameter.ParameterName = "@TaskDescription";
      testParameter.Value = "Test saving with parameters";

      cmd.Parameters.Add(testParameter);

      rdr = cmd.ExecuteReader();

      conn.Close();


    }
  }
}
