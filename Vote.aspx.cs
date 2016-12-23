using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;

public partial class Vote : System.Web.UI.Page
{
    const bool TEST_MODE = true;
    const string ORG_CHART_LINK = @"http://rswdapp00/dep/staff/Org%20Chart%20IT/Home.aspx";
        
    /// <summary>
    /// Corporate values
    /// </summary>
    enum Value { Unknown = -1, TeamSpirit = 0, Innovation = 1, Commitment = 2, Responsibility = 3};

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
    
    /// <summary>
    /// Employee Vote class
    /// </summary>
    private class EmployeeVote
    {
        public DateTime Date { get; set; }
        public string AccountFrom { get; set; }
        public string AccountTo { get; set; }
        public Value CorporateValue { get; set; }
        public string Comment { get; set; }
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
        EmployeeVote vote = GetVoteFromUI();
        SaveVote(vote);
        Response.Redirect(ORG_CHART_LINK);
    }
    
    /// <summary>
    /// Returns current account (baXXXXXX)
    /// </summary>
    /// <returns>Current account</returns>
    private string GetCurrentAccount()
    {
        const int LOGIN_LEN = 8;
        try
        {
            string login = "";
            int fullLoginLen = 0;
            if (HttpContext.Current != null)
            {
                login = HttpContext.Current.User.Identity.Name;
                fullLoginLen = HttpContext.Current.User.Identity.Name.Length;
                if (fullLoginLen > LOGIN_LEN)
                {
                    login = login.Remove(0, fullLoginLen - LOGIN_LEN);
                }
            }

            return login;
        }
        catch
        {
            // Todo: log
            return "";
        }
    }

    /// <summary>
    /// Returns voted account name
    /// </summary>
    /// <returns></returns>
    private string GetVotedAccount()
    {
        string account = Request.QueryString["Account"];
        return account;
    }

    /// <summary>
    /// Extracts user data by code in database and account name
    /// </summary>
    /// <param name="code">Code of user data item</param>
    /// <param name="account">Account name</param>
    /// <returns>User data</returns>
    private string GetUserData(int code, string account)
    {
        // Todo - move connection string to Web.config
        SqlConnection connect = new SqlConnection("Server=rswdapp88;Database=WSS_Content;Integrated Security=True");
        connect.Open();

        try
        {
            string sql = GetSqlUserDataByCode(code, account);

            SqlCommand command = new SqlCommand(sql, connect);
            string value = "";
            try
            {
                value = command.ExecuteScalar().ToString();
            }
            catch
            {
                // Todo
            }
            return value;
        }
        finally
        {
            connect.Close();
        }
    }

    /// <summary>
    /// Returns sql-script
    /// </summary>
    /// <param name="code">Code of user data item</param>
    /// <param name="account">Account name</param>
    /// <returns>Sql script</returns>
    private string GetSqlUserDataByCode(int code, string account)
    {
        string sql = @"
            SELECT Value
            FROM 
            (
	            SELECT pref.value('(text())[1]', 'nvarchar(255)') AS Account,
		            pref2.value('(text())[1]', 'nvarchar(255)') AS Value
	            FROM
		            [dbo].[UserData] 
			            CROSS APPLY
		            tp_ColumnSet.nodes('/nvarchar21') AS Account(pref)
			            CROSS APPLY
		            tp_ColumnSet.nodes('/nvarchar" + code.ToString() + @"') AS Value(pref2)
	            WHERE tp_ListId = 'F239CEB0-A683-411B-9378-D2EF15A85D54'
            ) t
            WHERE Account = '" + account + "'";
        // ToDo: add parameters
        return sql;
    }

    /// <summary>
    /// Returns current user info
    /// </summary>
    /// <returns>Current user info</returns>
    private Employee GetCurrentUser()
    {
        Employee employee = new Employee();

        if (TEST_MODE)
        {
            employee.Account = "BA000";
            employee.Name = "Сектоид Сектоидов";
            employee.Department = "Департамент ИТ";
            employee.ImageUrl = "~/Content/from.jpg";
        }
        else
        {
            employee.Account = GetCurrentAccount();
            employee.Name = GetUserData(1, employee.Account);
            employee.Department = GetUserData(9, employee.Account);
            employee.ImageUrl = GetUserData(28, employee.Account);
        }
        return employee;
    }

    /// <summary>
    /// Loads vote data from User Interface and returns it
    /// </summary>
    /// <returns>Vote</returns>
    private EmployeeVote GetVoteFromUI()
    {
        EmployeeVote vote = new EmployeeVote();
        vote.Date = DateTime.Now;
        vote.AccountFrom = GetCurrentAccount();
        vote.AccountTo = GetVotedAccount();
        switch (RadioButtonListValues.SelectedValue)
        {
            case "TeamSpirit":
                vote.CorporateValue = Value.TeamSpirit;
                break;
            case "Innovation":
                vote.CorporateValue = Value.Innovation;
                break;
            case "Commitment":
                vote.CorporateValue = Value.Commitment;
                break;
            case "Responsibility":
                vote.CorporateValue = Value.Responsibility;
                break;
            default:
                vote.CorporateValue = Value.Unknown;
                break;
        }

        vote.Comment = TextBoxComment.Text;

        return vote;
    }

    /// <summary>
    /// Returns voted user info
    /// </summary>
    /// <returns>Voted user info</returns>
    private Employee GetVotedUser()
    {
        Employee employee = new Employee();

        if (TEST_MODE)
        {
            employee.Account = "BA001";
            employee.Name = "Harly Quinn";
            employee.Department = "Департамент Маркетинг";
            employee.ImageUrl = "~/Content/to.jpg";
        }
        else
        {
            employee.Account = GetVotedAccount();
            employee.Name = GetUserData(1, employee.Account);
            employee.Department = GetUserData(9, employee.Account);
            employee.ImageUrl = GetUserData(28, employee.Account);
        }
        return employee;
    }

    /// <summary>
    /// Saves vote data
    /// </summary>
    /// <param name="vote">Vote structure</param>
    private void SaveVote(EmployeeVote vote)
    {
        string xmlFileName = AppDomain.CurrentDomain.GetData("DataDirectory").ToString() + "\\VoteResult.xml";

        XmlDocument doc = new XmlDocument();
        doc.Load(xmlFileName);

        XmlNode rootNode = doc.SelectSingleNode("Votes");
        XmlElement voteNode = doc.CreateElement("Vote");
        
        XmlElement fromNode = doc.CreateElement("AccountFrom");
        fromNode.InnerText = vote.AccountFrom;
        voteNode.AppendChild(fromNode);

        XmlElement toNode = doc.CreateElement("AccountTo");
        toNode.InnerText = vote.AccountTo;
        voteNode.AppendChild(toNode);

        XmlElement valueNode = doc.CreateElement("Value");
        valueNode.InnerText = vote.CorporateValue.ToString();
        voteNode.AppendChild(valueNode);

        XmlElement commentNode = doc.CreateElement("Comment");
        commentNode.InnerText = vote.Comment;
        voteNode.AppendChild(commentNode);

        XmlElement dateNode = doc.CreateElement("Date");
        dateNode.InnerText = vote.Date.ToString();
        voteNode.AppendChild(dateNode);

        rootNode.AppendChild(voteNode);
        
        doc.Save(xmlFileName);
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