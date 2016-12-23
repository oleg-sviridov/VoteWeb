using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Vote : System.Web.UI.Page
{
    
    /// <summary>
    /// Employee info
    /// </summary>
    private class Employee
    {
        public string Account { get; set; }
        public string Name { get; set; }
        public string Department { get; set; }
        public string ImageUrl { get; set; }
    }
    
    protected void Page_Load(object sender, EventArgs e)
    {
        Employee currentUser = GetCurrentUser();
        ShowCurrentUserInfo(currentUser);

        Employee votedUser = GetVotedUser();
        ShowVotedUserInfo(votedUser);
    }

    protected void ButtonVote_Click(object sender, EventArgs e)
    {
        // Todo
    }

    /// <summary>
    /// Returns current account name
    /// </summary>
    /// <returns></returns>
    private string GetCurrentAccount()
    {
        // Todo
        return "";
    }

    /// <summary>
    /// Returns voted account name
    /// </summary>
    /// <returns></returns>
    private string GetVotedAccount()
    {
        // Todo
        return "";
    }

    /// <summary>
    /// Returns current user info
    /// </summary>
    /// <returns>Current user info</returns>
    private Employee GetCurrentUser()
    {
        Employee employee = new Employee();
        
        // Test data
        employee.Account = "BA000";
        employee.Name = "Сектоид Сектоидов";
        employee.Department = "Департамент ИТ";
        employee.ImageUrl = "~/Content/from.jpg";

        employee.Account = GetCurrentAccount();
        // Todo Loading user info by account

        return employee;
    }

    /// <summary>
    /// Returns voted user info
    /// </summary>
    /// <returns>Voted user info</returns>
    private Employee GetVotedUser()
    {
        Employee employee = new Employee();

        // Test data
        employee.Account = "BA001";
        employee.Name = "Мутон Мутонов";
        employee.Department = "Департамент Маркетинг";
        employee.ImageUrl = "~/Content/to.png";

        employee.Account = GetVotedAccount();
        // Todo: Loading user info by account

        return employee;
    }

    /// <summary>
    /// Shows current user info on the interface
    /// </summary>
    /// <param name="currentUser"></param>
    private void ShowCurrentUserInfo(Employee currentUser)
    {
        LabelFromEmployeeName.Text = currentUser.Name;
        LabelFromEmployeeDep.Text = currentUser.Department;
        ImageFromEmployee.ImageUrl = currentUser.ImageUrl;
    }

    /// <summary>
    /// Shows voted user info on the interface
    /// </summary>
    /// <param name="currentUser"></param>
    private void ShowVotedUserInfo(Employee votedUser)
    {
        LabelToEmployeeName.Text = votedUser.Name;
        LabelToEmployeeDep.Text = votedUser.Department;
        ImageToEmployee.ImageUrl = votedUser.ImageUrl;
    }
}