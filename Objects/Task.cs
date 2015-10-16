using System.Collections.Generic;
using System.Data.SqlClient;

namespace ToDoList
{
  public class Task
  {
    private int id;
    private string description;

    public Task(string Description, int Id = 0)
    {
      id = Id;
      description = Description;
    }

    public int GetId()
    {
      return id;
    }


    public string GetDescription()
    {
      return description;
    }

    public void SetDescription(string newDescription)
    {
      description = newDescription;
    }

    public static List<Task> All()
    {
      List<Task> AllTasks = new List<Task>{};

      SqlConnection conn = DB.Connection();
      SqlDataReader rdr = null;
      conn.Open();

      SqlCommand cmd = new SqlCommand("SELECT * FROM tasks", conn);
      rdr = cmd.ExecuteReader();

      while(rdr.Read())
      {
        int taskId = rdr.GetInt32(0);
        string taskDescription = rdr.GetString(1);
        Task newTask = new Task(taskDescription, taskId);
        AllTasks.Add(newTask);
      }

      conn.Close();

      return AllTasks;
    }




    // public static List<string> All()
    // {
    //   List<string> AllDescriptions = new List<string>{};
    //   SqlConnection conn = DB.Connection();
    //   SqlDataReader rdr = null;
    //   conn.Open();
    //   try
    //   {
    //       SqlCommand cmd = new SqlCommand("SELECT * FROM tasks", conn);
    //       rdr = cmd.ExecuteReader();
    //
    //       while (rdr.Read())
    //       {
    //           AllDescriptions.Add(rdr.GetString(1));
    //       }
    //   }
    //   finally
    //   {
    //       if (rdr != null)
    //       {
    //           rdr.Close();
    //       }
    //
    //       if (conn != null)
    //       {
    //           conn.Close();
    //       }
    //   }
    //   return AllDescriptions;
    // }

    public void Save()
    {
      SqlConnection conn = DB.Connection();
      SqlDataReader rdr;
      conn.Open();

      SqlCommand cmd = new SqlCommand("INSERT INTO tasks (description) VALUES (@TaskDescription)", conn);

      SqlParameter testParameter = new SqlParameter();
      testParameter.ParameterName = "@TaskDescription";
      testParameter.Value = this.GetDescription();

      cmd.Parameters.Add(testParameter);

      rdr = cmd.ExecuteReader();

      conn.Close();


    }

    public static void DeleteAll()
    {
      SqlConnection conn = DB.Connection();
      conn.Open();
      SqlCommand cmd = new SqlCommand("DELETE FROM tasks;", conn);
      cmd.ExecuteNonQuery();
    }
  }
}
